namespace BusinessLogicTryout.Objects;

public class ParameterTypes // набор доступных типов параметров
{
    private readonly List<ParameterType> _types; // перечень типов
    public ParameterTypes() // добавляет типы. потом можно переделать на получение из БД,
                            // но на этих типах будет многое строиться (логика обработки, отображения...)
                            // - подумать, как реализовать
    {
        _types = [];
        AddType(new ParameterType(0, "Целое число"));
        AddType(new ParameterType(1, "Дробное число"));
        AddType(new ParameterType(2, "Строка"));
        AddType(new ParameterType(3, "Дата-время"));
        AddType(new ParameterType(4, "Выбор из списка"));
        AddType(new ParameterType(5, "Ссылка на объект"));
    }

    private void AddType(ParameterType type) // добавление типа
    {
        _types.Add(type);
    }

    public ParameterType? GetById(int id) // получить тип по id
    {
        return _types.FirstOrDefault(t => t.Id == id);
    }

    public ParameterType? GetByName(string name) // получить тип по наименованию
    {
        return _types.FirstOrDefault(t => t.Name == name);
    }
    
    public List<ParameterType> Types => _types; // список типов для доступа
}