using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductCatlog_Window.xaml
    /// </summary>
    public partial class ProductCatlog_Window : Window
    {
        private IBl bl;
        PO.ProductItem i = new();
        ObservableCollection<PO.ProductItem> List_p = new();
        ObservableCollection<BO.OrderItem> List_oi = new();
        BO.Cart myCart = new();
        public ProductCatlog_Window(IBl bl_, BO.Cart myCart_)
        {
            InitializeComponent();
            bl = bl_;
            myCart = myCart_;
            ProductsListview.DataContext = List_p;
            AttributeSelector.ItemsSource = Enum.GetNames<BO.Enums.eCategory>();
            ShowProducts();
        }
        private ObservableCollection<PO.ProductItem> ShowProducts(BO.Enums.eCategory category = default)
        {
            List_p.Clear();
            IEnumerable<BO.ProductItem> ListProduct = bl.Product.GetProducstItem(category);
           
            ListProduct.Select(tmp =>
            {
                i = Common.ConvertToPoPI(tmp);
                List_p.Add(i);
                return tmp;
            }).ToList();
            return List_p;
        }
        private void ProductClick(object sender, MouseButtonEventArgs e)
        {
            ProductWindow PW = new(bl, ((PL.PO.ProductItem)ProductsListview.SelectedItem).ID, "customer", myCart, List_p, this);
            PW.Show();
            this.Hide();
        }
        private void AttributeSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowProducts((BO.Enums.eCategory)Enum.Parse(typeof(BO.Enums.eCategory), AttributeSelector.SelectedItem.ToString()));
        }
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow OW = new(bl, myCart, this);
            OW.Show();
            this.Hide();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new();
            MW.Show();
            Close();
        }
    }
}
