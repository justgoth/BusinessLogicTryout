namespace BusinessLogicTryout.Actions;

public class ActionResultAutomationType
{
    private int _id;
    private string _name;

    public ActionResultAutomationType(int id, string name)
    {
        _id = id;
        _name = name;
    } 
    
    public int Id => _id;
    public string Name => _name;
}