using System;
using Microsoft.Data.Sqlite;

namespace RecipeBuddy.Core.Database
{
    public static class DataAccessHelpers
    {

        public static string LoadConnectionString(string id = "DataSource")
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}RecipeManagerDB.db";

            SqliteConnectionStringBuilder sQLiteConnectionStringBuilder = new SqliteConnectionStringBuilder();
            sQLiteConnectionStringBuilder.DataSource = DbPath;
            return sQLiteConnectionStringBuilder.ConnectionString;
        }

    }
}
