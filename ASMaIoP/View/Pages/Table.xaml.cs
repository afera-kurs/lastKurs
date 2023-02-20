using ASMaIoP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ASMaIoP.Model;
using CefSharp;
using System.IO;
using System.Threading;
using Windows.UI.Xaml.Controls;
using CefSharp.Wpf;
using CefSharp.DevTools.Profiler;

namespace ASMaIoP.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : System.Windows.Controls.Page
    {
        TabelVM vm;
        EditTable editTable;
        Thread tabelLoad;
        ProfileData profile;
        Mutex tabelLoadMutex;
        System.Windows.Controls.Grid loading;
        Thread thread;

        object locker = new object();

        public Table(ProfileData profile, System.Windows.Controls.Grid loading)
        {
            this.loading = loading;
            this.profile = profile;
            InitializeComponent();
            vm = new TabelVM(profile);
            DataContext = vm;   

        }

        ChromiumWebBrowser Browser;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (profile.accessLevel < 3)
            {
                BrowserControl.IsEnabled = false;
            }
            //
            tabelLoadMutex = new Mutex();

            DatePick.SelectedDateFormat = DatePickerFormat.Short;

            thread = new Thread(() =>
            {
                var settings = new CefSettings();
                //CefSharpSettings.ConcurrentTaskExecution = true;
                settings.CefCommandLineArgs.Add("disable-gpu-vsync"); //Disable GPU vsync
                settings.CefCommandLineArgs.Add("disable-gpu");
                settings.MultiThreadedMessageLoop = true;
                if(Cef.IsShutdown)
                    Cef.Initialize(settings);
                Dispatcher.Invoke(() =>
                {
                    Browser = new ChromiumWebBrowser();
                    BrowserControl.Content = Browser;
                    Browser.JavascriptMessageReceived += OnBrowserJavascriptMessageReceived;
                });
            });
            thread.Start();

        }
        private void OnBrowserJavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
                Dispatcher.Invoke(() =>
                {
                    var windowSelection = (string)e.Message;
                    editTable = new EditTable(windowSelection, vm.year, vm.month, () =>
                    {

                        UpdateTabel();
                    });
                    editTable.Show();
                });

        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            Browser.Print();
        }

        public void UpdateTabel()
        {
            loading.Visibility = Visibility.Visible;
            loading.IsEnabled = true;

            tabelLoad = new Thread(() =>
            {
                lock(locker)
                {
                    string code = vm.GetHtmlCode();

                    File.WriteAllText("tmp.html", code);

                    string formater = Directory.GetCurrentDirectory();

                    formater = formater.Replace('\\', '/');
                
                    Browser.Dispatcher.Invoke(() =>
                    {
                        Browser.Load($"file://{formater}/tmp.html");
                        loading.Visibility = Visibility.Collapsed;
                        loading.IsEnabled = false;
                    });
                }
            });

            DateTime timer = DateTime.Parse(DatePick.SelectedDate == null ? DateTime.Now.ToString() : DatePick.SelectedDate.Value.ToString());
            vm.year = timer.Year;
            vm.month = timer.Month;
            tabelLoad.Start();
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            UpdateTabel();
        }
    }
}
