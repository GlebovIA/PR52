using MySql.Data.MySqlClient;
using PR52.Classes.Common;
using PR52.Models;
using System;
using System.Collections.Generic;

namespace PR52.Classes.Contexts
{
    public class StudentContext : Students
    {
        public StudentContext(int id, string name, string lastname, int idGroup, bool expelled, DateTime dateExpelled) : base(id, name, lastname, idGroup, expelled, dateExpelled) { }
        public static List<StudentContext> AllStudents()
        {
            List<StudentContext> allStudents = new List<StudentContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader DBStudents = Connection.Query("Select * from `student` order by `LastName`", connection);
            while (DBStudents.Read())
            {
                allStudents.Add(new StudentContext(
                    DBStudents.GetInt32(0),
                    DBStudents.GetString(1),
                    DBStudents.GetString(2),
                    DBStudents.GetInt32(3),
                    DBStudents.GetBoolean(4),
                    DBStudents.IsDBNull(5) ? DateTime.Now : DBStudents.GetDateTime(5)));
            }
            Connection.CloseConnection(connection);
            return allStudents;
        }
    }
}
