namespace BusinessLogicTryout.Actions;

public class ActionResult
{
    private string _text;
    private List<ActionResultAutomation> _automations;
    private int _id;

    public ActionResult(string text)
    {
        _text = text;
        _automations = new List<ActionResultAutomation>();
    }

    public ActionResult(int id, string text)
    {
        _id = id;
        _text = text;
        _automations = new List<ActionResultAutomation>();
    }

    public void AddAutomation(ActionResultAutomation automation)
    {
        _automations.Add(automation);
        _automations.Last().SetId(_automations.Count - 1);
    }

    public ActionResultAutomation AddAutomation(ActionResultAutomationType type, ActionParameter mainparameter, ActionParameter dependparameter, ActionObject cobject)
    {
        _automations.Add(new ActionResultAutomation(type, mainparameter, dependparameter, cobject));
        _automations.Last().SetId(_automations.Count - 1);
        return _automations.Last();
    }

    public void SetId(int id)
    {
        _id = id;
    }
    
    public string Text => _text;
    public List<ActionResultAutomation> Automations => _automations;
    public int Id => _id;
}