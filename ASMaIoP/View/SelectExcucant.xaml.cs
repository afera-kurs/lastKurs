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
    /// Логика взаимодействия для SelectExcucant.xaml
    /// </summary>
    public partial class SelectExcucant : Window
    {
        TaskExecutantVM vm;
        ProfileData prof;
        public SelectExcucant(List<TaskExecutantRow> employeeSelected, ProfileData prof)
        {
            this.prof = prof;
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

        private void SelectedExcecut_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var r = e.Row;
            //var prod = 

            if(r.DataContext is TaskExecutantVMRow prod)
            {
                if(prof.accessLevel <= 3)
                {
                    switch (prof.accessLevel)
                    {
                        case 1:
                            {
                                SelectedExcecut.IsEnabled = false;
                                break;
                            }
                        case 2:
                            {
                                if(prod.level >= prof.accessLevel)
                                {
                                    r.IsEnabled = false;
                                }
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                        default:
                            SelectedExcecut.IsEnabled = false;
                            break;
                    }
                }
            }

        }
    }
}
    