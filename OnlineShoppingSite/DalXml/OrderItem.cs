
using Dal.DO;
using DalApi;
using Dall;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Dal.Exceptions;

namespace Dal;
internal class OrderItem : IOrderItem
{
    List<DO.OrderItem>? OrderItems = new();
    Config? IDs;

    /// <summary>
    /// The function return an order item id from the config.xml file and also increase the id. 
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int OrderItemId()
    {
        StreamReader configReader = new("..\\..\\..\\..\\xml\\Config.xml");
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Config";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(Config), xRoot);
        IDs = (Config?)ser.Deserialize(configReader);
        int id = IDs.OrderItemId;
        IDs.OrderItemId++;
        configReader.Close();
        StreamWriter configWriter = new("..\\..\\..\\..\\xml\\Config.xml");
        ser.Serialize(configWriter, IDs);
        configWriter.Close();
        return id;
    }

    /// <summary>
    /// The function add an order item  to the xml file.
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.OrderItem item)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;
            StreamReader r = new("..\\..\\..\\..\\xml\\OrderItem.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot);
            OrderItems = (List<DO.OrderItem>?)ser.Deserialize(r);
            item.ID = OrderItemId();
            r.Close();
            OrderItems?.Add(item);
            StreamWriter w = new("..\\..\\..\\..\\xml\\OrderItem.xml");
            ser.Serialize(w, OrderItems);
            w.Close();
            return item.ID;
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }

    /// <summary>
    /// The function delete an order item  from the xml file.
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;
            StreamReader r = new("..\\..\\..\\..\\xml\\OrderItem.xml");
            XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
            OrderItems = (List<DO.OrderItem>?)ser.Deserialize(r);
            r.Close();
            OrderItems?.Remove(OrderItems.Find(o => o.ID == id));
            StreamWriter w = new("..\\..\\..\\..\\xml\\OrderItem.xml");
            ser.Serialize(w, OrderItems);
            w.Close();
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }


    /// <summary>
    /// The function get an order item  from the xml file acording to lambada expression.
    /// </summary>
    /// <param name="func"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem Get(Func<DO.OrderItem, bool> func)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;
            StreamReader r = new("..\\..\\..\\..\\xml\\OrderItem.xml");
            XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
            OrderItems = (List<DO.OrderItem>?)ser.Deserialize(r);
            r.Close();
            Predicate<DO.OrderItem> myfunc = new(func);
            return OrderItems.Find(myfunc);
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }


    /// <summary>
    /// The function get all the order items  from the xml file.
    /// </summary>
    /// <param name="func"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;
            StreamReader r = new("..\\..\\..\\..\\xml\\OrderItem.xml");
            XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
            OrderItems = (List<DO.OrderItem>?)ser.Deserialize(r);
            r.Close();
            return func == null ? OrderItems : OrderItems?.Where(func);
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }


    /// <summary>
    /// The function update an order item to the xml file.
    /// </summary>
    /// <param name="item"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem item)
    {
        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;
            StreamReader r = new("..\\..\\..\\..\\xml\\OrderItem.xml");
            XmlSerializer ser = new(typeof(List<DO.OrderItem>), xRoot);
            OrderItems = (List<DO.OrderItem>?)ser.Deserialize(r);
            r.Close();
            OrderItems?.RemoveAt((int)(OrderItems?.FindIndex(Oi => Oi.ID == item.ID)));
            OrderItems?.Add(item);
            StreamWriter w = new("..\\..\\..\\..\\xml\\OrderItem.xml");
            ser.Serialize(w, OrderItems);
            w.Close();
        }
        catch (DataError Dexc)
        { throw Dexc; }
    }
}
