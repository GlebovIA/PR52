using MySql.Data.MySqlClient;
using PR52.Classes.Common;
using PR52.Models;
using System.Collections.Generic;

namespace PR52.Classes.Contexts
{
    public class GroupContext : Group
    {
        public GroupContext(int id, string name) : base(id, name) { }
        public static List<GroupContext> AllGroups()
        {
            List<GroupContext> allGroups = new List<GroupContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader DBGroup = Connection.Query("Select * from `group` order by `Name`;", connection);
            while (DBGroup.Read())
            {
                allGroups.Add(new GroupContext(
                    DBGroup.GetInt32(0),
                    DBGroup.GetString(1)));
            }
            Connection.CloseConnection(connection);
            return allGroups;
        }
    }
}
