using MySql.Data.MySqlClient;
using PR52.Classes.Common;
using PR52.Models;
using System.Collections.Generic;

namespace PR52.Classes.Contexts
{
    public class DisciplineContext : Discipline
    {
        public DisciplineContext(int id, string name, int idGroup) : base(id, name, idGroup) { }
        public static List<DisciplineContext> AllDisciplines()
        {
            List<DisciplineContext> allDisciplines = new List<DisciplineContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader DBDiscplines = Connection.Query("Select * from `discipline` order by `Name`;", connection);
            while (DBDiscplines.Read())
            {
                allDisciplines.Add(new DisciplineContext(
                    DBDiscplines.GetInt32(0),
                    DBDiscplines.GetString(1),
                    DBDiscplines.GetInt32(2)));
            }
            Connection.CloseConnection(connection);
            return allDisciplines;
        }
    }
}
