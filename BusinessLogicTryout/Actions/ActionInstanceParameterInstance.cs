using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;

public class ActionInstanceParameterInstance
{
    private ActionParameter _actionParameter;
    private ObjectParameter _objectParameter;
    private ObjectParameter _originParameter;

    public ActionInstanceParameterInstance(ActionParameter actionParameter, ObjectParameter objectParameter)
    {
        _actionParameter = actionParameter;
        _objectParameter = objectParameter;
    }

    public void SetOriginParameter(ObjectParameter originParameter)
    {
        _originParameter = originParameter;
    }
    
    public ActionParameter ActionParameter => _actionParameter;
    public ObjectParameter ObjectParameter => _objectParameter;
    public ObjectParameter OriginParameter => _originParameter;
}