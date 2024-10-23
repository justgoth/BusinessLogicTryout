using System.Collections.ObjectModel;
using BusinessLogicTryout.Actions;

namespace BusinessLogicTryout.Repositories;

public class ActionInstanceRepository   // репозиторий экземпляров действий
{
    private readonly ObservableCollection<ActionInstance> _actions = new(); // перечень экземпляров

    public ActionInstance AddNewActionInstance(CAction action, string name, string description) // добавляем экземпляр действия (новый)
    {
        _actions.Add(new ActionInstance(action, name, description));
        return _actions.Last();
    }

    public void AddActionInstance(ActionInstance actionInstance)    // добавляем экземпляр действия (ранее объявленный)
    {
        _actions.Add(actionInstance);
    }

    public ObservableCollection<ActionInstance> Actions => _actions;    // экземпляры действий для доступа
}