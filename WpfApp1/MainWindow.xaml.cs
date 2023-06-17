using BlApi;
using PL;
using System.Windows;
using System;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                bl = new BlImplementation.Bl();
            }
            catch (Exception e)
            {

            }
            // bl =   new BlImplementation.Bl();
            //bl.Cart.AddProduct(new BO.Cart(), 2);
        }
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow PlW = new(bl);
            PlW.Show();
        }
    }
}
