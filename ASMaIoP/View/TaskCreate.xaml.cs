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
    /// Логика взаимодействия для TaskCreate.xaml
    /// </summary>
    public partial class TaskCreate : Window
    {

        TaskCreateVM vm;
        ProfileData profile;
        public TaskCreate(ProfileData profile)
        {
            this.profile = profile;

            InitializeComponent();
            res = new List<TaskExecutantRow>();
            vm = new TaskCreateVM();
        }

        List<TaskExecutantRow> res;
        List<string> ids;

        private void ChangeExecut_Click(object sender, RoutedEventArgs e)
        {
            SelectExcucant excucant = new SelectExcucant(res);
            excucant.ShowDialog();
            res = excucant.GetRes();

            ids = new List<string>();

            List<string> names = new List<string>();

            foreach(TaskExecutantRow r in res)
            {
                names.Add(r.employeeName);
                ids.Add(r.employeeID.ToString());
            }
            ListExecut.ItemsSource = null;
            ListExecut.ItemsSource = names;

        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            DateTime? nullD = LastDay.SelectedDate;
            DateTime d = nullD.Value;
            bool a = Quickly.IsChecked.Value;
            int id = vm.CreateTask(TitleTask.Text, profile.id.ToString(), Description.Text, d, a.ToString());
            vm.CreateExecut(ids, $"{id}");
        }
    }
}
