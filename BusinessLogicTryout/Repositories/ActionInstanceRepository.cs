using System.Collections.ObjectModel;
using BusinessLogicTryout.Actions;

namespace BusinessLogicTryout.Repositories;

public class ActionInstanceRepository
{
    private ObservableCollection<ActionInstance> _actions;

    public ActionInstanceRepository()
    {
        _actions = new ObservableCollection<ActionInstance>();
    }

    public ActionInstance AddNewActionInstance(CAction action, string name, string description)
    {
        _actions.Add(new ActionInstance(action, name, description));
        return _actions.Last();
    }

    public void AddActionInstance(ActionInstance actionInstance)
    {
        _actions.Add(actionInstance);
    }

    public ObservableCollection<ActionInstance> Actions => _actions;
}