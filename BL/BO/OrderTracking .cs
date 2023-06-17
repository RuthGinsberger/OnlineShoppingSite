using static BO.Enums;
namespace BO;
public class OrderTracking
{
    public int ID { get; set; }
    public eOrderStatus? Status { get; set; }
    public List<Tuple<DateTime?, eOrderStatus?>> TrackList { get; set; } = new ();
    public override string ToString()
    {
        string toString =
            $@"ID: {ID}, 
            Status: {Status}, 
            dates: ";
        foreach (var i in TrackList) { toString += " \n \t \t" + i.Item2 + " on " + i.Item1; };
        return toString;
    }

}
