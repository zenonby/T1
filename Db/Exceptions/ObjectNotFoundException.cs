namespace T1.Db.Exceptions;

/// <summary>
/// Requested DB object was not found
/// </summary>
public class ObjectNotFoundException : Exception
{
    public ObjectNotFoundException()
        : base("Объект с указанным Id не найден")
    {
    }
}
