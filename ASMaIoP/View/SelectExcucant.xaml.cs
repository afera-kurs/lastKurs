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

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для SelectExcucant.xaml
    /// </summary>
    public partial class SelectExcucant : Window
    {
        TaskExecutantVM vm;
        public SelectExcucant(List<TaskExecutantRow> employeeSelected)
        {
            vm = new TaskExecutantVM(employeeSelected);
            InitializeComponent();
            DataContext = vm;
            vm.LoadUsersData();
            SelectedExcecut.ItemsSource = null;
            SelectedExcecut.ItemsSource = vm.TranslateUserIntoView();
        }

        public List<TaskExecutantRow> GetRes()
        {
            return vm.PrepairResult();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            vm.PrepairResult();
            this.Close();
        }
    }
}
