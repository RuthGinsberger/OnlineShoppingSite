
using DalApi;
using Dal.DO;
using System.Runtime.CompilerServices;

namespace Dal.dalObject;
public class DalProduct : IProduct
{

    /// <summary>
    /// This function add a product.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    /// <exception cref="EntityDuplicateException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product p)
    {
        p.ID = DataSource.Config.ProductID;
        if (DataSource.Products.Count() <= DataSource.NumOfProducts)
        {
            DataSource.Products.Add(p);
            return p.ID;
        }
        else
            throw new EntityDuplicateException("That not enuagh room");
    }

    /// <summary>
    /// This function return a product by using lambada expression
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(Func<Product, bool> func)
    {
        Predicate<Product> myFunc = new(func);
        Product? P = DataSource.Products.Find(myFunc);
        if (P == null) throw new EntityNotFoundException("This  does not exist");
        return (Product)P;
    }

    /// <summary>
    /// This function return a products by using lambada expression
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product> GetAll(Func<Product, bool>? func = null)
    {
        List<Product> NewProducts = DataSource.Products;
        return func == null ? NewProducts : NewProducts.Where(func);
    }

    /// <summary>
    /// This function delete a product.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        Product? p = DataSource.Products.Find(O => O.ID == id);
        if (p == null)
            throw new EntityNotFoundException("This product does not exist");
        DataSource.Products.Remove((Product)p);
        return;
    }

    /// <summary>
    /// This function update a product.
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product p)
    {
        DataSource.Products[DataSource.Products.FindIndex(O => O.ID == p.ID)] = p;
        return;
        throw new EntityNotFoundException("This product does not exist");
    }
}