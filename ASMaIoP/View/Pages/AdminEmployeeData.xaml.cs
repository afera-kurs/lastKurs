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
using ASMaIoP.View;
using ASMaIoP.ViewModel;
using ASMaIoP.Model;
using System.Threading;
using static ASMaIoP.ViewModel.AdminEmployeeDataVM;

namespace ASMaIoP.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Tabel.xaml
    /// </summary>
    public partial class AdminEmployeeData : Page
    {
        AdminEmployeeDataVM vm;
        object locker = new object();
        Thread loadDataGrid;
        Thread loadDataPerosnal;
        Grid loading;
        public ProfileData prof;
        object locker2 = new object();

        public AdminEmployeeData(ProfileData prof, Grid loading)
        {
            InitializeComponent();
            this.loading = loading;
            vm = new AdminEmployeeDataVM(prof);
            DataContext = vm;

            this.prof = prof;
        }

        private void UpdateData()
        {
            loading.IsEnabled = true;
            loading.Visibility = Visibility.Visible;

            loadDataGrid = new Thread(() =>
            {
                lock (locker)
                {
                    vm.UpdateData();
                    EmployeeDataGrid.Dispatcher.Invoke(() =>
                    {
                        vm.LoadDataToDataGrid((tabel) =>
                        {
                            EmployeeDataGrid.ItemsSource = null;
                            EmployeeDataGrid.ItemsSource = tabel;
                            loading.Visibility = Visibility.Collapsed;
                            loading.IsEnabled = false;
                        });
                    });
                }
            });
            loadDataGrid.Start();
        }

        private void CreateNewUser_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee employee = new CreateEmployee(prof);
            employee.ShowDialog();
            UpdateData();
        }
        

        private void EmployeeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        { 
            if (!Guard.EmployeeEditPanel ) return;

            var item = (EmployeeDataColumn)EmployeeDataGrid.SelectedItem;
            Dispatcher.Invoke(() =>
            {
                ShowEmployeeInfoWindow(item);
                vm.LoadDataToDataGrid((tabel) =>
                {
                    EmployeeDataGrid.ItemsSource = null;
                    EmployeeDataGrid.ItemsSource = tabel;
                });
            });
            
        }

        internal void ShowEmployeeInfoWindow(EmployeeDataColumn data)
        {
            vm.ShowWindow(data);
            UpdateData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateNewUser.Visibility =
                Guard.EmployeeEditPanel
                ? Visibility.Visible : Visibility.Collapsed;
            UpdateData();
        }

        private void ActiveUser_Click(object sender, RoutedEventArgs e)
        {
            UserOnShift userOnShift = new UserOnShift();
            userOnShift.ShowDialog();
        }
    }
}
