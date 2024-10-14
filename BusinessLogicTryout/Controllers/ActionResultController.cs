using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Objects;
using BusinessLogicTryout.Repositories;
using Eto.Forms;

namespace BusinessLogicTryout.Controllers;

public class ActionResultController
{
    private Button _controller;
    private ActionResult _actionResult;
    private DataContext _context;
    private Form _form;
    private ActionInstance _action;
    
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

    private void RunResult(object? sender, EventArgs e)
    {
        foreach (ActionResultAutomation automation in _actionResult.Automations)
        {
            switch (automation.Type.Name)
            {
                case "Завершить действие":
                    Console.WriteLine(_action.Name + " завершается");
                    _form.Close();
                    break;
                case ("Обновить объект"):
                    ObjectInstance currentObjectInstance =
                        _action.GetObjectByActionObject(automation.Object).ObjectInstance;
                    Console.WriteLine("Обновить объект: " + currentObjectInstance.Name);
                    foreach (ActionInstanceParameterInstance parameter in _action.Parameters)
                    {
                        if (parameter.ActionParameter.Object == automation.Object)
                        {
                            currentObjectInstance.SetParameterValue(parameter.ActionParameter.Parameter, _action.Parameters.FirstOrDefault(p => p.ActionParameter.Parameter == parameter.ActionParameter.Parameter).ObjectParameter.GetValue());
                            Console.WriteLine(parameter.ActionParameter.Parameter.Name);
                            Console.WriteLine("      принимает значение " +
                                              currentObjectInstance.GetParameterByType(parameter.ActionParameter.Parameter).GetVisibleValue());
                        }
                    }
                    continue;
                case ("Сохранить объект"):
                    ObjectInstance newObjectInstance = _action.Objects
                        .FirstOrDefault(o => o.ActionObject == automation.Object).ObjectInstance;
                    Console.WriteLine("Сохранить объект: " + newObjectInstance.Name);
                    foreach (ActionInstanceParameterInstance parameter in _action.Parameters)
                    {
                        if (parameter.ActionParameter.Object == automation.Object)
                        {
                            newObjectInstance.SetParameterValue(parameter.ActionParameter.Parameter, _action.Parameters.FirstOrDefault(p => p.ActionParameter.Parameter == parameter.ActionParameter.Parameter).ObjectParameter.GetValue());
                            Console.WriteLine(parameter.ActionParameter.Parameter.Name);
                            Console.WriteLine("      принимает значение " +
                                               newObjectInstance.GetParameterByType(parameter.ActionParameter.Parameter).GetVisibleValue());
                        }
                    }
                    _context.Objects.Objects.Add(newObjectInstance);
                    continue;
                case ("Принять значение параметра"):
                    Console.WriteLine("Принять значение параметра: " + automation.MainParameter.Parameter.Name + "(" +
                                      automation.MainParameter.Object.Object.Name + ") : " + automation.DependParameter.Parameter.Name +
                                      "(" + automation.DependParameter.Object.Object.Name + ") : " + _action.GetParameterByActionParameter(automation.DependParameter).ObjectParameter.GetValue().ToString());
                    // вот тут неверно!
                    _action.GetParameterByActionParameter(automation.MainParameter).ObjectParameter.SetValue(_action.GetParameterByActionParameter(automation.DependParameter).ObjectParameter.GetValue());
                    continue;
            }

            if (_form.IsDisposed) break;
        }

        if (!_form.IsDisposed) _form.Close();
    }

    public void AddToContainer(DynamicLayout container)
    {
        container.AddRow(_controller);
    }
}