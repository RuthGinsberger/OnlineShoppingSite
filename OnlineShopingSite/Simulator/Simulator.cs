using BlApi;
namespace Simulator;

public static class Simulator
{
    static string? previousStation;
    static string? stationAfter;
    static bool finishBool = true;

    public static event EventHandler? Stop;

    public static event EventHandler? ProgressUpdate;

    public static void DoStop()
    {
        if (Stop != null)
            FinishSimulator();
    }
    public class Details : EventArgs
    {
        public int Id;
        public int seconds;
        public string prev;
        public string After;
        public Details(int myid, string myPrev, string myAfter, int sec)
        {
            Id = myid;
            prev = myPrev;
            After = myAfter;
            seconds = (sec / 1000);
        }
    }
    public static void run()
    {

        Thread mainThreads = new Thread(choisenOrder);
        mainThreads.Start();
    }

    public static void choisenOrder()
    {
        IBl bl = new BlImplementation.Bl();
        int id;
        while (finishBool)
        {
            id = (int)bl.Order.ChooseOrder();
            if (id == 0)
                Stop("", EventArgs.Empty);
            else
            {
                previousStation = bl.Order.GetOrder(id).Status.ToString();
                Random rnd = new Random();
                int num = rnd.Next(1000, 5000);
                stationAfter = (previousStation == "Confirmed" ? "sent" : "deliver to customer");
                ProgressUpdate("", new Details(id, previousStation, stationAfter, num));
                Thread.Sleep(num);
                stationAfter = (previousStation == "Confirmed" ? bl.Order.SendOrder(id) : bl.Order.DaliverOrder(id)).Status.ToString();
            }
        }
        return;
    }
    public static void FinishSimulator()
    {
        finishBool = false;
        return;
    }
}