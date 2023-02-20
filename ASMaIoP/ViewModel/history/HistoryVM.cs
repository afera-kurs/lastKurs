using ASMaIoP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASMaIoP.Model.DatabaseInterface;

namespace ASMaIoP.ViewModel
{
    public class HistoryVM
    {
        public class HistoryRow
        {
            public string Date { get; set; }
            public string Name { get; set; }
            public string OwnerName { get; set; }
            public int EmployeeID { get; set; }
            public int OwnerID { get; set; }
            public int? DocsID { get; set; }
            public string Description { get; set; }
            internal DocInformation doc;
            public ProfileData employee;
            public ProfileData owner;

        }

        List<HistoryRow> HistoryList;

        List<HistroyInformation> data;
        DatabaseInterface db;

        ProfileData profile;

        public HistoryVM(ProfileData profile)
        {
            this.profile = profile;
            db = DatabaseFactory.CreateInterface();
        }

        public void LoadData()
        {
            data = new List<HistroyInformation>();
            if(profile.accessLevel > 3)
            {
                db.LoadDocs(data);
            }
            else
            {
                db.LoadDocs(data, profile.id);
            }

            HistoryList = new List<HistoryRow>();

            db.Connection.Open();
            foreach (HistroyInformation info in data)
            {
                HistoryRow row = new HistoryRow();
                ProfileData pd = db.GetEmployeeData(info.owner_ID, false);
                row.OwnerName = $"{pd.name} {pd.surname} {pd.patName}";
                row.OwnerID = info.owner_ID;
                row.EmployeeID = info.empl_ID;
                ProfileData emp = db.GetEmployeeData(info.empl_ID, false);
                row.Name = $"{emp.name} {emp.surname} {emp.patName}";
                row.Date = info.histroyDate.ToShortDateString();
                row.Description = info.histroyDesc;
                row.employee = emp;
                row.owner = pd;

                if(info.docs_ID != null)
                {
                    row.DocsID = info.docs_ID.Value;
                    row.doc = info.information;
                }
                else
                {
                    row.DocsID = null;
                }
                HistoryList.Add(row);
            }
            db.Connection.Close();
        }

        public List<HistoryRow> GetData()
        {
            return HistoryList;
        }

    }
}
