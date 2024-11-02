using System.Collections.ObjectModel;

namespace BusinessLogicTryout.Repositories;

using BusinessLogicTryout.Objects;

public class ObjectInstanceRepository
{
    private readonly ObservableCollection<ObjectInstance> _objects = new();

    public ObjectInstance AddNewObjectInstance(CObject objecttype)
    {
        ObjectInstance addedObject = new(objecttype);
        _objects.Add(addedObject);
        return _objects.Last();
    }
    
    public ObservableCollection<ObjectInstance> Objects => _objects;
}