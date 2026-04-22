using Microsoft.Data.SqlClient;

namespace SUEMS.Data
{
    public class Database
    {
        private string connectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SUEMS_DB;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}