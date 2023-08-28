using BO;
using System.Windows;
namespace PL.PO
{
    public class ProductForList : DependencyObject
    {
        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
        public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(ProductForList), new UIPropertyMetadata(0));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ProductForList), new UIPropertyMetadata(""));

        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(double), typeof(ProductForList), new UIPropertyMetadata(0.00));

        public Enums.eCategory Category
        {
            get { return (Enums.eCategory)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Category", typeof(Enums.eCategory), typeof(ProductForList), new UIPropertyMetadata(Enums.eCategory.baby));

    }
}