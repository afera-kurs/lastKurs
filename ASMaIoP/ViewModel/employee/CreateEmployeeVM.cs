using ASMaIoP.Model;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Controls;

namespace ASMaIoP.ViewModel
{
    internal class CreateEmployeeVM : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private string address;
        private string phoneNumber;
        private string cardId;
        private string login;
        private string password;
        private DateTime firstWorkDay;
        ProfileData prof;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PhoneNumber)));
            }
        }

        public string CardId
        {
            get => cardId;
            set
            {
                cardId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CardId)));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        public DateTime FirstWorkDay
        {
            get => firstWorkDay;
            set
            {
                firstWorkDay = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstWorkDay)));
            }
        }

        DatabaseInterface databaseInterface;

        public CreateEmployeeVM(ProfileData prof)
        {
            databaseInterface = DatabaseFactory.CreateInterface();
            this.prof = prof;
        }

        List<RoleData> roles;

        public void BindRolesCombobox(ComboBox comboBox)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("roleTitle");
            dt.Columns.Add("roleId");
            dt.Columns.Add("roleLevel");

            foreach(RoleData role in roles)
            {
                DataRow dr = dt.NewRow();
                dr["roleTitle"] = role.title;
                dr["roleId"] = role.id;
                dr["roleLevel"] = role.level;

                dt.Rows.Add(dr);
            }

            comboBox.Dispatcher.Invoke(() =>
            {
                comboBox.DataContext = dt;
                comboBox.DisplayMemberPath = dt.Columns[0].ToString();
            });
        }

        public void UpdateViewModelData()
        {
            try
            {
                roles = new List<RoleData>();
                databaseInterface.GetRolesData(roles);
            }
            catch(Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                Utils.CloseApplication();
            }
        }

        public bool CreateUser(int roleId)
        {
            try
            {
                databaseInterface.AddEmployee(Name, Surname, Address, PhoneNumber, roleId, Login, Password, CardId, FirstWorkDay, prof);
            }
            catch(Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                return false;
            }

            return true;
        }

        public void UpdateCardId(string id)
        {
            CardId = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
