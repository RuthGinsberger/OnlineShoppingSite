using BO;
namespace BlApi;
public interface ICart
    {
        public Cart AddProduct(Cart C, int Id);
        public Cart UpdateAmountProduct(Cart C, int Id, int amount);
        public void ConfirimCartToOrder(Cart C);

    }

