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
            int docId = db.DocsCreate(0, prof.id, data.EmployeeId, DateTime.Now.Date, DateDism.SelectedDate.Value, descght.Text, descghtDate.Text);
            db.HistroyCreateNew(DateTime.Now.Date, descght.Text, docId);
            db.ChangeUserStatusUser(data.EmployeeId, 0);
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            employee.Close();
        }
    }
}
