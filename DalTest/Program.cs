using Dal.DO;
using Dal.dalObject;
using DalApi;

/// <summary>
/// A program to test the DL.
/// </summary>

int id;
int idProduct;
int idOrder;
int category;
double price;
int amount;
IEnumerable<Product> productsList;
IEnumerable<OrderItem> orderItemList;
IEnumerable<Order> orderList;
Order tmpOrder = new Order();
Product tmpProduct = new Product();
OrderItem tmpOrderItem = new OrderItem();
IDal dallist =  Factory.Get();

//=========================================Product===========================================

/// <summary>
/// The function create a product.
/// </summary>
void createProduct()
{
    tmpProduct.ID = DataSource.Config.ProductID;
    Console.WriteLine("Enter product name.\n");
    tmpProduct.Name = Convert.ToString(Console.ReadLine());
    Console.WriteLine("Enter product Price\n");
    tmpProduct.Price = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("Enter the new category for the product: 1-Shoes, 2-Clothes, 3-Furniture, 4-Jewelry, 5-Sports.\n");
    category = Convert.ToInt32(Console.ReadLine());
    tmpProduct.Category = (eCategory)category;
    Console.WriteLine("Enter product stock.\n");
    tmpProduct.InStock = Convert.ToInt32(Console.ReadLine());
    dallist.Product.Add(tmpProduct);
}

