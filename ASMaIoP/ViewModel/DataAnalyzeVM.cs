using ASMaIoP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMaIoP.ViewModel
{
    internal class DataAnalyzeVM
    {
        DatabaseInterface db;
        EmployeeData data;

        public DataAnalyzeVM(EmployeeData data)
        {
            this.data = data;   
            db = DatabaseFactory.CreateInterface();   
        }

        public int[] LoadData()
        {
            return db.LoadAllTableFeature(data.EmployeeId);
        }
    }
}
