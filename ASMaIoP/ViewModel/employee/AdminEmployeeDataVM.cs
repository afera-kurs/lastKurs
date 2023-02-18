using ASMaIoP.Model;
using ASMaIoP.View.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ASMaIoP.ViewModel
{
    internal class AdminEmployeeDataVM : INotifyPropertyChanged
    {
        public class EmployeeDataColumn
        {
            public int Id { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string RoleTitle { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string Status { get; set; }
        }

        DatabaseInterface databaseInterface;

        List<EmployeeData> employees;

        

        public  delegate void UpdateEmployeeDataGrid(List<EmployeeDataColumn> tabel);

        ProfileData prof;

        public AdminEmployeeDataVM(ProfileData prof)
        {
            this.prof = prof;
            databaseInterface = DatabaseFactory.CreateInterface();
            employees = new List<EmployeeData>();
            dataGridData = new List<EmployeeDataColumn>();
        }

        void UpdateData()
        {
            employees.Clear();
            databaseInterface.LoadEmployeeData(employees);
        }

        List<EmployeeDataColumn> dataGridData;
        public List<EmployeeDataColumn> DataGridData 
        {
            get => dataGridData;
        }

        public void ShowWindow(EmployeeDataColumn col)
        {
            EmployeeData employee = new EmployeeData();

            bool IsFindEmp = false;
            foreach(EmployeeData emp in employees)
            {
                if (emp.EmployeeId == col.Id)
                {
                    employee = emp;
                    IsFindEmp = true;
                    break;
                }
            }
            
            if(!IsFindEmp)
            {
                throw new Exception($"failed to find employee with id={col.Id} in local database!");
            }
            

            EmployeeInformation wnd = new EmployeeInformation(prof, employee);
            wnd.ShowDialog();
        }

        public void LoadDataToDataGrid(UpdateEmployeeDataGrid datagridUpdateLayout)
        {
            UpdateData();

            dataGridData.Clear();

            foreach (EmployeeData data in employees)
            {
                EmployeeDataColumn employeeDataColumn = new EmployeeDataColumn();

                employeeDataColumn.Id = data.EmployeeId;

                employeeDataColumn.Address = data.address;
                employeeDataColumn.PhoneNumber = data.phoneNumber;
                employeeDataColumn.Name = data.name;
                employeeDataColumn.RoleTitle = data.roleTitle;
                employeeDataColumn.Surname = data.surname;
                employeeDataColumn.Status = data.IsDissmissed ? "Работает" : "Уволен";

                dataGridData.Add(employeeDataColumn);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataGridData)));
            datagridUpdateLayout(dataGridData);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
