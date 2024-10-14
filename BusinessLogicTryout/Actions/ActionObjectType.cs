using System.Security.Cryptography;

namespace BusinessLogicTryout.Actions;

public class ActionObjectType
{
    private int _id;
    private string _name;

    public ActionObjectType(int id, string name)
    {
        _id = id;
        _name = name;
    }

    public int Id => _id;
    
    public string Name => _name;
}