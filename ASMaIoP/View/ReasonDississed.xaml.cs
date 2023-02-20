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
using ASMaIoP.Model;
using ASMaIoP.View.Pages;
using ASMaIoP.ViewModel;
using System.IO;
using Microsoft.Win32;

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для ReasonDississed.xaml
    /// </summary>
    public partial class ReasonDississed : Window
    {
        EmployeeInformation employee;
        ProfileData prof;
        DatabaseInterface db;
        EmployeeData data;
        DocumentHelper helper;
        public ReasonDississed(EmployeeInformation emp, ProfileData prof, EmployeeData data)
        {
            InitializeComponent();
            this.prof = prof;
            db = DatabaseFactory.CreateInterface();
            this.data = data;
            employee = emp; 
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            //int docId = db.DocsCreate(0, prof.id, data.EmployeeId, DateTime.Now.Date, DateDism.SelectedDate.Value, descght.Text, descghtDate.Text);
            int docID = db.DocsCreate(0, data.firstDay, DateDism.SelectedDate.Value, descght.Text, descghtDate.Text);
            db.HistroyCreateNew(DateTime.Now.Date, descght.Text, docID, prof.id, data.EmployeeId);
            helper = new DocumentHelper(Properties.Resources.DismissedEmployee);
            helper.Replace("нумерок", docID.ToString());
            helper.Replace("блинбдата", DateTime.Now.Date.ToString());
            helper.Replace("день", data.firstDay.Day.ToString());
            helper.Replace("месяц", data.firstDay.Month.ToString());
            helper.Replace("год", data.firstDay.Year.ToString());
            helper.Replace("День", DateDism.SelectedDate.Value.Day.ToString());
            helper.Replace("Месяц", DateDism.SelectedDate.Value.Month.ToString());
            helper.Replace("Год", DateDism.SelectedDate.Value.Year.ToString());
            helper.Replace("ФИО", $"{data.name} {data.name} {data.patName}");
            helper.Replace("табель", data.EmployeeId.ToString());
            helper.Replace("РОЛЬ", data.roleTitle);
            helper.Replace("Причина", descght.Text);
            helper.Replace("краб", descghtDate.Text);
            helper.Replace("трудовой", $"{data.EmployeeId}{data.firstDay.Year}-{data.firstDay.Day}");

            var fileContent = string.Empty;
            var filePath = string.Empty;

            SaveFileDialog openFileDialog = new SaveFileDialog();
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                bool? result = openFileDialog.ShowDialog();

                if(result == null)
                {
                    
                }
                else if (!result.Value)
                {
                    
                }
                else if (result.Value)
                {
                    helper.Save(openFileDialog.FileName);
                }
            }
            
            db.ChangeUserStatusUser(data.EmployeeId, 0);
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            employee.Close();
        }
    }
}
