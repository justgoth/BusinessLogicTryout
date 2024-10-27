namespace BusinessLogicTryout.Objects;

public class ObjectInstanceLink // связь объекта
    (CParameter parameter, ObjectInstance objectInstance, LinkType? linkType)
{
    private ObjectInstance _linkedObject = objectInstance;
    private CParameter _coreParameter = parameter;
    private LinkType? _linkType = linkType;
    
    public ObjectInstance Object => _linkedObject;
    public LinkType? LinkType => _linkType;
    public CParameter CoreParameter => _coreParameter;
}