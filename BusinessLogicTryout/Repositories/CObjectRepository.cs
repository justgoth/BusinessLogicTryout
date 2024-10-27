using System.Collections.ObjectModel;
using BusinessLogicTryout.Objects;

namespace BusinessLogicTryout.Repositories;

public class CObjectRepository
{
    private ObservableCollection<CObject> _objects;
    private LinkTypes _linkTypes;

    public CObjectRepository()
    {
        _objects = new ObservableCollection<CObject>();
        _linkTypes = new LinkTypes();
    }

    public CObject AddNewObject(string name, string description)
    {
        _objects.Add(new CObject(name, description));
        _objects.Last().SetId(_objects.Count == 1 ? 0 : (_objects[^2].Id + 1));
        return _objects.Last();
    }

    public CObject AddNewObject(string name, string description, CObject? parent)
    {
        _objects.Add(new CObject(parent, name, description));
        _objects.Last().SetId(_objects.Count == 1 ? 0 : (_objects[^2].Id + 1));
        return _objects.Last();
    }

    public CObject AddObject(CObject addObject)
    {
        _objects.Add(addObject);
        _objects.Last().SetId(_objects.Count == 1 ? 0 : (_objects[^2].Id + 1));
        return _objects.Last();
    }
    
    public ObservableCollection<CObject> CObjects => _objects;
    
    public void UpdateById(CObject cobject, string name, string description)
    {
        CObject _object = new(name, description);
        _object.SetId(cobject.Id);
        _objects[_objects.IndexOf(cobject)] = _object;
    }
}