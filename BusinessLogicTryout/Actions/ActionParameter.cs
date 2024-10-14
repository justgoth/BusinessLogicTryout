using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;


public class ActionParameter
{
    private CParameter _parameter;
    private ActionObject _object;
    private bool _editable;
    private bool _obligatory;
    private bool _visible;
    private int _id;

    public ActionParameter(int id, CParameter parameter, ActionObject obj, bool editable = true, bool obligatory = false, bool visible = true)
    {
        _id = id;
        _parameter = parameter;
        _object = obj;
        _editable = editable;
        _obligatory = obligatory;
        _visible = visible;
    }

    public ActionParameter(CParameter parameter, ActionObject obj, bool editable = true, bool obligatory = false,
        bool visible = true)
    {
        _parameter = parameter;
        _object = obj;
        _editable = editable;
        _obligatory = obligatory;
        _visible = visible;
    }

    public void SetId(int id)
    {
        _id = id;
    }
    
    public ActionObject Object => _object;
    public CParameter Parameter
    {
        get => _parameter;
        set => _parameter = value;
    }

    public bool ReadOnly => !_editable;
    public bool Obligatory => _obligatory;
    public bool Visible => _visible;
    public int Id => _id;
}