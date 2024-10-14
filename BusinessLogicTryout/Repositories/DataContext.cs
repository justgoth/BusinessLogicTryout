namespace BusinessLogicTryout.Repositories;

public class DataContext
{
    private CObjectRepository _cobjects = new CObjectRepository();
    private CParameterRepository _cparameters = new CParameterRepository();
    private ObjectInstanceRepository _objects = new ObjectInstanceRepository();
    private CActionRepository _cactions = new CActionRepository();
    private ActionInstanceRepository _actions = new ActionInstanceRepository();
    
    public CObjectRepository CObjects => _cobjects;
    public CParameterRepository CParameters => _cparameters;
    public ObjectInstanceRepository Objects => _objects;
    public CActionRepository CActions => _cactions;
    public ActionInstanceRepository Actions => _actions;
}