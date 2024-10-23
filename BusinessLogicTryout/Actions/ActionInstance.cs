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
        foreach(ActionObject? obj in _action.Objects) _objects.Add(new ActionInstanceObjectInstance(obj, new ObjectInstance(obj.Object)));
        _parameters = new List<ActionInstanceParameterInstance>();
        foreach(ActionParameter? param in _action.Parameters) _parameters.Add(new ActionInstanceParameterInstance(param, new ObjectParameter(param.Parameter)));
    }

    public void SetObjectInstance(ActionInstanceObjectInstance obj, ObjectInstance? instance)    // назначает экземпляр объекта
    {
        obj.SetOriginObject(instance);
        foreach (ActionInstanceParameterInstance param in _parameters)  // и кроме этого - ещё и обновляет ссылки по всем пареметрам, если они ссылаются на объект
        {
            if (param.ActionParameter.Object == obj.ActionObject)
            {
                param.SetOriginParameter(instance.GetParameterByType(param.ObjectParameter.Type!));
            }
        }
    }
    
    public string Name { get; }    // наименование для доступа

    public string Description { get; }  // описание для доступа

    public List<ActionInstanceParameterInstance> Parameters => _parameters; // список параметров для доступа
    
    public List<ActionInstanceObjectInstance> Objects => _objects;  // список объектов для доступа
    public CAction Action => _action;   // исходное действие для доступа

    public ActionInstanceObjectInstance GetObjectByActionObject(ActionObject? obj)   // получает экземпляр объекта по объекту исходного действия
    {
        return _objects.FirstOrDefault(o => o.ActionObject == obj)!;
    }

    public ActionInstanceParameterInstance GetParameterByActionParameter(ActionParameter? param) // получает экземпляр параметра по параметру исходного действия
    {
        return _parameters.FirstOrDefault(p => p.ActionParameter == param)!;
    }
}