namespace CustomizedInterfaceTryout.Objects;

public class CObject
{
    private int _id;
    private string _name;
    private string _description;
    private List<CObjectLink> _linkedObjects;
    private List<CParameter> _parameters;
    private CParameter _visiblevalueparameter;

    public CObject(string name, string description)
    {
        _name = name;
        _description = description;
        _linkedObjects = new List<CObjectLink>();
        _parameters = new List<CParameter>();
    }

    public CObject(CObject parent, string name, string description) : this(name, description)
    {
        _linkedObjects.Add(new CObjectLink(LinkTypes.Parent, parent));
        _parameters.AddRange(parent.Parameters);
    }

    public void AddParameter(CParameter parameter)
    {
        _parameters.Add(parameter);
    }

    public void AddVisibleValueParameter(CParameter parameter)
    {
        _parameters.Add(parameter);
        _visiblevalueparameter = _parameters.FirstOrDefault(p => p.Name == parameter.Name);
    }
    
    public List<CParameter> Parameters => _parameters;
    
    public CParameter? VisibleValueParameter => _visiblevalueparameter;
    
    public string Name => _name;
    public string Description => _description;
}