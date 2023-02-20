using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ASMaIoP.ViewModel;
using ASMaIoP.View;
using ASMaIoP.Model;
using System.Threading;

namespace ASMaIoP.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeeInformation.xaml
    /// </summary>
    public partial class EmployeeInformation : Window
    {
        ReasonDississed reasonDississed;
        EmployeeInformationVM vm;
        ProfileData prof;
        EmployeeData data;
        Thread thread;
        Mutex locker = new Mutex();
        internal EmployeeInformation(ProfileData prof, EmployeeData data)
        {
            this.data = data;
            InitializeComponent();
            vm = new EmployeeInformationVM(prof, data, locker);
            DataContext = vm;
            this.prof = prof;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DataRowView v = (DataRowView)roleComboBox.SelectedValue;
            if(v==null)
            {
                vm.Apply();
            }
            else
            {
                int Id = int.Parse((string)v.Row.ItemArray[1]);
                string Name = (string)v.Row.ItemArray[2];   
                
                vm.Apply(Id, Name);
            }

            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            vm.DeleteUser();

        }

        private void dissmissed_Click(object sender, RoutedEventArgs e)
        {
            reasonDississed = new ReasonDississed(this, prof, data);
            reasonDississed.ShowDialog(); 
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            HistoryEmploy employ = new HistoryEmploy(data);
            employ.Show();
        }

        private void Hire_Click(object sender, RoutedEventArgs e)
        {
            vm.HireUser();
            this.Close();
        }

        private void Weakend_Click(object sender, RoutedEventArgs e)
        {
            AddWeakend weak = new AddWeakend(prof, data);
            weak.ShowDialog();
        }

        private void Analitics_Click(object sender, RoutedEventArgs e)
        {
            DataAnalyze analyze = new DataAnalyze(data);
            analyze.Show();
        }

        bool isReadingStarted = false;
        ArduinoAPI.CardReceivedHandler oldArduinoApiHandler;

        private void ReadCard_Click(object sender, RoutedEventArgs e)
        {
            if(isReadingStarted)
            {
                ArduinoAPI.cardReceivedHandler = oldArduinoApiHandler;
            }
            else
            {
                oldArduinoApiHandler = ArduinoAPI.cardReceivedHandler;
                ArduinoAPI.cardReceivedHandler = CardReceivedHandler;
            }

            isReadingStarted = !isReadingStarted;
        }

        void CardReceivedHandler(string CardId)
        {
            vm.UpdateCardId(CardId);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            thread = new Thread(() =>
            {
                vm.UpdateViewModelData();
                vm.BindRolesCombobox(roleComboBox);
            });
            thread.Start();
            Hire.Visibility = data.IsDissmissed ? Visibility.Collapsed : Visibility.Visible;
            dissmissed.Visibility = data.IsDissmissed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
