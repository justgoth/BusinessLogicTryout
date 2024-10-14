using System.Collections.ObjectModel;
using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Objects;
using BusinessLogicTryout.Repositories;
using Eto.Forms;


namespace BusinessLogicTryout.Controllers;

public class ActionParameterController
{
    private Control _controller;
    private Label _label;
    private ActionParameter _actionParameter;
    private DataContext _context;
    private ActionInstance _action;

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
                
                };
                ((TextBox)_controller).TextChanged += TextChange;
                break;
            case 1:
                _controller = new TextBox
                {

                };
                ((TextBox)_controller).TextChanged += TextChange;
                break;
            case 2:
                _controller = new TextBox
                {

                };
                ((TextBox)_controller).TextChanged += TextChange;
                break;
            case 3:
                _controller = new Calendar
                {

                };
                break;
            case 4:
                List<ObjectInstance> _instances = new();
                    _instances.AddRange(_context.Objects.Objects.Where(o => o.Type == actionParameter.Parameter.ObjectType).
                        OrderBy(o => o.VisibleValue).ToArray());
                _controller = new DropDown
                {
                    DataStore = _instances,
                    ItemTextBinding = Binding.Property<ObjectInstance, string>(o => o.VisibleValue)
                };
                ((DropDown)_controller).SelectedValueChanged += DropDownChange;
                break;
            case 5:
                _controller = new DropDown
                {

                };
                break;
        }

        _controller.Enabled = !actionParameter.ReadOnly;
        _controller.Visible = actionParameter.Visible;
    }

    public void Refresh()
    {
        ObjectParameter? originParameter = _action.GetParameterByActionParameter(_actionParameter).OriginParameter;
        if (originParameter != null)
        {
            switch (_actionParameter.Parameter.Type.Id)
            {
                case 0:
                    ((TextBox)_controller).Text = originParameter.GetValue().ToString();
                    break;
                case 1:
                    ((TextBox)_controller).Text = originParameter.GetValue().ToString();
                    break;
                case 2:
                    ((TextBox)_controller).Text = originParameter.GetValue().ToString();
                    break;
                case 3:
                    ((Calendar)_controller).SelectedDate = originParameter.GetValue();
                    break;
                case 4:
                    ((DropDown)_controller).SelectedValue = originParameter.GetValue();
                    break;
                case 5:
                    ((DropDown)_controller).SelectedValue = originParameter.GetValue();
                    break;
            }
        }
    }

    public void AddToContainer(DynamicLayout container)
    {
        if (_controller.Visible)
        {
            container.BeginHorizontal();
            container.AddRow(_label, new Label { Width = 20 }, _controller);
            container.EndHorizontal();
        }
    }

    private void DropDownChange(object sender, EventArgs e)
    {
        if (((DropDown)_controller).SelectedValue != null)
        {
            _action.Parameters.FirstOrDefault(p => p.ActionParameter == _actionParameter).ObjectParameter.SetValue(((DropDown)_controller).SelectedValue);
            foreach (ActionInstanceObjectInstance objectInstance in _action.Objects.Where(o =>
                         o.ActionObject.SourceParameter == _actionParameter))
            {
                objectInstance.SetInstance((ObjectInstance)((DropDown)_controller).SelectedValue);
            }
        }
    }

    private void TextChange(object sender, EventArgs e)
    {
        _action.Parameters.FirstOrDefault(p => p.ActionParameter == _actionParameter).ObjectParameter.SetValue(
            ((TextBox)_controller).Text);
    }
}