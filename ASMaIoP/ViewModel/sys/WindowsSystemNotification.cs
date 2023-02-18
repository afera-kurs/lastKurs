using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMaIoP.Model;
using Microsoft.Toolkit.Uwp.Notifications;

namespace ASMaIoP.ViewModel
{
    internal class WindowsSystemNotification : INotificationSystem
    {
        public void ErrorEventHandle(Event _event)
        {
            new ToastContentBuilder()
                .AddText("Ошибка!")
                .AddText(_event.Message)
                .AddButton(new ToastButton().SetContent("OK"))
                .Show();
        }

        public void InfoEventHandle(Event _event)
        {
            new ToastContentBuilder()
                .AddText(_event.Message)
                .AddButton(new ToastButton().SetContent("OK"))
                .Show();
        }

        void INotificationSystem.Initialize()
        {
            DataLog.Log.AddErrorEventHandler(ErrorEventHandle);
            DataLog.Log.AddInfoEventHandler(InfoEventHandle);
        }

        void INotificationSystem.Shutdown()
        {
            DataLog.Log.RemoveErrorEventHandler(ErrorEventHandle);
            DataLog.Log.AddInfoEventHandler(InfoEventHandle);
        }
    }
}
