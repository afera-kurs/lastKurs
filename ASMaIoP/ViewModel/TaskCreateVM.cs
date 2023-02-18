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

namespace ASMaIoP.ViewModel
{
    public class TaskCreateVM : INotifyPropertyChanged
    {

        DatabaseInterface db;

        private string roleTitle;
        public string RoleTitle
        {
            get => roleTitle;
            set
            {
                roleTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTitle)));
            }
        }

        public TaskCreateVM()
        {
            db = DatabaseFactory.CreateInterface();
        }


        public void CreateTask(string OwnerId, string Desc, DateTime LastDate, string StatusId)
        {
            int n = StatusId == "" ? 1 : 0;
            db.CreateTask(OwnerId, Desc, LastDate, n.ToString()); ;
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
