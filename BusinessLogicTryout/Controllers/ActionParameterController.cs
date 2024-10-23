using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Objects;
using BusinessLogicTryout.Repositories;
using Eto.Forms;


namespace BusinessLogicTryout.Controllers;

public class ActionParameterController  // контроллер ETO для параметра в действии
{
    private readonly Control _controller;   // собственно контроллер ETO
    private readonly Label _label;  // описание
    private readonly ActionParameter _actionParameter;  // DI: ссылка на параметр действия
    private readonly DataContext _context;   // DI: контекст данных
    private readonly ActionInstance _action; // DI: ссылка на экземпляр действия

    public ActionParameterController(ActionInstance action, ActionParameter actionParameter, DataContext context)
    {
        _context = context;
        _actionParameter = actionParameter;
        _action = action;
        _label = new Label();
        _label.Text = actionParameter.Parameter.Description + ":";
        switch (actionParameter.Parameter.Type.Id)
        {
            case 0:
                _controller = new TextBox
                {
                    // пока что просто пустой Edit
                };
                ((TextBox)_controller).TextChanged += TextChange;
                break;
            case 1:
                _controller = new TextBox
                {
                    // пока что просто пустой Edit
                };
                ((TextBox)_controller).TextChanged += TextChange;
                break;
            case 2:
                _controller = new TextBox
                {
                    // пока что просто пустой Edit
                };
                ((TextBox)_controller).TextChanged += TextChange;
                break;
            case 3:
                _controller = new Calendar
                {
                    // пока что просто пустой DateTimeCombo
                };
                break;
            case 4: // это - выбор из контекста объектов, соответствующих по типу
                List<ObjectInstance> instances = new();
                    instances.AddRange(_context.Objects.Objects.Where(o => o.Type == actionParameter.Parameter.ObjectType).
                        OrderBy(o => o.VisibleValue).ToArray());
                _controller = new DropDown
                {
                    DataStore = instances,
                    ItemTextBinding = Binding.Property<ObjectInstance, string>(o => o.VisibleValue)
                };
                ((DropDown)_controller).SelectedValueChanged += DropDownChange;
                break;
            case 5:
                _controller = new DropDown
                {
                    // пока что - пустой ComboBox
                };
                break;
        }

        _controller!.Enabled = !actionParameter.ReadOnly;
        _controller.Visible = actionParameter.Visible;
    }

    public void Refresh()   // устанавливает значение в ETO-контроллере в соответствии с текущим значением исходного параметра
    {
        ObjectParameter? originParameter = _action.GetParameterByActionParameter(_actionParameter).OriginParameter; // получаем исходный экземпляр параметра
        if (originParameter != null)    // дальше - только если он не пустой
        {
            switch (_actionParameter.Parameter.Type.Id) // в зависимости от его типа...
            {
                case 0: // если int
                    ((TextBox)_controller).Text = originParameter.GetValue().ToString();
                    break;
                case 1: // если float
                    ((TextBox)_controller).Text = originParameter.GetValue().ToString();
                    break;
                case 2: // если String
                    ((TextBox)_controller).Text = originParameter.GetValue().ToString();
                    break;
                case 3: // если DateTime
                    ((Calendar)_controller).SelectedDate = originParameter.GetValue();
                    break;
                case 4: // если ссылка на объект
                    ((DropDown)_controller).SelectedValue = originParameter.GetValue();
                    break;
                case 5: // и ещё раз если ссылка на объект
                    ((DropDown)_controller).SelectedValue = originParameter.GetValue();
                    break;
            }
        }
    }

    public void AddToContainer(DynamicLayout container) // добавляет в ETO-контейнер
    {
        if (_controller.Visible)
        {
            container.BeginHorizontal();
            container.AddRow(_label, new Label { Width = 20 }, _controller);
            container.EndHorizontal();
        }
    }

    private void DropDownChange(object? sender, EventArgs e)    // в случае смены значения для параметра, представленного ComboBox
    {
        if (((DropDown)_controller).SelectedValue != null)  // только в том случае, если значение выбрано
        {
            _action.Parameters.FirstOrDefault(p => p.ActionParameter == _actionParameter)!.ObjectParameter.SetValue(((DropDown)_controller).SelectedValue);
                // сменим параметр для экземпляра действия
            foreach (ActionInstanceObjectInstance objectInstance in _action.Objects.Where(o =>
                         o.ActionObject!.SourceParameter == _actionParameter))   // в цикле по всем объектам действия, для которых текущий параметр является исходным
            {
                objectInstance.SetInstance((ObjectInstance)((DropDown)_controller).SelectedValue);  // назначим экземпляр в соответствии с выбранным вариантом...
            }
        }
    }

    private void TextChange(object? sender, EventArgs e)    // в случае смены значения для параметра, представленного Edit
    {
        _action.Parameters.FirstOrDefault(p => p.ActionParameter == _actionParameter)!.ObjectParameter.SetValue(
            ((TextBox)_controller).Text);   // зададим значение параметра в соответствие с введённым в Edit
    }
}