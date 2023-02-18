using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ASMaIoP.Model;

namespace ASMaIoP.ViewModel
{
    internal class CreateJobVM : INotifyPropertyChanged
    {
        private string title;
        private int level;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }
        public int Level
        {
            get => level;
            set
            {
                level = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Level)));
            }
        }

        DatabaseInterface databaseInterface;

        public CreateJobVM()
        {
            databaseInterface = DatabaseFactory.CreateInterface();
        }

        public void Create(int nLevel)
        {
            databaseInterface.AddRole(Title, nLevel);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
