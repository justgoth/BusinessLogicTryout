using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Actions;

public class CAction // шаблон действия
    (string name, string description)
{

    private readonly List<ActionObject> _objects = new();   // объекты в шаблоне действия
    private readonly List<ActionParameter> _parameters = new();    // параметры в шаблоне действия
    private readonly List<ActionResult> _results = new();   // результаты действия

    public void AddObject(ActionObject obj)     // добавляет ранее созданный объект действия в действие
    {
        int id = _objects.Count;
        obj.SetId(id);
        _objects.Add(obj);
    }

    public ActionObject AddObject(CObject obj, ActionObjectType type)  // добавляет новый объект в действие
    {
        int id = _objects.Count;
        _objects.Add(new ActionObject(id, obj, type));
        return _objects.Last();
    }
    
    public void AddParameter(ActionParameter param) // добавляет ранее созданный параметр действия в действие
    {
        int id = _parameters.Count;
        param.SetId(id);
        _parameters.Add(param);
    }

    public ActionParameter AddParameter(CParameter param, ActionObject? obj, bool editable = true, bool obligatory = false, bool visible = true)
        // добавляет параметр в действие
    {
        int id = _parameters.Count;
        _parameters.Add(new ActionParameter(id, param, obj, editable, obligatory, visible));
        return _parameters.Last();
    }

    public void AddResult(ActionResult result)  // добавляет ранее созданный результат в действие
    {
        int id = _results.Count;
        result.SetId(id);
        _results.Add(result);
    }

    public ActionResult AddResult(string text)  // добавляет новый результат в действие
    {
        _results.Add(new ActionResult(text));
        _results.Last().SetId(_results.Count - 1);
        return _results.Last();
    }
    
    public List<ActionObject> Objects => _objects;  // объекты для доступа
    public List<ActionParameter> Parameters => _parameters; // параметры для доступа
    public List<ActionResult> Results => _results;  // результаты для доступа
    public string Name => name; // наименование для доступа
    public string Description => description;   // описание для доступа
}