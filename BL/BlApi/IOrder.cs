using BO;
using System.Collections;

namespace BlApi;
public interface IOrder
{
    public Order SendOrder(int OrderId);
    public Order GetOrder(int OrderId);
    public IEnumerable<OrderForList> GetOrdersList();
    public List<OrderItem> Items(int OrderId);
    public Order DaliverOrder(int OrderId);
    public OrderTracking TrackOrder(int orderId);
    int? ChooseOrder();
}

