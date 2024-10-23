using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;

public class ActionInstanceParameterInstance // экземпляр параметра в экземпляре действия
    (ActionParameter? actionParameter, ObjectParameter objectParameter)
{
    public void SetOriginParameter(ObjectParameter? originParameter)
    {
        OriginParameter = originParameter;
    }
    
    public ActionParameter? ActionParameter { get; } = actionParameter;  // параметр в действии для доступа

    public ObjectParameter ObjectParameter { get; } = objectParameter;  // экземпляр параметра для доступа

    public ObjectParameter? OriginParameter { get; private set; }   // параметр-прообраз для доступа
}