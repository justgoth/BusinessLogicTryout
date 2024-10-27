namespace BusinessLogicTryout.Objects;

public class CObjectLink // связь между объектами
    (LinkType linkType, CObject linkObject)
{
    private LinkType _linkType = linkType;    // тип связи
    private CObject _linkObject = linkObject;    // объект, с которым связан текущий объект
    
    public LinkType LinkType => _linkType;
    public CObject Object => _linkObject;
}