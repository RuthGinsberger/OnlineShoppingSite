namespace BlApi;
/// <summary>
/// Exceptions' page.
/// </summary>

/// <summary>
/// Exception for error that came form the data.
/// </summary>
public class DataError : Exception
{
    public DataError(Exception inner) : base("", inner) { }

    public override string Message => InnerException.Message;
}
/// <summary>
/// Exception to invalid input that the customer entered.
/// </summary>
public class InvalidValue : Exception
{
    public string msg { get; set; }
    public InvalidValue(string m) { msg = m; }
    public override string Message => $"Invalid {msg} entered";
}
/// <summary>
/// Exception when we want to delete prodact that alredy exist in order.
/// </summary>
public class IdInOrder : Exception
{
    public IdInOrder(string m) : base(m) { }
}

/// <summary>
/// Exception when we alredy update.
/// </summary>
public class BlNoNeedToUpdateException : Exception
{
    public string msg { get; set; }
    public BlNoNeedToUpdateException(string m) { msg = m; }
    public override string Message => $"The {msg} has already been updated";
}

/// <summary>
/// Exception when the object not exist and we want to update or delete.
/// </summary>
public class NotExist : Exception
{
    public string msg { get; set; }
    public NotExist(string m) { msg = m; }
    public override string Message => $"This {msg} not exist.";
}


/// <summary>
/// Exception when the item amount out of stock.
/// </summary>
public class BlOutOfStockException : Exception
{
    public override string Message =>"Out Of Stock";
}

