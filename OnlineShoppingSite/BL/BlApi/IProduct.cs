namespace BlApi;
public interface IProduct
{
   public  IEnumerable<BO.ProductForList> GetProductsList(BO.Enums.eCategory category=default);
   public IEnumerable<BO.ProductItem> GetProducstItem(BO.Enums.eCategory category = default);
   public BO.Product GetProductManager(int Id);
   public BO.ProductItem GetProductCustomer(int Id, BO.Cart c);
   public void AddProduct(BO.Product P);
   public void DeleteProduct(int ProductId);
   public void UpDateProduct(BO.Product P);
}

