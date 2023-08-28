using DalApi;
namespace Dal;

sealed public class DalXml : IDal
{
    private DalXml() {}
    public static IDal Instance { get; } = new DalXml();
    public IProduct Product { get; } = new Product();
    public IOrder Order { get; } = new Order();
    public IOrderItem OrderItem { get; } = new OrderItem();

}
