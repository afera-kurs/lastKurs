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
using System.Threading;

namespace ASMaIoP.ViewModel
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        private ProfileData profile;

        public ProfileData Profile
        {
            get { return profile; }
        }

        private string fullname;

        public string FullName
        {
            get => fullname;
            set
            {
                fullname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(fullname)));
            }
        }

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

        private string currentTime;

        public string CurrentTime
        {
            get => currentTime;
            set
            {
                currentTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTime)));
            }
        }

        private ImageSource profileImage;

        public ImageSource PhotoEmloyee
        {
            get => profileImage;
            set
            {
                profileImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PhotoEmloyee)));
            }
        }

        ImageSystem images;

        DispatcherTimer LiveTime;

        Thread iconLoadThread;
        object locker = new object();
        internal MainWindowVM(ProfileData profile)
        {
            fullname = $"{profile.name} {profile.surname}";
            this.profile = profile;

            RoleTitle = profile.roleTitle;

            LiveTime  = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();

            db = DatabaseFactory.CreateInterface();
        }

        DatabaseInterface db;

        public void loadImage(Window wnd)
        {
            iconLoadThread = new Thread(() =>
            {
                lock (locker)
                {
                    images = new ImageSystem();
                    Bitmap bp2 = images.DownloadImage(profile.pictureUrl);
                    wnd.Dispatcher.Invoke(() =>
                    {
                        var bp = ImageSystem.BitmapToImageSource(bp2);
                        PhotoEmloyee = bp;
                    });
                }
            });
            iconLoadThread.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            CurrentTime = $"Текущее время:{DateTime.Now.ToString("HH:mm:ss")}";
        }

        public void StartWork()
        {
            db.EnterAddRow(profile.id);
        }

        public void EndWork()
        {
            db.ExitAddRow(profile.id);
        }

        public void ChangeImage(string path)
        {
            images.UploadFile(path, profile.pictureUrl);

            PhotoEmloyee = ImageSystem.BitmapToImageSource(images.DownloadImage(profile.pictureUrl));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
