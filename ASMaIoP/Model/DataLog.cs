using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;

namespace ASMaIoP.Model
{
    enum EventPriority
    {
        Error,
        Warning,
        Info
    }

    internal class Event
    {
        private string message;
        private EventPriority eventPriority;

        public Event(string Message, EventPriority EventPriority)
        {
            message = Message;
            eventPriority = EventPriority;
        }

        public string Message { get => message; }
        public EventPriority EventPriority { get => eventPriority; }
    }

    internal class WarnEvent : Event
    {
        public WarnEvent()
            : base("не хватает деняг", EventPriority.Warning)
        {

        }
    }

    internal class DataLog
    {
        private static DataLog dataLog;
        public static DataLog Log
        {
            get
            {
                if (dataLog == null)
                    dataLog = new DataLog();

                return dataLog;
            }
        }

        public delegate void HandleEvent(Event _event);
        private event HandleEvent handlerEventError;
        private event HandleEvent handlerEventInfo;
        private event HandleEvent handlerEventWarn;

        public void AddErrorEventHandler(HandleEvent handler) => handlerEventError += handler;
        public void AddWarnEventHandler(HandleEvent handler) => handlerEventWarn += handler;
        public void AddInfoEventHandler(HandleEvent handler) => handlerEventInfo += handler;
        public void RemoveErrorEventHandler(HandleEvent handler) => handlerEventError -= handler;
        public void RemoveWarnEventHandler(HandleEvent handler) => handlerEventWarn -= handler;
        public void RemoveInfoEventHandler(HandleEvent handler) => handlerEventInfo -= handler;

        public void AddLog(Event _event)
        {
            switch(_event.EventPriority)
            {
                case EventPriority.Error:
                    {
                        handlerEventError(_event);
                        return;
                    }
                case EventPriority.Warning:
                    {
                        handlerEventWarn(_event);
                        return;
                    }
                case EventPriority.Info:
                    {
                        handlerEventInfo(_event);
                        return;
                    }

            }
        }
    }
}
