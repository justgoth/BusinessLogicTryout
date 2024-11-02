namespace BusinessLogicTryout.Objects;

public class LinkTypes // Типы связей между объектами
{
    private readonly List<LinkType> _types = new();

    public LinkTypes()
    {
        AddType(1, "Связан");
        AddType(2, "Родитель");
        AddType(3, "Ребёнок");
        AddType(4, "Входит в");
        AddType(5, "Содержит");
    }

    private void AddType(int id, string name)   // добавляет новый тип
    {
        _types.Add(new LinkType(id, name));
    }
    
    public LinkType GetById(int id) => _types.Find(t => t.Id == id);    // вернуть по идентификатору
    public LinkType GetByName(string name) => _types.Find(t => t.Name == name); // вернуть по имени
}