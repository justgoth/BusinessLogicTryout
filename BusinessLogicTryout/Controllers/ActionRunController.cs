using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Repositories;
using Eto.Forms;

namespace BusinessLogicTryout.Controllers;


public class ActionRunController : Form
{
    private ActionInstance _action;
    private List<ActionObjectController> _objectControllers;
    private List<ActionParameterController> _controllers;
    private DataContext _context;
    
    
    public ActionRunController(CAction action, DataContext context)
    {
        _action = new ActionInstance(action, action.Name, action.Description);
        _context = context;
    }

    public void Initialize()
    {
        _controllers = new List<ActionParameterController>();
        _objectControllers = new List<ActionObjectController>();
        Title = _action.Description;
        Height = 800;
        Width = 1200;
        DynamicLayout _layout = new DynamicLayout();
        Content = _layout;
        foreach (ActionInstanceObjectInstance objectInstance in _action.Objects)
        {
            if (objectInstance.ActionObject.Type == _context.CActions.ObjectTypes.GetByName("Выбирается"))
            {
                _objectControllers.Add(new ActionObjectController(this, _action, objectInstance.ActionObject, _context));
                _objectControllers.Last().AddToContainer(_layout);
            }
        }
        
        foreach (ActionInstanceParameterInstance parameterInstance in _action.Parameters)
        {
            _controllers.Add(new ActionParameterController(_action, parameterInstance.ActionParameter, _context));
            _controllers.Last().AddToContainer(_layout);
        }

        
        _layout.BeginHorizontal();
        foreach (ActionResult result in _action.Action.Results)
        {
            ActionResultController resultController = new ActionResultController(_action, result, _context, this);
            resultController.AddToContainer(_layout);
        }
        _layout.EndHorizontal();
        
        _layout.BeginHorizontal();
        _layout.EndHorizontal();
    }

    public void RefreshParameterControllers()
    {
        foreach (ActionParameterController parameterController in _controllers)
        {
            parameterController.Refresh();
        }
    }
}