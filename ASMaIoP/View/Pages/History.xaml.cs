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
using ASMaIoP.Model;
using ASMaIoP.ViewModel;

namespace ASMaIoP.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
        HistoryVM vm;

        public History(ProfileData profile)
        {
            InitializeComponent();
            vm = new HistoryVM(profile);
            vm.LoadData();

            HistoryDataGrid.ItemsSource = null;
            HistoryDataGrid.ItemsSource = vm.GetData();
        }
    }
}
