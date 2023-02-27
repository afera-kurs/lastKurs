using CefSharp.DevTools.Profiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMaIoP.Model;
using ASMaIoP.View;

namespace ASMaIoP.ViewModel
{
    internal class AddWeakendVM
    {
        ProfileData profile;
        EmployeeData data;
        DatabaseInterface db;

        public AddWeakendVM(ProfileData profile, EmployeeData data)
        {
            db = DatabaseFactory.CreateInterface();
            this.profile = profile;
            this.data = data;
        }

        public void CreateWeakend(string desc, DateTime first, DateTime end, DatabaseInterface.FillTableType type)
        {
            DocumentHelper helper;
            switch (type)
            {
                case DatabaseInterface.FillTableType.Weekend:
                    {
                        int docId = db.DocsCreate(3, first, end, type.ToString(), "");
                        db.HistroyCreateNew(DateTime.Now.Date, "Отпуск", docId, profile.id, data.EmployeeId);
                        helper = new DocumentHelper(Properties.Resources.Weekend);
                        helper.Replace("нДок", docId.ToString());
                        helper.Replace("ДАТАС", DateTime.Now.Date.ToString());
                        helper.Replace("ТБН", data.EmployeeId.ToString());
                        helper.Replace("ИМЯП", $"{data.name} {data.surname} {data.patName}");
                        helper.Replace("РОЛЬ", data.roleTitle);
                        helper.Replace("ДА", "");
                        helper.Replace("МА", "");
                        helper.Replace("ГА", "");
                        helper.Replace("ДАК", "");
                        helper.Replace("МАК", "");
                        helper.Replace("ГАК", "");
                        helper.Replace("АДН", "");
                        helper.Replace("МА", first.Day.ToString());
                        helper.Replace("ГА", first.Day.ToString());
                        helper.Replace("ДБ", first.Day.ToString());
                        helper.Replace("МБ", first.Month.ToString());
                        helper.Replace("ГБ", first.Year.ToString());
                        helper.Replace("ДБК", end.Day.ToString());
                        helper.Replace("МБК", end.Month.ToString());
                        helper.Replace("ГБК", end.Year.ToString());
                        helper.Replace("БДН", (first - end).TotalDays.ToString());
                        break;
                    }
                case DatabaseInterface.FillTableType.WeekendAge:
                    {
                        int docId = db.DocsCreate(3, first, end, type.ToString(), "");
                        db.HistroyCreateNew(DateTime.Now.Date, "Отпуск", docId, profile.id, data.EmployeeId);
                        helper = new DocumentHelper(Properties.Resources.Weekend);
                        helper.Replace("нДок", docId.ToString());
                        helper.Replace("ДАТАС", DateTime.Now.Date.ToString());
                        helper.Replace("ТБН", data.EmployeeId.ToString());
                        helper.Replace("ИМЯП", $"{data.name} {data.surname} {data.patName}");
                        helper.Replace("РОЛЬ", data.roleTitle);
                        helper.Replace("ДА", first.Day.ToString());
                        helper.Replace("МА", first.Month.ToString());
                        helper.Replace("ГА", first.Year.ToString());
                        helper.Replace("ДАК", end.Day.ToString());
                        helper.Replace("МАК", first.Month.ToString());
                        helper.Replace("ГАК", first.Year.ToString());
                        helper.Replace("АДН", (first - end).TotalDays.ToString());
                        helper.Replace("ДБ", "");
                        helper.Replace("МБ", "");
                        helper.Replace("ГБ", "");
                        helper.Replace("ДБК", "");
                        helper.Replace("МБК", "");
                        helper.Replace("ГБК", "");
                        helper.Replace("БДН", "");
                        break;
                    }
                    
            }
            db.AddFillTable(desc, profile.name, data.name, data.EmployeeId, first, end, type);
        }
    }
}
