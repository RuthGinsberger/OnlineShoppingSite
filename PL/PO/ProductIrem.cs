using BO;
using System.Windows;
namespace PL.PO;
public class ProductItem : DependencyObject
{
    public int ID
    {
        get { return (int)GetValue(IDProperty); }
        set { SetValue(IDProperty, value); }
    }
    public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(ProductItem), new UIPropertyMetadata(0));
    public string Name
    {
        get { return (string)GetValue(NameProperty); }
        set { SetValue(NameProperty, value); }
    }
    public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ProductItem), new UIPropertyMetadata(""));

    public double Price
    {
        get { return (double)GetValue(PriceProperty); }
        set { SetValue(PriceProperty, value); }
    }
    public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(double), typeof(ProductItem), new UIPropertyMetadata(0.00));
    public Enums.eCategory Category
    {
        get { return (Enums.eCategory)GetValue(CategoryProperty); }
        set { SetValue(CategoryProperty, value); }
    }
    public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Category", typeof(Enums.eCategory), typeof(ProductItem), new UIPropertyMetadata(Enums.eCategory.baby));

    public int Amount
    {
        get { return (int)GetValue(AmountProperty); }
        set { SetValue(AmountProperty, value); }
    }
    public static readonly DependencyProperty AmountProperty = DependencyProperty.Register("Amount", typeof(int), typeof(ProductItem), new UIPropertyMetadata(0));
    public bool InStock
    {
        get { return (bool)GetValue(InStockProperty); }
        set { SetValue(InStockProperty, value); }
    }
    public static readonly DependencyProperty InStockProperty = DependencyProperty.Register("InStock", typeof(bool), typeof(ProductItem), new UIPropertyMetadata(false));
}