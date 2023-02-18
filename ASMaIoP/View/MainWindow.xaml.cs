using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Windows.UI.ViewManagement;
using ASMaIoP.Model;
using ASMaIoP.ViewModel;
using ASMaIoP.View;
using CefSharp.DevTools.Profiler;
using ASMaIoP.View.Pages;
using Microsoft.Win32;

namespace ASMaIoP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    /// 

    public partial class MainWindow : MetroWindow
    {
        DatabaseInterface db;
        MainWindowVM vm;
        bool FlagActrion = false;

        ProfileData prof;

        internal MainWindow(ProfileData profile)
        {
            this.prof = profile;
            InitializeComponent();
            vm = new MainWindowVM(profile);
            DataContext = vm;
            db = DatabaseFactory.CreateInterface();

            SwitchShiftStatus(profile.isShiftStarted);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AdminPanel.Visibility =
                Guard.EmployeeControlPanel 
                ? Visibility.Visible : Visibility.Collapsed;

            RolePanel.Visibility =
                Guard.RoleControlsPanel ?
                Visibility.Visible : Visibility.Collapsed;

        }
        //Правая верхняя кнопочка для выхода из аккаунта
        private void ProfileAction_Click(object sender, RoutedEventArgs e)
        {
            if(FlagActrion == false)
            {
                ExitGrid.Visibility = Visibility.Visible;
                FlagActrion = true;
            }
            else
            {
                ExitGrid.Visibility = Visibility.Collapsed;
                FlagActrion = false;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Auth wnd = new Auth();
            wnd.Show();
            this.Close();
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new View.Pages.AdminEmployeeData(prof);
        }

        private void RolePanel_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new View.Pages.JobsList();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new View.Pages.Table(prof);
            Page.HorizontalContentAlignment = HorizontalAlignment.Left;
            Page.VerticalContentAlignment = VerticalAlignment.Top;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new View.Pages.History(prof);
        }

        private void StartWork_Click(object sender, RoutedEventArgs e)
        {
            SwitchShiftStatus(false);
            vm.StartWork();
        }

        private void EndWork_Click(object sender, RoutedEventArgs e)
        {
            SwitchShiftStatus(true);
            vm.EndWork();
        }
        private void SwitchShiftStatus(bool status)
        {
            EndWork.Visibility = status? Visibility.Collapsed : Visibility.Visible;
            StartWork.Visibility = status?  Visibility.Visible : Visibility.Collapsed;
        }

        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG Files (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                //MessageBox.Show(openFileDialog.FileName);
                

                vm.ChangeImage(openFileDialog.FileName);
            }

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new View.Pages.TaskView(prof);
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            DocumentHelper helper = new DocumentHelper(Properties.Resources.CreateEmploye);
            helper.Replace("FIO", "123");
            helper.Save("tmp.docx");
        }
    }

}