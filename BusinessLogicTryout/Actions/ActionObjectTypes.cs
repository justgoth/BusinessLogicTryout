namespace BusinessLogicTryout.Actions;

public class ActionObjectTypes
{
    private List<ActionObjectType> _types;

    public ActionObjectTypes()
    {
        _types = new List<ActionObjectType>();
        AddType(new ActionObjectType(1, "Выбирается"));
        AddType(new ActionObjectType(2, "Передаётся"));
        AddType(new ActionObjectType(3, "Создается"));
        AddType(new ActionObjectType(4, "На основании"));
    }

    public void AddType(ActionObjectType type)
    {
        _types.Add(type);
    }

    public ActionObjectType GetById(int id)
    {
        return _types.FirstOrDefault(x => x.Id == id);
    }

    public ActionObjectType GetByName(string name)
    {
        return _types.FirstOrDefault(x => x.Name == name);
    }
    
    public List<ActionObjectType> Types => _types;
}