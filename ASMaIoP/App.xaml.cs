using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using ASMaIoP.ViewModel;
using ASMaIoP.Model;

namespace ASMaIoP
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            // инициализации системы оповещения
            GlobalNotification.Init();
            FTPClientFactory.SetupConnectionData("osp74.beget.tech", "osp74_eberc", "t&lmo9Sj");
            DatabaseFactory.SetupConnectionString("server=192.168.25.23;port=33333;user=st_3_20_32;database=is_3_20_st32_KURS;password=61180056;");
            if(!ArduinoAPI.UsePort("COM3"))
            {
                
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            ArduinoAPI.Shutdown();
        }
    }
}
