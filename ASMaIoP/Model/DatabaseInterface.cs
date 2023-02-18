using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Net.Sockets;
using System.Web.UI.WebControls;
using System.Web.Security;
using ControlzEx.Standard;
using System.Security.Cryptography;
using Org.BouncyCastle.Utilities.Collections;

namespace ASMaIoP.Model
{
    public struct ProfileData
    {
        public int id;
        public string name;
        public string surname;
        public string patName;
        public string pictureUrl;
        public string roleTitle;

        public bool isShiftStarted;
        public bool isDismissed;

        public int accessLevel;
        public string cardId;
    }

    public struct EmployeeData
    {
        public int EmployeeId;

        public int PeopleId;
        public string name;
        public string surname;
        public string patName;
        public string address;
        public string phoneNumber;

        public bool isShiftStarted;

        public bool IsDissmissed;

        public string cardIndex;
        public string cardId;

        public int roleId;
        public string roleTitle;

        DateTime firstDay;
    }

    internal struct RoleData
    {
        public int id;
        public string title;
        public int level;
        public int employeeCount;
    }

    internal struct HistroyInfo
    {
        public string id;
        public string owner;
        public string employee_target_ID;
        public string targetName;
        public string date;
        public string desc;
    }

    internal struct TaskBDInfo
    {
        public string id;
        public string ownerId;
        public string ownerFullName;
        public string description;
        public string tasksState;
        public string tasksStatus;
        public string lastDay;
        public string taskTitle;

        public void UpdateOwnerName(string name)
        {
            ownerFullName = name;
        }
    }

    internal struct TaskBDFullInfo
    {
        public string id;
        public string ownerId;
        public string ownerFullName;
        public string description;
        public string tasksState;
        public string tasksStatus;
        public string lastDay;
        public string taskTitle;

        public void UpdateOwnerName(string name)
        {
            ownerFullName = name;
        }

        public List<EmployeeData> employees;
    }

    internal static class DatabaseFactory
    {
        private static string connectionString;
        public static void SetupConnectionString(string connectionString)
        {
            DatabaseFactory.connectionString = connectionString;
        }

        public static DatabaseConnectionWrapper CreateConnection()
        {
            if (connectionString.Length <= 0)
            {
                throw new Exception($"попытка создания соединения хотя строка подлючения пуста: '{connectionString}'");
            }

            return new DatabaseConnectionWrapper(connectionString);
        }

        public static DatabaseInterface CreateInterface()
        {
            if (connectionString.Length <= 0)
            {
                throw new Exception($"попытка создания соединения хотя строка подлючения пуста: '{connectionString}'");
            }

            return new DatabaseInterface(connectionString);
        }
    }

    internal class DatabaseConnectionWrapper
    {

        public DatabaseConnectionWrapper(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }

        private MySqlConnection connection;

        public MySqlConnection SqlConnection
        {
            get => connection;
        }

        public void Open()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                return;
            else
                connection.Open();
        }

