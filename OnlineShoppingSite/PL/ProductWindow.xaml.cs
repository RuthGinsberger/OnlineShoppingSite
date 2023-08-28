using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// Firstly we checked if its update or add and initialize the input fild and the buttons.
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl;
        BO.Product tmpProduct = new();
        PO.ProductItem pi = new();
        BO.Cart myCart = new();
        ObservableCollection<PO.ProductForList> list;
        ObservableCollection<PO.ProductItem> list_p;
        Window w;
        int id;
        string myPermissen;

        public ProductWindow(IBl bl_, int ID, string Permission, BO.Cart myCart_, ObservableCollection<PO.ProductForList> List_, Window PLW)
        {
            id = ID;
            bl = bl_;
            myCart = myCart_;
            myPermissen = Permission;
            list = List_;
            w = PLW;
            InitializeComponent();
            try
            {

                nameLBL.Visibility = Visibility.Hidden;
                priceLBL.Visibility = Visibility.Hidden;
                amountLBL.Visibility = Visibility.Hidden;
                CategoryLbl.Visibility = Visibility.Hidden;
                cartBtn.Visibility = Visibility.Hidden;

                categorySelector.ItemsSource = Enum.GetNames<BO.Enums.eCategory>();
                categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.eCategory));
                if (ID != -1)
                {
                    ShowProductDatails(ID);
                    updateOrAddProductBtn.Content = "Update!";
                }
                else
                {
                    deleteProductBtn.Visibility = Visibility.Hidden;
                    DataContext = tmpProduct;
                    updateOrAddProductBtn.Content = "Add!";
                }



            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        private void ShowProductDatails(int ID)
        {
            tmpProduct = bl.Product.GetProductManager(ID);
            pi.ID = tmpProduct.ID;
            pi.Name = tmpProduct.Name;
            pi.Price = tmpProduct.Price;
            pi.Category = (BO.Enums.eCategory)tmpProduct.Category;
            pi.Amount = tmpProduct.InStock;
            DataContext = pi;
        }
        public ProductWindow(IBl bl_, int ID, string Permission, BO.Cart myCart_, ObservableCollection<PO.ProductItem> List, Window PLW)
        {
            id = ID;
            bl = bl_;
            myCart = myCart_;
            myPermissen = Permission;
            list_p = List;
            w = PLW;
            InitializeComponent();

            try
            {
                tmpProduct = bl.Product.GetProductManager(id);
                ShowProductDatails(ID);
                deleteProductBtn.Visibility = Visibility.Hidden;
                updateOrAddProductBtn.Visibility = Visibility.Hidden;
                categorySelector.Visibility = Visibility.Hidden;
                amount.Visibility = Visibility.Hidden;
                price.Visibility = Visibility.Hidden;
                name.Visibility = Visibility.Hidden;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        private BO.Product casting()
        {
            tmpProduct.ID = pi.ID;
            tmpProduct.Name = pi.Name;
            tmpProduct.Price = pi.Price;
            tmpProduct.InStock = pi.Amount;
            tmpProduct.Category = pi.Category;
            return tmpProduct;
        }
        /// <summary>
        /// This function deleted the product that the customer entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void deleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.DeleteProduct(id);
                MessageBox.Show("Delete succefuly!");
                w.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("can't delete product in order");
            }
        }
        /// <summary>
        /// This function update or add acording to the customer's choice a product that the customer entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void updateOrAddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (updateOrAddProductBtn.Content == "Update!")
                {
                    bl.Product.UpDateProduct(casting());
                    list.Clear();
                    IEnumerable<BO.ProductForList> bl_product = bl.Product.GetProductsList();
                    foreach (BO.ProductForList tmp in bl_product)
                    {
                        PO.ProductForList p = Common.ConvertToPoPFL(tmp);
                        list.Add(p);
                    }

                    MessageBox.Show("Update succefuly!");

                    w.Show();
                    this.Hide();
                }
                else
                {
                    bl.Product.AddProduct(tmpProduct);
                    list.Clear();
                    IEnumerable<BO.ProductForList> bl_product = bl.Product.GetProductsList();
                    foreach (BO.ProductForList tmp in bl_product)
                    {
                        PO.ProductForList p = Common.ConvertToPoPFL(tmp);
                        list.Add(p);
                    }

                    MessageBox.Show("Add succefuly!");
                    w.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("sorry cant do this action");
            }
        }
        private void cartBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myCart.Items.Count == 0)
                {
                    bl.Cart.AddProduct(myCart, id);
                    CartWindow cartWindow = new CartWindow(bl, myCart, this);
                }
                else
                {

                    bl.Cart.AddProduct(myCart, id);
                }
                MessageBox.Show("Add succefuly!");
                w.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("out of stock!");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (myPermissen == "customer")
            {
                ProductCatlog_Window PCW = new(bl, myCart);
                PCW.Show();
            }
            else
            {
                w.Show();
            }

            Close();
        }

    }
}
