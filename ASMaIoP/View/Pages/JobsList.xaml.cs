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

namespace ASMaIoP.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для JobsList.xaml
    /// </summary>
    public partial class JobsList : Page
    {
        JobsListVM vm;
        Grid AnimationGrid;

        public JobsList(Grid AnimationGrid)
        {
            this.AnimationGrid = AnimationGrid;
            InitializeComponent();
        }

        private void CreateJob_Click(object sender, RoutedEventArgs e)
        {
            CreateJob wnd = new CreateJob();
            wnd.ShowDialog();
        }

        private void JobsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                       e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null && !Guard.RoleEditPanel) return;

            JobsListVM.RoleColumn column = (JobsListVM.RoleColumn)JobsDataGrid.SelectedItem;
            
            EditJob job = new EditJob(column);
            job.ShowDialog();
        }

        Task pageLoad = null;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {   
            CreateJob.Visibility =
            Guard.RoleEditPanel
            ? Visibility.Visible : Visibility.Collapsed;
            pageLoad = Task.Factory.StartNew(() =>
            {
                vm = new JobsListVM();
                vm.UpdateRoles();
                
                vm.LoadDataToDataGrid((roles) =>
                {
                    JobsDataGrid.Dispatcher.Invoke(() =>
                    {
                        JobsDataGrid.ItemsSource = null;
                        JobsDataGrid.ItemsSource = roles;
                        AnimationGrid.IsEnabled = false;
                        AnimationGrid.Visibility = Visibility.Collapsed;
                    });
                });
            });
        }

        
    }
}
