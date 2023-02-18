using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ASMaIoP.Model;

namespace ASMaIoP.ViewModel
{

    class TabelLoader
    {
        ASMaIoP.Model.DatabaseInterface databaseInterface;
        ASMaIoP.Model.DatabaseConnectionWrapper connection;
        public TabelLoader()
        {
            databaseInterface = DatabaseFactory.CreateInterface();
            connection = databaseInterface.Connection;
        }

        struct tabelEmployee
        {
            public string Id;
            public string FullName;
            public long Hours;
            public string Role;

            public List<TabelMark> marks;
        }

        List<tabelEmployee> general;

        List<EmployeeData> workers;

        public string FormatDateToSql(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public string FormatDateHourToSql(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm");
        }

        public string FormatOnlyDateToSql(DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }

        public string FormatOnlyMonthToSql(DateTime time)
        {
            return time.ToString("yyyy-MM");
        }

        public void LoadDataFromBD(DateTime month)
        {
            workers = new List<EmployeeData>();
            general = new List<tabelEmployee>();
            databaseInterface.LoadEmployeeData(workers);

            //connection.SqlConnection.Open();

            foreach (EmployeeData worker in workers)
            {
                tabelEmployee emp = new tabelEmployee();

                emp.FullName = $"{worker.name} {worker.surname}";
                emp.marks = new List<TabelMark>();
                emp.Id = worker.EmployeeId.ToString();
                emp.Role = worker.roleTitle;

                string sql = $"SELECT table_ID, table_feature, table_date FROM `table` WHERE table_employee={worker.EmployeeId} AND table_date LIKE '{FormatOnlyMonthToSql(month)}%' ORDER BY table_date ASC";
                MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(table);

                long hoursCount = 0;
                bool fillEmpty = false;
                int count = 0;

                List<TabelDate> dates = new List<TabelDate>();

                foreach(DataRow row in table.Rows)
                {
                    string id = row.ItemArray[0].ToString();
                    string mark = row.ItemArray[1].ToString();
                    string day = row.ItemArray[2].ToString();

                    DateTime time = DateTime.Parse(day);
                    count++;

                    if (FormatOnlyDateToSql(time) == FormatOnlyDateToSql(DateTime.Now))
                    {
                        break;
                    }
                    if (Char.IsDigit(mark[0]))
                    {
                        hoursCount += Int32.Parse(mark);
                    }

                    TabelDate d = new TabelDate();
                    d.mark = mark;
                    d.time = time;
                    d.id = id;
                    
                    dates.Add(d);

                    //emp.marks.Add(mark);
                }

                int SystemCount = DateTime.DaysInMonth(month.Year, month.Month);
                int ResCount = SystemCount - count;

                emp.marks = new List<TabelMark>(WorkTimeTabelGenerator.ConvertMarksToTabelFormat(month.Year, month.Month, dates.ToArray()));

                /*for(int i = 0; i< ResCount; i++)
                {
                    emp.marks.Add("X");
                }*/

                emp.Hours = hoursCount;
                general.Add(emp);
            }
            //Connection.SqlConnection.Close();
            
        }

        public void LoadAllIntoBuilder(WorkTimeTabelGenerator generator)
        {
            foreach(tabelEmployee emp0 in general)
            {
                LoadIntoBuilder(emp0, generator);
            }
        }

        private void LoadIntoBuilder(tabelEmployee d, WorkTimeTabelGenerator generator)
        {
            WorkTimeEmployee emp2 = new WorkTimeEmployee();
            emp2.FullName = d.FullName;
            emp2.Id = int.Parse(d.Id);
            emp2.Hours = d.Hours;
            emp2.Role = d.Role;

            generator.PushHTMLContent(emp2, d.marks.ToArray());
        }

                // я еще тут еще 23:18 
        //public void LoadDataFromDatabase()
        //{
        //    rows = new List<tabelRow>();

        //    connection.Open();

        //    //я пока что так не могу 
        //    string sql = $"SELECT table_id, table_feature, table_employee, table_date, people_name, people_surname, role_title FROM `table` JOIN employee ON employee_ID=table_employee JOIN people ON people_ID=employee_people_ID JOIN role ON role_ID=employee_role_ID";
        //    MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
        //    DataTable table = new DataTable();

        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    adapter.SelectCommand = cmd;
        //    adapter.Fill(table); 

        //    foreach (DataRow row in table.Rows)
        //    {
        //        tabelRow localRow = new tabelRow();

        //        localRow.Id = row.ItemArray[0].ToString();
        //        localRow.Mark = row.ItemArray[1].ToString();
        //        localRow.EmployeeId = row.ItemArray[2].ToString();
        //        localRow.Date = row.ItemArray[3].ToString();
        //        localRow.name = row.ItemArray[4].ToString();
        //        localRow.surname = row.ItemArray[5].ToString();
        //        localRow.roleTitle = row.ItemArray[6].ToString();

        //        rows.Add(localRow);
        //    }

        //    connection.Close();
        //}

        //void PushDataToWorkTimeTabelGenerator(WorkTimeTabelGenerator generator)
        //{
        //    int i = 0;

        //    WorkTimeEmployee employee;
        //    List<string> marks;

        //    foreach(tabelRow row in rows)
        //    {
        //        if(i == 0)
        //        {
        //            employee = new WorkTimeEmployee();
        //            employee.Id = int.Parse(row.Id);
        //            employee.FullName = row.name;
        //            employee.Role = row.roleTitle;
        //            employee.Hours = 20;

        //            marks = new List<string>();
        //        }



        //        if(i == 30)
        //        {
        //            i = 0;
        //            continue;
        //        }
        //        i++;
        //    }
        //}
    }
}
