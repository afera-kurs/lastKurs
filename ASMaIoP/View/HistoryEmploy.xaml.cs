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
                                    helper = new DocumentHelper(Properties.Resources.TrudDocx);
                                    helper.Replace("ДТСЕЙЧАС", item.Date);
                                    helper.Replace("ИМЯПОЛНОЕ", item.Name);
                                    helper.Replace("ИМЯПОЛНОЕ", item.Name);
                                    helper.Replace("нДГ", $"{item.employee.id}{item.employee.employeeWordDay.Year}-{item.employee.employeeWordDay.Day}");
                                    helper.Replace("ИМЯРОЛИ", item.employee.roleTitle);
                                    helper.Replace("ДАТАПЕРВЫЙД", item.doc.secondDay.Date.ToString());
                                    helper.Replace("ДАТАПЕРВЫЙД", item.doc.secondDay.Date.ToString());
                                    helper.Replace("ДАТАПЕРВЫЙДК", item.doc.secondDay.Date.AddDays(7).ToString());
                                    helper.Replace("АДРЕСС", item.employee.adress);
                                    break;
                                }
                            case 2:
                                {
                                    helper = new DocumentHelper(Properties.Resources.Perevod);
                                    helper.Replace("ДАТАП", item.Date);
                                    helper.Replace("нРаб", item.EmployeeID.ToString());
                                    helper.Replace("ИМЯП", item.Name);
                                    helper.Replace("РОЛЬ", item.doc.descSecond);
                                    helper.Replace("ПРИЧИНА", item.doc.descFirst);
                                    helper.Replace("РОЛЬН", item.employee.roleTitle);
                                    helper.Replace("ДТ", item.doc.firstDay.Day.ToString());
                                    helper.Replace("МТ", item.doc.firstDay.Month.ToString());
                                    helper.Replace("ГТ", item.doc.firstDay.Year.ToString());
                                    helper.Replace("нТруд", $"{item.employee.id}{item.employee.employeeWordDay.Year}-{item.employee.employeeWordDay.Day}");
                                    break;
                                }
                            case 3:
                                {
                                    DatabaseInterface.FillTableType type = (DatabaseInterface.FillTableType)Convert.ToInt32(item.doc.descFirst);
                                    switch (type)
                                    {
                                        case DatabaseInterface.FillTableType.Weekend:
                                            {
                                                helper = new DocumentHelper(Properties.Resources.Weekend);
                                                helper.Replace("нДок", item.doc.id.ToString());
                                                helper.Replace("ДАТАС", item.Date);
                                                helper.Replace("ТБН", item.employee.id.ToString());
                                                helper.Replace("ИМЯП", $"{item.employee.name} {item.employee.surname} {item.employee.patName}");
                                                helper.Replace("РОЛЬ", item.employee.roleTitle);
                                                helper.Replace("ДА", "");
                                                helper.Replace("МА", "");
                                                helper.Replace("ГА", "");
                                                helper.Replace("ДАК", "");
                                                helper.Replace("МАК", "");
                                                helper.Replace("ГАК", "");
                                                helper.Replace("АДН", "");
                                                helper.Replace("МА", item.doc.firstDay.Day.ToString());
                                                helper.Replace("ГА", item.doc.firstDay.Day.ToString());
                                                helper.Replace("ДБ", item.doc.firstDay.Day.ToString());
                                                helper.Replace("МБ", item.doc.firstDay.Month.ToString());
                                                helper.Replace("ГБ", item.doc.firstDay.Year.ToString());
                                                helper.Replace("ДБК", item.doc.secondDay.Day.ToString());
                                                helper.Replace("МБК", item.doc.secondDay.Month.ToString());
                                                helper.Replace("ГБК", item.doc.secondDay.Year.ToString());
                                                helper.Replace("БДН", (item.doc.firstDay - item.doc.secondDay).TotalDays.ToString());
                                                break;
                                            }
                                        case DatabaseInterface.FillTableType.WeekendAge:
                                            {
                                                helper = new DocumentHelper(Properties.Resources.Weekend);
                                                helper.Replace("нДок", item.doc.id.ToString());
                                                helper.Replace("ДАТАС", item.Date);
                                                helper.Replace("ТБН", item.employee.id.ToString());
                                                helper.Replace("ИМЯП", $"{item.employee.name} {item.employee.surname} {item.employee.patName}");
                                                helper.Replace("РОЛЬ", item.employee.roleTitle);
                                                helper.Replace("ДА", item.doc.firstDay.Day.ToString());
                                                helper.Replace("МА", item.doc.firstDay.Month.ToString());
                                                helper.Replace("ГА", item.doc.firstDay.Year.ToString());
                                                helper.Replace("ДАК", item.doc.secondDay.Day.ToString());
                                                helper.Replace("МАК", item.doc.secondDay.Month.ToString());
                                                helper.Replace("ГАК", item.doc.secondDay.Year.ToString());
                                                helper.Replace("АДН", (item.doc.firstDay - item.doc.secondDay).TotalDays.ToString());
                                                helper.Replace("ДБ", "");
                                                helper.Replace("МБ", "");
                                                helper.Replace("ГБ", "");
                                                helper.Replace("ДБК", "");
                                                helper.Replace("МБК", "");
                                                helper.Replace("ГБК", "");
                                                helper.Replace("БДН", "");
                                                break;
                                            }
                                    }
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
