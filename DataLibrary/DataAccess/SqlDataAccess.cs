using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLibrary.DataAccess
{
    class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "LoadTestDB") {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql) {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString())) {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static Models.UploadModel LoadSingleData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                var Query = cnn.Query<Models.UploadModel>(sql);

                if (Query == null || !Query.Any()) {
                    var obj = new Models.UploadModel();
                    obj.Id = -1;

                    return obj;
                }

                return Query.First();
            }
        }

        public static int SaveData<T>(string sql, T data) {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static bool DeleteData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql) > 0;
            }
        }

    }
}
