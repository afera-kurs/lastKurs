using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using ASMaIoP.Model;
using ASMaIoP.ViewModel;
using static ASMaIoP.Model.ArduinoAPI;

namespace ASMaIoP.View
{
    /// <summary>
    /// Логика взаимодействия для CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        CreateEmployeeVM vm;
        Task UpdateData;

        Action UpdateDataDelegate;

        public CreateEmployee(ProfileData prof)
        {
            InitializeComponent();
            FirstWorkDay.SelectedDate = DateTime.Now;
            vm = new CreateEmployeeVM(prof);
            DataContext = vm;
            UpdateDataDelegate = () =>
            {
                try
                {
                    vm.UpdateViewModelData();
                    vm.BindRolesCombobox(roleComboBox);
                }
                catch(Exception ex)
                {
                    DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
                    Close();
                }
            };

            RunUpdateData();
        }

        public void RunUpdateData()
        {
            UpdateData = new Task(UpdateDataDelegate);
            UpdateData.Start();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Employee.Visibility = Visibility.Collapsed;
            Physical.Visibility = Visibility.Visible;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {

            DataRowView v = (DataRowView)roleComboBox.SelectedValue;
            int Id = int.Parse((string)v.Row.ItemArray[1]);
            if (vm.CreateUser(Id))
            {
                DataLog.Log.AddLog(new Event("Пользователь успешно создан", EventPriority.Info));
                this.Close();
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            Employee.Visibility = Visibility.Visible;
            Physical.Visibility = Visibility.Collapsed;
        }

        bool isReadingStarted = false;
        ArduinoAPI.CardReceivedHandler oldArduinoApiHandler;

        private void CardRead_Click(object sender, RoutedEventArgs e)
        {
            if (isReadingStarted)
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
    }
}
