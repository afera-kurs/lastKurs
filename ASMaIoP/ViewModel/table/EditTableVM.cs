using ASMaIoP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;

namespace ASMaIoP.ViewModel
{
    internal class EditTableVM : INotifyPropertyChanged
    {
        private string dateString;
        private string employeeName;
        private string tableID;

        public string DateString
        {
            get => dateString;
            set
            {
                dateString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateString)));
            }
        }

        public string WorkerName
        {
            get => employeeName;
            set
            {
                employeeName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorkerName)));
            }

        }

        DatabaseInterface databaseInterface;
        ProfileData data;

        public EditTableVM(string employeeID, int year, int month, string date, string tableID)
        {
            databaseInterface = DatabaseFactory.CreateInterface();
            data = databaseInterface.GetEmployeeData(Convert.ToInt32(employeeID));
            WorkerName = data.name + " " + data.surname;
            this.tableID = tableID;
            DateString = $"{year}-{month}-{(int.Parse(date) + 1).ToString()}";
        }

        public void SaveData(string mark)
        {
            if(tableID == "NULL")
            {
                databaseInterface.CreateTable(mark, data.id.ToString(), dateString);
            }
            if (char.IsDigit(tableID[0]))
            {        
                databaseInterface.UpdateTable(tableID, mark);
            }
            databaseInterface.  New(DateTime.Now.Date, $"Утсановил отметку {mark}, в табеле для работника {employeeName} за {DateString}" , null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
