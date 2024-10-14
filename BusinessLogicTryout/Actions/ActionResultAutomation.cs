namespace BusinessLogicTryout.Actions;

public class ActionResultAutomation
{
    private ActionResultAutomationType _type;
    private ActionParameter _mainparameter;
    private ActionParameter _dependparameter;
    private ActionObject _object;
    private int _id;

    public ActionResultAutomation(ActionResultAutomationType type, ActionParameter mainparameter = null, ActionParameter dependparameter = null, ActionObject cobject = null)
    {
        _type = type;
        _mainparameter = mainparameter;
        _dependparameter = dependparameter;
        _object = cobject;
    }
    public ActionResultAutomation(int id, ActionResultAutomationType type, ActionParameter mainparameter, ActionParameter dependparameter, ActionObject cobject = null)
    {
        _type = type;
        _mainparameter = mainparameter;
        _dependparameter = dependparameter;
        _object = cobject;
        _id = id;
    }
    public void SetId(int id)
    {
        _id = id;
    }
    
    public ActionParameter MainParameter
    {
        get => _mainparameter;
        set => _mainparameter = value;
    }

    public ActionParameter DependParameter => _dependparameter;
    public ActionObject Object => _object;
    public ActionResultAutomationType Type => _type;
    public int Id => _id;
}