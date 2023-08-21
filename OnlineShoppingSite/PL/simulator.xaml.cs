using System;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using static Simulator.Simulator;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PL
{
    /// <summary>
    /// Interaction logic for simulator.xaml
    /// </summary>
    public partial class simulator : Window
    {
        private Stopwatch stopWatch;
        BackgroundWorker clock;
        ProgressBar PBar2;
        public simulator()
        {
            InitializeComponent();
            this.Oclock.Text = DateTime.Now.ToString("T");
            Loaded += ToolWindow_Loaded;
            stopWatch = new Stopwatch();
            clock = new BackgroundWorker();
            clock.DoWork += Clock_DoWork;
            clock.ProgressChanged += clock_ProgressChanged;
            clock.RunWorkerCompleted += Clock_RunWorkerCompleted;
            clock.WorkerReportsProgress = true;
            clock.WorkerSupportsCancellation = true;
            clock.RunWorkerAsync();
        }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void clock_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            string clock = DateTime.Now.ToString("T");
            clock = clock.Substring(0, 8);
            this.Oclock.Text = clock;
        }
        private void Clock_RunWorkerCompleted(object? sender, EventArgs e)
        {
            Simulator.Simulator.ProgressUpdate -= changeOrder;
            Simulator.Simulator.ProgressUpdate -= CreateDynamicProgressBarControl;
            Simulator.Simulator.Stop -= Button_Finish;
            MessageBox.Show("The simulation has either finished or stopped.\nGoodbye...");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Clock_DoWork(object? sender, DoWorkEventArgs e)
        {
            stopWatch.Restart();
            Simulator.Simulator.ProgressUpdate += changeOrder;
            Simulator.Simulator.ProgressUpdate += CreateDynamicProgressBarControl;
            Simulator.Simulator.Stop += Button_Finish;
            Simulator.Simulator.run();
            while (clock.WorkerSupportsCancellation)
            {
                clock.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        private void changeOrder(object sender, EventArgs e)
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            else
            {
                if (!(e is Details))
                    return;
                Details details = e as Details;
                var dataCntxt = new Tuple<int, string, string, int>(details.Id, details.prev, details.After, details.seconds);
                DataContext = dataCntxt;
            }
        }
        private void Button_Finish(object sender, EventArgs e)
        {
            Simulator.Simulator.DoStop();
            if (clock.WorkerSupportsCancellation == true)
                clock.WorkerSupportsCancellation = false;
        }
        private void CreateDynamicProgressBarControl(object sender, EventArgs e)
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(CreateDynamicProgressBarControl, sender, e);
            }
            else
            {
                if (!(e is Details))
                    return;
                Details details = e as Details;
                if (PBar2 != null)
                {
                    SBar.Items.Remove(PBar2);
                }
                PBar2 = new ProgressBar();
                PBar2.IsIndeterminate = false;
                PBar2.Orientation = Orientation.Horizontal;
                PBar2.Width = 800;
                PBar2.Height = 30;
                Duration duration = new Duration(TimeSpan.FromSeconds(details.seconds * 3));
                DoubleAnimation doubleanimation = new DoubleAnimation(200.0, duration);
                PBar2.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
                SBar.Items.Add(PBar2);
            }
        }
    }
}