namespace BusinessLogicTryout.Actions;

public class ActionObjectTypes  // типы объектов в действии
{
    public ActionObjectTypes()
    {
        Types = new List<ActionObjectType>();
        AddType(new ActionObjectType(1, "Выбирается"));
        AddType(new ActionObjectType(2, "Передаётся"));
        AddType(new ActionObjectType(3, "Создается"));
        AddType(new ActionObjectType(4, "На основании"));
    }

    private void AddType(ActionObjectType type) // добавляет новый тип
    {
        Types.Add(type);
    }

    public ActionObjectType GetById(int id) // поиск по Id
    {
        return Types.FirstOrDefault(x => x.Id == id)!;
    }

    public ActionObjectType GetByName(string name)  // поиск по наименованию
    {
        return Types.FirstOrDefault(x => x.Name == name)!;
    }

    private List<ActionObjectType> Types { get; }   // список доступных типов
}