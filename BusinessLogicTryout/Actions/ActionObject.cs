using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;

public class ActionObject   // объект в действии
    (int id, CObject obj, ActionObjectType type)
{
    public ActionObject(CObject obj, ActionObjectType type) : this(0, obj, type)
    {
    }

    public void SetId(int id)   // назначить уникальный идентификатор
    {
        Id = id;
    }

    public void AddParameterLink(ActionParameter parameter) // связать с параметром действия
    {
        SourceParameter = parameter;
    }
    
    public CObject Object { get; } = obj;   // тип объекта для доступа

    public ActionObjectType Type { get; } = type;   // тип объекта в действии для доступа

    public ActionParameter? SourceParameter { get; set; }   // ссылка на связанный параметр

    public int Id { get; private set; } = id;   // Id для доступа
}