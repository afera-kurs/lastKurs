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

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для UserOnShift.xaml
    /// </summary>
    public partial class UserOnShift : Window
    {

        public struct UserInfo
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string RoleTitle { get; set; }
        }

        DatabaseInterface database;

        public UserOnShift()
        {
            InitializeComponent();
            database = DatabaseFactory.CreateInterface();
            List<EmployeeData> emps = new List<EmployeeData>();
            database.LoadUserOnWorkShift(emps, 0);

            List<UserInfo> users = new List<UserInfo>();  

            foreach(EmployeeData emp in emps)
            {
                users.Add(new UserInfo { Name = emp.name, Surname = emp.surname, RoleTitle = emp.roleTitle });
            }

            EmployeeDataGrid.ItemsSource = null;
            EmployeeDataGrid.ItemsSource = users;
        }
    }
}
