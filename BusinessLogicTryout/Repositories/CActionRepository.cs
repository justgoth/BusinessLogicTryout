using System.Collections.ObjectModel;
using BusinessLogicTryout.Actions;

namespace BusinessLogicTryout.Repositories;

public class CActionRepository
{
    private ObservableCollection<CAction> _actions;
    private ActionObjectTypes _types;
    private ActionResultAutomationTypes _automationTypes;

    public CActionRepository()
    {
        _actions = new ObservableCollection<CAction>();
        _types = new ActionObjectTypes();
        _automationTypes = new ActionResultAutomationTypes();
    }

    public CAction AddNewAction(string name, string description)
    {
        _actions.Add(new CAction(name, description));
        return _actions.Last();
    }

    public void AddAction(CAction action)
    {
        _actions.Add(action);
    }

    public ObservableCollection<CAction> Actions => _actions;
    public ActionObjectTypes ObjectTypes => _types;
    
    public ActionResultAutomationTypes AutomationTypes => _automationTypes;
}