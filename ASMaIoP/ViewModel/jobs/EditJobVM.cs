using ASMaIoP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace ASMaIoP.ViewModel
{
    internal class EditJobVM : INotifyPropertyChanged
    {
        private string title;
        private int level;
        public string Title 
        {   get => title; 
            set
            {
                title = value;
            }
        }
        public int Level
        {
            get => level;
            set
            {
                level = value;
            }
        }

        public string edit_Title
        {
            set
            {
                title = value;
            }
        }
        private JobsListVM.RoleColumn data;

        DatabaseInterface databaseInterface;

        public EditJobVM(JobsListVM.RoleColumn column)
        {
            data = column;

            Title = data.RoleTitle;
            Level = data.Level;

            databaseInterface = DatabaseFactory.CreateInterface();
        }
        public void Apply(int nAccessLevel)
        {
            try
            {
                databaseInterface.ChageRoleData(data.Id, Title, nAccessLevel);
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                Utils.CloseApplication();
            }
        }

        public void DeleteRole()
        {
            if (data.EmployeeCount > 0)
            {
                DataLog.Log.AddLog(new Event("Ошибка не возможно удалить данную должность так как есть сотрудники занимающие ее", EventPriority.Error));
            }

            try
            {
                databaseInterface.CastToVoidRole(data.Id);
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                Utils.CloseApplication();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
