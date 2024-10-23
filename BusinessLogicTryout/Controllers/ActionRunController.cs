using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Repositories;
using Eto.Forms;

namespace BusinessLogicTryout.Controllers;


public class ActionRunController : Form // контроллер интерфейса ETO для запуска действия - новая форма
{
    private readonly ActionInstance _action;    // экземпляр действия, создаётся новым при создании контроллера
    private List<ActionObjectController>? _objectControllers;    // список контроллеров объектов
    private List<ActionParameterController>? _parameterControllers;  // список контроллеров параметров
    private readonly DataContext _context;   // DI: контекст данных
    
    
    public ActionRunController(CAction action, DataContext context)
    {
        _action = new ActionInstance(action, action.Name, action.Description);
        _context = context;
    }

    public new void Initialize()   // инициализация, вынесена в отдельный метод преднамеренно. Идея в том, что дальше - для действий с жёстко "предопределёнными"
        // экземплярами объекта мы сначала конструируем действие, и только потом будем вызывать инициализацию
    {
        _parameterControllers = new List<ActionParameterController>();
        _objectControllers = new List<ActionObjectController>();
        Title = _action.Description;
        Height = 800;
        Width = 1200;
        DynamicLayout layout = new DynamicLayout();
        Content = layout;
        foreach (ActionInstanceObjectInstance objectInstance in _action.Objects)    // в цикле по всем экземплярам объектов в действии
        {
            if (objectInstance.ActionObject!.Type == _context.CActions.ObjectTypes.GetByName("Выбирается")) // если выбирается пользователем - то добавим соответствующий контроллер
            {
                _objectControllers.Add(new ActionObjectController(this, _action, objectInstance.ActionObject, _context));
                _objectControllers.Last().AddToContainer(layout);
            }
        }
        
        foreach (ActionInstanceParameterInstance parameterInstance in _action.Parameters)   // в цикле по всем экземплярам параметров в действии 
        {
            _parameterControllers.Add(new ActionParameterController(_action, parameterInstance.ActionParameter!, _context));    // добавим контроллер интерфейса
            _parameterControllers.Last().AddToContainer(layout);
        }

        
        layout.BeginHorizontal();
        foreach (ActionResult result in _action.Action.Results) // в цикле по всем результатам действия
        {
            ActionResultController resultController = new ActionResultController(_action, result, _context, this);  // создадим контроллер результата
            resultController.AddToContainer(layout);    // и добавим его на ETO-форму
        }
        layout.EndHorizontal();
        
        layout.BeginHorizontal();
        layout.EndHorizontal();
    }

    public void RefreshParameterControllers()   // обновляет контроллеры парметров - просто в цикле дёргает рефреш для каждого
    {
        foreach (ActionParameterController parameterController in _parameterControllers)
        {
            parameterController.Refresh();
        }
    }
}