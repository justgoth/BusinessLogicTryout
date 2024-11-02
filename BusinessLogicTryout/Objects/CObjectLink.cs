namespace BusinessLogicTryout.Objects;

public class CObjectLink // связь между объектами
    (LinkType linkType, CObject linkObject)
{
    public LinkType LinkType { get; } = linkType;   // тип связи

    public CObject Object { get; } = linkObject;    // объект, с которым установлена связь
}