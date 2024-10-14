namespace BusinessLogicTryout.Actions;

using BusinessLogicTryout.Objects;

public class ActionInstance
{
    private string _name;
    private string _description;
    private CAction _action;
    private List<ActionInstanceObjectInstance> _objects;
    private List<ActionInstanceParameterInstance> _parameters;

    public ActionInstance(CAction action, string name, string description)
    {
        _name = name;
        _description = description;
        _action = action;
        _objects = new List<ActionInstanceObjectInstance>();
        foreach(ActionObject obj in _action.Objects) _objects.Add(new ActionInstanceObjectInstance(obj, new ObjectInstance(obj.Object)));
        _parameters = new List<ActionInstanceParameterInstance>();
        foreach(ActionParameter param in _action.Parameters) _parameters.Add(new ActionInstanceParameterInstance(param, new ObjectParameter(param.Parameter)));
    }

    public void SetObjectInstance(ActionInstanceObjectInstance obj, ObjectInstance instance)
    {
        obj.SetOriginObject(instance);
        foreach (ActionInstanceParameterInstance param in _parameters)
        {
            if (param.ActionParameter.Object == obj.ActionObject)
            {
                param.SetOriginParameter(instance.GetParameterByType(param.ObjectParameter.Type));
            }
        }
    }
    
    public string Name => _name;
    public string Description => _description;
    
    public List<ActionInstanceParameterInstance> Parameters => _parameters;
    
    public List<ActionInstanceObjectInstance> Objects => _objects;
    public CAction Action => _action;

    public ActionInstanceObjectInstance GetObjectByActionObject(ActionObject obj)
    {
        return _objects.FirstOrDefault(o => o.ActionObject == obj);
    }

    public ActionInstanceParameterInstance GetParameterByActionParameter(ActionParameter param)
    {
        return _parameters.FirstOrDefault(p => p.ActionParameter == param);
    }
}