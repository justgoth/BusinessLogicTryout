namespace BusinessLogicTryout.Objects;

public class ObjectInstance // экземпляр объекта
{
    private readonly CObject _objectType;    // ссылка на тип объекта
    private readonly List<ObjectParameter> _parameters;  // перечень значений параметров

    public ObjectInstance(CObject objectType)
    {
        _objectType = objectType;
        _parameters = new List<ObjectParameter>();
        foreach (CParameter parameter in objectType.Parameters)
        {
            _parameters.Add(new ObjectParameter(parameter));
        }
    }
    
    public List<ObjectParameter> Parameters => _parameters; // параметры для доступа

    public void SetParameterValue(CParameter changingParameter, dynamic value)  // задать значение параметру по параметру
    {
        ObjectParameter? parameter = _parameters.FirstOrDefault(c => c.Type == changingParameter);
        if (parameter != null)
        {
            parameter.SetValue(value);
        }
    }

    public void SetParameterValueByName(string name, dynamic value) // задать значение параметру по наименованию
    {
        ObjectParameter? parameter = _parameters.FirstOrDefault(c => c.Type?.Name == name);
        if (parameter != null)
        {
            parameter.SetValue(value);
        }
    }

    public dynamic? GetParameterValue(CParameter? parameterToGet)  // получить значение параметра по параметру
    {
        ObjectParameter? parameter = _parameters.FirstOrDefault(c => c.Type == parameterToGet);
        return parameter?.GetValue();
    }

    public ObjectParameter? GetParameterByType(CParameter type)  // получить параметр определённого типа
    {
        return _parameters.FirstOrDefault(c => c.Type == type)!;
    }
    
    public CObject Type => _objectType; // тип для доступа

    public string Name => Type.Name;    // наименование (наименование типа) для доступа
    public string Description => Type.Description;  // обозначение (обозначение типа) для доступа
    
    public string VisibleValue => GetParameterValue(Type.VisibleValueParameter)?.ToString() ?? "NULL"; // видимое значение (значение видимого параметра) для доступа
}