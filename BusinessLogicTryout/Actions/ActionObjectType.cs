using System.Security.Cryptography;

namespace BusinessLogicTryout.Actions;

public class ActionObjectType // тип объекта в действии
    (int id, string name)
{
    public int Id { get; } = id;

    public string Name { get; } = name;
}