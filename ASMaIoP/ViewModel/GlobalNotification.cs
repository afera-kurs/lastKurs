using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMaIoP.ViewModel
{
    internal interface INotificationSystem
    {
        void Initialize();
        void Shutdown();
    }

    internal static class GlobalNotification
    {
        static INotificationSystem[] systems = { new WindowsSystemNotification()};

        public static void Init()
        {
            foreach(INotificationSystem system in systems)
            {
                system.Initialize();
            }
        }

        public static void Stop()
        {
            foreach (INotificationSystem system in systems)
            {
                system.Shutdown();
            }
        }
    }
}
