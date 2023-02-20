using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ASMaIoP.Model;
using ASMaIoP.ViewModel;


namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        private AuthVM vm;
        private static Auth instance;
        public static Auth Instance
        {
            get
            {
                if (instance == null)
                    instance = new Auth();
                return instance;
            }
        }
        public Auth()
        {
            instance = this;
            InitializeComponent();
            vm = new AuthVM(AnimationGrid);
            el.Position = new TimeSpan(0, 0, 1);
            el.Play();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //BlurEffect effect = new BlurEffect(this) { BlurOpacity = 70 };
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            AuthPanel.Visibility = Visibility.Collapsed;
            AnimationGrid.IsEnabled = true;
            AnimationGrid.Visibility = Visibility.Visible;
            vm.Auth(Login.Text, Password.Password);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void movieLoader_MediaEnded(object sender, RoutedEventArgs e)
        {
            el.Position = new TimeSpan(0, 0, 1);
            el.Play();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }
}
