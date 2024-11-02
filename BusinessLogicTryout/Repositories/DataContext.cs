namespace BusinessLogicTryout.Repositories;

public class DataContext
{
    public CObjectRepository CObjects { get; } = new CObjectRepository();

    public CParameterRepository CParameters { get; } = new CParameterRepository();

    public ObjectInstanceRepository Objects { get; } = new ObjectInstanceRepository();

    public CActionRepository CActions { get; } = new CActionRepository();

    public ActionInstanceRepository Actions { get; } = new ActionInstanceRepository();
}