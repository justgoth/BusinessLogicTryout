namespace BusinessLogicTryout.Objects;

public class CObject // объект
    (string name, string description)    // наименование
                                         // описание
{
    private int _id;    // id
    private readonly List<CObjectLink> _linkedObjects = [];   // связанные объекты
    private readonly List<CParameter> _parameters = [];   // параметры
    private CParameter? _visiblevalueparameter;  // параметр, который определяет, что выводится как обозначение экземпляра объекта при выборе объекта

    public CObject(CObject parent, string name, string description) : this(name, description)
    {
        _linkedObjects.Add(new CObjectLink(LinkTypes.Parent, parent));
        _parameters.AddRange(parent.Parameters);
    }

    public void AddParameter(CParameter parameter)  // добавляет новый параметр
    {
        _parameters.Add(parameter);
    }

    public void AddVisibleValueParameter(CParameter parameter)  // добавляет новый параметр и назначает его как вывод обозначения объекта
    {
        _parameters.Add(parameter);
        _visiblevalueparameter = _parameters.FirstOrDefault(p => p.Name == parameter.Name);
    }

    public CParameter GetParameterByName(string parameterName)   // возвращает параметр по имени
    {
        return _parameters.FirstOrDefault(p => p.Name == parameterName)!;
    }
    
    public List<CParameter> Parameters => _parameters;  // параметры для доступа
    
    public CParameter? VisibleValueParameter => _visiblevalueparameter; // видимый параметр для доступа
    
    public string Name => name;    // наименование для доступа
    public string Description => description;  // обозначение для доступа
    public int Id => _id;   // id для доступа

    public void SetId(int id)   // присваивает id
    {
        _id = id;
    }
}