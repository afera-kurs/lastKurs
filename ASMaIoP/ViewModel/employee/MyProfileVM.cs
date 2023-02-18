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
    internal class MyProfileVM : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = $"имя: {value}";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private string surname;

        public string Surname
        {
            get => surname;
            set
            {
                surname = $"фамилия: {value}";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }

        private string roleTitle;

        public string RoleTitle
        {
            get => roleTitle;
            set
            {
                roleTitle = $"роль: {value}";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoleTitle)));
            }
        }

        public MyProfileVM(ProfileData profile)
        {
            Name = profile.name;
            Surname = profile.surname;
            RoleTitle = profile.roleTitle;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
