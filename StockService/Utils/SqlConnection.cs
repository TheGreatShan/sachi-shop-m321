using System.Data;
using MySqlConnector;

namespace StockService.Utils;

public static class SqlConnection
{
    public static IDbConnection GetConnection()
    {
        // TODO - Replace with appsettings.json
        return new MySqlConnection("Server=localhost,3306;Database=inventory;Uid=root;Pwd=humanities@best-password.ByFar2025;");
    }
}