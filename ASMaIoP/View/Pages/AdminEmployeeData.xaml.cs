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
using static ASMaIoP.ViewModel.AdminEmployeeDataVM;

namespace ASMaIoP.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Tabel.xaml
    /// </summary>
    public partial class AdminEmployeeData : Page
    {
        AdminEmployeeDataVM vm;

        Task loadDataGrid;
        Action loadDataGridDelegate;

        public ProfileData prof;

        public AdminEmployeeData(ProfileData prof)
        {
            InitializeComponent();
            vm = new AdminEmployeeDataVM(prof);
            DataContext = vm;

            this.prof = prof;

            loadDataGridDelegate = () =>
            {
                EmployeeDataGrid.Dispatcher.Invoke(() =>
                {
                    vm.LoadDataToDataGrid((tabel) =>
                    {
                        EmployeeDataGrid.ItemsSource = null;
                        EmployeeDataGrid.ItemsSource = tabel;
                    });
                });
            };

            UpdateData();
        }

        private void UpdateData()
        {
            loadDataGrid = new Task(loadDataGridDelegate);
            loadDataGrid.Start();
        }

        private void CreateNewUser_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee employee = new CreateEmployee(prof);
            employee.ShowDialog();
            vm.LoadDataToDataGrid((tabel) =>
            {
                EmployeeDataGrid.ItemsSource = null;
                EmployeeDataGrid.ItemsSource = tabel;
            });
        }

        private void EmployeeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                       e.OriginalSource as DependencyObject) as DataGridRow;

            if (     !Guard.EmployeeEditPanel ) return;

            var item = (EmployeeDataColumn)EmployeeDataGrid.SelectedItem;
            ShowEmployeeInfoWindow(item);
            vm.LoadDataToDataGrid((tabel) =>
            {
                EmployeeDataGrid.ItemsSource = null;
                EmployeeDataGrid.ItemsSource = tabel;
            });
        }

        internal void ShowEmployeeInfoWindow(EmployeeDataColumn data)
        {
            vm.ShowWindow(data);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateNewUser.Visibility =
                Guard.EmployeeEditPanel
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ActiveUser_Click(object sender, RoutedEventArgs e)
        {
            UserOnShift userOnShift = new UserOnShift();
            userOnShift.Show();
        }
    }
}
