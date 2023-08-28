using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// Firstly we get the product list in order to initialize the selector.
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl;
        PO.ProductForList i = new();
        BO.Cart myCart = new();
        ObservableCollection<PO.ProductForList> List_p = new();
        public ProductListWindow(IBl bl_, BO.Cart myCart_ = null)
        {
            InitializeComponent();
            bl = bl_;
            myCart = myCart_;
            AttributeSelector.ItemsSource = Enum.GetNames<BO.Enums.eCategory>();
            ProductsListview.DataContext = List_p;
            ShowProducts();
        }
        private ObservableCollection<PO.ProductForList> ShowProducts(BO.Enums.eCategory category = default)
        {
            List_p.Clear();
            IEnumerable<BO.ProductForList> ListProduct = bl.Product.GetProductsList(category);
            
            ListProduct.Select(tmp =>
            {
                i = Common.ConvertToPoPFL(tmp);
                List_p.Add(i);
                return tmp;
            }).ToList();
            return List_p;
        }
        /// <summary>
        /// This function shows the product list sorted by the customer chosen in the selector.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttributeSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowProducts((BO.Enums.eCategory)Enum.Parse(typeof(BO.Enums.eCategory), AttributeSelector.SelectedItem.ToString()));
        }
        /// <summary>
        /// This function passes to ProductWindow page with id=-1 (this id is not exist) 
        /// so that the function in ProductWindow page will know that we want to add and not to update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow PW = new(bl, -1, "manager", myCart, List_p, this);
            PW.Show();
            this.Hide();
        }
        /// <summary>
        /// This function passes to ProductWindow page with the product's id when the customer click on a product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductSelected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ProductWindow PW = new(bl, ((PL.PO.ProductForList)ProductsListview.SelectedItem).ID, "manager", myCart, List_p, this);
            PW.Show();
            this.Hide();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            HomeAdmin HAW = new(bl);
            HAW.Show();
            Close();
        }

        private void X_Click(object sender, RoutedEventArgs e)
        {
            ShowProducts();
        }
    }
}
