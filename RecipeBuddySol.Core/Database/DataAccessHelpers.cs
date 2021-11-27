using System;
using System.Data.SQLite;

namespace RecipeBuddy.Core.Database
{
    public static class DataAccessHelpers
    {

        public static string LoadConnectionString(string id = "DataSource")
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}RecipeManagerDB.db";

            SQLiteConnectionStringBuilder sQLiteConnectionStringBuilder = new SQLiteConnectionStringBuilder();
            sQLiteConnectionStringBuilder.Version = 3;
            sQLiteConnectionStringBuilder.DataSource = DbPath;
            return sQLiteConnectionStringBuilder.ConnectionString;
        }

    }
}
