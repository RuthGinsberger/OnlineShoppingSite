using BlApi;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl;
        public ProductListWindow(IBl bl_)
        {
            InitializeComponent();
            bl = bl_;
            ProductsListview.ItemsSource = bl.Product.GetProductsList();

        }
    }
}
