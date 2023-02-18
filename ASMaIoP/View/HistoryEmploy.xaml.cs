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
using ASMaIoP.ViewModel;
using ASMaIoP.Model;

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для HistoryEmploy.xaml
    /// </summary>
    public partial class HistoryEmploy : Window
    {
        HistoryEmployeeVM vm;

        public HistoryEmploy(EmployeeData data)
        {
            InitializeComponent();
            vm = new HistoryEmployeeVM(data);
            vm.LoadData();
            HistoryDataGrid.ItemsSource = null;
            HistoryDataGrid.ItemsSource = vm.GetData();
        }

        private void HistoryDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                      e.OriginalSource as DependencyObject) as DataGridRow;
            var item = (HistoryEmployeeVM.HistoryRow)HistoryDataGrid.SelectedItem;
        }
    }
}
