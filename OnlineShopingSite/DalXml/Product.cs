using Dal.DO;
using DalApi;
using System.Linq;
using System;
using System.Xml.Linq;
using static Dal.Exceptions;
using System.Runtime.CompilerServices;

namespace Dal;
internal class Product : IProduct
{
    XDocument? root = XDocument.Load("..\\..\\..\\..\\xml\\Product.xml");
    DO.Product p = new();


    /// <summary>
    /// The function add an product to the xml file.
    /// </summary>
    /// <param name="Item"></param>
    /// <returns></returns>
    private IEnumerable<DO.Product> Casting(IEnumerable<XElement>? allProduct)
    {
        try
        {
            return from product in allProduct
                   select new DO.Product
                   {
                       ID = Convert.ToInt32(product.Attribute("ID")?.Value),
                       Name = product.Attribute("Name")?.Value,
                       Category = Enum.Parse<eCategory>(product.Attribute("Category").Value),
                       Price = Convert.ToInt32(product.Attribute("Price")?.Value),
                       InStock = Convert.ToInt32(product.Attribute("InStock")?.Value)
                   };
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }
    /// <summary>
    /// The function delete an product from the xml file.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Product p)
    {
        try
        {
            XDocument? configRoot = XDocument.Load("..\\..\\..\\..\\xml\\Config.xml");
            string? id = configRoot.Descendants("ProductId").FirstOrDefault()?.Value;
            XElement? ProductId = new XElement("ProductId", Convert.ToString(Convert.ToInt32(id) + 1));
            XElement? product = new("Product",
             new XAttribute("ID", id),
             new XAttribute("Name", p.Name),
             new XAttribute("Price", p.Price),
             new XAttribute("Category", p.Category),
             new XAttribute("InStock", p.InStock));
            root?.Element("Products")?.Add(product);
            root?.Save("..\\..\\..\\..\\xml\\Product.xml");
            configRoot?.Descendants("ProductId").FirstOrDefault()?.ReplaceWith(ProductId);
            configRoot?.Save("..\\..\\..\\..\\xml\\Config.xml");
            return Convert.ToInt32(id);
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }

    /// <summary>
    /// The function delete an product from the xml file.
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        try
        {
            root?.Descendants("Product")
               .Where(x => x.Attribute("ID")?.Value == id.ToString())
               .Remove();
            root?.Save("..\\..\\..\\..\\xml\\Product.xml");
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }


    /// <summary>
    /// The function get an product from the xml file acording to lambada expression.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Product Get(Func<DO.Product, bool> func)
    {
        try
        {
            IEnumerable<XElement>? ListXElement = root?.Element("Products")?.Elements("Product").ToList();
            List<DO.Product> productsList = Casting(ListXElement).ToList();
            return productsList.Where(func).FirstOrDefault();
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }

    /// <summary>
    /// The function get all the products from the xml file.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {
        try
        {
            IEnumerable<XElement>? ListXElement = root?.Element("Products")?.Elements("Product").ToList();
            List<DO.Product>? productsList = new();
            if (ListXElement != null)
            {
                productsList = Casting(ListXElement).ToList();
                if (func != null)
                    return productsList.Where(func);
            }
            return productsList;
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }


    /// <summary>
    /// The function update an product to the xml file.
    /// </summary>
    /// <param name="item"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Product item)
    {
        try
        {
            XElement? product = new("Product",
               new XAttribute("ID", item.ID),
               new XAttribute("Name", item.Name),
               new XAttribute("Price", item.Price),
               new XAttribute("Category", item.Category),
               new XAttribute("InStock", item.InStock));
            root?.Descendants("Product")
                    .Where(x => x.Attribute("ID")?.Value == item.ID.ToString()).FirstOrDefault()?.ReplaceWith(product);
            Console.WriteLine(root?.Descendants("Product")
                    .Where(x => x.Attribute("ID")?.Value == item.ID.ToString()));
            root?.Save("..\\..\\..\\..\\xml\\Product.xml");
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }
}
