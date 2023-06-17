using Dal.DO;
namespace Dal.dalObject;
public static class DataSource
{
    static internal int NumOfProducts = 50;
    static internal int NumOfOrders = 100;
    static internal int NumOfOrderItems = 200;
    static public List<Product> Products = new List<Product>();
    static public List<Order> Orders = new List<Order>();
    static public List<OrderItem> OrderItems = new List<OrderItem>();
    static internal readonly Random rand = new Random();
    static internal string[] productNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    //orders
    static string[] customerName = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    static string[] customerEmail = { "A@", "B@", "C@", "D@", "E@", "F@", "G@", "H@", "I@", "J@", "K@", "L@", "M@", "N@", "O@", "P@", "Q@", "R@", "S@", "T@", "U@", "V@", "W@", "X@", "Y@", "Z@" };
    static string[] customerAdress = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    static DataSource() { s_Initialize(); }
    public static void s_Initialize()
    {
        CreateProductsList();
        CreateOrdersList();
        CreateOrderItemsList();
    }

    /// <summary>
    /// This function create ptoducts list
    /// </summary>
    static public void CreateProductsList()
    {
        for (int i = 0; i < 10; i++)
        {
            Product newProduct = new Product();
            newProduct.ID = Config.ProductID;
            newProduct.Name = productNames[i % 26];
            newProduct.Price = (int)rand.NextInt64(1, 100000);
            newProduct.Category = (eCategory)((i % 5)+1);
            newProduct.InStock = (int)rand.NextInt64(15, 50);
            Products.Add(newProduct);
        }
    }

    /// <summary>
    /// This function create orders list
    /// </summary>
    static public void CreateOrdersList()
    {
        TimeSpan t_ShipDate = TimeSpan.FromDays(10);
        TimeSpan t_DeliveryDate = TimeSpan.FromDays(30);
        for (int i = 0; i < 50; i++)
        {
            Order newOrders = new Order();
            newOrders.ID = Config.OrderID;
            newOrders.CustomerName = customerName[i % 26];
            newOrders.CustomerEmail = customerEmail[i % 26];
            newOrders.CustomerAdress = customerAdress[i % 26];
            newOrders.OrderDate = null;
            newOrders.ShipDate = (newOrders.OrderDate + t_ShipDate);
            newOrders.DeliveryDate = (newOrders.ShipDate + t_DeliveryDate);
            Orders.Add(newOrders);
        }
    }

    /// <summary>
    /// This function create order item list
    /// </summary>
    static public void CreateOrderItemsList()
    {
        OrderItem newOrderItems = new OrderItem();
        for (int i = 0; i < 40;)
        {
            int indexOrders = (int)rand.NextInt64(0, Orders.Count());
            int numOfProduct = (int)rand.NextInt64(1, 4);

            for (int j = 0; j < numOfProduct; j++)
            {
                int indexProduct = (int)rand.NextInt64(0, Products.Count());
                newOrderItems.ID = Config.OrderItemID;
                newOrderItems.ProductID = Products[indexProduct].ID;
                newOrderItems.OrderID = Orders[indexOrders].ID;
                newOrderItems.Amount = (int)rand.NextInt64(0, Products[indexProduct].InStock);
                newOrderItems.Price = (Products[indexProduct].Price) * newOrderItems.Amount;
                Product p = Products[indexProduct];
                p.InStock -= newOrderItems.Amount;
                OrderItems.Add(newOrderItems);
                i++;
            }

        }
    }

    /// <summary>
    /// A class for the ids that we want the program to enter and not the customer.
    /// </summary>
    public static class Config
    {
        private static int orderItemID = 1000000;
        private static int orderID = 3000000;
        private static int productID = 6000000;
       
        public static int OrderItemID { get { return orderItemID++; } }
        public static int OrderID { get { return orderID++; } }
        public static int ProductID { get { return productID++; } }
    }
}

