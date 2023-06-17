using DalApi;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Threading;

namespace Dal;

/// <summary>
/// this class is singlethon in order to instnce the class only once 
/// the class is also threadsafe using "lock" and also Lazy Initialization
/// </summary>

//Lazy- Makes sure that the instance should not be allocated immediately when the function that returns it is called,
//but just when its values are used.This is made to avoid a situation that a variable is initialized but never used,
//and wastes resources unnecessarily.

//Threadsafe- To avoid a situation that two threads will access an instance together and then it will be allocated twice.
//When accessing an instance we lock it until we finish with it.By doing so, if two threads try accessing an instance together
//the first one will enter and lock the instance, the second one will wait until the first one will finish and only when the
//first one finished with the instance and releases it, the second one will have access to there, then it'll see that the 
//instance is initialized and will use it.



internal sealed class DalList : IDal
{
    private static Lazy<IDal>? instance;
    public static IDal Instance { get { return GetInstence(); } }

    public static IDal GetInstence()
    {
        lock (instance ??= new Lazy<IDal>(() => new DalList())) 
        {
            return instance.Value;
        }
    }

    private DalList() { }
    public IProduct Product => new Dal.dalObject.DalProduct();
    public IOrder Order => new Dal.dalObject.DalOrder();
    public IOrderItem OrderItem => new Dal.dalObject.DalOrderItem();
}






