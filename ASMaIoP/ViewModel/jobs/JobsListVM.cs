using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMaIoP.Model;

namespace ASMaIoP.ViewModel
{
    internal class JobsListVM : INotifyPropertyChanged
    {
        private List<RoleData> roles;

        public class RoleColumn
        {
            public int Id { get; set; }
            public string RoleTitle { get; set; }
            public int Level { get; set; }
            public int EmployeeCount { get; set; }
        }

        private DatabaseInterface databaseInterface;

        public delegate void UpdateRolesDelegate(List<RoleColumn> roles);

        public JobsListVM()
        {
            roles = new List<RoleData>();
            databaseInterface = DatabaseFactory.CreateInterface();
        }

        public void UpdateRoles()
        {
            try
            {
                databaseInterface.GetRolesData(roles);
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
            }   
        }

        public void LoadDataToDataGrid(UpdateRolesDelegate del)
        {
            List<RoleColumn> mem = new List<RoleColumn>();

            foreach(RoleData data in roles)
            {
                RoleColumn roleColumn = new RoleColumn();
                roleColumn.RoleTitle = data.title;
                roleColumn.Id = data.id;
                roleColumn.EmployeeCount = data.employeeCount;
                roleColumn.Level = data.level;

                mem.Add(roleColumn);
            }

            del(mem);
            

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
