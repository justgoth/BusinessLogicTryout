namespace BusinessLogicTryout.Objects;

public class CObjectLink // связь между объектами
    (LinkTypes linkType, CObject linkObject)
{
    private LinkTypes _linkType = linkType;    // тип связи
    private CObject _linkObject = linkObject;    // объект, с которым связан текущий объект
}