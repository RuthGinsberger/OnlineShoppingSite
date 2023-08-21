using BlApi;
using DalApi;
namespace BlImplementation;
using Dal;
using System;
using static BO.Enums;
using System.Runtime.CompilerServices;
internal class BlOrder : BlApi.IOrder
{
    private IDal Dal { get; set; } =// DalXml.Instance;
        DalApi.Factory.Get();
    /// <summary>
    /// This function return the status of the order.
    /// </summary>
    /// <param name="DeliveryDate"></param>
    /// <param name="ShipDate"></param>
    /// <param name="OrderDate"></param>
    /// <returns></returns>
    /// 

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Enums.eOrderStatus Status(DateTime? DeliveryDate, DateTime? ShipDate)
    {
        DateTime TimeNow = DateTime.Now;
        if (DeliveryDate <= TimeNow && DeliveryDate != DateTime.MinValue)
            return BO.Enums.eOrderStatus.DeliveredToCustomer;
        else if (ShipDate <= TimeNow && ShipDate != DateTime.MinValue)
            return BO.Enums.eOrderStatus.Sent;
        else return BO.Enums.eOrderStatus.Confirmed;
    }

    /// <summary>
    /// This function return the order's amout of items.
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    private int AmountOfItems_(int OrderId)
    {
        int sum;
        IEnumerable<Dal.DO.OrderItem> OrderItemsList = new List<Dal.DO.OrderItem>();
        lock (Dal) { OrderItemsList = Dal.OrderItem.GetAll(oi => oi.OrderID == OrderId); }
        sum = OrderItemsList.Count();
        return sum;
    }

    /// <summary>
    /// This function return order items list.
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    /// <exception cref="DataError"></exception>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]
    public List<BO.OrderItem> Items(int OrderId)
    {
        try
        {
            IEnumerable<Dal.DO.OrderItem> OrderItemsList = new List<Dal.DO.OrderItem>();
            List<BO.OrderItem> OrderItems = new();
            lock (Dal) { OrderItemsList = Dal.OrderItem.GetAll(oi => oi.OrderID == OrderId); }
            OrderItemsList.ToList().ForEach(OI =>
            OrderItems.Add(new BO.OrderItem
            {
                ID = OI.ID,
                Amount = OI.Amount,
                Price = OI.Price,
                TotalPrice = OI.Price * OI.Amount,
                ProductID = OI.ProductID

            }));
            return OrderItems;
        }
        catch (EntityDuplicateException Dexc)
        {
            throw new DataError(Dexc);
        }
    }

    /// <summary>
    /// The function return a OrderTracking object and also update a list with all the order's status.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="DataError"></exception>

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.OrderTracking TrackOrder(int orderId)
    {
        DateTime min = DateTime.MinValue;
        try
        {
            Dal.DO.Order order;
            lock (Dal) { order = Dal.Order.Get(o => o.ID == orderId); }
            if (order.ID == default)
            {
                throw new EntityNotFoundException("This order does not exist");
            }
            BO.OrderTracking ot = new()
            {
                ID = order.ID,
                Status = Status(order.DeliveryDate, order.ShipDate),
                TrackList = new()
            };
            if (order.OrderDate != min)
            {
                ot.TrackList.Add(new Tuple<DateTime?, eOrderStatus?>(order.OrderDate, BO.Enums.eOrderStatus.Confirmed));
                if (order.ShipDate != min)
                {
                    ot.TrackList.Add(new Tuple<DateTime?, eOrderStatus?>(order.ShipDate, BO.Enums.eOrderStatus.Sent));
                    if (order.DeliveryDate != min)
                        ot.TrackList.Add(new Tuple<DateTime?, eOrderStatus?>(order.DeliveryDate, BO.Enums.eOrderStatus.DeliveredToCustomer));
                }
            }
            return ot;
        }
        catch (EntityNotFoundException Dexc)
        {
            throw new DataError(Dexc);
        }
    }

    /// <summary>
    /// This function return the total price of an order.
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    private double TotalPrice_(int OrderId)
    {
        IEnumerable<Dal.DO.OrderItem> OrderItemsList = new List<Dal.DO.OrderItem>();
        lock (Dal) { OrderItemsList = Dal.OrderItem.GetAll(oi => oi.OrderID == OrderId); }
        var TotalPrice = (from oi in OrderItemsList
                          select oi.Price * oi.Amount).Sum();
        return TotalPrice;
    }

    /// <summary>
    /// This function return a products list.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList> GetOrdersList()
    {
        IEnumerable<Dal.DO.Order> OrdersList = new List<Dal.DO.Order>();
        List<BO.OrderForList> Orders = new();

        try
        {
            lock (Dal) { OrdersList = Dal.Order.GetAll(); }
            OrdersList.ToList().ForEach(O => Orders.Add(new BO.OrderForList()
            {
                ID = O.ID,
                CustomerName = O.CustomerName,
                Status = Status(O.DeliveryDate, O.ShipDate),
                AmountOfItems = AmountOfItems_(O.ID),
                TotalPrice = TotalPrice_(O.ID),
            }));

            return Orders;
        }
        catch (EntityNotFoundException Dexc)
        {
            throw new DataError(Dexc);
        }
    }

