using BlApi;
using System.Windows;

namespace PL
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
            bl = new BlImplementation.Bl();
        }
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow PlW = new(bl);
            PlW.Show();
        }
    }
}
