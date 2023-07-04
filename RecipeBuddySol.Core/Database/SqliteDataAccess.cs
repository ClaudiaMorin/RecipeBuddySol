
using Microsoft.Data.Sqlite;
using RecipeBuddy.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Windows.System;

namespace RecipeBuddy.Core.Database     
{
    public static class SqliteDataAccess
    {
        public static List<List<object>> LoadData(string sqlStatement, int paramsPerRecord, Dictionary<string, object> parameters = null, string connectionName = "DataSource")
        {
            string myconnectionStr = DataAccessHelpers.LoadConnectionString(connectionName);

            List<List<object>> rows = new List<List<object>>();

            try
            {
                SqliteConnection myconnection = new SqliteConnection(myconnectionStr);
                myconnection.Open();
                SqliteCommand sqliteCommand = myconnection.CreateCommand();
                sqliteCommand.CommandText = sqlStatement;

                if (parameters != null)
                {
                    foreach (var key in parameters.Keys)
                    {
                        sqliteCommand.Parameters.AddWithValue(key, parameters[key]);
                    }
                }

                using (var reader = sqliteCommand.ExecuteReader())
                {
                    List<object> currRow;
                    object result = new object();
                    while (reader.Read())
                    {
                        currRow = new List<object>();
                        for (int count = 0; count < paramsPerRecord; count++)
                        {
                            result = reader.GetValue(count);
                            currRow.Add(result);
                        }
                        
                        rows.Add(currRow);
                    }   
                }

                myconnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return rows;
        }

        public static Byte[] GetPasswordFromDB(string userName, string connectionName = "DataSource")
        {
            Dictionary<string, object> myResults = new Dictionary<string, object>();
            string queryStr = "select Password from Users Where Name='" + userName.ToLower() + "'"; ;
            List<List<object>> Data = LoadData(queryStr, 1, myResults);
            Byte[] result = (byte[])Data[0][0];

            return result;

        }

        public static void UpdateData(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "DataSource")
        {
            string myconnectionStr = DataAccessHelpers.LoadConnectionString(connectionName);

            try 
            {
                SqliteConnection myconnection = new SqliteConnection(myconnectionStr);
                myconnection.Open();
                SqliteCommand sqliteCommand = myconnection.CreateCommand();
                sqliteCommand.CommandText = sqlStatement;

                //var rows = cnn.Query<T>(sqlStatement, p);
                if (parameters != null)
                {
                    foreach (var key in parameters.Keys)
                    {
                        sqliteCommand.Parameters.AddWithValue(key, parameters[key]);
                    }
                }

                int results = sqliteCommand.ExecuteNonQuery();
                myconnection.Close();
            }
            catch (Exception e)
            {
                string foo = e.Message;
            }
        }
    }
}
