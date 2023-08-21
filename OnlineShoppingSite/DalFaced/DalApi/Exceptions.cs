namespace DalApi;

/// <summary>
/// This page contain the Expetions.
/// </summary>
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) { }
}
public class EntityDuplicateException : Exception
{
    public EntityDuplicateException(string message) : base(message) { }
}
