
using DalApi;
using Dal.DO;
using System.Runtime.CompilerServices;

namespace Dal.dalObject;
public class DalOrderItem : IOrderItem
{

    /// <summary>
    /// This function add a order item.
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="EntityDuplicateException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.ID = DataSource.Config.OrderItemID;
        if (DataSource.OrderItems.Count() <= DataSource.NumOfOrderItems)
        {
            DataSource.OrderItems.Add(newOrderItem);
            return newOrderItem.ID;
        }
        else
            throw new EntityDuplicateException("That not enuagh room");
    }

    /// <summary>
    /// This function update a order item.
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem orderItem)
    {
        DataSource.OrderItems[DataSource.OrderItems.FindIndex(OI => OI.ID == orderItem.ID)] = orderItem;
        return;
        throw new EntityNotFoundException("This order does not exist");
    }
    /// <summary>
    /// This function delete a order item.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        OrderItem? OI = DataSource.OrderItems.Find(OI => OI.ID == id);
        if (OI == null)
            throw new EntityNotFoundException("This order does not exist");
        DataSource.OrderItems.Remove((OrderItem)OI);
        return;
    }
    
    /// <summary>
    /// This function return a order item.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem Get(Func<OrderItem, bool> func)
    {
        Predicate<OrderItem> myFunc = new(func);
        OrderItem? OI = DataSource.OrderItems.Find(myFunc);
        if (OI == null) throw new EntityNotFoundException("This order item does not exist");
        return (OrderItem)OI;
    }

    /// <summary>
    /// This function return order items by using lambada expression.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? func = null)
    {
        List<OrderItem> OrderItems = DataSource.OrderItems;
        return func == null ? OrderItems : OrderItems.Where(func);
    }
}

