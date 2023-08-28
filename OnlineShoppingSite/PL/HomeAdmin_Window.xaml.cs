using BlApi;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        private IBl bl;
        public HomeAdmin(IBl bl_)
        {
            InitializeComponent();
            bl = bl_;
        }
        /// <summary>
        /// This function passes to ProductListWindow page on click the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow PlW = new(bl);
            PlW.Show();
            this.Hide();
        }
        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            OrderListWindow OlW = new(bl);
            OlW.Show();
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


