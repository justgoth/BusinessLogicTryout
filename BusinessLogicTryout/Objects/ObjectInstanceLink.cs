namespace BusinessLogicTryout.Objects;

public class ObjectInstanceLink // связь объекта
    (CParameter parameter, ObjectInstance objectInstance, LinkType linkType)
{
    public ObjectInstance Object { get; } = objectInstance; // экземпляр объекта

    public LinkType LinkType { get; } = linkType;   // тип связи

    public CParameter CoreParameter { get; } = parameter;   // ключевой параметр связи
}