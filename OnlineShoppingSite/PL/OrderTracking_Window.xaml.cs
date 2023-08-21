using BlApi;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTracking_Window.xaml
    /// </summary>
    public partial class OrderTracking_Window : Window
    {
        private IBl bl;
        BO.OrderTracking ot = new();
        int id;
        Window Ord;
        Window main;
        public OrderTracking_Window(IBl bl_, int ID, Window ord)
        {
            InitializeComponent();
            bl = bl_;
            id = ID;
            Ord = ord;
            ot = bl.Order.TrackOrder(ID);
            DataContext = ot.TrackList;
        }
        private void OrderDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            Ord.Show();
            this.Hide();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            main.Show();
            Close();
        }
    }
}
