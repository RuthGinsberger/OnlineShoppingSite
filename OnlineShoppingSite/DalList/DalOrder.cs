using DalApi;
using Dal.DO;
using System.Runtime.CompilerServices;

namespace Dal.dalObject;

/// <summary>
/// This function add a order and return the id's order. 
/// </summary>
public class DalOrder : IOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order newOrder)
    {
        newOrder.ID = DataSource.Config.OrderID;
        if (DataSource.Orders.Count() <= DataSource.NumOfOrders)
        {
            DataSource.Orders.Add(newOrder);
            return newOrder.ID;
        }
        else
            throw new EntityDuplicateException("That not enuagh room");
    }

    /// <summary>
    /// This function return a order and using lambade expression.
    /// </summary>
    /// <param name="func">lambada expression</param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(Func<Order, bool> func)
    {
        Predicate<Order> myFunc = new(func);
        Order? O = DataSource.Orders.Find(myFunc);
        if (O == null) throw new EntityNotFoundException("This order does not exist");
        return (Order)O;
    }

    /// <summary>
    /// This function update a order.
    /// </summary>
    /// <param name="order">order to update</param>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order order)
    {
        DataSource.Orders[DataSource.Orders.FindIndex(P => P.ID == P.ID)] = order;
        return;
        throw new EntityNotFoundException("This order does not exist");
    }

    /// <summary>
    /// This function delete a order.
    /// </summary>
    /// <param name="id">order id to delete</param>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        Order? O = DataSource.Orders.Find(O => O.ID == id);
        if (O == null)
            throw new EntityNotFoundException("This order does not exist");
        DataSource.Orders.Remove((Order)O);
        return;
    }

    /// <summary>
    /// This function get all order by using lambada expression.
    /// </summary>
    /// <param name="func">lambada expression to get by</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> GetAll(Func<Order, bool>? func = null)
    {
        List<Order> Order = DataSource.Orders;
        return func == null ? Order : Order.Where(func);
    }
}
