namespace BusinessLogicTryout.Actions;

public class ActionResultAutomationTypes    // список доступных типов автоматизаций
{
    private readonly List<ActionResultAutomationType> _types;

    public ActionResultAutomationTypes()
    {
        _types = new List<ActionResultAutomationType>();
        AddType(0, "Принять значение параметра");
        AddType(1, "Сохранить объект");
        AddType(2, "Обновить объект");
        AddType(3, "Завершить действие");
    }

    public void AddType(ActionResultAutomationType type)    // добавить ранее инициализированный тип
    {
        _types.Add(type);
    }

    private void AddType(int id, string name)   // добавить новый тип
    {
        _types.Add(new ActionResultAutomationType(id, name));
    }

    public ActionResultAutomationType GetById(int id)   // получить по Id
    {
        return _types.FirstOrDefault(t => t.Id == id)!;
    }

    public ActionResultAutomationType GetByName(string name)    // получить по наименованию
    {
        return _types.FirstOrDefault(t => t.Name == name)!;
    }
}