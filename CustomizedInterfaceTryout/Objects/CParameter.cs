namespace CustomizedInterfaceTryout.Objects;

public class CParameter
{
    private int _id;
    private string _name;
    private string _description;
    private ParameterTypes _type;
    private CObject? _objectType;

    public CParameter(string name, string description, ParameterTypes type)
    {
        _name = name;
        _description = description;
        _type = type;
    }
    
    public CParameter(string name, string description, ParameterTypes type, CObject parentobject) : this(name, description, type)
    {
        _objectType = parentobject;
    }

    public ParameterTypes Type => _type;
    public string Name => _name;
    public string Description => _description;
}