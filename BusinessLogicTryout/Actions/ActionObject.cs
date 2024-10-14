using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;

public class ActionObject
{
    private CObject _object;
    private ActionObjectType _type;
    private int _id;
    private ActionParameter _sourceparameter;

    public ActionObject(int id, CObject obj, ActionObjectType type)
    {
        _id = id;
        _object = obj;
        _type = type;
    }

    public ActionObject(CObject obj, ActionObjectType type)
    {
        _object = obj;
        _type = type;
    }

    public void SetId(int id)
    {
        _id = id;
    }

    public void AddParameterLink(ActionParameter parameter)
    {
        _sourceparameter = parameter;
    }
    
    public CObject Object => _object;
    public ActionObjectType Type => _type;
    public ActionParameter SourceParameter
    {
        get => _sourceparameter;
        set => _sourceparameter = value;
    }

    public int Id => _id;
}