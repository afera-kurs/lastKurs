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
using System.Threading;

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для HistoryEmploy.xaml
    /// </summary>
    public partial class HistoryEmploy : Window
    {
        HistoryEmployeeVM vm;
        Thread loadDocs;
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
            if (HistoryDataGrid.SelectedItem != null)
            {
                var item = (HistoryVM.HistoryRow)HistoryDataGrid.SelectedItem;
                if (item.DocsID != null)
                {
                    loadDocs = new Thread(() =>
                    {
                        DocumentHelper helper = null;
                        switch (item.doc.type)
                        {
                            case 0:
                                {
                                    helper = new DocumentHelper(Properties.Resources.DismissedEmployee);
                                    helper.Replace("нумерок", item.doc.id.ToString());
                                    helper.Replace("блинбдата", item.Date);
                                    helper.Replace("день", item.doc.firstDay.Day.ToString());
                                    helper.Replace("месяц", item.doc.firstDay.Month.ToString());
                                    helper.Replace("год", item.doc.firstDay.Year.ToString());
                                    helper.Replace("День", item.doc.secondDay.Day.ToString());
                                    helper.Replace("Месяц", item.doc.secondDay.Month.ToString());
                                    helper.Replace("Год", item.doc.secondDay.Year.ToString());
                                    helper.Replace("ФИО", item.Name);
                                    helper.Replace("табель", item.EmployeeID.ToString());
                                    helper.Replace("РОЛЬ", item.employee.roleTitle);
                                    helper.Replace("Причина", item.doc.descFirst);
                                    helper.Replace("краб", item.doc.descSecond);
                                    helper.Replace("трудовой", $"{item.employee.id}{item.employee.employeeWordDay.Year}-{item.employee.employeeWordDay.Day}");
                                    break;
                                }
                            case 1:
                                {
                                    //helper = new DocumentHelper(Properties.Resources.CreateEmploye);
                                    break;
                                }
                            case 2:
                                {
                                    //helper = new DocumentHelper(Properties.Resources.CreateEmploye);
                                    break;
                                }

                        }
                        helper.Save("test.docx");

                    });
                    loadDocs.Start();
                }
            }
        }
    }
}
