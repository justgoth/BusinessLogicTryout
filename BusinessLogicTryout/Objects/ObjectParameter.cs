using System.Globalization;

namespace BusinessLogicTryout.Objects;

public class ObjectParameter(CParameter parametertype)
{
    private readonly List<int> _integervalue = new();  // целочисленное значение
    private readonly List<float> _floatingvalue = new();   // нецелочисленное значение
    private readonly List<string> _stringvalue = new();   // строковое значение
    private readonly List<DateTime> _datetimevalue = new();    // значение дата-время
    private readonly List<ObjectInstance> _objectvalue = new();   // значение - ссылка на экземпляр объекта

    public void SetValue(dynamic value) // задание значения
    {
        switch (parametertype.Type.Id)
        {
            case 0:
                _integervalue.Clear();
                _integervalue.Add(Convert.ToInt32(value));
                break;
            case 1:
                _floatingvalue.Clear();
                _floatingvalue.Add(Convert.ToSingle(value));
                break;
            case 2:
                _stringvalue.Clear();
                _stringvalue.Add((string)value);
                break;
            case 3:
                _datetimevalue.Clear();
                _datetimevalue.Add((DateTime)value);
                break;
            case 4:
                _objectvalue.Clear();
                _objectvalue.Add((ObjectInstance)value);
                break;
            case 5:
                _objectvalue.Clear();
                _objectvalue.Add((ObjectInstance)value);
                break;
        }
    }

    public void AddValue(dynamic value) // добавление значения для множественного параметра
    {
        switch (parametertype.Type.Id)
        {
            case 0:
                if (!IsMultiple) _integervalue.Clear();
                _integervalue.Add(Convert.ToInt32(value));
                break;
            case 1:
                if (!IsMultiple) _floatingvalue.Clear();
                _floatingvalue.Add(Convert.ToSingle(value));
                break;
            case 2:
                if (!IsMultiple) _stringvalue.Clear();
                _stringvalue.Add((string)value);
                break;
            case 3:
                if (!IsMultiple) _datetimevalue.Clear();
                _datetimevalue.Add((DateTime)value);
                break;
            case 4:
                if (!IsMultiple) _objectvalue.Clear();
                _objectvalue.Add((ObjectInstance)value);
                break;
            case 5:
                if (!IsMultiple) _objectvalue.Clear();
                _objectvalue.Add((ObjectInstance)value);
                break;
        }
    }

    public void RemoveValue(bool clear, dynamic value) // удаление значения для множественного параметра
    {
        if (!IsMultiple) return;    // не должно работать для немножественного параметра
        switch (parametertype.Type.Id)
        {
            case 0:
                _integervalue.Remove(Convert.ToInt32(value));
                break;
            case 1:
                _floatingvalue.Remove(Convert.ToSingle(value));
                break;
            case 2:
                _stringvalue.Remove((string)value);
                break;
            case 3:
                _datetimevalue.Remove((DateTime)value);
                break;
            case 4:
                _objectvalue.Remove((ObjectInstance)value);
                break;
            case 5:
                _objectvalue.Remove((ObjectInstance)value);
                break;
        }
    }
    
    
    public dynamic GetValue()  // получение значения
    {
        return parametertype.Type.Id switch
        {
            0 => IsMultiple ? _integervalue : _integervalue[0],
            1 => IsMultiple ? _floatingvalue : _floatingvalue[0],
            2 => IsMultiple ? _stringvalue : (_stringvalue[0] ?? "Не задано"),
            3 => IsMultiple ? _datetimevalue : _datetimevalue[0],
            4 => IsMultiple ? (dynamic)_objectvalue : (dynamic)_objectvalue[0],
            5 => IsMultiple ? (dynamic)_objectvalue : (dynamic)_objectvalue[0],
            _ => "ОШИБКА"
        };
    }
    public string GetVisibleValue() // получение видимого значения (всегда строка!)
    {
        Console.WriteLine(parametertype.Name);
        switch (parametertype.Type.Id)
        {
            case 0:
                if (_integervalue.Count == 0) return "0";
                return _integervalue[0].ToString() + ((_integervalue.Count == 1)
                    ? ""
                    : " и ещё " + (_integervalue.Count - 1).ToString());
            case 1:
                if (_floatingvalue.Count == 0) return "0";
                return _floatingvalue[0].ToString(CultureInfo.InvariantCulture) + ((_floatingvalue.Count == 1)
                        ? ""
                        : " и ещё " + (_floatingvalue.Count - 1).ToString() + "..");
            case 2:
                if (_stringvalue.Count == 0) return "Не задано";
                return _stringvalue[0]  + ((_stringvalue.Count == 1)
                        ? ""
                        : " и ещё " + (_stringvalue.Count - 1).ToString() + "..");
            case 3:
                if (_datetimevalue.Count == 0) return "Не задано";
                return _datetimevalue[0].ToString(CultureInfo.InvariantCulture) + ((_datetimevalue.Count == 1)
                        ? ""
                        : " и ещё " + (_datetimevalue.Count - 1).ToString() + "..");
            case 4:
                if (_objectvalue.Count > 0)
                    return (_objectvalue[0].GetParameterValue(_objectvalue[0].Type.VisibleValueParameter)?.ToString() ?? "NULL") +
                           ((_objectvalue.Count == 1)
                           ? ""
                           : " и ещё " + (_objectvalue.Count - 1).ToString() + "..");
                else return "Не задано";
            case 5:
                if (_objectvalue.Count > 0)
                    return (_objectvalue[0].GetParameterValue(_objectvalue[0].Type.VisibleValueParameter)?.ToString() ?? "NULL") +
                           ((_objectvalue.Count == 1)
                               ? ""
                               : " и ещё " + (_objectvalue.Count - 1).ToString() + "..");
                else return "Не задано";
        }
        return "ОШИБКА!";
    }
    
    public CParameter Type => parametertype;  // тип параметра
    public bool IsMultiple => Type!.IsMultiple;

    public string Name => parametertype.Name;  // наименование (наименование типа) для доступа
    
    public string Description => parametertype.Description;    // обозначение (обозначение типа) для доступа
}