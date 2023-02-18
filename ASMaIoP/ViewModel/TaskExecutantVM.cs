using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMaIoP.Model;

namespace ASMaIoP.ViewModel
{
    public struct TaskExecutantRow
    {
        public int employeeID;
        public string employeeName;
    }

    public struct TaskExecutantVMRow
    {
        public string Name { get; set; }
        public string RoleTitle { get; set; }

        public bool isSelected;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;

                TaskExecutantVMRow replace = new TaskExecutantVMRow();
                replace.Name = this.Name;
                replace.RoleTitle = this.RoleTitle;
                replace.isSelected = isSelected;
                replace.Id = Id;
                parentVM.store[index] = replace;
            }
        }
        public int Id;

        public int index;
        public TaskExecutantVM parentVM;
    }

    public class TaskExecutantVM
    {
        DatabaseInterface db;
        
        public TaskExecutantVM(int TaskId)
        {
            db = DatabaseFactory.CreateInterface();
        }


        List<TaskExecutantRow> Selected;

        public TaskExecutantVM(List<TaskExecutantRow> employeeSelected)
        {
            this.Selected = employeeSelected;
            db = DatabaseFactory.CreateInterface();
        }

        List<EmployeeData> employees;

        public void LoadUsersData()
        {
            employees = new List<EmployeeData>();
            db.LoadEmployeeData(employees);
        }

        public List<TaskExecutantVMRow> store;

        public List<TaskExecutantVMRow> TranslateUserIntoView()
        {
            store = new List<TaskExecutantVMRow>();
            int i = 0;
            foreach (EmployeeData emp in employees)
            {
                TaskExecutantVMRow r = new TaskExecutantVMRow();
                r.RoleTitle = emp.roleTitle;
                r.Name = emp.name+" "+emp.surname;
                r.Id = emp.EmployeeId;
                r.index = i;
                r.parentVM = this;
                
                if(Selected != null)
                {
                    foreach (var s in Selected)
                    {
                        if(s.employeeID == r.Id)
                        {
                            r.isSelected = true;
                        }
                    }
                }

                store.Add(r);
                i++;
            }

            return store;
        }

        public List<TaskExecutantRow> PrepairResult()
        {
            List<TaskExecutantRow> res = new List<TaskExecutantRow>();
            foreach (TaskExecutantVMRow r in store)
            {
                if(r.IsSelected)
                {
                    TaskExecutantRow resRow = new TaskExecutantRow();
                    resRow.employeeID = r.Id;
                    resRow.employeeName = r.Name;
                    res.Add(resRow);
                }
            }

            return res;
        }

    }
}
