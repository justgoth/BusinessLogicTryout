namespace CustomizedInterfaceTryout.Objects;

public class CObjectLink
{
    private LinkTypes _linkType;
    private CObject _linkObject;

    public CObjectLink(LinkTypes linkType, CObject linkObject)
    {
        _linkType = linkType;
        _linkObject = linkObject;
    }
}