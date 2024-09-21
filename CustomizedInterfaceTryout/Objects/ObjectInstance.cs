namespace CustomizedInterfaceTryout.Objects;

public class ObjectInstance
{
    private CObject _objecttype;
    private List<ObjectParameter> _parameters;

    public ObjectInstance(CObject objecttype)
    {
        _objecttype = objecttype;
        _parameters = new List<ObjectParameter>();
        foreach (CParameter parameter in objecttype.Parameters)
        {
            _parameters.Add(new ObjectParameter(parameter));
        }
    }
    
    public List<ObjectParameter> Parameters => _parameters;

    public void SetParameterValue(CParameter parameter, dynamic value)
    {
        ObjectParameter _parameter = _parameters.FirstOrDefault(c => c.Type == parameter);
        if (_parameter != null)
        {
            _parameter.SetValue(value);
        }
    }

    public void SetParameterValueByName(string name, dynamic value)
    {
        ObjectParameter _parameter = _parameters.FirstOrDefault(c => c.Type.Name == name);
        if (_parameter != null)
        {
            _parameter.SetValue(value);
        }
    }

    public dynamic GetParameterValue(CParameter parameter)
    {
        ObjectParameter _parameter = _parameters.FirstOrDefault(c => c.Type == parameter);
        if (_parameter != null)
        {
            return _parameter.GetValue();
        }
        else return null;
    }
    
    public CObject getType => _objecttype;

    public string Name => getType.Name;
    public string Description => getType.Description;
}