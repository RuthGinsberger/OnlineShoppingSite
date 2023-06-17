using BlApi;
using System;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl;
        BO.Cart myCart = new();
        public MainWindow()
        {
            InitializeComponent();
            bl = new BlImplementation.Bl();
        }
        private void AdminBtn_Click(object sender, RoutedEventArgs e)
        {
            HomeAdmin MW = new(bl);
            MW.Show();
            this.Hide();
        }
        private void NewOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductCatlog_Window PCW = new(bl, myCart);
            PCW.Show();
            this.Hide();
        }
        private void TrackBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderTracking_Window OTW = new(bl, Convert.ToInt32(OrderID.Text), this);
                OTW.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("This order does not exist");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            simulator SW = new();
            SW.Show();
            this.Hide();
        }
    }
}
