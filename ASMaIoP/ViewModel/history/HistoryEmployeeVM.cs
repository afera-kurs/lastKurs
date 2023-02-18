using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMaIoP.Model;

namespace ASMaIoP.ViewModel
{
    public class HistoryEmployeeVM
    {
        public class HistoryRow
        {
            public string Date { get; set; }
            public string Name { get; set; }
            public string EmployeeID { get; set; }
            public string OwnerName { get; set; }
            public string Description { get; set; }
        }

        List<HistoryRow> HistoryList;

        List<HistroyInfo> data;
        DatabaseInterface db;

        EmployeeData target;

        public HistoryEmployeeVM(EmployeeData data)
        {
            target = data;
            db = DatabaseFactory.CreateInterface();
        }

        public void CreateDocument()
        {
            
        }

        public void LoadData()
        {
            data = new List<HistroyInfo>();
            db.LoadHistroy(data,target.EmployeeId);

            HistoryList = new List<HistoryRow>();

            foreach (HistroyInfo info in data)
            {
                HistoryRow row = new HistoryRow();
                row.Name = info.targetName;
                row.Date = info.date;
                row.EmployeeID = info.employee_target_ID;
                row.Description = info.desc;
                row.OwnerName = info.owner;

                HistoryList.Add(row);
            }
        }

        public List<HistoryRow> GetData()
        {
            return HistoryList;
        }

    }
}
