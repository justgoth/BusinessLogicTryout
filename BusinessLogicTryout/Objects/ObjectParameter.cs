using System.Globalization;

namespace BusinessLogicTryout.Objects;

public class ObjectParameter(CParameter parametertype)
{
    // тип параметра 
    private int _integervalue;  // целочисленное значение
    private float _floatingvalue;   // нецелочисленное значение
    private string? _stringvalue;   // строковое значение
    private DateTime _datetimevalue;    // значение дата-время
    private ObjectInstance? _objectvalue;   // значение - ссылка на экземпляр объекта

    public void SetValue(dynamic value) // задание значения
    {
        switch (parametertype.Type.Id)
        {
            case 0:
                _integervalue = Convert.ToInt32(value);
                break;
            case 1:
                _floatingvalue = Convert.ToSingle(value);
                break;
            case 2:
                _stringvalue = (string)value;
                break;
            case 3:
                _datetimevalue = (DateTime)value;
                break;
            case 4:
                _objectvalue = (ObjectInstance)value;
                break;
            case 5:
                _objectvalue = (ObjectInstance)value;
                break;
        }
    }

    public dynamic GetValue()  // получение значения
    {
        return parametertype.Type.Id switch
        {
            0 => _integervalue,
            1 => _floatingvalue,
            2 => _stringvalue ?? "NULL",
            3 => _datetimevalue,
            4 => (dynamic)_objectvalue! ?? "NULL",
            5 => (dynamic)_objectvalue! ?? "NULL",
            _ => "ОШИБКА"
        };
    }
    public string GetVisibleValue() // получение видимого значения (всегда строка!)
    {
        switch (parametertype.Type.Id)
        {
            case 0:
                return _integervalue.ToString();
            case 1:
                return _floatingvalue.ToString(CultureInfo.InvariantCulture);
            case 2:
                return _stringvalue ?? "NULL";
            case 3:
                return _datetimevalue.ToString(CultureInfo.InvariantCulture);
            case 4:
                if (_objectvalue != null)
                    return _objectvalue.GetParameterValue(_objectvalue.Type.VisibleValueParameter)?.ToString() ?? "NULL";
                else return "";
            case 5:
                if (_objectvalue != null)
                    return _objectvalue.GetParameterValue(_objectvalue.Type.VisibleValueParameter)?.ToString() ?? "NULL";
                else return "";
        }
        return "ОШИБКА!";
    }
    
    public CParameter? Type => parametertype;  // тип для доступа

    public string Name => parametertype.Name;  // наименование (наименование типа) для доступа
    
    public string Description => parametertype.Description;    // обозначение (обозначение типа) для доступа
}