        public void Close()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                return;
            else
                connection.Close();
        }
    }

    internal class DatabaseInterface
    {
        private DatabaseConnectionWrapper connection;

        public DatabaseConnectionWrapper Connection
        {
            get => connection;
        }

        public DatabaseInterface(string connectionString)
        {
            connection = new DatabaseConnectionWrapper(connectionString);
        }

        public ProfileData GetEmployeeData(int nId)
        {
            ProfileData data = new ProfileData();
            data.id = nId;
            connection.Open();

            MySqlCommand cmd = new MySqlCommand(
                $"SELECT employee.employee_image_url, people.people_name, people.people_surname, role.role_title, role.role_lvl, employee.employee_work_shift, employee_status, cards_ID, people.people_patName FROM employee INNER JOIN people ON employee.employee_people_ID=people.people_ID INNER JOIN role ON employee.employee_role_ID=role.role_ID INNER JOIN cards ON cards.cards_employee_ID = employee.employee_ID WHERE employee.employee_ID={nId}"
            , connection.SqlConnection);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception($"Информация о пользователе:{nId} не найдена");
            }
            else
            {
                data.pictureUrl = reader[0].ToString();
                data.name = reader[1].ToString();
                data.surname = reader[2].ToString();
                data.roleTitle = reader[3].ToString();
                data.accessLevel = reader.GetInt32(4);
                data.isShiftStarted = reader.GetBoolean(5);
                data.isDismissed = reader.GetBoolean(6);
                data.cardId = reader[7].ToString();
                data.patName = reader[8].ToString();
            }

            reader.Close();
            connection.Close();

            return data;
        }

        public void GetRolesData(List<RoleData> roles)
        {
            List<RoleData> datas = new List<RoleData>();

            connection.Open();

            MySqlCommand cmd = new MySqlCommand($"SELECT role_ID, role_title, role_lvl FROM role", connection.SqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                RoleData data = new RoleData();
                data.id = (int)reader[0];
                data.title = reader[1].ToString();
                data.level = (int)reader[2];
                datas.Add(data);
            }

            connection.Close();

            foreach (RoleData roleData in datas)
            {
                RoleData tmpData = roleData;
                tmpData.employeeCount = CountEmployeeWithRoleId(tmpData.id);

                roles.Add(tmpData);
            }
        }

        public RoleData GetRoleDataFromID(int id)
        {
            RoleData data = new RoleData();
            data.id = id;

            connection.Open();

            MySqlCommand cmd = new MySqlCommand($"SELECT role_title, role_lvl, FROM role WHERE role_ID={id}", connection.SqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                data.title = reader[0].ToString();
                data.level = (int)reader[1];
            }
            else
            {
                throw new Exception($"не получилось считать информацию о должности с идентифекатором:{id}");
            }

            connection.Close();

            return data;
        }

        public void AddRole(string RoleTitle, int Level)
        {
            connection.Open();

            string sql = $"INSERT INTO role(role_title, role_lvl) VALUES('{RoleTitle}', {Level})";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

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

        public void EnterAddRow(int employeeId)
        {
            try
            {
                connection.Open();

                string sql = $"SELECT enterExit_id FROM enterExitTime WHERE enterExitTime.enterExit_date LIKE '{FormatOnlyDateToSql(DateTime.Now)}%' AND enterExitTime.enterExit_type=1 AND enterExit_employee_id={employeeId}";

                MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                sql = $"UPDATE employee SET employee_work_shift=0  WHERE employee_ID={employeeId}";
                cmd = new MySqlCommand(sql, connection.SqlConnection);
                cmd.ExecuteNonQuery();

                if (dt.Rows.Count == 0)
                {
                    sql = $"INSERT enterExitTime(enterExit_employee_id, enterExit_date, enterExit_type) VALUES({employeeId},'{FormatDateHourToSql(DateTime.Now)}', 1)";
                    //
                    //
                    cmd = new MySqlCommand(sql, connection.SqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
            }
            finally
            {
                connection.Close();
            }
        }

        public void ExitAddRow(int employeeId)
        {
            try
            {
                connection.Open();

                string sql = $"SELECT enterExit_id FROM enterExitTime WHERE enterExitTime.enterExit_date LIKE '{FormatOnlyDateToSql(DateTime.Now)}%' AND enterExitTime.enterExit_type=0 AND enterExit_employee_id={employeeId}";

                MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                sql = $"UPDATE employee SET employee_work_shift=1  WHERE employee_ID={employeeId}";
                cmd = new MySqlCommand(sql, connection.SqlConnection);
                cmd.ExecuteNonQuery();

                if (dt.Rows.Count > 0)
                {
                    string Id = dt.Rows[0].ItemArray[0].ToString();
                    string IdTable;
                    sql = $"UPDATE enterExitTime SET enterExitTime.enterExit_date='{FormatDateHourToSql(DateTime.Now)}' WHERE enterExit_id={Id}";

                    cmd = new MySqlCommand(sql, connection.SqlConnection);
                    cmd.ExecuteNonQuery();
                    sql = $"SELECT enterExit_date FROM enterExitTime WHERE enterExitTime.enterExit_date LIKE '{FormatOnlyDateToSql(DateTime.Now)}%' AND enterExitTime.enterExit_type=1 AND enterExit_employee_id={employeeId}";
                    cmd = new MySqlCommand(sql, connection.SqlConnection);

                    

                    string FullDate = cmd.ExecuteScalar().ToString();
                    int difference = TimeWorkShift(FullDate);
                    sql = $"SELECT table_id FROM `table` WHERE table_date = '{FormatOnlyDateToSql(DateTime.Now)}' AND table_employee = {employeeId}  ";
                    cmd = new MySqlCommand(sql, connection.SqlConnection);
                    IdTable = cmd.ExecuteScalar().ToString();
                    sql = $"UPDATE `table` SET table_feature = {difference}, table_employee = {employeeId}, table_date = '{FormatOnlyDateToSql(DateTime.Now)}' WHERE table_ID ={IdTable}";
                    cmd = new MySqlCommand(sql, connection.SqlConnection);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = $"INSERT enterExitTime(enterExit_employee_id, enterExit_date, enterExit_type) VALUES({employeeId},'{FormatDateHourToSql(DateTime.Now)}', 0)";
                    cmd = new MySqlCommand(sql, connection.SqlConnection);
                    cmd.ExecuteNonQuery();

                    sql = $"SELECT enterExit_id FROM enterExitTime WHERE enterExitTime.enterExit_date LIKE '{FormatOnlyDateToSql(DateTime.Now)}%' AND enterExitTime.enterExit_type=1 AND enterExit_employee_id={employeeId}";
                    cmd = new MySqlCommand(sql, connection.SqlConnection);

                    object scal = cmd.ExecuteScalar();

                    if (scal == null)
                    {
                        sql = $"UPDATE employee SET employee_work_shift=1  WHERE employee_ID={employeeId}";
                        cmd = new MySqlCommand(sql, connection.SqlConnection);
                        cmd.ExecuteNonQuery();
                        return;
                    }

                    string DateID = scal.ToString();
                    sql = $"SELECT enterExit_date FROM enterExitTime WHERE enterExit_id={DateID}";
                    cmd = new MySqlCommand(sql, connection.SqlConnection);

                    string FullDate = cmd.ExecuteScalar().ToString();
                    int difference = TimeWorkShift(FullDate);
                    sql = $"INSERT `table`(table_feature, table_employee, table_date) VALUES ({difference}, {employeeId}, '{FormatOnlyDateToSql(DateTime.Now)}')";
                    cmd = new MySqlCommand(sql, connection.SqlConnection);
                    cmd.ExecuteNonQuery();
                }

                // ну ок но всеравно нам нужно это делать тут чтобы финальный запрос не повторять
            }
            catch (Exception ex)
            {
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
            }
            finally
            {
                connection.Close();
            }

        }

        private int TimeWorkShift(string time)
        {
            time = time.Replace(":", "").Remove(0,11).Remove(2,4);
            string thisMoment = FormatDateHourToSql(DateTime.Now).Replace(":", "").Remove(0, 11).Remove(2, 2);

            return int.Parse(thisMoment)-int.Parse(time);
        }

        private bool AddPeople(string name, string surname, string address, string phoneNumber)
        {
            connection.Open();

            address = address.Length > 0 ? address : "NULL";
            phoneNumber = phoneNumber.Length > 0 ? phoneNumber : "NULL";

            MySqlCommand cmd = new MySqlCommand($"INSERT INTO people(people_name, people_surname, people_address, people_phone_number) " +
                $"VALUES('{name}', '{surname}', '{address}', '{phoneNumber}')", connection.SqlConnection);

            int nCount = cmd.ExecuteNonQuery();

            connection.Close();

            return nCount > 0;
        }

        public void LoadEmployeeData(List<EmployeeData> employees)
        {
            connection.Open();

            MySqlCommand cmd = new MySqlCommand($"SELECT employee_ID, employee_people_ID, employee_role_ID, people_name, people_surname, people_address, people_phone_number, role_title, employee_status, cards_INDEX, cards_ID, people_patName FROM employee INNER JOIN people ON employee_people_ID=people_ID INNER JOIN role ON employee_role_ID=role.role_ID INNER JOIN cards ON cards_employee_ID=employee_ID", connection.SqlConnection);

            MySqlDataReader reader = cmd.ExecuteReader();
            
            while(reader.Read())
            {
                EmployeeData data = new EmployeeData();

                data.EmployeeId = reader.GetInt32(0);
                data.PeopleId = reader.GetInt32(1);
                data.roleId = reader.GetInt32(2);

                data.name = reader[3].ToString();
                data.surname = reader[4].ToString();
                data.address = reader[5].ToString();
                data.phoneNumber = reader[6].ToString();

                data.roleTitle = reader[7].ToString();
                data.IsDissmissed = reader.GetBoolean(8);

                data.cardIndex = reader[9].ToString();
                data.cardId = reader[10].ToString();
                data.patName = reader[11].ToString();

                employees.Add(data);
            }

            connection.Close();
        }

        public bool AddEmployee(
            string name, string surname,
            string address, string phoneNumber,
            int roleID,
            string login, string password,
            string cardId,
            DateTime fistWorkDay,
            ProfileData prof)
        {
            bool IsSuccess = AddPeople(name, surname, address, phoneNumber);
            connection.Open();

            MySqlCommand cmd = new MySqlCommand($"INSERT INTO employee(employee_people_ID, employee_role_ID, employee_login, employee_pass, employee_work_day)" +
                $"VALUES(LAST_INSERT_ID(),{roleID}, '{login}', '{Utils.sha256(password)}, '{FormatOnlyDateToSql(fistWorkDay)}')", connection.SqlConnection);

            IsSuccess &= cmd.ExecuteNonQuery() > 0;

            cmd = new MySqlCommand($"SELECT LAST_INSERT_ID()", connection.SqlConnection);

            string employeeID = cmd.ExecuteScalar().ToString();


            if(cardId != null)
            {
                if (cardId.Length > 0)
                {
                    cmd = new MySqlCommand($"INSERT INTO cards(cards_ID, cards_employee_ID) VALUES ({Utils.sha256(cardId)}, {employeeID})", connection.SqlConnection);
                    IsSuccess &= cmd.ExecuteNonQuery() > 0;
                }
                else
                {
                    cmd = new MySqlCommand($"INSERT INTO cards(cards_ID, cards_employee_ID) VALUES (NULL,  {employeeID})", connection.SqlConnection);
                    IsSuccess &= cmd.ExecuteNonQuery() > 0;
                }
            }
            else
            {
                cmd = new MySqlCommand($"INSERT INTO cards(cards_ID, cards_employee_ID) VALUES (NULL,  {employeeID})", connection.SqlConnection);
                IsSuccess &= cmd.ExecuteNonQuery() > 0;
            
            }

            int docId = DocsCreate();
            HistroyCreateNew()

            connection.Close();

            return IsSuccess;
        }

        public int EditEmploy(int employeeID,
            string name, string surname,
            string address, string phoneNumber,
            int roleID,
            string cardId)
        {

            if (cardId == null)
            {
                cardId = "NULL";
            }
            else if (cardId.Length < 1)
            {
                cardId = "NULL";
            }
            else
            {
                cardId = $"'{cardId}'";
            }

            string sql = $"UPDATE employee INNER JOIN people ON people.people_ID=employee.employee_people_ID INNER JOIN cards ON cards_employee_ID=employee_ID SET employee.employee_role_ID='{roleID}', people.people_name ='{name}',people.people_surname='{surname}', people.people_address='{address}', people.people_phone_number='{phoneNumber}' , cards.cards_ID={cardId} WHERE employee.employee_ID={employeeID}";

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
            cmd.ExecuteNonQuery();

            connection.Close();
            return 0;
        }

        public int CountEmployeeWithRoleId(int roleId)
        {
            connection.Open();

            string sql = $"SELECT COUNT(employee_ID) FROM employee JOIN role ON employee_role_ID=role_ID WHERE role_ID={roleId}";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            connection.Close();
            return count;
        }

        public int AuthorizeUser(string cardID)
        {
            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT employee_ID FROM employee JOIN cards ON cards.cards_employee_ID=employee_ID WHERE cards_ID=@ci", connection.SqlConnection);

                //Определяем параметры
                cmd.Parameters.Add("@ci", MySqlDbType.VarChar, 255);
                //Присваиваем параметрам значение
                cmd.Parameters["@ci"].Value = cardID;

                int Id = int.Parse(cmd.ExecuteScalar().ToString());

                connection.Close();
                return Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка авторизации");
            }
            finally
            {
                connection.Open();
            }
        }

        public void EditCardId(int employeeId, string newCardId)
        {
            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE cards SET cards_ID=@ci WHERE employee_ID=@ei", connection.SqlConnection);

                //Определяем параметры
                cmd.Parameters.Add("@ci", MySqlDbType.VarChar, 255);
                cmd.Parameters.Add("@ei", MySqlDbType.Int32);
                //Присваиваем параметрам значение
                cmd.Parameters["@ci"].Value = newCardId;
                cmd.Parameters["@ei"].Value = employeeId;

                cmd.ExecuteNonQuery();
                //int Id = int.Parse(cmd.ExecuteScalar().ToString());

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка!");
            }
            finally
            {
                connection.Open();
            }
        }

        public int AuthorizeUser(string login, string password)
        {
            try
            {
                //шифруем пароль
                password = Utils.sha256(password);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT employee_ID FROM employee WHERE employee_login= @lg and employee_pass= @ps", connection.SqlConnection);

                //Определяем параметры
                cmd.Parameters.Add("@lg", MySqlDbType.VarChar, 25);
                cmd.Parameters.Add("@ps", MySqlDbType.VarChar, 25);
                //Присваиваем параметрам значение
                cmd.Parameters["@lg"].Value = login;
                cmd.Parameters["@ps"].Value = password;

                int Id = int.Parse(cmd.ExecuteScalar().ToString());

                connection.Close();
                return Id;
            }
            catch
            {
                throw new Exception("Ошибка авторизации");
            }
            finally
            {
                connection.Open();
            }
        }

        public void CastToVoidEmployee(EmployeeData data)
        {
            try
            {
                connection.Open();

                string sql = $"SELECT tasks.tasks_ID FROM tasks WHERE tasks.tasks_owner_employee_ID={data.EmployeeId}";
                string sqlTask1 = "DELETE task_executant_group FROM task_executant_group WHERE task_executant_group.tasks_ID=";
                string sqlTask2 = $"DELETE task_executant_group FROM task_executant_group WHERE task_executant_group.executant_employee_ID={data.EmployeeId}";
                string sqlTask3 = $"DELETE tasks FROM tasks WHERE tasks.tasks_owner_employee_ID={data.EmployeeId}";
                string sqlFinalDeleteTask = $"DELETE employee, people, cards FROM cards JOIN employee ON employee_ID=cards_employee_ID JOIN people ON employee_people_ID=people_ID WHERE employee_ID ={data.EmployeeId}";
               
                MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<int> targets = new List<int>();

                while(reader.Read())
                {
                    targets.Add(reader.GetInt32(0));

                }
                reader.Close();

                foreach(int id in targets)
                {
                    MySqlCommand localCmd = new MySqlCommand($"{sqlTask1}{id}", connection.SqlConnection);
                    localCmd.ExecuteNonQuery();
                }

                cmd = new MySqlCommand(sqlTask2, connection.SqlConnection);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(sqlTask3, connection.SqlConnection);
                cmd.ExecuteNonQuery();
                
                cmd = new MySqlCommand(sqlFinalDeleteTask, connection.SqlConnection);
                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                DataLog.Log.AddLog(new Event(ex.Message, EventPriority.Error));
            }
        }

        public void CastToVoidRole(int roleId)
        {
            connection.Open();
            string sql = $"DELETE FROM role WHERE role_ID = {roleId}";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            cmd.ExecuteNonQuery();
        }

        public void ChageRoleData(int roleId, string roleTitle, int nAccessLeveL)
        {
            connection.Open();
            string sql = $"UPDATE role SET role_title='{roleTitle}', role_lvl={nAccessLeveL} WHERE role_ID = {roleId}";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            cmd.ExecuteNonQuery();

            connection.Close();
        } 
        public void UpdateTable(string tableID, string mark, bool OpenConnection = true)
        {
            if(OpenConnection)
            connection.Open();
            string sql = $"UPDATE `table` SET table_feature='{mark}' WHERE table_ID={tableID}";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            cmd.ExecuteNonQuery();

            if(OpenConnection)
            connection.Close();
        }
        public void CreateTable(string mark, string employeeID, string date, bool OpenConnection = true)
        {
            if(OpenConnection)
            connection.Open();
            string sql = $"INSERT INTO `table` (table_feature, table_employee, table_date) VALUES ('{mark}', {employeeID}, '{date}')";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            cmd.ExecuteNonQuery();

            if(OpenConnection)
            connection.Close();
        }

        public void HistroyCreateNew(DateTime dt, string desc, int? docsId, bool openConnection = true)
        {
            if(openConnection)
                connection.Open();

            string docsIdPlaceHolder;
            if  (docsId == null)
            {
                docsIdPlaceHolder = "NULL";
            }
            else
            {
                docsIdPlaceHolder = $"{docsId.Value}";
            }

            string sql = $"INSERT history(histroy_date, history_description, history_docs_ID) VALUES('{FormatDateToSql(dt)}', '{desc}', {docsIdPlaceHolder})";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            
            cmd.ExecuteNonQuery();

            if  (openConnection)
                connection.Close();
        }

        public int DocsCreate(int docsType, int docsOwnerId, int docsEmployeeId, DateTime firstDay, DateTime secondDay, string descFirst, string descSecond, bool openConnection = true)
        {
            if (openConnection)
                connection.Open();

            string sql = $"INSERT docs(docs_type, docs_owner_ID, docs_employe_ID, docs_firstDay, docs_secondDay) VALUES({docsType},{docsOwnerId}, {docsEmployeeId}, '{FormatDateToSql(firstDay)}', '{FormatDateToSql(secondDay)}', '{descFirst}', '{descSecond}')";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand("SELECT LAST_INSERT_ID();");
            string id = cmd.ExecuteScalar();

            if (openConnection)
                connection.Close();

            return int.Parse(id);
        }

        public void HistoryCreate(string owner, string id, string name, DateTime date, string desc, string desc2, bool openConnection = true)
        {
            if(openConnection)
            connection.Open();
            MySqlCommand cmd;
          
            string sql = $"INSERT INTO history (history_owner, history_emp_ID, history_empl_name, history_date, history_description) VALUES ('{owner}', {id}, '{name}', '{FormatOnlyDateToSql(date)}', '{desc}', '{desc2}')";
            cmd = new MySqlCommand(sql, connection.SqlConnection);
            
            cmd.ExecuteNonQuery();

            if (openConnection)
            connection.Close();
        }

        public void LoadHistroyNew(List<HistroyInfo> res)
        {
            MySqlCommand cmd;
            connection.Open();

            string sql = $"SELECT FROM history";
            cmd = new MySqlCommand(sql, connection.SqlConnection);

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                HistroyInfo inf = new HistroyInfo();
                inf.id = row.ItemArray[0].ToString();
                inf.owner = row.ItemArray[1].ToString();
                inf.employee_target_ID = row.ItemArray[2].ToString();
                inf.targetName = row.ItemArray[3].ToString();
                inf.date = row.ItemArray[4].ToString();
                inf.desc = row.ItemArray[5].ToString();

                res.Add(inf);
            }

            connection.Close();

        }

        public void LoadHistroy(List<HistroyInfo> res)
        {
            MySqlCommand cmd;
            connection.Open();

            string sql = $"SELECT history_ID, history_owner, history_emp_ID, history_empl_name, history_date, history_description FROM history";
            cmd = new MySqlCommand(sql, connection.SqlConnection);

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            foreach(DataRow row in table.Rows)
            {
                HistroyInfo inf = new HistroyInfo();
                inf.id = row.ItemArray[0].ToString();
                inf.owner = row.ItemArray[1].ToString();
                inf.employee_target_ID = row.ItemArray[2].ToString();
                inf.targetName = row.ItemArray[3].ToString();
                inf.date = row.ItemArray[4].ToString();
                inf.desc = row.ItemArray[5].ToString();

                res.Add(inf);
            }

            connection.Close();
        }

        public void LoadHistroy(List<HistroyInfo> res, int employee_ID)
        {
            MySqlCommand cmd;
            connection.Open();

            string sql = $"SELECT history_ID, history_owner, history_empl_name, history_date, history_description FROM history WHERE history_emp_ID={employee_ID}";
            cmd = new MySqlCommand(sql, connection.SqlConnection);

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                HistroyInfo inf = new HistroyInfo();
                inf.id = row.ItemArray[0].ToString();
                inf.owner = row.ItemArray[1].ToString();
                inf.targetName = row.ItemArray[2].ToString();
                inf.date = row.ItemArray[3].ToString();
                inf.desc = row.ItemArray[4].ToString();
                inf.employee_target_ID = employee_ID.ToString();

                res.Add(inf);
            }

            connection.Close();
        }

        public void ChangeUserStatusUser(int id, int status)
        {
            connection.Open();
            string sql = $"UPDATE employee SET employee_status={status} WHERE employee_ID = {id}";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            cmd.ExecuteNonQuery();

            connection.Close();

        }

        public int CreateTask(string Title, string OwnerId, string Desc, DateTime LastDate, string StatusId)
        {
            connection.Open();

            string date = FormatDateToSql(LastDate);

            string sql = $"INSERT INTO tasks(tasks_title, tasks_owner_employee_ID, tasks_description, tasks_lastday, tasks_st_ID, tasks_status) VALUES ('{Title}', {OwnerId}, '{Desc}', '{date}', 3, {StatusId})";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            cmd.ExecuteNonQuery();
            
            connection.Close();

            return GetTaskId();
        }

        public int GetTaskId()
        {
            connection.Open();

            string sql = $"SELECT LAST_INSERT_ID()";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            int res = Convert.ToInt32(cmd.ExecuteScalar());

            connection.Close();
            return res;
        }

        public void AddExecutant(string[] EmployeeID, string TaskID)
        {
            MySqlCommand cmd;
            connection.Open();

            foreach(string id in EmployeeID)
            {
                string sql = $"INSERT INTO task_executant_group(tasks_ID, executant_employee_ID) VALUES ('{TaskID}', '{id}')";
                cmd = new MySqlCommand(sql, connection.SqlConnection);
                cmd.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void RemoveExecutant(string TaskID)
        {
            MySqlCommand cmd;
            connection.Open();

            string sql = $"DELETE FROM task_executant_group WHERE tasks_ID={TaskID}";
            cmd = new MySqlCommand(sql, connection.SqlConnection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void LoadAllTask(List<TaskBDInfo> res)
        {
            MySqlCommand cmd;
            connection.Open();

            string sql = $"SELECT tasks_ID, tasks_owner_employee_ID, tasks_description, tasks_st_ID, tasks_status, tasks_lastday, tasks_title FROM tasks";
            cmd = new MySqlCommand(sql, connection.SqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                TaskBDInfo taskBDInfo = new TaskBDInfo();
                taskBDInfo.id = reader[0].ToString();
                taskBDInfo.ownerId = reader[1].ToString();
                taskBDInfo.description = reader[2].ToString();
                taskBDInfo.tasksState = reader[3].ToString();
                taskBDInfo.tasksStatus = reader[4].ToString();
                taskBDInfo.lastDay = reader[5].ToString();
                taskBDInfo.taskTitle = reader[6].ToString();

                res.Add(taskBDInfo);
            }

            reader.Close();

            for(int i = 0; i < res.Count; i++)
            {
                sql = $"SELECT people_name, people_surname FROM people JOIN employee ON employee_people_ID=people_ID WHERE employee_ID={res[i].ownerId}";
                cmd = new MySqlCommand(sql, connection.SqlConnection);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string fullname = $"{reader[0].ToString()} {reader[1].ToString()}";
                    TaskBDInfo m = res[i];
                    m.ownerFullName = fullname;

                    res[i]= m;
                }

                reader.Close();
            }

            connection.Close();
        }

        public void LoadEmployees(List<EmployeeData> employees, int id)
        {
            connection.Open();

            string sql = $"SELECT executant_ID, people_name, people_surname, employee_ID FROM task_executant_group JOIN employee ON employee_ID=executant_employee_ID JOIN people ON employee_people_ID=people_ID WHERE tasks_ID = {id}";
            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    EmployeeData worker = new EmployeeData();

                    worker.name = reader[1].ToString();
                    worker.surname = reader[2].ToString();
                    worker.EmployeeId = reader.GetInt32(3);

                    employees.Add(worker);
                }
            reader.Close();

            connection.Close();
        }

        public void LoadFullTasksForEmployee(List<TaskBDFullInfo> res, int employeeID)
        {
            connection.Open();

            List<TaskBDFullInfo> tmp = new List<TaskBDFullInfo>();

            string sql = $"SELECT tasks_ID, tasks_owner_employee_ID, tasks_description, tasks_st_ID, tasks_status, tasks_lastday, tasks_title FROM tasks";
            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TaskBDFullInfo taskBDInfo = new TaskBDFullInfo();
                taskBDInfo.id = reader[0].ToString();
                taskBDInfo.ownerId = reader[1].ToString();
                taskBDInfo.description = reader[2].ToString();
                taskBDInfo.tasksState = reader[3].ToString();
                taskBDInfo.tasksStatus = reader[4].ToString();
                taskBDInfo.lastDay = reader[5].ToString();
                taskBDInfo.taskTitle = reader[6].ToString();

                tmp.Add(taskBDInfo);
            }

            reader.Close();

            for (int i = 0; i < tmp.Count; i++)
            {
                sql = $"SELECT people_name, people_surname FROM people JOIN employee ON employee_people_ID=people_ID WHERE employee_ID={tmp[i].ownerId}";
                cmd = new MySqlCommand(sql, connection.SqlConnection);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string fullname = $"{reader[0].ToString()} {reader[1].ToString()}";
                    TaskBDFullInfo inf = tmp[i];
                    inf.ownerFullName = fullname;
                    tmp[i] = inf;
                }

                reader.Close();
            }

            bool isFindWorker = false;
            for (int i = 0; i < tmp.Count; i++)
            {
                List<EmployeeData> workers = new List<EmployeeData>();
                sql = $"SELECT executant_ID, people_name, people_surname, employee_ID FROM task_executant_group JOIN employee ON employee_ID=executant_employee_ID JOIN people ON employee_people_ID=people_ID WHERE tasks_ID = {tmp[i].id}";
                cmd = new MySqlCommand(sql, connection.SqlConnection);
                reader = cmd.ExecuteReader();

                isFindWorker = false;
                while (reader.Read())
                {
                    EmployeeData worker = new EmployeeData();

                    worker.name = reader[1].ToString();
                    worker.surname = reader[2].ToString();
                    worker.EmployeeId = reader.GetInt32(3);

                    if(worker.EmployeeId == employeeID)
                    {
                        isFindWorker = true;
                    }

                    workers.Add(worker);
                }

                TaskBDFullInfo inf = tmp[i];
                inf.employees = workers;
                tmp[i] = inf;
                if (isFindWorker)
                {
                    res.Add(tmp[i]);
                }
                reader.Close();
            }

            connection.Close();

        }

        public void LoadFullTasks(List<TaskBDFullInfo> res)
        {
            connection.Open();

            string sql = $"SELECT tasks_ID, tasks_owner_employee_ID, tasks_description, tasks_st_ID, tasks_status, tasks_lastday, tasks_title FROM tasks";
            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TaskBDFullInfo taskBDInfo = new TaskBDFullInfo();
                taskBDInfo.id = reader[0].ToString();
                taskBDInfo.ownerId = reader[1].ToString();
                taskBDInfo.description = reader[2].ToString();
                taskBDInfo.tasksState = reader[3].ToString();
                taskBDInfo.tasksStatus = reader[4].ToString();
                taskBDInfo.lastDay = reader[5].ToString();
                taskBDInfo.taskTitle = reader[6].ToString();

                res.Add(taskBDInfo);
            }

            reader.Close();

            for (int i = 0; i < res.Count; i++)
            {
                sql = $"SELECT people_name, people_surname FROM people JOIN employee ON employee_people_ID=people_ID WHERE employee_ID={res[i].ownerId}";
                cmd = new MySqlCommand(sql, connection.SqlConnection);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string fullname = $"{reader[0].ToString()} {reader[1].ToString()}";
                    TaskBDFullInfo inf = res[i];
                    inf.ownerFullName = fullname;
                    res[i] = inf;
                }

                reader.Close();
            }

            for (int i = 0; i < res.Count; i++)
            {
                List<EmployeeData> workers = new List<EmployeeData>();
                sql = $"SELECT executant_ID, people_name, people_surname, employee_ID FROM task_executant_group JOIN employee ON employee_ID=executant_employee_ID JOIN people ON employee_people_ID=people_ID WHERE tasks_ID = {res[i].id}";
                cmd = new MySqlCommand(sql, connection.SqlConnection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeData worker = new EmployeeData();

                    worker.name = reader[1].ToString();
                    worker.surname = reader[2].ToString();
                    worker.EmployeeId = reader.GetInt32(3);

                    workers.Add(worker);
                }

                TaskBDFullInfo inf = res[i];
                inf.employees = workers;
                res[i] = inf;
                reader.Close();
            }

            connection.Close(); 

        }

        public void UpdateTask(int taskId, string desc, DateTime lastDate, string title, string status, string state)
        {
            connection.Open();

            string date = FormatDateToSql(lastDate);
            string sql = $"UPDATE tasks SET tasks_title='{title}', tasks_description='{desc}',  tasks_lastday='{date}', tasks_st_ID={state}, tasks_status={status} WHERE tasks_ID={taskId}";

            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            cmd.ExecuteNonQuery();

            connection.Close();

        }

        public void LoadUserOnWorkShift(List<EmployeeData> emps, int workShiftType)
        {
            connection.Open();

            string sql = $"SELECT employee.employee_ID, people.people_name, people.people_surname, role.role_title FROM people JOIN employee ON people.people_ID=employee.employee_people_ID JOIN role ON employee.employee_role_ID=role.role_ID WHERE employee.employee_work_shift = {workShiftType}";
            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                EmployeeData employee = new EmployeeData();
                employee.EmployeeId = reader.GetInt32(0);
                employee.name = reader[1].ToString();
                employee.surname = reader[2].ToString();
                employee.roleTitle = reader[3].ToString();

                emps.Add(employee);
            }

            connection.Close();
        }

        public enum FillTableType
        {
            Weekend,
            Medical
        }

        public void AddFillTable(string desc, string owner, string employeeName, int employeeId, DateTime firstDate, DateTime endDateTime, FillTableType type)
        {
            string placeHolder;
            string historyMessage;

            switch (type)
            {
                case FillTableType.Weekend:
                    placeHolder = "О";
                    historyMessage = $"внесен отпуск с {firstDate.ToString()} по {endDateTime.ToString()} по причине {desc}";
                    break;

                case FillTableType.Medical:
                    placeHolder = "Б";
                    historyMessage = $"внесен больничный с {firstDate.ToString()} по {endDateTime.ToString()} по причине {desc}";
                    break;

                default:
                    throw new Exception("неизвестный тип!");
            }

            connection.Open();

            for (DateTime dt = firstDate; dt <= endDateTime; dt = dt.AddDays(1))
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT `table`.table_ID FROM `table` JOIN employee ON employee.employee_ID=`table`.table_employee WHERE employee.employee_ID={employeeId} AND `table`.`table_date` LIKE '{FormatOnlyDateToSql(dt)}%' ",connection.SqlConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = cmd;
                adapter.Fill(table);

                if(table.Rows.Count > 0)
                {
                    string tableID = table.Rows[0].ItemArray[0].ToString();
                    UpdateTable(tableID, placeHolder, false);
                }
                else
                {
                    CreateTable(placeHolder, employeeId.ToString(), FormatDateToSql(dt), false);
                }
            }


            HistoryCreate(owner, employeeId.ToString(), employeeName, DateTime.Now, historyMessage, false);
            connection.Close();
        }
        public void LoadTasksForTheUser(int employeeId, List<Task> res)
        {
            string sql = $"SELECT tasks_ID, tasks_owner_employee_ID, tasks_description, tasks_st_ID, tasks_status, tasks_lastday, tasks_title FROM tasks JOIN task_executant_group ON tasks.tasks_ID=task_executant_group.tasks_ID WHERE task_executant_group.executant_employee_ID={employeeId};";
        }

        public int[] LoadAllTableFeature(int id)
        {
            string sql = $"SELECT table_feature FROM `table` WHERE table_employee = {id}";

            connection.Open();
            MySqlCommand cmd = new MySqlCommand(sql, connection.SqlConnection);

            MySqlDataReader reader = cmd.ExecuteReader();

            int[] res = new int[5];
            res[0] = 0;
            res[1] = 0;
            res[2] = 0;
            res[3] = 0;
            res[4] = 0;


            while (reader.Read())
            {
                string mark = reader[0].ToString();

                if (char.IsDigit(mark[0]))
                {
                    res[4] += 1;
                }
                else if(mark == "Н")
                {
                    res[0] += 1;
                }
                else if(mark == "О")
                {
                    res[1] += 1;
                }
                else if(mark == "В")
                {
                    res[3] += 1;
                }
                else if (mark == "Б")
                {
                    res[2] += 1;
                }
            }

            reader.Close();

            connection.Close();

            return res;
        }

    }
}
