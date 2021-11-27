using Dapper;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace RecipeBuddy.Core.Database     
{
    public static class SqliteDataAccess
    {
        // LoadDate<PersonModel>("Select * from Person", null) = List<PersonModel>
        //somethign that should trigger a rebuild
        public static List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "DataSource")
        {
            //for Dapper
            DynamicParameters p = parameters.ToDynamicParameters();
            string myconnectionStr = DataAccessHelpers.LoadConnectionString(connectionName);
            IDbConnection cnn;

            try
            {
                SQLiteConnection myconnection = new SQLiteConnection(myconnectionStr);
                cnn = (IDbConnection)myconnection;

                using (cnn)
                {
                    var rows = cnn.Query<T>(sqlStatement, p);
                    return rows.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return new List<T>();
        }


        public static void UpdateData(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "DataSource")
        {
            //for Dapper
            DynamicParameters p = parameters.ToDynamicParameters();

            using (IDbConnection cnn = new SQLiteConnection(DataAccessHelpers.LoadConnectionString(connectionName)))
            {
                cnn.Execute(sqlStatement, p);
            }
        }

        public static DynamicParameters ToDynamicParameters(this Dictionary<string, object> p)
        {
            //for Dapper
            DynamicParameters output = new DynamicParameters();

            foreach (var param in p)
            {
                output.Add(param.Key, param.Value);
            }

            return output;
        }
    }
}
