using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;

public class ActionInstanceObjectInstance
{
    private ActionObject _actionObject;
    private ObjectInstance _objectInstance;
    private ObjectInstance _originObject;

    public ActionInstanceObjectInstance(ActionObject actionObject, ObjectInstance objectInstance)
    {
        _actionObject = actionObject;
        _objectInstance = objectInstance;
    }

    public void SetInstance(ObjectInstance objectInstance)
    {
        _objectInstance = objectInstance;
    }

    public void SetOriginObject(ObjectInstance originObject)
    {
        _originObject = originObject;
    }
    
    public ActionObject ActionObject => _actionObject;
    public ObjectInstance ObjectInstance => _objectInstance;
    public ObjectInstance OriginObject => _originObject;
}