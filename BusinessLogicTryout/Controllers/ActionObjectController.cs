using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Objects;
using BusinessLogicTryout.Repositories;
using Eto.Forms;

namespace BusinessLogicTryout.Controllers;

public class ActionObjectController // контроллер интерфейса ETO для объекта в действии
{
    private readonly Control _controller;    // собственно контроллер ETO
    private readonly Label _label;   // описание
    private readonly ActionObject _actionObject;    // DI: связь с объектом в действии
    private readonly DataContext _context;   // DI: контекст данных
    private readonly ActionInstance _action; // DI: ссылка на экземпляр действия
    private readonly ActionRunController _parent;    // DI: ссылка на контроллер интерфейса ETO для действия

    public ActionObjectController(ActionRunController parent, ActionInstance action, ActionObject obj, DataContext context)
    {
        _parent = parent;
        _action = action;
        _actionObject = obj;
        _context = context;
        _label = new Label();
        _label.Text = _actionObject!.Object.Description + ":";
        List<ObjectInstance> instances = new List<ObjectInstance>();
        instances.AddRange(_context.Objects.Objects.Where(o => o.Type == _actionObject.Object).
            OrderBy(o => o.VisibleValue).ToArray());    // контроллер - всегда ComboBox, доступные значения - все объекты такого типа из контекста
        _controller = new DropDown
        {
            DataStore = instances,
            ItemTextBinding = Binding.Property<ObjectInstance, string>(o => o.VisibleValue),
            Visible = true,
            Enabled = true
        };
        ((DropDown)_controller).SelectedIndexChanged += CSelectedItemChanged;
    }
    
    public void AddToContainer(DynamicLayout container) // добавляет контроллер в контейнер ETO
    {
        if (!_controller.Visible) return;
        container.BeginHorizontal();
        container.AddRow(_label, new Label { Width = 20 }, _controller);
        container.EndHorizontal();
    }

    private void CSelectedItemChanged(object? sender, EventArgs e)    // обработчик изменения выбранного экземпляра объекта
    {
        _action.SetObjectInstance(_action.GetObjectByActionObject(_actionObject), 
            (ObjectInstance)((DropDown)_controller).SelectedValue); // зададим экземпляр для действия
        _parent.RefreshParameterControllers();  // Обновим контроллеры параметров (т.к. они могут зависеть от экземпляра объекта)
    }
}