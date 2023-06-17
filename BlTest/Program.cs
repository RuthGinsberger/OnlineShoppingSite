using BlApi;
using BlImplementation;

/// <summary>
/// A program to test the BL.
/// </summary>

IBl bl = new Bl();
BO.Cart cart = new BO.Cart();
cart.Items = new List<BO.OrderItem>();
int choice;
int orderId;
int productId;
//=====================Product===============
void product()
{
    try
    {
        do
        {
            Console.WriteLine("Enter your choice:\n" +
                "0- Exit .\n" +
                "1- Get products .\n" +
                "2- Get catalog.\n" +
                "3- Get product manager.\n" +
                "4- Get Product customer .\n" +
                "5- Add product.\n" +
                "6- Delete product.\n" +
                "7- Update product.\n"); ;
            if (!(int.TryParse(Console.ReadLine(), out choice)))
                throw new BlApi.InvalidValue("choice");
            switch (choice)
            {
                case 0:
                    main();
                    break;
                case 1:
                    getProducts();
                    break;
                case 2:
                    getCatalog();
                    break;
                case 3:
                    getProductManager();
                    break;
                case 4:
                    getProductCustomer();
                    break;
                case 5:
                    addProduct();
                    break;
                case 6:
                    deleteProduct();
                    break;
                case 7:
                    updateProduct();
                    break;
                default:
                    Console.WriteLine("Wrong choice");
                    break;
            }
        } while (choice != 0);
    }
    catch (InvalidValue ex)
    {
        throw new InvalidValue($"{ex}");
    }
    catch (DataError ex)
    {
        throw new DataError(ex);
    }
}

/// <summary>
/// The function console the products list.
/// </summary>
void getProducts()
{
    IEnumerable<BO.ProductForList> orderList = bl.Product.GetProductsList();
    foreach (var item in orderList)
    {
        Console.WriteLine(item);
    }
}

/// <summary>
/// The function console the products item list.
/// </summary>
void getCatalog()
{
    IEnumerable<BO.ProductItem> productItems = bl.Product.GetProducstItem();
    foreach (var item in productItems)
    {
        Console.WriteLine(item);
    }
}

/// <summary>
/// The function console the product for manager.
/// </summary>
void getProductManager()
{
    Console.WriteLine("Enter product id");
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new InvalidValue(" id product");
    BO.Product item = bl.Product.GetProductManager(productId);
    Console.WriteLine(item);
}

/// <summary>
/// The function console the product for customer.
/// </summary>
void getProductCustomer()
{
    Console.WriteLine("Enter product id");
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new InvalidValue("product id");
    BO.ProductItem item = bl.Product.GetProductCustomer(productId, cart);
    Console.WriteLine(item);
}

/// <summary>
/// The function add a product.
/// </summary>
void addProduct()
{
    BO.Product product = new BO.Product();
    product.ID = 0;
    Console.WriteLine("Enter product name");
    product.Name = Console.ReadLine();
    Console.WriteLine("Enter product price");
    product.Price = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("Enter the Product's category: 1 - Shoes, 2 - Clothes, 3 - Furniture, 4-Jewelry, 5-Sports");
    int choice = Convert.ToInt32(Console.ReadLine());
    product.Category = (BO.Enums.eCategory)choice;
    Console.WriteLine("Enter product amount in stock");
    product.InStock = Convert.ToInt32(Console.ReadLine());
    bl.Product.AddProduct(product);
}

/// <summary>
/// The function delete a product.
/// </summary>
void deleteProduct()
{
    Console.WriteLine("Enter product id");
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new InvalidValue("product id");
    bl.Product.DeleteProduct(productId);
}

/// <summary>
/// The function update a product.
/// </summary>
void updateProduct()
{
    string name;
    BO.Product BoProduct = new BO.Product();
    Console.WriteLine("Enter product id");
    BoProduct.ID = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter new name for the Product");
    name = Console.ReadLine();
    if (!string.IsNullOrEmpty(name))
        BoProduct.Name = name;

    Console.WriteLine("Enter the new category for the Product: 1 - kitchen, 2 - washRoom, 3 - otherRoom");
    string choice1;
    choice1 = Console.ReadLine();
    if (!string.IsNullOrEmpty(choice1))
    {
        int choice2 = Convert.ToInt32(choice1);
        BoProduct.Category = (BO.Enums.eCategory)choice2;
    }

    Console.WriteLine("Enter the new price for the Product");
    string price1 = Console.ReadLine();
    if (!string.IsNullOrEmpty(price1))
    {
        double price2 = Convert.ToDouble(price1);
        BoProduct.Price = price2;
    }

    Console.WriteLine("Enter the new amount in stock");
    string inStock1 = Console.ReadLine();
    if (!string.IsNullOrEmpty(inStock1))
    {
        int inStock2 = Convert.ToInt32(inStock1);
        BoProduct.InStock = inStock2;
    }
    else
        BoProduct.InStock = -1;
    bl.Product.UpDateProduct(BoProduct);
}
//=======================Cart==================
void carts()
{
    try
    {
        do
        {
            Console.WriteLine("Enter your choice:\n" +
                "0- Exit .\n" +
                "1- Add product to cart .\n" +
                "2- Update product amount.\n" +
                "3- Confirm cart.\n");
            if (!(int.TryParse(Console.ReadLine(), out choice)))
                throw new BlApi.InvalidValue("choice");
            switch (choice)
            {
                case 0:
                    main();
                    break;
                case 1:
                    AddProductToCart();
                    break;
                case 2:
                    UpdateProductAmount();
                    break;
                case 3:
                    ConfirmCart();
                    break;
                default:
                    Console.WriteLine("wrong choice");
                    break;
            }
        } while (choice != 0);
    }
   catch (DalApi.EntityNotFoundException)
    {
        throw new NotExist("product");
    }
    catch (DataError ex)
    {
        throw new DataError(ex);
    }
}

