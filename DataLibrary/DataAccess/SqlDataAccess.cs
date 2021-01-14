using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess
{
    class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "DemoDb") {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}
