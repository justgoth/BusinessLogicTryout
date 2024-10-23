using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;

public class ActionInstanceObjectInstance // экземпляр объекта в экземпляре действия
    (ActionObject? actionObject, ObjectInstance objectInstance)
{
    public void SetInstance(ObjectInstance objectInstance)  // задаёт ссылку на экземпляр объекта
    {
        ObjectInstance = objectInstance;
    }

    public void SetOriginObject(ObjectInstance? originObject)   // задаёт ссылку на объект-прообраз 
    {
        OriginObject = originObject;
    }
    
    public ActionObject? ActionObject { get; } = actionObject;   // объект действия для доступа

    public ObjectInstance ObjectInstance { get; private set; } = objectInstance;    // экземпляр объекта для доступа

    public ObjectInstance? OriginObject { get; private set; } // объект-прообраз для доступа
}