/// <summary>
/// The function add a product to the cart
/// </summary>
void AddProductToCart()
{
    Console.WriteLine("Enter product id");
    int productId;
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new InvalidValue("product id");
    cart = bl.Cart.AddProduct(cart, productId);
}

/// <summary>
/// The function update a product amount.
/// </summary>
void UpdateProductAmount()
{
    int productId, newAmount;
    Console.WriteLine("Enter product id");
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new BlApi.InvalidValue("product id");
    Console.WriteLine("Enter new amount for the product");
    if (!(int.TryParse(Console.ReadLine(), out newAmount)))
        throw new BlApi.InvalidValue("amount");
    cart = bl.Cart.UpdateAmountProduct(cart, productId, newAmount);
}

/// <summary>
/// The function confirm the cart.
/// </summary>
void ConfirmCart()
{
    Console.WriteLine("Enter the customer's name");
    string CustomerName = Console.ReadLine();
    Console.WriteLine("Enter the customer's email");
    string CustomerEmail = Console.ReadLine();
    Console.WriteLine("Enter the customer's address");
    string CustomerAddress = Console.ReadLine();
    bl.Cart.ConfirimCartToOrder(cart);
}
//======================================Orders=====================================

/// <summary>
/// The function return products list.
/// </summary>
void GetProductsList()
{
    IEnumerable<BO.OrderForList> orderList = bl.Order.GetOrdersList();
    foreach (var item in orderList)
    {
        Console.WriteLine(item);
    }
}

/// <summary>
/// The function get an order.
/// </summary>
void GetOrder()
{
    Console.WriteLine("Enter order id");
    int orderId;
    if (!(int.TryParse(Console.ReadLine(), out orderId)))
        throw new InvalidValue("order id");
    List<BO.OrderItem> BoOrder = bl.Order.Items(orderId);
    Console.WriteLine(BoOrder);
}

/// <summary>
/// The function send an order.
/// </summary>
void SendOrder()
{
    Console.WriteLine("Enter order id");
    if (!(int.TryParse(Console.ReadLine(), out orderId)))
        throw new InvalidValue("order id");
    BO.Order BoOrder = bl.Order.SendOrder(orderId);
    Console.WriteLine(BoOrder);
}

/// <summary>
/// The function deliver an order.
/// </summary>
void DaliverOrder()
{
    Console.WriteLine("Enter order id");
    if (!(int.TryParse(Console.ReadLine(), out orderId)))
        throw new InvalidValue("order id");
    BO.Order BoOrder = bl.Order.DaliverOrder(orderId);
    Console.WriteLine(BoOrder);
}


void orders()
{
    try
    {
        Console.WriteLine("Enter your choice:" +
            "0- Exit .\n" +
            "1- Get Products List.\n" +
            "2- Get Order.\n" +
            "3- Send Order.\n" +
            "4- Daliver Order.\n");
        choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 0:
                main();
                break;
            case 1:
                GetProductsList();
                break;
            case 2:
                GetOrder();
                break;
            case 3:
                SendOrder();
                break;
            case 4:
                DaliverOrder();
                break;
            default:
                break;
        }
    }
    catch (InvalidValue err)
    {
        Console.WriteLine(err);
    }
    catch (BlNoNeedToUpdateException err)
    {
        throw new DataError(err);
    }
    catch (DataError err)
    {
        throw new DataError(err);
    }
}

void main()
{
    BO.Enums.eOption choice;
    try
    {
        do
        {
            Console.WriteLine("Enter your choice:\n" +
                "0- Exit.\n" +
                "1- Order .\n" +
                "2- Product.\n" +
                "3- Cart.\n");

            choice = (BO.Enums.eOption)Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case BO.Enums.eOption.Exit:
                    break;
                case BO.Enums.eOption.Order:
                    orders();
                    break;
                case BO.Enums.eOption.Product:
                    product();
                    break;
                case BO.Enums.eOption.Cart:
                    carts();
                    break;
                default:
                    break;
            }
        } while (choice != 0);
    }
    catch (Exception err)
    {
        Console.WriteLine(err);
    }
}
main();
