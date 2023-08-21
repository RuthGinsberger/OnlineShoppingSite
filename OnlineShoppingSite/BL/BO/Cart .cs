namespace BO;
public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public List<OrderItem?>? Items { get; set; } = new List<OrderItem>();
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        string toString =
                     $@"Cart: customer mame {CustomerName}, 
                     email {CustomerEmail}, address {CustomerAddress}. 
                     total price {TotalPrice} items: ";
                     foreach (var i in Items) { toString += "\n \t \t " + i; };
                     return toString;
    }
}