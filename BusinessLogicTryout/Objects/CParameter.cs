namespace BusinessLogicTryout.Objects;

public class CParameter // параметр объекта
    (string name, string description, ParameterType type, bool multiple = false)
{
    private int _id;    // id
    private string _name = name;   // наименование
    private string _description = description;    // описание
    private ParameterType _type = type;    // тип
    private CObject? _objectType;   // ссылка на объект (тип объекта) для типа, в котором значение является объектом
    private LinkType? _linkType; // тип связи (для параметров являющихся ссылкой на объект)
    private bool _multiple = multiple; // множественный параметр или нет...

    public CParameter(string name, string description, ParameterType type, CObject parentobject, bool multiple = false) : this(name, description, type, multiple)
    {
        _objectType = parentobject;
    }

    public CParameter(string name, string description, ParameterType type, CObject parentobject, LinkType linktype, bool multiple = false) :
        this(name, description, type, parentobject, multiple)
    {
        _linkType = linktype;
    }

    public void Update(CParameter parameter)    // обновляет текущий параметр значениями из переданного (копирование)
    {
        _id = parameter.Id;
        _name = parameter.Name;
        _description = parameter.Description;
        _type = parameter.Type;
        _objectType = parameter.ObjectType;
        _linkType = parameter.LinkType;
        _multiple = parameter.IsMultiple;
    }

    public LinkType? LinkType => _linkType; // тип связи для доступа
    public ParameterType Type => _type; // тип для доступа
    public string Name => _name;    // наименование для доступа
    public string Description => _description;  // обозначение для доступа
    public CObject? ObjectType => _objectType;  // объектный тип для доступа

    public void SetId(int id)   // присваивает параметру ID
    {
        _id = id;
    }
    
    public int Id => _id;   // ID для доступа
    public bool IsMultiple => _multiple; // признак множественности для доступа
}