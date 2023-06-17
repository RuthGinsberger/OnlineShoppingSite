using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class OrderForList : DependencyObject { 
        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
        public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(OrderForList), new UIPropertyMetadata(0));

        public string CustomerName
        {
            get { return (string)GetValue(CustomerNameProperty); }
            set { SetValue(CustomerNameProperty, value); }
        }
        public static readonly DependencyProperty CustomerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(OrderForList), new UIPropertyMetadata(""));

        public double TotalPrice
        {
            get { return (double)GetValue(TotalPriceProperty); }
            set { SetValue(TotalPriceProperty, value); }
        }
        public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(OrderForList), new UIPropertyMetadata(0.00));

        public Enums.eOrderStatus Status
        {
            get { return (Enums.eOrderStatus)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Status", typeof(Enums.eOrderStatus), typeof(OrderForList), new UIPropertyMetadata(Enums.eOrderStatus.Confirmed));


        public int AmountOfItems
        {
            get { return (int)GetValue(AmountOfItemsProperty); }
            set { SetValue(AmountOfItemsProperty, value); }
        }
        public static readonly DependencyProperty AmountOfItemsProperty = DependencyProperty.Register("AmountOfItems", typeof(int), typeof(OrderForList), new UIPropertyMetadata(0));

    }


}

