using ASMaIoP.Model;
using ASMaIoP.ViewModel;
using CefSharp.DevTools.Profiler;
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
using System.Windows.Shapes;
using System.Threading;

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для AddWeakend.xaml
    /// </summary>
    public partial class AddWeakend : Window
    {
        AddWeakendVM vm;
        object locker = new object();
        Thread thread;

        public AddWeakend(ProfileData profile, EmployeeData data)
        {
            InitializeComponent();
            vm = new AddWeakendVM(profile, data);
            DataContext = vm;
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DatabaseInterface.FillTableType fill = Type.SelectedIndex == 1 ? DatabaseInterface.FillTableType.Weekend : Type.SelectedIndex == 0 ? DatabaseInterface.FillTableType.Medical : DatabaseInterface.FillTableType.Weekend;
            if (LastDay.SelectedDate == null || FirstDay.SelectedDate == null) return;
            string desc = Description.Text;
            DateTime dt1 = FirstDay.SelectedDate.Value;
            DateTime dt2 = LastDay.SelectedDate.Value;
            thread = new Thread(() =>
            {
                lock(locker)
                {
                    vm.CreateWeakend(desc, dt1, dt2, fill);
                }
            });
            thread.Start();
            this.Close();
        }
    }
}
