using BlApi;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {

        private IBl bl;
        PO.OrderForList i = new();

        ObservableCollection<PO.OrderForList> List_o = new();
        public OrderListWindow(IBl bl_)
        {
            InitializeComponent();
            bl = bl_;
            var ListOrder = bl.Order.GetOrdersList();

            ListOrder.Select(bP =>
            {
                i = Common.ConvertToPoOFL(bP);
                List_o.Add(i);
                return bP;
            }).ToList();

            OrdersListview.DataContext = List_o;

        }
        private void OrdersListview_SelectionChanged(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OrderWindow PW = new(bl, ((PO.OrderForList)OrdersListview.SelectedItem).ID, List_o, "manager", this);
            PW.Show();
            this.Hide();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            HomeAdmin HAW = new(bl);
            HAW.Show();
            Close();
        }
    }
        
}
