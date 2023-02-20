using ASMaIoP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Threading;
using System.Windows.Controls;

namespace ASMaIoP.ViewModel
{
    internal class EmployeeInformationVM : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private string address;
        private string phone;

        private string roleTitle;
        private string cardId;

        public string Name
        {
            get => name;
            set  
            {
                name = value;
               
            }
        }

        public string edit_Name
        {
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(edit_Name)));
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                
            }
        }

        public string edit_Surname
        {
            set
            {
                surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(edit_Surname)));
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                
            }
        }

        public string edit_Address
        { 
            set
            {
                surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(edit_Address)));
            }
        }


        public string Phone
        {
            get => phone;
            set
            {
                phone = value;   
            }
        }


        public string edit_Phone
        {
            set
            {
                phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(edit_Phone)));
            }
        }

        public string RoleTitle
        {
            get => roleTitle;
            set
            {
                roleTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTitle)));
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

        EmployeeData Data;
        ProfileData prof;
        Mutex locker;

        internal EmployeeInformationVM(ProfileData prof, EmployeeData data, Mutex locker)
        {
            this.locker = locker;
            this.prof = prof;
            Data = data;
            Name = data.name;
            Surname = data.surname;
            Address = data.address;
            Phone = data.phoneNumber;
            RoleTitle = data.roleTitle;
            CardId = data.cardId;

            databaseInterface = DatabaseFactory.CreateInterface();
        }

        List<RoleData> roles;
        DataTable dt;
        public void BindRolesCombobox(ComboBox comboBox)
        {
            comboBox.Dispatcher.Invoke(() =>
            {
                comboBox.DataContext = dt;
                comboBox.DisplayMemberPath = dt.Columns[0].ToString();
            });
        }

        DatabaseInterface databaseInterface;

        public void Apply(int RoleId, string NewRoleTitle)
        {
            try
            {
                if(RoleId != Data.roleId)
                {
                    //databaseInterface.HistoryCreate(prof.id.ToString(), Data.EmployeeId.ToString(), Name, DateTime.Now, $"Переведен с должности {Data.roleTitle} на {NewRoleTitle}");
                }

                databaseInterface.EditEmploy(Data.EmployeeId, Name, Surname, Address, Phone, RoleId, CardId);
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                Utils.CloseApplication();
            }
        }

        public void Apply()
        {
            try
            {
                databaseInterface.EditEmploy(Data.EmployeeId, Name, Surname, Address, Phone, Data.roleId, CardId);
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                Utils.CloseApplication();
            }
        }

        public void UpdateViewModelData()
        {
            try
            {
                roles = new List<RoleData>();
                databaseInterface.GetRolesData(roles);
                dt = new DataTable();
                dt.Columns.Add("roleTitle");
                dt.Columns.Add("roleId");
                dt.Columns.Add("roleLevel");

                foreach (RoleData role in roles)
                {
                    DataRow dr = dt.NewRow();
                    dr["roleTitle"] = role.title;
                    dr["roleId"] = role.id;
                    dr["roleLevel"] = role.level;

                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                Utils.CloseApplication();
            }
        }

        public void DeleteUser()
        {
            databaseInterface.CastToVoidEmployee(Data);
        }

        public void HireUser()
        {
            databaseInterface.ChangeUserStatusUser(Data.EmployeeId, 1);
        }

        public void UpdateCardId(string newCi)
        {
            this.CardId = newCi;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
