using MySql.Data.MySqlClient;

namespace PR52.Classes.Common
{
    public class Connection
    {
        public static string config = "server=127.0.0.1;uid=root;pwd=;database=journal;";
        public static MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(config);
            connection.Open();
            return connection;
        }
        public static MySqlDataReader Query(string query, MySqlConnection connection) => new MySqlCommand(query, connection).ExecuteReader();
        public static void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
            MySqlConnection.ClearPool(connection);
        }
    }
}