    /// <summary>
    /// This function return an order acording to id.
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidValue"></exception>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order GetOrder(int OrderId)
    {
        if (OrderId < 0)
            throw new InvalidValue("idOrder");
        else
        {
            Dal.DO.Order O = new();
            BO.Order Order = new();
            try
            {
                O = Dal.Order.Get(v => v.ID == OrderId);
                Order.ID = O.ID;
                Order.CustomerName = O.CustomerName;
                Order.CustomerName = O.CustomerName;
                Order.CustomerEmail = O.CustomerAdress;
                Order.CustomerAddress = O.CustomerAdress;
                Order.OrderDate = O.OrderDate;
                Order.ShipDate = O.ShipDate;
                Order.DeliveryDate = O.DeliveryDate;
                Order.Status = Status(O.DeliveryDate, O.ShipDate);
                Order.Items = Items(O.ID);
                Order.TotalPrice = TotalPrice_(O.ID);

                return Order;
            }
            catch (DataError Dexc)
            {
                throw new DataError(Dexc);
            }
        }
    }

    /// <summary>
    /// This function return the BOorder and update the ShipDate's order to now.
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidValue"></exception>
    /// <exception cref="BlNoNeedToUpdateException"></exception>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order SendOrder(int OrderId)
    {
        Dal.DO.Order DoOrder = new();
        BO.Order BoOrder = new();
        BO.Enums.eOrderStatus OrderStatus;
        if (OrderId < 0)
            throw new InvalidValue("idOrder");
        else
        {
            try
            {
                lock (Dal) { DoOrder = Dal.Order.Get(v => v.ID == OrderId); }
                OrderStatus = Status(DoOrder.DeliveryDate, DoOrder.ShipDate);
                if (OrderStatus == BO.Enums.eOrderStatus.Confirmed)
                {
                    DoOrder.ShipDate = DateTime.Now;
                    lock (Dal) { Dal.Order.Update(DoOrder); }
                    BoOrder.ID = DoOrder.ID;
                    BoOrder.CustomerName = DoOrder.CustomerName;
                    BoOrder.CustomerName = DoOrder.CustomerName;
                    BoOrder.CustomerEmail = DoOrder.CustomerAdress;
                    BoOrder.CustomerAddress = DoOrder.CustomerAdress;
                    BoOrder.OrderDate = DoOrder.OrderDate;
                    BoOrder.ShipDate = DateTime.Now;
                    BoOrder.DeliveryDate = DoOrder.DeliveryDate;
                    BoOrder.Status = BO.Enums.eOrderStatus.Sent;
                    BoOrder.Items = Items(DoOrder.ID);
                    BoOrder.TotalPrice = TotalPrice_(DoOrder.ID);
                    return BoOrder;
                }
                else
                {
                    throw new BlNoNeedToUpdateException("order");
                }
            }
            catch (DataError Dexc)
            {
                throw new DataError(Dexc);
            }
        }
    }

    /// <summary>
    /// This function return the BOorder and update the DeliveryDate's order to now.
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidValue"></exception>
    /// <exception cref="BlNoNeedToUpdateException"></exception>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order DaliverOrder(int OrderId)
    {
        Dal.DO.Order DoOrder = new();
        BO.Order BoOrder = new();
        BO.Enums.eOrderStatus OrderStatus;
        if (OrderId < 0)
            throw new InvalidValue("idOrder");
        else
        {
            try
            {
                lock (Dal) { DoOrder = Dal.Order.Get(v => v.ID == OrderId); }
                OrderStatus = Status(DoOrder.DeliveryDate, DoOrder.ShipDate);
                if (OrderStatus == BO.Enums.eOrderStatus.Sent)
                {
                    DoOrder.DeliveryDate = DateTime.Now;
                    lock (Dal) { Dal.Order.Update(DoOrder); }
                    BoOrder.ID = DoOrder.ID;
                    BoOrder.CustomerName = DoOrder.CustomerName;
                    BoOrder.CustomerName = DoOrder.CustomerName;
                    BoOrder.CustomerEmail = DoOrder.CustomerAdress;
                    BoOrder.CustomerAddress = DoOrder.CustomerAdress;
                    BoOrder.OrderDate = DoOrder.OrderDate;
                    BoOrder.ShipDate = DoOrder.ShipDate;
                    BoOrder.DeliveryDate = DateTime.Now;
                    BoOrder.Status = BO.Enums.eOrderStatus.DeliveredToCustomer;
                    BoOrder.Items = Items(DoOrder.ID);
                    BoOrder.TotalPrice = TotalPrice_(DoOrder.ID);
                    return BoOrder;
                }
                else
                {
                    throw new BlNoNeedToUpdateException("order");
                }
            }
            catch (DataError Dexc)
            {
                throw new DataError(Dexc);
            }
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? ChooseOrder()
    {
        List<Dal.DO.Order> ConfirmOrders;
        List<Dal.DO.Order> SentOrders;
        lock (Dal) { ConfirmOrders = Dal.Order.GetAll(O => Status(O.DeliveryDate, O.ShipDate) == eOrderStatus.Confirmed).ToList(); }
        lock (Dal) { SentOrders = Dal.Order.GetAll(O => Status(O.DeliveryDate, O.ShipDate) == eOrderStatus.Sent).ToList(); }
        DateTime? minConfirm = ConfirmOrders?.Where(o=> o.OrderDate!=DateTime.MinValue).Min(o => o.OrderDate);
        DateTime? minSent = SentOrders?.Where(o=> o.ShipDate!=DateTime.MinValue).Min(o => o.ShipDate);
        Dal.DO.Order o1 = ConfirmOrders.Where(o => o.OrderDate == minConfirm).FirstOrDefault();
        Dal.DO.Order o2 = SentOrders.Where(o => o.ShipDate == minSent).FirstOrDefault();
        int chosenOrderID = o1.OrderDate < o2.ShipDate ? o1.ID : o2.ID;
        return chosenOrderID;
    }
}

