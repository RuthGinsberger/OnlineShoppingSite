using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl bl;
        BO.OrderForList tmpOrder = new();
        PO.OrderForList PtmpOrder = new();
        BO.Order order = new();
        BO.OrderItem OrderItem = new();
        bool ifInitilized = false;
        int Id;
        string myPermission;
        ObservableCollection<PO.OrderForList> ListO;
        Tuple<BO.OrderForList, bool, bool> dc;
        bool customerVisebility = false;
        Window OrderList;
        public OrderWindow(IBl bl_, int ID, ObservableCollection<PO.OrderForList> OL, string permission, Window? OrdL)
        {
            Id = ID;
            bl = bl_;
            myPermission = permission;
            OrderList = OrdL;
            ListO = OL;
            InitializeComponent();
            GetOrderForList(Id, permission);

        }
        public void GetOrderForList(int Id, string permission)
        {
            order = bl.Order.GetOrder(Id);
            tmpOrder.ID = Id;
            tmpOrder.CustomerName = order.CustomerName;
            tmpOrder.AmountOfItems = order.Items.Count;
            tmpOrder.TotalPrice = order.TotalPrice;
            tmpOrder.Status = order.Status;
            dc = new Tuple<BO.OrderForList, bool, bool>(tmpOrder, customerVisebility, !customerVisebility);
            DataContext = dc;
            if (permission == "customer")
            {
                customerVisebility = true;
            }
            else
            {
                customerVisebility = false;
                StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.eOrderStatus));
            }
        }
        private void StatusSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ifInitilized == true)
            {
                try
                {
                    IEnumerable<BO.OrderForList> bl_order;
                    order.Status = (BO.Enums.eOrderStatus)StatusSelector.SelectedItem;
                    if (order.Status == BO.Enums.eOrderStatus.Sent)
                    {
                        bl.Order.SendOrder(tmpOrder.ID);
                        ListO.Clear();
                        bl_order = bl.Order.GetOrdersList();
                        bl_order.Select(tmp =>
                        {
                            PO.OrderForList O = Common.ConvertToPoOFL(tmp);
                            ListO.Add(O);
                            return tmp;
                        }).ToList();
                    }
                    if (order.Status == BO.Enums.eOrderStatus.DeliveredToCustomer)
                    {
                        bl.Order.DaliverOrder(tmpOrder.ID);
                        ListO.Clear();
                        bl_order = bl.Order.GetOrdersList();
                        bl_order.Select(tmp =>
                        {
                            PO.OrderForList O = Common.ConvertToPoOFL(tmp);
                            ListO.Add(O);
                            return tmp;
                        }).ToList();
                    }
                    if (order.Status == BO.Enums.eOrderStatus.Confirmed)
                        throw new Exception();
                   
                    MessageBox.Show("Update succefuly!");
                }
                catch
                {
                    MessageBox.Show("Error to update", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OrderList.Show();
                this.Hide();
            }
            else
                ifInitilized = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (myPermission == "customer")
            {
                OrderTracking_Window OTW = new(bl, Id, this);
                OTW.Show();
            }
            else
            {
                OrderList.Show();
            }
            Close();
        }

        //private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    order = bl.Order.GetOrder(tmpOrder.ID);
        //    OrderItem = bl.Product.GetProductManager(Convert.ToInt32(TextBox_ID.Text));
        //    order.Items.Add(Convert.ToInt32(TextBox_ID.Text));

        //}
    }
}