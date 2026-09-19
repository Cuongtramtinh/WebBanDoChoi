using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ToySmart
{
    public class DB
    {
        private static string connectionString =
            "Server=localhost;Database=dtdochoi;Uid=root;Pwd=Cuong22122004@";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch   
            {
                return false;
            }
        }
    }
}