namespace BusinessLogicTryout.Actions;

public class ActionResultAutomationTypes
{
    private List<ActionResultAutomationType> _types;

    public ActionResultAutomationTypes()
    {
        _types = new List<ActionResultAutomationType>();
        AddType(0, "Принять значение параметра");
        AddType(1, "Сохранить объект");
        AddType(2, "Обновить объект");
        AddType(3, "Завершить действие");
    }

    public void AddType(ActionResultAutomationType type)
    {
        _types.Add(type);
    }

    public void AddType(int id, string name)
    {
        _types.Add(new ActionResultAutomationType(id, name));
    }

    public ActionResultAutomationType GetById(int id)
    {
        return _types.FirstOrDefault(t => t.Id == id);
    }

    public ActionResultAutomationType GetByName(string name)
    {
        return _types.FirstOrDefault(t => t.Name == name);
    }
}