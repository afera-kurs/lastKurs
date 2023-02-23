using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using ASMaIoP.Model;
using System.Windows.Media.Imaging;
using Windows.ApplicationModel.Email;
using System.IO;
using System.Windows.Threading;
using System.Drawing;
using System.Windows.Media.Animation;

namespace ASMaIoP.ViewModel.tasks
{
    internal class TaskEditVM : INotifyPropertyChanged
    {
        TaskColumView data;

        string oldTaskTitle;
        public string OldTaskTitle
        {
            get => oldTaskTitle;
            set
            {
                oldTaskTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OldTaskTitle)));
            }
        }
        string newTaskTitle;
        public string NewTaskTitle
        {
            get => newTaskTitle;
            set
            {
                newTaskTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewTaskTitle)));
            }
        }

        string taskDesc;
        public string TaskDesc
        {
            get => taskDesc;
            set
            {
                taskDesc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaskDesc)));
            }
        }

        string oldLastDate;
        public string OldLastDate
        {
            get => oldLastDate;
            set
            {
                oldLastDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OldLastDate)));
            }
        }
        string newLastDate;
        public string NewLastDate
        {
            get => newLastDate;
            set
            {
                newLastDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewLastDate)));
            }
        }
        string oldState;
        public string OldState
        {
            get => oldState;
            set
            {
                switch(int.Parse(value))
                {
                    case 1:
                        oldState = "Выполнен";
                        break;
                    case 2:
                        oldState = "Ожидает";
                        break;
                    case 3:
                        oldState = "Создан";
                        break;
                    case 4:
                        oldState = "Провален";
                        break;
                }
            }
        }
        public TaskEditVM(TaskColumView tasks)
        {
            OldTaskTitle= tasks.TaskTitle;
            TaskDesc = tasks.Description;
            OldLastDate = tasks.LastDay;
            OldState = tasks.TasksState;
            data= tasks;
            db = DatabaseFactory.CreateInterface();
        }

        DatabaseInterface db;
        List<EmployeeData> taskEmps;

        public void LoadTaskUsers()
        {
            taskEmps = new List<EmployeeData>();
            db.LoadEmployees(taskEmps, int.Parse(data.Id));
        }

        public List<string> GetViewNames()
        {
            List<string> res = new List<string>();

            foreach(EmployeeData e in taskEmps)
            {
                string name = $"{e.name} {e.surname}";
                res.Add(name);
            }

            return res;
        }

        public List<TaskExecutantRow> GetSelectedEmployes()
        {
            List<TaskExecutantRow> res = new List<TaskExecutantRow>();

            foreach (EmployeeData e in taskEmps)
            {
                TaskExecutantRow row = new TaskExecutantRow();
                row.employeeID = e.EmployeeId;
                row.employeeName = $"{e.name} {e.surname}";
                res.Add(row);
            }

            return res;
        }

        public void updateTask(List<string> ids, string taskTitle, string desck, DateTime lastDay, string status, string state, int taskId)
        {
            db.RemoveExecutant(taskId.ToString());
            if(ids == null)
            {
                ids = new List<string>();
                foreach (EmployeeData emp in taskEmps)
                {
                    ids.Add(emp.EmployeeId.ToString());
                }
            }
            db.UpdateTask(taskId, desck, lastDay, taskTitle, status, state);
            db.AddExecutant(ids.ToArray(), taskId.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
