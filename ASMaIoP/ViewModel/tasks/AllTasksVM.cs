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


namespace ASMaIoP.ViewModel.tasks
{
    public class TaskColumView
    {
        public string Id;
        public string OwnerId;
        public string OwnerFullName { get; set; }
        public string Description { get; set; }
        public string TasksState { get; set; }
        public string TasksStatus { get; set; }
        public string LastDay { get; set; }
        public string TaskTitle { get; set; }
    }

    public class AllTasksVM
    {

        List<TaskBDInfo> tasks;
        DatabaseInterface bd;
        public AllTasksVM()
        {
            bd = DatabaseFactory.CreateInterface();
        }
        
        public void LoadData()
        {
            tasks = new List<TaskBDInfo>();
            bd.LoadAllTask(tasks);
        }

        public void LoadData(int employeeID)
        {
            tasks = new List<TaskBDInfo>();

            List<TaskBDFullInfo> rawTasks = new List<TaskBDFullInfo>();
            bd.LoadFullTasksForEmployee(rawTasks, employeeID);
            foreach(var t in rawTasks)
            {
                TaskBDInfo v = new TaskBDInfo();
                v.id = t.id;
                v.description = t.description;
                v.tasksStatus = t.tasksStatus;
                v.taskTitle = t.taskTitle;
                v.ownerFullName = t.ownerFullName;
                v.lastDay = t.lastDay;
                v.description = t.description;
                v.tasksState = t.tasksState;

                tasks.Add(v);
            }
            
        }


        public List<TaskColumView> GetTasksView()
        {
            List<TaskColumView> tmp = new List<TaskColumView>();

            foreach (TaskBDInfo t in tasks)
            {
                TaskColumView v = new TaskColumView();
                v.Id = t.id;
                v.TaskTitle = t.taskTitle;
                v.TasksStatus = t.tasksStatus;
                v.OwnerFullName = t.ownerFullName;
                v.LastDay = t.lastDay;
                v.Description = t.description;
                v.TasksState = t.tasksState;

                tmp.Add(v);
            }

            return tmp;
        }
    }
}
