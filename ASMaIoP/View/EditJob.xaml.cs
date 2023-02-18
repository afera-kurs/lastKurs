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
using ASMaIoP.View;
using ASMaIoP.ViewModel;

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для EditJob.xaml
    /// </summary>
    public partial class EditJob : Window
    {
        EditJobVM vm;

        internal EditJob(JobsListVM.RoleColumn column)
        {
            InitializeComponent();
            vm = new EditJobVM(column);
            DataContext = vm;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            vm.Apply(roleComboBox.SelectedIndex+1);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            vm.DeleteRole();
        }
    }
}
