using MySql.Data.MySqlClient;
using PR52.Classes.Common;
using System.Collections.Generic;

namespace PR52.Classes.Contexts
{
    public class EvaluationContext
    {
        public EvaluationContext(int id, int idWork, int idStudent, string value, string lateness) : base(id, idWork, idStudent, value, lateness) { }
        public static List<EvaluationContext> AllEvaluations()
        {
            List<EvaluationContext> allEvaluations = new List<EvaluationContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader DBEvaluation = Connection.Query("Select * from `evaluations`;", connection);
            while (DBEvaluation.Read())
            {
                allEvaluations.Add(new EvaluationContext(
                    DBEvaluation.GetInt32(0),
                    DBEvaluation.GetInt32(1),
                    DBEvaluation.GetInt32(2),
                    DBEvaluation.GetString(3),
                    DBEvaluation.GetString(4)));
            }
            Connection.CloseConnection(connection);
            return allEvaluations;
        }
    }
}
