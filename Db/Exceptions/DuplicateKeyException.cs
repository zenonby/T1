namespace T1.Db.Exceptions;

/// <summary>
/// Duplicate key exception
/// </summary>
public class DuplicateKeyException : Exception
{
    public DuplicateKeyException()
        : base("Объект с указанным Id уже существует")
    {
    }
}
