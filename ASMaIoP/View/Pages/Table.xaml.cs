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
using ASMaIoP.ViewModel;
using CefSharp;
using System.IO;
using System.Threading;
using Windows.UI.Xaml.Controls;


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

        Mutex tabelLoadMutex;

        object locker = new object();

        public Table(ProfileData profile)
        {
            InitializeComponent();

            DatePick.SelectedDateFormat = DatePickerFormat.Short;
            vm = new TabelVM(profile);
            DataContext = vm;   
            if(profile.accessLevel < 3)
            {
                Browser.IsEnabled = false;
            }
            Browser.JavascriptMessageReceived += OnBrowserJavascriptMessageReceived;
            tabelLoadMutex = new Mutex();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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
            tabelLoad = new Thread(() =>
            {
                lock(locker)
                {
                    string code = vm.GetHtmlCode();

                    File.WriteAllText("tmp.html", code);

                    string formater = Directory.GetCurrentDirectory();

                    formater = formater.Replace('\\', '/');
                
                    Browser.Dispatcher.Invoke(() => Browser.Load($"file://{formater}/tmp.html"));
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
