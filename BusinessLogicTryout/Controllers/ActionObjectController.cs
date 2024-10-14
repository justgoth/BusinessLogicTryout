using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Objects;
using BusinessLogicTryout.Repositories;
using Eto.Forms;

namespace BusinessLogicTryout.Controllers;

public class ActionObjectController
{
    private Control _controller;
    private Label _label;
    private ActionObject _actionObject;
    private DataContext _context;
    private ActionInstance _action;
    private ActionRunController _parent;

    public ActionObjectController(ActionRunController parent, ActionInstance action, ActionObject obj, DataContext context)
    {
        _parent = parent;
        _action = action;
        _actionObject = obj;
        _context = context;
        _label = new Label();
        _label.Text = _actionObject.Object.Description + ":";
        List<ObjectInstance> _instances = new List<ObjectInstance>();
        _instances.AddRange(_context.Objects.Objects.Where(o => o.Type == _actionObject.Object).
            OrderBy(o => o.VisibleValue).ToArray());
        _controller = new DropDown
        {
            DataStore = _instances,
            ItemTextBinding = Binding.Property<ObjectInstance, string>(o => o.VisibleValue),
            Visible = true,
            Enabled = true
        };
        ((DropDown)_controller).SelectedIndexChanged += CSelectedItemChanged;
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

    public void CSelectedItemChanged(object sender, EventArgs e)
    {
        _action.SetObjectInstance(_action.GetObjectByActionObject(_actionObject), 
            (ObjectInstance)((DropDown)_controller).SelectedValue);
        _parent.RefreshParameterControllers();
    }
}