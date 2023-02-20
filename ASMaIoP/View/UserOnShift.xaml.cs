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
using System.Threading;
using ASMaIoP.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using Org.BouncyCastle.Asn1.X509;

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для UserOnShift.xaml
    /// </summary>
    public partial class UserOnShift : Window
    {
        List<EmployeeData> emps;
        List<UserInfo> users;
        public struct UserInfo
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string RoleTitle { get; set; }
        }

        DatabaseInterface database;

        Thread thread;

        public UserOnShift()
        {

            InitializeComponent();
            emps = new List<EmployeeData>();
            users = new List<UserInfo>();  
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            thread = new Thread(() =>
            {
                database = DatabaseFactory.CreateInterface();
                database.LoadUserOnWorkShift(emps, 0);
                foreach (EmployeeData emp in emps)
                {
                    users.Add(new UserInfo { Name = emp.name, Surname = emp.surname, RoleTitle = emp.roleTitle });
                }
                EmployeeDataGrid.Dispatcher.Invoke(() =>
                {
                    EmployeeDataGrid.ItemsSource = null;
                    EmployeeDataGrid.ItemsSource = users;
                });
            });

            thread.Start();
        }
    }
}
