namespace BusinessLogicTryout.Actions;

public class ActionResultAutomation // автоматизация
    (int id,
    ActionResultAutomationType type,
    ActionParameter? mainParameter,
    ActionParameter? dependParameter,
    ActionObject? cObject = null)
{
    public ActionResultAutomation(ActionResultAutomationType type, ActionParameter? mainParameter = null, ActionParameter? dependParameter = null, ActionObject? cObject = null) : this(0, type, mainParameter, dependParameter, cObject)
    {
    }

    public void SetId(int id)   // назначить Id
    {
        Id = id;
    }
    
    public ActionParameter? MainParameter { get; set; } = mainParameter;    // параметр, который будет принимать значение

    public ActionParameter? DependParameter { get; } = dependParameter; // параметр, значение которого будем принимать

    public ActionObject? Object { get; } = cObject; // связь с объектом в действии

    public ActionResultAutomationType Type { get; } = type; // тип автоматизации

    public int Id { get; private set; } = id;   // Id
}