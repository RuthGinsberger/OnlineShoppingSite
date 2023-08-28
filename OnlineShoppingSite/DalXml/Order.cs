
using DalApi;
using Dall;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static Dal.Exceptions;

namespace Dal;
internal class Order : IOrder
{
    List<DO.Order>? Orders = new();
    Config? IDs = new();

    /// <summary>
    /// The function return an order id from the config.xml file and also increase the id. 
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int OrderId() { 
        StreamReader configReader = new("..\\..\\..\\..\\xml\\Config.xml");
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Config";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(Config), xRoot);
        var IDs = (Config?)ser.Deserialize(configReader);
        int id = IDs.OrderId;
        IDs.OrderId++;
        configReader.Close();
        StreamWriter configWriter = new("..\\..\\..\\..\\xml\\Config.xml");
        ser.Serialize(configWriter, IDs);
        configWriter.Close();
        return id;
    }
    /// <summary>
    /// The function add an order to the xml file.
    /// </summary>
    /// <param name="item"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Order item)
    {
        try
        {
            StreamReader r = new("..\\..\\..\\..\\xml\\Order.xml");
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>), xRoot);
            Orders = (List<DO.Order>?)ser.Deserialize(r);
            r.Close();
            item.ID = OrderId();
            StreamWriter w = new("..\\..\\..\\..\\xml\\Order.xml");
            Orders?.Add(item);
            ser.Serialize(w, Orders);
            w.Close();
            return item.ID;
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }

    /// <summary>
    /// The function delete an order from the xml file.
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>), xRoot);
            StreamReader r = new("..\\..\\..\\..\\xml\\Order.xml");
            Orders = (List<DO.Order>?)ser.Deserialize(r);
            r.Close();
            Orders?.Remove(Orders.Find(o => o.ID == id));
            StreamWriter w = new("..\\..\\..\\..\\xml\\Order.xml");
            ser.Serialize(w, Orders);
            w.Close();
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }

    /// <summary>
    /// The function get an order from the xml file acording to lambada expression.
    /// </summary>
    /// <param name="func"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order Get(Func<DO.Order, bool> func)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>), xRoot);
            StreamReader r = new("..\\..\\..\\..\\xml\\Order.xml");
            Orders = (List<DO.Order>?)ser.Deserialize(r);
            r.Close();
            Predicate<DO.Order> myfunc = new(func);
            return Orders.Find(myfunc);
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }


    /// <summary>
    /// The function get all the orders from the xml file.
    /// </summary>
    /// <param name="func"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>), xRoot);
            StreamReader r = new("..\\..\\..\\..\\xml\\Order.xml");
            Orders = (List<DO.Order>?)ser.Deserialize(r);
            r.Close();
            return func == null ? Orders : Orders.Where(func);
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }

    /// <summary>
    /// The function update an order to the xml file.
    /// </summary>
    /// <param name="item"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order item)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>), xRoot);
            StreamReader r = new("..\\..\\..\\..\\xml\\Order.xml");
            Orders = (List<DO.Order>?)ser.Deserialize(r);
            r.Close();
            Orders?.RemoveAt((int)(Orders?.FindIndex(Oi => Oi.ID == item.ID)));
            Orders?.Add(item);
            StreamWriter w = new("..\\..\\..\\..\\xml\\Order.xml");
            ser.Serialize(w, Orders);
            w.Close();
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }
}
