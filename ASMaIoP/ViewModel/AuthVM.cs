using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media;
using ASMaIoP.Model;

namespace ASMaIoP.ViewModel
{
    internal class AuthVM : INotifyPropertyChanged
    {
        DatabaseInterface db;
        static Task AuthTask;
        public void Auth(string login, string password)
        {
            AuthTask = new Task(()=>
            {
                try
                {
                    int Id = db.AuthorizeUser(login, password);
                    ProfileData profile = db.GetEmployeeData(Id);

                    Guard.AccesLevel = profile.accessLevel;

                    ASMaIoP.View.Auth.Instance.Dispatcher.Invoke(() =>
                    {
                        ASMaIoP.MainWindow mainWindow = new MainWindow(profile);
                        mainWindow.Show();
                        ASMaIoP.View.Auth.Instance.Close();
                    });

                }
                catch (Exception ex)
                {
                    DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                    return;
                }
            });

            AuthTask.Start();
        }

        public void DeviceInput(string cardID)
        {
            AuthTask = new Task(() =>
            {
                try
                {
                    int Id = db.AuthorizeUser(cardID);
                    ProfileData profile = db.GetEmployeeData(Id);

                    Guard.AccesLevel = profile.accessLevel;

                    ASMaIoP.View.Auth.Instance.Dispatcher.Invoke(() =>
                    {
                        ASMaIoP.MainWindow mainWindow = new MainWindow(profile);
                        mainWindow.Show();
                        ASMaIoP.View.Auth.Instance.Close();
                    });

                }
                catch (Exception ex)
                {
                    DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                    return;
                }
            });

            AuthTask.Start();
        }

        public AuthVM()
        {
            db = DatabaseFactory.CreateInterface();
            ArduinoAPI.cardReceivedHandler = DeviceInput;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
