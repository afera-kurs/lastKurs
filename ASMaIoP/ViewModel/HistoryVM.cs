using ASMaIoP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMaIoP.ViewModel
{
    public class HistoryVM
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

        public HistoryVM()
        {
            db = DatabaseFactory.CreateInterface();
        }

        public void LoadData()
        {
            data = new List<HistroyInfo>();
            db.LoadHistroy(data);

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
