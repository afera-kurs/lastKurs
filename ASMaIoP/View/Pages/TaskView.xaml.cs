using ASMaIoP.Model;
using ASMaIoP.ViewModel;
using ASMaIoP.ViewModel.tasks;
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
using System.Threading;


namespace ASMaIoP.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для TaskView.xaml 
    /// </summary>
    public partial class TaskView : Page
    {
        AllTasksVM vm;
        TaskCreate taskCreate;
        ProfileData prof;
        EditTask task;
        DatabaseInterface bd;
        Grid loading;
        public TaskView(ProfileData prof, Grid loading)
        {
            this.loading = loading;
            InitializeComponent();
            this.prof = prof;
            vm = new AllTasksVM();
            DataContext = vm;
            bd = DatabaseFactory.CreateInterface();
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            taskCreate = new TaskCreate(prof);
            taskCreate.ShowDialog();
            UpdateData();
        }

        private void TaskDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                     e.OriginalSource as DependencyObject) as DataGridRow;

            if(TaskDataGrid.SelectedItem is TaskColumView item)
            {
                task = new EditTask(item, prof);
                task.ShowDialog();
                UpdateData();
            }
            
        }

        Thread thread;
        object locker = new object();

        private void UpdateData()
        {
            loading.IsEnabled = true;
            loading.Visibility = Visibility.Visible;
            thread = new Thread(() =>
            {
                lock(locker)
                {
                    if (prof.accessLevel > 3)
                    {
                        vm.LoadData();
                    }
                    else
                    {
                        vm.LoadData(prof.id);
                    }
                    TaskDataGrid.Dispatcher.Invoke(() =>
                    {
                        TaskDataGrid.ItemsSource = null;
                        TaskDataGrid.ItemsSource = vm.GetTasksView();
                        loading.Visibility = Visibility.Collapsed;
                        loading.IsEnabled = false;
                    });
                }
            });

            thread.Start();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();

        }
    }
}
