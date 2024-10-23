namespace BusinessLogicTryout.Actions;

public class ActionResult   // результат выполнения действия
    (string text)
{
    public ActionResult(int id, string text) : this(text)
    {
        Id = id;
    }

    public void AddAutomation(ActionResultAutomation automation)    // добавить ранее инициализированную автоматизацию
    {
        Automations.Add(automation);
        Automations.Last().SetId(Automations.Count - 1);
    }

    public ActionResultAutomation AddAutomation(ActionResultAutomationType type, ActionParameter? mainParameter, ActionParameter? dependParameter, ActionObject? cObject)  // добавить новую автоматизацию
    {
        var newAutomation = new ActionResultAutomation(type, mainParameter, dependParameter, cObject);
        AddAutomation(newAutomation);
        return newAutomation;
    }

    public void SetId(int id)   // присвоить Id
    {
        Id = id;
    }

    public string Text { get; } = text; // видимый текст для доступа

    public List<ActionResultAutomation> Automations { get; } = new List<ActionResultAutomation>();  // перечень автоматизаций

    public int Id { get; private set; } // Id
}