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
using System.Threading;
using Microsoft.Win32;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Xml.Linq;

namespace ASMaIoP.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
        HistoryVM vm;
        Grid loading;
        Thread loadThread;
        Thread loadDocs;
        public History(ProfileData profile, Grid loading)
        {
            InitializeComponent();
            vm = new HistoryVM(profile);
            this.loading = loading;
        }

        private void HistoryDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(HistoryDataGrid.SelectedItem != null)
            {
                var item = (HistoryVM.HistoryRow)HistoryDataGrid.SelectedItem;
                if(item.DocsID != null)
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
                                    helper.Replace("трудовой", $"{item.employee.id}{item.employee.employeeWordDay.Year}-{item.employee.employeeWordDay.Day}") ;
                                    break;
                                }
                            case 1:
                                {
                                    helper = new DocumentHelper(Properties.Resources.TrudDocx);
                                    helper.Replace("ДТСЕЙЧАС", item.Date);
                                    helper.Replace("ИМЯПОЛНОЕ", item.Name);
                                    helper.Replace("нДГ", $"{item.employee.id}{item.employee.employeeWordDay.Year}-{item.employee.employeeWordDay.Day}");
                                    helper.Replace("ИМЯРОЛИ", item.employee.roleTitle);
                                    helper.Replace("ДАТАПЕРВЫЙД", item.doc.secondDay.ToString());
                                    helper.Replace("ДАТАПЕРВЫЙДК", item.doc.secondDay.AddDays(7).ToString());
                                    helper.Replace("АДРЕСС", item.employee.adress);
                                    break;
                                }
                            case 2:
                                {
                                    //helper = new DocumentHelper(Properties.Resources.CreateEmploye);
                                    break;
                                }
                                
                        }
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.Filter = "Word Documents|*.docx";
                        dialog.FilterIndex = 2;
                        dialog.RestoreDirectory = true;
                        bool? some = dialog.ShowDialog();
                        if (some != null && some.Value)
                        {
                            helper.Save(dialog.FileName);
                        }
                                              
                    });
                    loadDocs.Start();
                }
            }
        }


        object locker = new object();

        public void UpdateData()
        {
            loading.IsEnabled = true;
            loading.Visibility = Visibility.Visible;
            loadThread = new Thread(() =>
            {
                lock(locker)
                {
                    vm.LoadData();
                    HistoryDataGrid.Dispatcher.Invoke(() =>
                    {
                        HistoryDataGrid.ItemsSource = null;
                        HistoryDataGrid.ItemsSource = vm.GetData();
                        loading.IsEnabled = false;
                        loading.Visibility = Visibility.Collapsed;
                    });
                }
            });
            loadThread.Start();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
    }
}
