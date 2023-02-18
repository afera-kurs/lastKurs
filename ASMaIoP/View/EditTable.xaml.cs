using ASMaIoP.ViewModel;
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

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для EditTable.xaml
    /// </summary>
    public partial class EditTable : Window
    {
        EditTableVM vm;
        string dayAndWorker;
        string tableID;
        string workerID;
        string date;

        Action updateTable;

        public EditTable(string mes, int year, int month, Action updateTable)
        {
            this.updateTable = updateTable;
            InitializeComponent();
            dayAndWorker = mes;
            SplitStr();
            vm = new EditTableVM(workerID, year, month, date, tableID);
            DataContext = vm;
        }

        private void SplitStr()
        {
            string[] words = dayAndWorker.Split('|');
            tableID = words[0];
            workerID = words[1];
            date = words[2];
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            ComboBoxItem typeItem = (ComboBoxItem)markComboBox.SelectedItem;
            string value = typeItem.Content.ToString();
            this.updateTable();
            vm.SaveData(value);
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
