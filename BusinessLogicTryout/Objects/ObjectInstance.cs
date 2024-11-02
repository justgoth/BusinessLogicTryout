using Object = Atk.Object;

namespace BusinessLogicTryout.Objects;

public class ObjectInstance // экземпляр объекта
{
    private readonly List<ObjectParameter> _parameters;  // перечень значений параметров
    private readonly List<ObjectInstanceLink> _linkedInstances; // связанные объекты

    public ObjectInstance(CObject objectType)
    {
        Type = objectType;
        _parameters = new List<ObjectParameter>();
        _linkedInstances = new List<ObjectInstanceLink>();
        foreach (CParameter parameter in objectType.Parameters)
        {
            _parameters.Add(new ObjectParameter(parameter));
        }
    }
    
    public List<ObjectParameter> Parameters => _parameters; // параметры для доступа

    public void SetParameterValue(CParameter changingParameter, dynamic value, bool clean = false) // задать значение параметру по параметру
    {
        ObjectParameter? parameterToChange = _parameters.FirstOrDefault(p => p.Type == changingParameter);
        if (parameterToChange is null) return;  // если вдруг (НО КАК?) окажется, что такого нет - то не будем ничего делать 
        if (changingParameter.LinkType is not null) // если параметр задаёт связи... 
        {
            if ((clean)||(!changingParameter.IsMultiple)) // если указали, что нужно очистить, или параметр не множественный то удалим эти связи...
            {
                foreach (ObjectInstanceLink link in _linkedInstances.Where(l => l.CoreParameter == changingParameter))
                    _linkedInstances.Remove(link);
            }
            // и добавим новую связь
            _linkedInstances.Add(new ObjectInstanceLink(changingParameter, (ObjectInstance)value, changingParameter.LinkType));
        }
        // а теперь если указали, что нужно очистить или параметр не множественный - то установим значение, а иначе - добавим значение...
        if ((clean) || (!changingParameter.IsMultiple))
            parameterToChange.SetValue(value);
        else
            parameterToChange.AddValue(value);
    }

    public void SetParameterValueByName(string name, dynamic value, bool clean = false) // задать значение параметру по наименованию
    {
        // приведём name к CParameter
        CParameter changingParameter = _parameters.Find(c => c.Type?.Name == name)?.Type;
        if (changingParameter != null)
        {
            SetParameterValue(changingParameter, value);
        }
    }

    public dynamic? GetParameterValue(CParameter? parameterToGet)  // получить значение параметра по параметру
    {
        ObjectParameter? parameter = _parameters.Find(c => c.Type == parameterToGet);
        return parameter?.GetValue();
    }

    public ObjectParameter? GetParameterByType(CParameter type)  // получить параметр определённого типа
    {
        return _parameters.Find(c => c.Type == type)!;
    }

    public List<ObjectInstance> GetLinkedObjects(LinkType linkType) // возвращает список связанных объектов по типу связи
    {
        List<ObjectInstance> returnValue = new List<ObjectInstance>();
        foreach (ObjectInstanceLink link in _linkedInstances.Where(l => l.LinkType == linkType))
        {
            returnValue.Add(link.Object);
        }
        return returnValue;
    }
    
    public CObject Type { get; }

    public string Name => Type.Name;    // наименование (наименование типа) для доступа
    public string Description => Type.Description;  // обозначение (обозначение типа) для доступа
    public string VisibleValue => GetParameterValue(Type.VisibleValueParameter)?.ToString() ?? "NULL"; // видимое значение (значение видимого параметра) для доступа
}