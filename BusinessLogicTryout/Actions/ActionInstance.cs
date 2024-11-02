using BusinessLogicTryout.Repositories;

namespace BusinessLogicTryout.Actions;

using BusinessLogicTryout.Objects;

public class ActionInstance // экземпляр действия
{
    private readonly CAction _action;   // действие-основа
    private readonly List<ActionInstanceObjectInstance> _objects;   // объекты в действии
    private readonly List<ActionInstanceParameterInstance> _parameters; // параметры в действии

    public ActionInstance(CAction action, string name, string description)
    {
        Name = name;
        Description = description;
        _action = action;
        _objects = new List<ActionInstanceObjectInstance>();
        foreach(ActionObject obj in _action.Objects) _objects.Add(new ActionInstanceObjectInstance(obj, new ObjectInstance(obj.Object)));
        _parameters = new List<ActionInstanceParameterInstance>();
        foreach(ActionParameter? param in _action.Parameters) _parameters.Add(new ActionInstanceParameterInstance(param, new ObjectParameter(param.Parameter)));
    }

    public void SetParameterValue(ActionParameter parameter, dynamic value) // задаёт значение параметру по ActionParameter
    {
        ActionInstanceParameterInstance? parameterInstance = _parameters.Find(p => p.ActionParameter == parameter);
        if (parameterInstance == null) return;
        parameterInstance.ObjectParameter.SetValue(value);
    }

    public void SetObjectInstanceOnParameterValueChange(ActionParameter parameter, ObjectInstance instance) 
        // устанавливает экземпляры объектов если они связаны с параметром
    {
        foreach (ActionInstanceObjectInstance objectInstance in _objects.Where(o => o.ActionObject!.SourceParameter == parameter))
        {
            objectInstance.SetInstance(instance);
        }
    }
    public void SetObjectInstance(ActionInstanceObjectInstance obj, ObjectInstance instance)    // назначает экземпляр объекта
    {
        obj.SetOriginObject(instance);
        foreach (ActionInstanceParameterInstance param in _parameters)  // и кроме этого - ещё и обновляет ссылки по всем пареметрам, если они ссылаются на объект
        {
            if (param.ActionParameter!.Object == obj.ActionObject)
            {
                param.SetOriginParameter(instance.GetParameterByType(param.ObjectParameter.Type!));
            }
        }
    }

    public void ExecuteResult(ActionResult result, DataContext context) // выполняет действия результата
    {
        foreach (ActionResultAutomation automation in result.Automations)
        {
            switch (automation.Type.Name)
            {
                case "Завершить действие":  // если нужно завершить - выйдем прямо сейчас
                    return;
                case ("Обновить объект"):   // обновление объекта - даёт значения параметрам объекта в соответствии с выбранными значениями в действии в том случае, если обновляется именно этот объект
                    ObjectInstance currentObjectInstance =
                        GetObjectByActionObject(automation.Object).ObjectInstance; // получаем экземпляр объекта
                    foreach (ActionInstanceParameterInstance parameter in Parameters) // в цикле по параметрам действия
                    {
                        if (parameter.ActionParameter!.Object == automation.Object)  // если параметр принадлежит объекту, обновляемому автоматизацией
                        {
                            currentObjectInstance.SetParameterValue(parameter.ActionParameter.Parameter, Parameters.Find(p => p.ActionParameter!.Parameter == parameter.ActionParameter.Parameter)!.ObjectParameter.GetValue());
                            // то задаём ему новое значение, соответствующее параметру в действии
                        }
                    }
                    continue;
                case ("Сохранить объект"): // сохранение объекта - образует новый объект 
                    ObjectInstance newObjectInstance = Objects
                        .FirstOrDefault(o => o.ActionObject == automation.Object)!.ObjectInstance;  // получаем экземпляр объекта
                    foreach (ActionInstanceParameterInstance parameter in Parameters)   // в цикле по параметрам действия -
                    {
                        if (parameter.ActionParameter!.Object == automation.Object)  // если объект параметра совпадает с сохраняемым объектом, то
                        {
                            newObjectInstance.SetParameterValue(parameter.ActionParameter.Parameter, Parameters.Find(p => p.ActionParameter!.Parameter == parameter.ActionParameter.Parameter)!.ObjectParameter.GetValue());
                                // определяем ему соответствующее значение
                        }
                    }
                    context.Objects.Objects.Add(newObjectInstance);
                    continue;
                case ("Принять значение параметра"): // приём значения параметра - устанавливаем значение основного параметра в значение зависимого параметра 
                    GetParameterByActionParameter(automation.MainParameter).ObjectParameter.SetValue(GetParameterByActionParameter(automation.DependParameter).ObjectParameter.GetValue());
                    continue;                
            }
        }
        return;
    }
    
    public string Name { get; }    // наименование для доступа

    public string Description { get; }  // описание для доступа

    public List<ActionInstanceParameterInstance> Parameters => _parameters; // список параметров для доступа
    
    public List<ActionInstanceObjectInstance> Objects => _objects;  // список объектов для доступа
    public CAction Action => _action;   // исходное действие для доступа

    public ActionInstanceObjectInstance GetObjectByActionObject(ActionObject? obj)   // получает экземпляр объекта по объекту исходного действия
    {
        return _objects.Find(o => o.ActionObject == obj)!;
    }

    public ActionInstanceParameterInstance GetParameterByActionParameter(ActionParameter? param) // получает экземпляр параметра по параметру исходного действия
    {
        return _parameters.Find(p => p.ActionParameter == param)!;
    }
}