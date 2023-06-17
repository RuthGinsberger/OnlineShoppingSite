//using BO;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Windows;

//namespace PL.PO
//{
//    public class Cart : DependencyObject

//    {
//        public string CustomerName
//        {
//            get { return (string)GetValue(CustomerNameProperty); }
//            set { SetValue(CustomerNameProperty, value); }
//        }
//        public static readonly DependencyProperty CustomerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(Cart), new UIPropertyMetadata(""));

//        public string CustomerEmail
//        {
//            get { return (string)GetValue(CustomerEmailProperty); }
//            set { SetValue(CustomerEmailProperty, value); }
//        }
//        public static readonly DependencyProperty CustomerEmailProperty = DependencyProperty.Register("CustomerEmail", typeof(string), typeof(Cart), new UIPropertyMetadata("nahghg"));

//        public string CustomerAddress
//        {
//            get { return (string)GetValue(CustomerAddressProperty); }
//            set { SetValue(CustomerAddressProperty, value); }
//        }
//        public static readonly DependencyProperty CustomerAddressProperty = DependencyProperty.Register("CustomerEmail", typeof(string), typeof(Cart), new UIPropertyMetadata(""));

//        public double TotalPrice
//        {
//            get { return (double)GetValue(TotalPriceProperty); }
//            set { SetValue(TotalPriceProperty, value); }
//        }
//        public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(Cart), new UIPropertyMetadata(0.00));

//        public ObservableCollection<PO.OrderItem?> Items
//        {
//            get { return (ObservableCollection<PO.OrderItem?>)GetValue(ItemsProperty); }
//            set { SetValue(ItemsProperty, value); }
//        }
//        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<PO.OrderItem?>), typeof(Cart), new UIPropertyMetadata(new ObservableCollection<PO.OrderItem?>()));

//    }
//}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO;
/// <summary>
/// A PO entity of shopping cart
/// for the shopping cart management screen and order confirmation
/// </summary>
public class Cart : DependencyObject
{
    public static readonly DependencyProperty customerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(Cart), new UIPropertyMetadata(""));
    public static readonly DependencyProperty customerEmailProperty = DependencyProperty.Register("CustomerEmail", typeof(string), typeof(Cart), new UIPropertyMetadata(""));
    public static readonly DependencyProperty customerAddressProperty = DependencyProperty.Register("CustomerAddress", typeof(string), typeof(Cart), new UIPropertyMetadata(""));
    public static readonly DependencyProperty totalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(Cart), new UIPropertyMetadata(0.0));
    public static readonly DependencyProperty itemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<PO.OrderItem?>), typeof(Cart), new UIPropertyMetadata(new ObservableCollection<PO.OrderItem?>()));
    public string? CustomerName
    {
        get { return (string)GetValue(customerNameProperty); }
        set { SetValue(customerNameProperty, value); }
    }
    public string? CustomerEmail
    {
        get { return (string)GetValue(customerEmailProperty); }
        set { SetValue(customerEmailProperty, value); }
    }
    public string? CustomerAddress
    {
        get { return (string)GetValue(customerAddressProperty); }
        set { SetValue(customerAddressProperty, value); }
    }
    public ObservableCollection<PO.OrderItem?> Items//a list of the items in the shopping cart 
    {
        get { return (ObservableCollection<PO.OrderItem?>)GetValue(itemsProperty); }
        set { SetValue(itemsProperty, value); }
    }
    public double TotalPrice//the total price of the shopping cart
    {
        get { return (double)GetValue(totalPriceProperty); }
        set { SetValue(totalPriceProperty, value); }
    }

    public override string ToString() => $@"
        customerName: {CustomerName},
        customerEmail: {CustomerEmail},
        customerAddress: {CustomerAddress},
        items: {Items},
        totalPrice: {TotalPrice}
        ";
}
