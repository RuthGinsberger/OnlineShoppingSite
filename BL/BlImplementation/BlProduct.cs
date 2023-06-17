using DalApi;
using BlApi;
using Dal;
using System.Xml.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal { get; set; } = DalApi.Factory.Get();

    /// <summary>
    /// This function return products list.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> GetProductsList(BO.Enums.eCategory category = default)
    {
        IEnumerable<Dal.DO.Product> ProductsList = new List<Dal.DO.Product>();
        try
        {
            lock (Dal) { ProductsList = (category == default ? Dal.Product.GetAll() : Dal.Product.GetAll(v => v.Category == (Dal.DO.eCategory)category)); }

            List<BO.ProductForList> products = new();
            ProductsList.ToList().ForEach(p => products.Add(new BO.ProductForList()
            {
                ID = p.ID,
                Name = p.Name,
                Category = (BO.Enums.eCategory)p.Category,
                Price = p.Price
            }));
            return products;
        }
        catch (DataError Dexc)
        {
            throw new DataError(Dexc);
        }
    }

    /// <summary>
    /// This function return  products list.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem> GetProducstItem(BO.Enums.eCategory category = default)
    {
        IEnumerable<Dal.DO.Product> ProductsList = new List<Dal.DO.Product>();
        List<BO.ProductItem> products = new();
        try
        {
            lock (Dal) { ProductsList = (category == default ? Dal.Product.GetAll() : Dal.Product.GetAll(v => v.Category == (Dal.DO.eCategory)category)); }
            ProductsList.ToList().ForEach(p => products.Add(new BO.ProductItem()
            {
                ID = p.ID,
                Name = p.Name,
                Category = (BO.Enums.eCategory)p.Category,
                Price = p.Price,
                Amount = p.InStock,
                InStock = p.InStock > 0 ? true : false
            }));

            return products;
        }
        catch (DataError Dexc)
        {
            throw new DataError(Dexc);
        }
    }
    /// <summary>
    /// This function return a product for manager.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidValue"></exception>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product GetProductManager(int Id)
    {
        if (Id < 0)
            throw new InvalidValue("idProduct");
        else
        {
            BO.Product product = new();
            try
            {
                Dal.DO.Product P;
                lock (Dal) { P = Dal.Product.Get(v => v.ID == Id); }
                product.ID = P.ID;
                product.Name = P.Name;
                product.Category = (BO.Enums.eCategory)P.Category;
                product.Price = P.Price;
                product.InStock = P.InStock;
                return product;
            }
            catch (DataError Dexc)
            {
                throw new DataError(Dexc);
            }
        }
    }

    /// <summary>
    /// This function return a product for customer.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidValue"></exception>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem GetProductCustomer(int Id, BO.Cart c)
    {
        if (Id < 0)
            throw new InvalidValue("id");
        else
        {
            try
            {
                Dal.DO.Product P;
                lock (Dal) { P = Dal.Product.Get(p => p.ID == Id); }
                //BO.ProductItem product = new();
                BO.ProductItem pi = (from oi in c.Items
                                     where oi.ProductID == Id
                                     select new BO.ProductItem()
                                     {
                                         ID = oi.ID,
                                         Name = oi.Name,
                                         Category = (BO.Enums.eCategory)P.Category,
                                         Price = oi.Price,
                                         Amount = oi.Amount,
                                         InStock = P.InStock >= oi.Amount ? true : false
                                     }).First() ?? throw new Exception();

                return pi;
            }
            catch (DataError Dexc)
            {
                throw new DataError(Dexc);
            }
        }
    }

    /// <summary>
    /// This function add a product.
    /// </summary>
    /// <param name="P"></param>
    /// <exception cref="InvalidValue"></exception>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddProduct(BO.Product P)
    {
        if (P.ID < 0)
            throw new InvalidValue("id");
        else if (P.Name == "")
            throw new InvalidValue("name");
        else if (P.Price < 0)
            throw new InvalidValue("price");
        else if (P.InStock < 0)
            throw new InvalidValue("inStock");
        try
        {
            Dal.DO.Product tmpProduct = new();
            tmpProduct.ID = P.ID;
            tmpProduct.Name = P.Name;
            tmpProduct.Price = P.Price;
            tmpProduct.Category = (Dal.DO.eCategory)P.Category;
            tmpProduct.InStock = P.InStock;
            lock (Dal) { Dal.Product.Add(tmpProduct); }
        }
        catch (DataError Dexc)
        {
            throw new DataError(Dexc);
        }
    }

    /// <summary>
    /// This function dalate a product acording to a product's id.
    /// </summary>
    /// <param name="ProductId"></param>
    /// <exception cref="DataError"></exception>
    /// <exception cref="IdInOrder"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteProduct(int ProductId)
    {
        IEnumerable<Dal.DO.OrderItem> OrderItemsList = new List<Dal.DO.OrderItem>();
        IEnumerable<Dal.DO.Order> OrdersList = new List<Dal.DO.Order>();
        Dal.DO.Product P = new();
        //int count;

        try
        {
            lock (Dal) { P = Dal.Product.Get(p => p.ID == ProductId); }


            lock (Dal) { OrderItemsList = Dal.OrderItem.GetAll(oi => oi.ProductID == ProductId); }
            if (OrderItemsList.Count() == 0)
            {
                lock (Dal) { Dal.Product.Delete(ProductId); }
                return;
            }
            lock (Dal)
            {
                OrdersList = (IEnumerable<Dal.DO.Order>)(from oi in OrderItemsList
                                                         select Dal.Order.GetAll(o => o.ID == oi.OrderID).Distinct());
            }
            int count = (from O in OrdersList
                         select O.ShipDate < DateTime.Now).Count();
        }
        catch (DataError Dexc)
        {
            throw new DataError(Dexc);
        }
        if (OrderItemsList.Count() > 0)
            throw new IdInOrder("product in order");
        lock (Dal) { Dal.Product.Delete(ProductId); }
    }

    /// <summary>
    /// This function update a product .
    /// </summary>
    /// <param name="P"></param>
    /// <exception cref="InvalidValue"></exception>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UpDateProduct(BO.Product P)
    {
        if (P.ID < 0)
            throw new InvalidValue("id");
        else if (P.Name == "")
            throw new InvalidValue("name");
        else if (P.Price < 0)
            throw new InvalidValue("price");
        else if (P.InStock < 0)
            throw new InvalidValue("inStock");
        try
        {
            Dal.DO.Product tmpProduct = new();
            tmpProduct.ID = P.ID;
            tmpProduct.Name = P.Name;
            tmpProduct.Price = P.Price;
            tmpProduct.Category = (Dal.DO.eCategory)P.Category;
            tmpProduct.InStock = P.InStock;
            lock (Dal) { Dal.Product.Update(tmpProduct); }
        }
        catch (DataError Dexc)
        {
            throw new DataError(Dexc);
        }
    }
}


