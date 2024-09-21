namespace CustomizedInterfaceTryout.Objects;

public class ObjectParameter
{
    private CParameter _parametertype;
    private int _integervalue;
    private float _floatingvalue;
    private string? _stringvalue;
    private DateTime _datetimevalue;
    private ObjectInstance? _objectvalue;

    public ObjectParameter(CParameter parametertype)
    {
        _parametertype = parametertype;
    }

    public void SetValue(dynamic value)
    {
        switch (_parametertype.Type)
        {
            case ParameterTypes.Integer:
                _integervalue = (int)value;
                break;
            case ParameterTypes.Floating:
                _floatingvalue = (float)value;
                break;
            case ParameterTypes.String:
                _stringvalue = (string)value;
                break;
            case ParameterTypes.DateTime:
                _datetimevalue = (DateTime)value;
                break;
            case ParameterTypes.ListItem:
                _objectvalue = (ObjectInstance)value;
                break;
            case ParameterTypes.CObjectLink:
                _objectvalue = (ObjectInstance)value;
                break;
        }
    }

    public dynamic? GetValue()
    {
        switch (_parametertype.Type)
        {
            case ParameterTypes.Integer:
                return _integervalue;
            case ParameterTypes.Floating:
                return _floatingvalue;
            case ParameterTypes.String:
                return _stringvalue;
            case ParameterTypes.DateTime:
                return _datetimevalue;
            case ParameterTypes.ListItem:
                return _objectvalue; 
            case ParameterTypes.CObjectLink:
                return _objectvalue; 
        }
        return null;
    }

    public string? GetVisibleValue()
    {
        switch (_parametertype.Type)
        {
            case ParameterTypes.Integer:
                return _integervalue.ToString();
            case ParameterTypes.Floating:
                return _floatingvalue.ToString();
            case ParameterTypes.String:
                return _stringvalue;
            case ParameterTypes.DateTime:
                return _datetimevalue.ToString();
            case ParameterTypes.ListItem:
                return _objectvalue.GetParameterValue(_objectvalue.getType.VisibleValueParameter).ToString();
            case ParameterTypes.CObjectLink:
                return _objectvalue.GetParameterValue(_objectvalue.getType.VisibleValueParameter).ToString();
        }
        return null;
    }
    
    public CParameter? Type => _parametertype;

    public string Name => _parametertype.Name;
    
    public string Description => _parametertype.Description;
}