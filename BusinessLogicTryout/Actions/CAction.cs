using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;

public class CAction
{
    private string _name;
    private string _description;
    private List<ActionObject?> _objects;
    private List<ActionParameter?> _parameters;
    private List<ActionResult> _results;
    
    public CAction(string name, string description)
    {
        _name = name;
        _description = description;
        _objects = new List<ActionObject?>();
        _parameters = new List<ActionParameter?>();
        _results = new List<ActionResult>();
    }

    public void AddObject(ActionObject? obj)
    {
        int _id = _objects.Count;
        obj.SetId(_id);
        _objects.Add(obj);
    }

    public ActionObject? AddObject(CObject obj, ActionObjectType type)
    {
        int _id = _objects.Count;
        _objects.Add(new ActionObject(_id, obj, type));
        return _objects.Last();
    }
    
    public void AddParameter(ActionParameter? param)
    {
        int _id = _parameters.Count;
        param.SetId(_id);
        _parameters.Add(param);
    }

    public ActionParameter? AddParameter(CParameter param, ActionObject? obj, bool editable = true, bool obligatory = false, bool visible = true)
    {
        int _id = _parameters.Count;
        _parameters.Add(new ActionParameter(_id, param, obj, editable, obligatory, visible));
        return _parameters.Last();
    }

    public void AddResult(ActionResult result)
    {
        int _id = _results.Count;
        result.SetId(_id);
        _results.Add(result);
    }

    public ActionResult AddResult(string text)
    {
        _results.Add(new ActionResult(text));
        return _results.Last();
    }
    
    public List<ActionObject?> Objects => _objects;
    public List<ActionParameter?> Parameters => _parameters;
    public List<ActionResult> Results => _results;
    public string Name => _name;
    public string Description => _description;
}