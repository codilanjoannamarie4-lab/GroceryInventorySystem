using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace GroceryInventorySystem
{
    public static class Database
    {
        // Reads the connection string named "MySqlConnection" from App.config
        public static string GetConnectionString()
        {
            var cs = ConfigurationManager.ConnectionStrings["MySqlConnection"];
            if (cs == null)
                throw new InvalidOperationException("Connection string 'MySqlConnection' not found in App.config");
            return cs.ConnectionString;
        }

        // Returns an unopened MySqlConnection. Caller should open and dispose it.
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }

        // Helper to test opening a connection. Returns true on success; false and an error message on failure.
        public static bool TestConnection(out string errorMessage)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    errorMessage = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
