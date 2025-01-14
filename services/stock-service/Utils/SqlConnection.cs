using System.Data;
using MySqlConnector;

namespace stock;

public static class SqlConnection
{
    public static IDbConnection GetConnection()
    {
        return new MySqlConnection("Server=localhost,3306;Database=inventory;Uid=root;Pwd=humanities@best-password.ByFar2025;");
    }
}
