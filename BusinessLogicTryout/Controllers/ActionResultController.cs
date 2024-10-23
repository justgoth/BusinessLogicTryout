using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Objects;
using BusinessLogicTryout.Repositories;
using Eto.Forms;

namespace BusinessLogicTryout.Controllers;

public class ActionResultController // контроллер интерфейса ETO для результата действия
{
    private readonly Button _controller; // собственно ETO-контроллер, всегда кнопка
    private readonly ActionResult _actionResult; // DI: ссылка на результат
    private readonly DataContext _context; // DI: ссылка на контекст данных
    private readonly Form _form; // DI: ссылка на вызвавшую действие форму
    private readonly ActionInstance _action; // DI: ссылка на экземпляр действия
    
    public ActionResultController(ActionInstance action, ActionResult actionResult, DataContext context, Form form)
    {
        _context = context;
        _actionResult = actionResult;
        _action = action;
        _form = form;
        _controller = new Button
        {
            Text = _actionResult.Text
        };
        _controller.Click += RunResult;
    }

    private void RunResult(object? sender, EventArgs e) // обработчик нажатия на кнопку
    {
        foreach (ActionResultAutomation automation in _actionResult.Automations)    // цикл по автоматизациям в результате...
        {
            switch (automation.Type.Name)   // различные действия в зависимости от типа
            {
                case "Завершить действие":  // для завершения действия - просто закрываем текущую форму
                    _form.Close();
                    break;
                case ("Обновить объект"):   // обновление объекта - даёт значения параметрам объекта в соответствии с выбранными значениями в действии в том случае, если обновляется именно этот объект
                    ObjectInstance currentObjectInstance =
                        _action.GetObjectByActionObject(automation.Object).ObjectInstance; // получаем экземпляр объекта
                    foreach (ActionInstanceParameterInstance parameter in _action.Parameters) // в цикле по параметрам действия
                    {
                        if (parameter.ActionParameter!.Object == automation.Object)  // если параметр принадлежит объекту, обновляемому автоматизацией
                        {
                            currentObjectInstance.SetParameterValue(parameter.ActionParameter.Parameter, _action.Parameters.FirstOrDefault(p => p.ActionParameter!.Parameter == parameter.ActionParameter.Parameter)!.ObjectParameter.GetValue());
                            // то задаём ему новое значение, соответствующее параметру в действии
                        }
                    }
                    continue;
                case ("Сохранить объект"): // сохранение объекта - образует новый объект 
                    ObjectInstance newObjectInstance = _action.Objects
                        .FirstOrDefault(o => o.ActionObject == automation.Object)!.ObjectInstance;  // получаем экземпляр объекта
                    foreach (ActionInstanceParameterInstance parameter in _action.Parameters)   // в цикле по параметрам действия -
                    {
                        if (parameter.ActionParameter!.Object == automation.Object)  // если объект параметра совпадает с сохраняемым объектом, то
                        {
                            newObjectInstance.SetParameterValue(parameter.ActionParameter.Parameter, _action.Parameters.FirstOrDefault(p => p.ActionParameter.Parameter == parameter.ActionParameter.Parameter).ObjectParameter.GetValue());
                                // определяем ему соответствующее значение
                        }
                    }
                    _context.Objects.Objects.Add(newObjectInstance);
                    continue;
                case ("Принять значение параметра"): // приём значения параметра - устанавливаем значение основного параметра в значение зависимого параметра 
                    _action.GetParameterByActionParameter(automation.MainParameter).ObjectParameter.SetValue(_action.GetParameterByActionParameter(automation.DependParameter).ObjectParameter.GetValue());
                    continue;
            }

            if (_form.IsDisposed) break;    // дополнительно обработаем закрытие формы - чтобы в случае, если раньше просили закрыть, дальше по циклу не пойти
                                            // даже в том случае, если там по настройке действия что-то предполагается
        }

        if (!_form.IsDisposed) _form.Close();   // если ранее не закрывали форму - то закроем теперь, потому что после отработки результата она нам не нужна
    }

    public void AddToContainer(DynamicLayout container) // добавляет контроллер в ETO-контейнер
    {
        container.AddRow(_controller);
    }
}