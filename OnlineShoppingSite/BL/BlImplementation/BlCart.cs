using BlApi;
namespace BlImplementation;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Dal;
internal class BlCart : ICart
{
    private DalApi.IDal Dal { get; set; } = DalApi.Factory.Get();

    /// <summary>
    /// This function add a product th the OrderItem list.
    /// </summary>
    /// <param name="C"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart AddProduct(BO.Cart C, int Id)
    {
        try
        {
            Dal.DO.Product ProductToAdd;
            lock (Dal)
                ProductToAdd = Dal.Product.Get(v => v.ID == Id);
            BO.OrderItem? oi = new();
            oi = null;
            if (C.Items?.Count() != 0)
                oi = C.Items?.Find(item => item.ProductID == Id);
            if (ProductToAdd.InStock > 0)
            {
                if (oi == null)
                {
                    BO.OrderItem tmpOrderItem = new();
                    tmpOrderItem.ID = 0;
                    tmpOrderItem.Name = ProductToAdd.Name;
                    tmpOrderItem.ProductID = ProductToAdd.ID;
                    tmpOrderItem.Price = ProductToAdd.Price;
                    tmpOrderItem.Amount += 1;
                    tmpOrderItem.TotalPrice += ProductToAdd.Price;
                    C.TotalPrice += ProductToAdd.Price;
                    C.Items?.Add(tmpOrderItem);
                }
                else
                {
                    oi.Amount += 1;
                    oi.TotalPrice += ProductToAdd.Price;
                    C.TotalPrice += ProductToAdd.Price;
                }
                ProductToAdd.InStock--;
            }
            else
            {
                throw new BlOutOfStockException();
            }

        }
        catch (DataError Dexc)
        {
            throw new DataError(Dexc);
        }
        catch (BlOutOfStockException)
        {
            throw new BlOutOfStockException();
        }
        return C;
    }

    /// <summary>
    /// This function update the amount of a product.
    /// </summary>
    /// <param name="C"></param>
    /// <param name="Id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart UpdateAmountProduct(BO.Cart C, int Id, int amount)
    {
        BO.OrderItem TmpOrderItem = new();
        TmpOrderItem = C.Items.Find(item => item.ProductID == Id);
        Dal.DO.Product tmpProduct;
        lock (Dal) { tmpProduct = Dal.Product.Get(v => v.ID == Id); }
        try
        {
            if (amount > TmpOrderItem.Amount)
            {

                TmpOrderItem.TotalPrice += tmpProduct.Price * (amount - TmpOrderItem.Amount);
                C.TotalPrice += tmpProduct.Price * (amount - TmpOrderItem.Amount);
                TmpOrderItem.Amount = amount;
            }
            else if (amount == 0)
            {
                C.TotalPrice -= TmpOrderItem.TotalPrice;
                C.Items.Remove(TmpOrderItem);
            }
            else if (amount < TmpOrderItem.Amount)
            {

                TmpOrderItem.TotalPrice -= tmpProduct.Price * (TmpOrderItem.Amount - amount);
                C.TotalPrice -= tmpProduct.Price * (TmpOrderItem.Amount - amount);
                TmpOrderItem.Amount = amount;
            }
        }
        catch (DataError Dexc)
        {
            throw new DataError(Dexc);
        }
        return C;
    }

    /// <summary>
    /// This function confirm the a cart's order.
    /// </summary>
    /// <param name="C"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="adress"></param>
    /// <exception cref="InvalidValue"></exception>
    /// <exception cref="NotExist"></exception>
    /// <exception cref="DataError"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void ConfirimCartToOrder(BO.Cart C)
    {
        try
        {
            Dal.DO.Order tmpOrder;
            int orderId;
            lock (Dal) { tmpOrder = new(); }
            if (C.CustomerName == null)
                throw new InvalidValue("name");
            if (C.CustomerAddress == null)
                throw new InvalidValue("adress");
            if (!IsValidEmail(C.CustomerEmail))
                throw new InvalidValue("email");
            tmpOrder.ID = 0;
            tmpOrder.CustomerName = C.CustomerName;
            tmpOrder.CustomerEmail = C.CustomerEmail;
            tmpOrder.OrderDate = DateTime.Now;
            tmpOrder.ShipDate = DateTime.MinValue;
            tmpOrder.DeliveryDate = DateTime.MinValue;
            lock (Dal) { orderId = Dal.Order.Add(tmpOrder); }

            C.Items.ForEach(oi =>
            {
                Dal.DO.Product tmpProduct;
                lock (Dal) { tmpProduct = Dal.Product.Get(v => v.ID == oi.ProductID); }
                if (tmpProduct.ID == null)
                    throw new NotExist("product");
                if (oi.Amount > tmpProduct.InStock)
                    throw new InvalidValue($"amount in {tmpProduct.ID} product");
                Dal.DO.OrderItem tmpOrderItem;
                lock (Dal) { tmpOrderItem = new(); }
                tmpOrderItem.ID = 0;
                tmpOrderItem.ProductID = oi.ProductID;
                tmpOrderItem.Price = oi.Price;
                tmpOrderItem.Amount = oi.Amount;
                tmpOrderItem.OrderID = orderId;
                lock (Dal) { Dal.OrderItem.Add(tmpOrderItem); }
                tmpProduct.InStock -= tmpOrderItem.Amount;
                lock (Dal) { Dal.Product.Update(tmpProduct); }
            });
        }
        catch (DataError Dexc)
        {
            throw new DataError(Dexc);
        }
    }

    /// <summary>
    /// This function check if the customer's email is valid.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private bool IsValidEmail(string email)
    {
        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(email);
    }
}

