using BusinessLogicTryout.Actions;
using BusinessLogicTryout.Objects;
using BusinessLogicTryout.Repositories;
using Eto.Forms;

namespace BusinessLogicTryout.Controllers;

public class ActionResultController // контроллер интерфейса ETO для результата действия
{
    private readonly Button _controller; // собственно ETO-контроллер, всегда кнопка
    private readonly ActionResult _actionResult; // DI: ссылка на результат
    private readonly DataContext _context; // DI: ссылка на контекст данных
    private readonly Form _form; // DI: ссылка на вызвавшую действие форму
    private readonly ActionInstance _action; // DI: ссылка на экземпляр действия
    
    public ActionResultController(ActionInstance action, ActionResult actionResult, DataContext context, Form form)
    {
        _context = context;
        _actionResult = actionResult;
        _action = action;
        _form = form;
        _controller = new Button
        {
            Text = _actionResult.Text
        };
        _controller.Click += RunResult;
    }

    private void RunResult(object? sender, EventArgs e) // обработчик нажатия на кнопку
    {
        _action.ExecuteResult(_actionResult, _context);     // вызовем обработку результата
        if (!_form.IsDisposed) _form.Close();   // если ранее не закрывали форму - то закроем теперь, потому что после отработки результата она нам не нужна
    }

    public void AddToContainer(DynamicLayout container) // добавляет контроллер в ETO-контейнер
    {
        container.AddRow(_controller);
    }
}