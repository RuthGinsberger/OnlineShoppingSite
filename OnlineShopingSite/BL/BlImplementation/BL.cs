using BlApi;
namespace BlImplementation;
sealed public class Bl : IBl
{
    public IOrder Order => new BlOrder();
    public IProduct Product => new BlProduct();
    public ICart Cart => new BlCart();
 
}
