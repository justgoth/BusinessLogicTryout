using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;


public class ActionParameter // параметр в действии
    (int id, CParameter parameter, ActionObject? obj, bool editable = true, bool obligatory = false, bool visible = true)
{
    public ActionParameter(CParameter parameter, ActionObject? obj, bool editable = true, bool obligatory = false,
        bool visible = true) : this(0, parameter, obj, editable, obligatory, visible)
    {
    }

    public void SetId(int id)   // назначить Id
    {
        Id = id;
    }
    
    public ActionObject? Object { get; } = obj;  // объект, параметром которого является параметр для доступа

    public CParameter Parameter { get; set; } = parameter;  // параметр для доступа

    public bool ReadOnly => !editable;  // только чтение?
    public bool Obligatory { get; } = obligatory;   // обязателен для заполнения экземпляром?

    public bool Visible { get; } = visible; // должен ли быть видимым в интерфейсе пользователя?

    public int Id { get; private set; } = id;   // Id
}