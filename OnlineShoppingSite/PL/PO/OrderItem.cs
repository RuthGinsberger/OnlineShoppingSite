using System.Windows;
namespace PL.PO;

public class OrderItem : DependencyObject
{
    public int ID
    {
        get { return (int)GetValue(IDProperty); }
        set { SetValue(IDProperty, value); }
    }
    public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(OrderItem), new UIPropertyMetadata(0));

    public string Name
    {
        get { return (string)GetValue(NameProperty); }
        set { SetValue(NameProperty, value); }
    }
    public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(OrderItem), new UIPropertyMetadata(""));

    public double Price
    {
        get { return (double)GetValue(PriceProperty); }
        set { SetValue(PriceProperty, value); }
    }
    public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(double), typeof(OrderItem), new UIPropertyMetadata(0.00));

    public int ProductID
    {
        get { return (int)GetValue(ProductIDProperty); }
        set { SetValue(ProductIDProperty, value); }
    }
    public static readonly DependencyProperty ProductIDProperty = DependencyProperty.Register("ProductID", typeof(int), typeof(OrderItem), new UIPropertyMetadata(0));

    public int Amount
    {
        get { return (int)GetValue(AmountProperty); }
        set { SetValue(AmountProperty, value); }
    }

    public static readonly DependencyProperty AmountProperty = DependencyProperty.Register("Amount", typeof(int), typeof(OrderItem), new UIPropertyMetadata(0));

    public double TotalPrice
    {
        get { return (double)GetValue(TotalPriceProperty); }
        set { SetValue(TotalPriceProperty, value); }
    }

    public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(OrderItem), new UIPropertyMetadata(0.0));
}
