
namespace Dal.DO;

/// <summary>
/// This page contain the enums that we use in Dl.
/// </summary>
public enum eCategory
{
    baby=1, boys, girls, womans, mans
}
public enum eOption
{
    Exit, Product, Order, OrderItem
}
public enum eCrud
{
    Create=1, Read, ReadAll, Update, Delete
}
public enum eCrudOrderItem
{
    Create=1, Read, ReadByOrderProductIds, ReadByOrderid, ReadAll, Update, Delete
}

