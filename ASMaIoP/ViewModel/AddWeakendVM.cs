using CefSharp.DevTools.Profiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMaIoP.Model;

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
            db.AddFillTable(desc, profile.name, data.name, data.EmployeeId, first, end, type);
        }
    }
}
