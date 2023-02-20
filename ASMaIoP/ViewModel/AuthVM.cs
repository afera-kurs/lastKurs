using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media;
using ASMaIoP.Model;
using System.Windows.Controls;
using System.Windows;

namespace ASMaIoP.ViewModel
{
    internal class AuthVM : INotifyPropertyChanged
    {
        DatabaseInterface db;
        static Thread AuthTask;
        object locker = new object();
        public void Auth(string login, string password)
        {
            AuthTask = new Thread(()=>
            {
                lock(locker)
                {
                    try
                    {
                        int Id = db.AuthorizeUser(login, password);
                        ProfileData profile = db.GetEmployeeData(Id);

                        Guard.AccesLevel = profile.accessLevel;

                        ASMaIoP.View.Auth.Instance.Dispatcher.Invoke(() =>
                        {
                            if(AnimationGrid != null)
                            {
                                AnimationGrid.IsEnabled = false;
                                AnimationGrid.Visibility = Visibility.Collapsed;
                            }
                            ASMaIoP.MainWindow mainWindow = new MainWindow(profile);
                            mainWindow.Show();
                            ASMaIoP.View.Auth.Instance.Close();
                        });

                    }
                    catch (Exception ex)
                    {
                        DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                        ASMaIoP.View.Auth.Instance.Dispatcher.Invoke(() =>
                        {
                            if (AnimationGrid != null)
                            {
                                AnimationGrid.IsEnabled = false;
                                AnimationGrid.Visibility = Visibility.Collapsed;
                            }

                            ASMaIoP.View.Auth.Instance.AuthPanel.Visibility = Visibility.Visible;
                        });

                        return;
                    }
                }
            });

            AuthTask.Start();
        }

        Grid AnimationGrid;

        public void DeviceInput(string cardID)
        {
            AuthTask = new Thread(() =>
            {
                ASMaIoP.View.Auth.Instance.Dispatcher.Invoke(() =>
                {
                    AnimationGrid.IsEnabled = true;
                    AnimationGrid.Visibility = Visibility.Visible;
                });
                try
                {
                    int Id = db.AuthorizeUser(cardID);
                    ProfileData profile = db.GetEmployeeData(Id);

                    Guard.AccesLevel = profile.accessLevel;

                    ASMaIoP.View.Auth.Instance.Dispatcher.Invoke(() =>
                    {
                        ASMaIoP.MainWindow mainWindow = new MainWindow(profile);
                        AnimationGrid.IsEnabled = false;
                        AnimationGrid.Visibility = Visibility.Collapsed;
                        mainWindow.Show();
                        ASMaIoP.View.Auth.Instance.Close();
                    });

                }
                catch (Exception ex)
                {
                    ASMaIoP.View.Auth.Instance.Dispatcher.Invoke(() =>
                    {
                        AnimationGrid.IsEnabled = false;
                        AnimationGrid.Visibility = Visibility.Collapsed;
                    });
                    DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                    return;
                }
            });

            AuthTask.Start();
        }

        public AuthVM(Grid AnimationGrid)
        {
            this.AnimationGrid = AnimationGrid;
            db = DatabaseFactory.CreateInterface();
            ArduinoAPI.cardReceivedHandler = DeviceInput;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
