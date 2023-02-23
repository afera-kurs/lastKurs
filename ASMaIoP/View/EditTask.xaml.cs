using ASMaIoP.Model;
using ASMaIoP.ViewModel;
using ASMaIoP.ViewModel.tasks;
using Org.BouncyCastle.Crypto;
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
    /// Логика взаимодействия для EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        List<TaskExecutantRow> res;
        List<string> ids;
        TaskEditVM vm;
        TaskColumView tasks;

        ProfileData prof;
        public EditTask(TaskColumView tasks, ProfileData prof)
        {
            this.prof = prof;
            InitializeComponent();
            vm = new TaskEditVM(tasks);
            DataContext = vm;
            this.tasks = tasks;
            vm.LoadTaskUsers();

            Quickly.IsChecked = Convert.ToBoolean(int.Parse(tasks.TasksStatus)); 
            ListExecut.ItemsSource = null;
            ListExecut.ItemsSource = vm.GetViewNames();
        }

        private void ChangeExecut_Click(object sender, RoutedEventArgs e)
        {
            SelectExcucant excucant = new SelectExcucant(vm.GetSelectedEmployes(), prof);
            excucant.ShowDialog();
            res = excucant.GetRes();

            ids = new List<string>();

            List<string> names = new List<string>();

            foreach (TaskExecutantRow r in res)
            {
                names.Add(r.employeeName);
                ids.Add(r.employeeID.ToString());
            }

            ListExecut.ItemsSource = null;
            ListExecut.ItemsSource = names;

        }

        private void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            DateTime lastDay = LastDay.SelectedDate == null ? DateTime.Parse(tasks.LastDay) : LastDay.SelectedDate.Value;
            string desc = Description.Text;
            string title = TitleTask.Text == "" ? tasks.TaskTitle : TitleTask.Text;
            bool Quickly = this.Quickly.IsChecked == null ? false : this.Quickly.IsChecked.Value;
            string state = ReturnState();
            vm.updateTask(ids, title, desc, lastDay, Quickly.ToString(), state, int.Parse(tasks.Id));
            this.Close();
        }

        private string ReturnState()
        {
            if(State.SelectedIndex + 1 == 0)
            {
                switch (StateTK.Text)
                {
                    case "Выполнен":
                        return "1";
                    case "Ожидает":
                        return "2";
                    case "Создан":
                        return "3";
                    case "Провален":
                        return "4";
                }
            }
            return (State.SelectedIndex + 1).ToString();
        }
    }
}
