using MySql.Data.MySqlClient;
using PR52.Classes.Common;
using System;
using System.Collections.Generic;

namespace PR52.Classes.Contexts
{
    public class WorkContext
    {
        public WorkContext(int id, int idDiscipline, int idType, DateTime date, string name, int semester) : base(id, idDiscipline, idType, date, name, semester) { }
        public static List<WorkContext> AllWorks()
        {
            List<WorkContext> allWorks = new List<WorkContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader DBWorks = Connection.Query("Select * from `work` order by `Date`;", connection);
            while (DBWorks.Read())
            {
                allWorks.Add(new WorkContext(
                    DBWorks.GetInt32(0),
                    DBWorks.GetInt32(1),
                    DBWorks.GetInt32(2),
                    DBWorks.GetDateTime(3),
                    DBWorks.GetString(4),
                    DBWorks.GetInt32(5)));
            }
            Connection.CloseConnection(connection);
            return allWorks;
        }
    }
}
