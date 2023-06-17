using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>

    public partial class CartWindow : Window
    {
        private IBl bl;
        private BO.Cart myCart;
        Window W;

        public PO.Cart PoCart { get; set; } = new();
        PO.OrderItem myOrderItem;

        //ObservableCollection<PO.Cart> List_c = new();
        public CartWindow(IBl BL, BO.Cart cart,Window productC)
        {
            InitializeComponent();
            bl = BL;
            myCart = cart;
            W = productC;
            DataContext = PoCart;
            ShowCart();
            ConfirmBtnEnabled();
        }
        private PO.Cart ShowCart()
        {
            if (PoCart != null)
            { PoCart.Items.Clear(); }
           
           

            myCart.Items.Select(tmp =>
            {
                myOrderItem = Common.convertItemsToPOOI(tmp);
                PoCart?.Items.Add(myOrderItem);
                PoCart.TotalPrice += (myOrderItem.TotalPrice);
                return tmp;
            }).ToList();


            return PoCart;
        }
        private void ConfirmBtnEnabled()
        {
            if (myCart?.Items?.Count > 0)
                ConfirmBtn.IsEnabled = true;
            else { ConfirmBtn.IsEnabled = false; }
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            try
            {
                myCart = Common.ConvertToBoCart(PoCart);
                PO.OrderItem changed = (PO.OrderItem)((Button)sender).DataContext;
                bl.Cart.UpdateAmountProduct(myCart, changed.ProductID, 0);
                PoCart.Items.Clear();
                PoCart = Common.ConvertToPoCart(myCart);
                DataContext = PoCart;
                ConfirmBtnEnabled();
                
                //CartWindow cartWindow = new CartWindow(bl, myCart);
                //cartWindow.Show();
                //this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Delete Product");
            }
        }

        private void Increase(object sender, RoutedEventArgs e)
        {
            try
            {
                myCart = Common.ConvertToBoCart(PoCart);
                PO.OrderItem changed = (PO.OrderItem)((Button)sender).DataContext;
                bl.Cart.UpdateAmountProduct(myCart, changed.ProductID, changed.Amount + 1);
                PoCart.Items.Clear();
                PoCart = Common.ConvertToPoCart(myCart);
                DataContext = PoCart;

            }
            catch (Exception)
            {
                MessageBox.Show("Can't increase Product's amount");
            }
        }

        private void Decrease(object sender, RoutedEventArgs e)
        {
            try
            {
                myCart = Common.ConvertToBoCart(PoCart);
                PO.OrderItem changed = (PO.OrderItem)((Button)sender).DataContext;
                bl.Cart.UpdateAmountProduct(myCart, changed.ProductID, changed.Amount - 1);
                PoCart.Items.Clear();
                PoCart = Common.ConvertToPoCart(myCart);
                DataContext = PoCart;
                ConfirmBtnEnabled();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Can't decrease Product's amount");
            }
        }

        //private void updateCart()
        //{
        //    myCart = Common.ConvertToBoCart(PoCart);
        //    PO.OrderItem changed = (PO.OrderItem)((Button)sender).DataContext;
        //    bl.Cart.UpdateAmountProduct(myCart, changed.ProductID, 0);
        //    PoCart.Items.Clear();
        //    PoCart = Common.ConvertToPoCart(myCart);
        //}
        private void Confirm(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.ConfirimCartToOrder(Common.ConvertToBoCart( PoCart));
                MessageBox.Show("Confirmed!");
                PoCart.Items.Clear();
                W.Show();   
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Can't confirm this cart");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            W.Show();
            this.Close();
        }
    }
}