/// <summary>
/// The function update a product.
/// </summary>
void updateProduct()
{
    Console.WriteLine("Enter product id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    tmpProduct = dallist.Product.Get(p => p.ID == id);
    Console.WriteLine(tmpProduct);
    Console.WriteLine("Enter product name.\n");
    string name = Convert.ToString(Console.ReadLine());
    if (name != null)
        tmpProduct.Name = name;
    Console.WriteLine("Enter product price.\n");
    price = Convert.ToDouble(Console.ReadLine());
    if (price != null)
        tmpProduct.Price = price;
    Console.WriteLine("Enter the new category for the product: 1-Shoes, 2-Clothes, 3-Furniture, 4-Jewelry, 5-Sports.\n");
    category = Convert.ToInt32(Console.ReadLine());
    if (category != null)
        tmpProduct.Category = (eCategory)category;
    Console.WriteLine("Enter product stock.\n");
    int stock = Convert.ToInt32(Console.ReadLine());
    if (stock != null)
        tmpProduct.InStock = stock;
    dallist.Product.Update(tmpProduct);
}

/// <summary>
/// The function read a product.
/// </summary>
void ReadProduct()
{
    Console.WriteLine("Enter product id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(dallist.Product.Get(p => p.ID == id));
}

/// <summary>
/// The function read products.
/// </summary>
void ReadProducts()
{
    productsList = dallist.Product.GetAll();
    foreach (Product p in productsList)
        Console.WriteLine(p);
}

/// <summary>
/// The function delete a product.
/// </summary>
void deleteProduct()
{
    Console.WriteLine("Enter product id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    dallist.Product.Delete(id);
}

void product()
{
    eCrud choice;
    Console.WriteLine("1- Add an product.\n" +
        "2- Display an product.\n" +
        "3- Display product list.\n" +
        "4- Update an product.\n" +
        "5- Delete an product.\n");
    choice = (eCrud)Convert.ToInt32(Console.ReadLine());
    try
    {
        switch (choice)
        {
            case eCrud.Create:
                createProduct();
                break;
            case eCrud.Read:
                ReadProduct();
                break;
            case eCrud.ReadAll:
                ReadProducts();
                break;
            case eCrud.Update:
                updateProduct();
                break;
            case eCrud.Delete:
                deleteProduct();
                break;
            default:
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

//================================Order================================================

/// <summary>
/// The function create an order.
/// </summary>
void CreateOrder()
{
    tmpOrder.ID = DataSource.Config.OrderID;
    Console.WriteLine("Enter customer name.\n");
    tmpOrder.CustomerName = Console.ReadLine();
    Console.WriteLine("Enter customer email.\n");
    tmpOrder.CustomerEmail = Console.ReadLine();
    Console.WriteLine("Enter customer adress.\n");
    tmpOrder.CustomerAdress = Console.ReadLine();
    tmpOrder.OrderDate = DateTime.Now;
    tmpOrder.ShipDate = null;
    tmpOrder.DeliveryDate = null;
    dallist.Order.Add(tmpOrder);
}

/// <summary>
/// The function read an order.
/// </summary>
void ReadOrder()
{
    Console.WriteLine("Enter order Id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(dallist.Order.Get(o => o.ID == id));
}

/// <summary>
/// The function read all orders.
/// </summary>
void ReadAllOrders()
{
    orderList = dallist.Order.GetAll();
    foreach (Order o in orderList)
        Console.WriteLine(o);
}

/// <summary>
/// The function update an order.
/// </summary>
void UpdateOrder()
{
    DateTime userDateTime;
    Console.WriteLine("Enter order Id\n");
    id = Convert.ToInt32(Console.ReadLine());
    Order tmpOrder = dallist.Order.Get(o => o.ID == id);
    Console.WriteLine(tmpOrder);
    Console.WriteLine("Enter order customer name.\n");
    tmpOrder.CustomerName = Console.ReadLine();
    Console.WriteLine("Enter order customer email.\n");
    tmpOrder.CustomerEmail = Console.ReadLine();
    Console.WriteLine("Enter order customer adress.\n");
    tmpOrder.CustomerAdress = Console.ReadLine();
    Console.WriteLine("Enter order order date.\n");
    if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
        tmpOrder.OrderDate = userDateTime;
    else
        throw new Exception("You haven't entered a valid datetime value.\n");
    Console.WriteLine("Enter order ship date\n");
    if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
        tmpOrder.ShipDate = userDateTime;
    else
        throw new Exception("You haven't entered a valid datetime value.\n");
    Console.WriteLine("Enter order delivery date.\n");
    if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
        tmpOrder.DeliveryDate = userDateTime;
    else
        throw new Exception("You haven't entered a valid datetime value.\n");
    dallist.Order.Update(tmpOrder);
}

/// <summary>
/// The function delete an order.
/// </summary>
void DeleteOrder()
{
    Console.WriteLine("Enter order id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    dallist.Order.Delete(id);
}

void CrudOrders()
{
    eCrud choice;
    Console.WriteLine("1- Add an order.\n" +
        "2- Display an order.\n" +
        "3- Display orders list.\n" +
        "4- Update an order.\n" +
        "5- Delete an order.\n");
    choice = (eCrud)Convert.ToInt32(Console.ReadLine());
    try
    {
        switch (choice)
        {
            case eCrud.Create:
                CreateOrder();
                break;
            case eCrud.Read:
                ReadOrder();
                break;
            case eCrud.ReadAll:
                ReadAllOrders();
                break;
            case eCrud.Update:
                UpdateOrder();
                break;
            case eCrud.Delete:
                DeleteOrder();
                break;
            default:
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
//========================OrderItem========================

/// <summary>
/// The function create an order item.
/// </summary>
void CreateOrderItem()
{
    Console.WriteLine("Enter order item id.\n");
    tmpOrderItem.ID = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter order id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    tmpOrder = dallist.Order.Get(o => o.ID == id); ;
    tmpOrderItem.OrderID = tmpOrder.ID;
    Console.WriteLine("Enter product id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    tmpProduct = dallist.Product.Get(p => p.ID == id);
    tmpOrderItem.ProductID = tmpProduct.ID;
    Console.WriteLine("Enter product amount.\n");
    amount = Convert.ToInt32(Console.ReadLine());
    if (amount <= 0 || amount > tmpProduct.InStock)
        throw new Exception("Price is not valid.\n");
    tmpOrderItem.Amount = amount;
    tmpOrderItem.Price = (amount * tmpProduct.Price);
    dallist.OrderItem.Add(tmpOrderItem);
}

/// <summary>
/// The function update an order item.
/// </summary>
void UpdateOrderItem()
{
    Console.WriteLine("Enter order item id.\n");
    tmpOrderItem.ID = Convert.ToInt32(Console.ReadLine());
    tmpOrderItem = dallist.OrderItem.Get(oi => oi.ID == tmpOrderItem.ID);
    Console.WriteLine(tmpOrderItem);
    Console.WriteLine("Enter order id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    tmpOrder = dallist.Order.Get(o => o.ID == id);
    tmpOrderItem.OrderID = id;
    Console.WriteLine("Enter product id.\n");
    id = Convert.ToInt32(Console.ReadLine());
    tmpProduct = dallist.Product.Get(p => p.ID == id);
    tmpOrderItem.ProductID = id;
    Console.WriteLine("Enter product amount.\n");
    amount = Convert.ToInt32(Console.ReadLine());
    if (amount <= 0 || amount > tmpProduct.InStock)
        throw new Exception("Price is not valid.\n");
    tmpOrderItem.Amount = amount;
    tmpOrderItem.Price = (amount * tmpProduct.Price);
    dallist.OrderItem.Update(tmpOrderItem);
}

/// <summary>
/// The function remove an order item.
/// </summary>
void ReadOrderItem()
{
    Console.WriteLine("Enter order item Id\n");
    id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(dallist.OrderItem.Get(oi => oi.ID == id));
}

/// <summary>
/// The function read an order item by order's id and product's id.
/// </summary>
void ReadByOrderProductIds()
{
    Console.WriteLine("Enter product Id.\n");
    idProduct = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter order Id.\n");
    idOrder = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(dallist.OrderItem.Get(oi => oi.OrderID == idOrder&&oi.ProductID==idProduct));
}

/// <summary>
/// TThe function read an order item by order's id.
/// </summary>
void ReadByOrderid()
{
    Console.WriteLine("Enter order Id.\n");
    idOrder = Convert.ToInt32(Console.ReadLine());
    orderItemList = dallist.OrderItem.GetAll(oi => oi.OrderID == idOrder);
    foreach (OrderItem Oi in orderItemList)
    {
        if (Oi.ID != 0)
            Console.WriteLine(Oi);
    }
}

/// <summary>
/// TThe function read an order item.
/// </summary>
void ReadOrderItems()
{
    orderItemList = dallist.OrderItem.GetAll();
    foreach (OrderItem Oi in orderItemList)
    {
        if (Oi.ID != 0)
            Console.WriteLine(Oi);
    }
}

/// <summary>
/// TThe function delete an order item.
/// </summary>
void DeleteOrderItem()
{
    Console.WriteLine("Enter order item Id\n");
    id = Convert.ToInt32(Console.ReadLine());
    dallist.OrderItem.Delete(id);
}

void orderItem()
{
    eCrudOrderItem choice;
    Console.WriteLine("1- Add an order item.\n" +
        "2- Display an order item.\n" +
        "3- Display order item by order and product ids.\n" +
        "4- Display order item by order id.\n" +
        "5- Display order item list.\n" +
        "6- Update an order item.\n" +
        "7- Delete an order item.\n");
    choice = (eCrudOrderItem)Convert.ToInt32(Console.ReadLine());
    try
    {
        switch (choice)
        {

            case eCrudOrderItem.Create:
                CreateOrderItem();
                break;
            case eCrudOrderItem.Read:
                ReadOrderItem();
                break;
            case eCrudOrderItem.ReadByOrderProductIds:
                ReadByOrderProductIds();
                break;
            case eCrudOrderItem.ReadByOrderid:
                ReadByOrderid();
                break;
            case eCrudOrderItem.ReadAll:
                ReadOrderItems();
                break;
            case eCrudOrderItem.Update:
                UpdateOrderItem();
                break;
            case eCrudOrderItem.Delete:
                DeleteOrderItem();
                break;
            default:
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

//===============Main====================

void main()
{
    DataSource.s_Initialize();
    eOption choice;
    do
    {
        Console.WriteLine("enter your choice:\n" +
            "0- exit.\n" +
            "1- product crud.\n" +
            "2- order crud.\n" +
            "3- order item crud.\n");
        choice = (eOption)Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case eOption.Exit:
                break;
            case eOption.Product:
                product();
                break;
            case eOption.Order:
                CrudOrders();
                break;
            case eOption.OrderItem:
                orderItem();
                break;
            default:
                break;
        }
    } while (choice != 0);
}
main();





