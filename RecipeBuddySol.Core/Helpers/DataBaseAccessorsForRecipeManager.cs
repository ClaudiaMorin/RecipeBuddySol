using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RecipeBuddy.Core.Database;
using RecipeBuddy.Core.Scrapers;
using System;

namespace RecipeBuddy.Core.Helpers
{
    public static class DataBaseAccessorsForRecipeManager
    {
        public static UserDBModel UserDBModelAccessor { get; private set; }

        public static object ListOfUserAccountsInDB { get; private set; }

        /// <summary>
        /// Creates a dictionary of the properties in the recipe for insertion into the DB
        /// </summary>
        /// <returns></returns>
        public static void ConvertRecipeToDictionaryForDBInsertion(Dictionary<string, object> dictionaryValuePairs, List<string> paramsForSQLStatment, RecipeRecordModel recipeCard, int UserIDInDB)
        {
            Dictionary<string, object> recipeEntries;
            recipeEntries = GetRecipeDictionaryForDBFunctions(recipeCard, UserIDInDB);

            string temp = "";
            //Shuffle through and determin if there is actually a value in the entry.  
            foreach (KeyValuePair<string, object> entry in recipeEntries)
            {
                if (entry.Value != null)
                {
                    dictionaryValuePairs.Add(entry.Key, entry.Value);
                    temp = entry.Key.Substring(1);
                    paramsForSQLStatment.Add(temp);
                }
            }
        }

        /// <summary>
        /// Creates a dictionary of the properties in the recipe for insertion into the DB
        /// </summary>
        /// <returns></returns>
        public static void ConvertRecipeToDictionaryForDBUpdate(Dictionary<string, object> dictionaryValuePairs, List<string> paramsForSQLStatment, RecipeRecordModel recipeCard)
        {
            Dictionary<string, object> recipeEntries;
            recipeEntries = GetRecipeDictionaryForDBUpdate(recipeCard);

            string temp = "";
            //Shuffle through and determin if there is actually a value in the entry.  
            foreach (KeyValuePair<string, object> entry in recipeEntries)
            {
                if (entry.Value != null)
                {
                    dictionaryValuePairs.Add(entry.Key, entry.Value);
                    temp = entry.Key.Substring(1);
                    paramsForSQLStatment.Add(temp);
                }
            }
        }

        /// <summary>
        /// Provides a full dictionary of all the recipe properties to be added to the DB
        /// </summary>
        /// <returns>empty dictionary of all the recipe properties</returns>
        public static Dictionary<string, object> GetRecipeDictionaryForDBFunctions(RecipeRecordModel recipeCard, int UserIDinDB)
        {
            string ingredients = StringManipulationHelper.TurnListIntoStringForDB(recipeCard.ListOfIngredientStrings);
            string directions = StringManipulationHelper.TurnListIntoStringForDB(recipeCard.ListOfDirectionStrings);

            Dictionary<string, object> recipeDictionary = new Dictionary<string, object>
            {
                { "@Title", recipeCard.Title },
                { "@Author",  recipeCard.Author },
                { "@TypeAsInt", recipeCard.TypeAsInt },
                { "@StringOfIngredientForListFromDB", ingredients},
                { "@StringOfDirectionsForListFromDB", directions },
                { "@UserID", UserIDinDB}
            };

            return recipeDictionary;
        }


        /// <summary>
        /// Provides a full dictionary of all the recipe properties needed for a recipe update
        /// </summary>
        /// <returns>empty dictionary of all the recipe properties needed for the update</returns>
        public static Dictionary<string, object> GetRecipeDictionaryForDBUpdate(RecipeRecordModel recipeCard)
        {
            string ingredients = StringManipulationHelper.TurnListIntoStringForDB(recipeCard.ListOfIngredientStrings);
            string directions = StringManipulationHelper.TurnListIntoStringForDB(recipeCard.ListOfDirectionStrings);

            Dictionary<string, object> recipeDictionary = new Dictionary<string, object>
            {
                { "@Title", recipeCard.Title },
                { "@Author",  recipeCard.Author },
                { "@TypeAsInt", recipeCard.TypeAsInt },
                { "@StringOfIngredientForListFromDB", ingredients},
                { "@StringOfDirectionsForListFromDB", directions },
            };

            return recipeDictionary;
        }


        /// <summary>
        /// Gets a list of all of the items that need to be queried from the Recipies DB table
        /// </summary>
        /// <returns>a list of all of the items that need to be queried from the Recipies DB table</returns>
        public static List<string> GetListOfRecipeColumnsForDBQuery()
        {
            List<string> recipeColumnList = new List<string>()
            {
                { "Title"},
                { "Author"},
                { "RecipeID" },
                { "TypeAsInt" },
                { "ListOfIngredientStringsForDisplay"},
                { "ListOfDirectionStringsForDisplay" },
            };

            return recipeColumnList;
        }

        /// <summary>
        /// Creates the string of values that are going to be inserted into the DB
        /// </summary>
        /// <param name="UserID">The User Key that the strings are associated with</param>
        /// <param name="recipeCard">The recipe that is being saved</param>
        public static int SaveRecipeToDatabase(RecipeDisplayModel recipeCard, int UserIDInDB)
        {
            //so that the correct type will be saved to the DB
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            List<string> paramsForSQLStatment = new List<string>();

            RecipeRecordModel recipeRecordModel = new RecipeRecordModel(recipeCard);
            //Put the new users information into a dictionary which will be part of the SQL query
            ConvertRecipeToDictionaryForDBInsertion(parameters, paramsForSQLStatment, recipeRecordModel, UserIDInDB);

            string sqlString1 = "insert into Recipes (";
            string sqlString2 = " values (";

            for (int count = 0; count < paramsForSQLStatment.Count; count++)
            {
                //last part of the loop!
                if (count == parameters.Count - 1)
                {
                    sqlString1 += paramsForSQLStatment[count];
                    sqlString2 += "@" + paramsForSQLStatment[count];
                    sqlString1 = sqlString1 + ") " + sqlString2 + ") ";
                }
                else
                {
                    sqlString1 += paramsForSQLStatment[count] + ", ";
                    sqlString2 += "@" + paramsForSQLStatment[count] + ", ";
                }
            }

            //save Recipe to DB
            SqliteDataAccess.UpdateData(sqlString1, parameters);
            return DataBaseAccessorsForRecipeManager.GetPKeyofMostRecientlyAddedRecipeFromDB();
        }

        /// <summary>
        /// used to remove a recipe from the database, either because the user
        /// has selected to remove it or because we need to replace it with an edited version
        /// </summary>
        /// <param name="RecipeIDInDB">The ID of the recipe to delete</param>
        public static void DeleteRecipeFromDatabase(int RecipeIDInDB)
        {

            //Recipe can't be found so we can't delete, ignore.
            if (RecipeIDInDB == -1)
            {

                Dictionary<string, object> dictionaryforQuery2 = new Dictionary<string, object>
                {
                    {"@RecipeID", RecipeIDInDB }
                };

                string sqlStatment = "delete from Recipes Where RecipeID= @RecipeID";
                SqliteDataAccess.UpdateData(sqlStatment, dictionaryforQuery2);
            }
        }

        /// <summary>
        /// used to update a recipe in the database
        /// </summary>
        /// <param name="recipeCard">The recipe we are updating</param>
        public static void UpdateRecipeFromDatabase(RecipeDisplayModel recipeCard, int UserIDInDB)
        {
            //We need a valid RecipeID that exists in the DB
            if (recipeCard.RecipeDBID != -1)
            {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                List<string> paramsForSQLStatment = new List<string>();

                RecipeRecordModel recipeRecordModel = new RecipeRecordModel(recipeCard);
                //Put the new users information into a dictionary which will be part of the SQL query
                ConvertRecipeToDictionaryForDBUpdate(parameters, paramsForSQLStatment, recipeRecordModel);

                string sqlString1 = "UPDATE Recipes SET ";

                //Shortened loop from the add recipe because we don't need the UserID for an update
                for (int count = 0; count < paramsForSQLStatment.Count; count++)
                {
                    //last part of the loop! Removing the last entry because the UserID isn't changing so we don't need to send it in.
                    if (count == parameters.Count - 1)
                    {
                        sqlString1 += paramsForSQLStatment[count] + " = " + "@" + paramsForSQLStatment[count];
                    }
                    else
                    {
                        sqlString1 += paramsForSQLStatment[count] + " = " + "@" + paramsForSQLStatment[count] + " , ";
                    }
                }

                //Update Recipe in DB
                string sqlStatment = sqlString1 + " WHERE RecipeID = " + recipeRecordModel.RecipeDBID;
                SqliteDataAccess.UpdateData(sqlStatment, parameters);
            }
        }

        /// <summary>
        /// Get the DB id of the most reciently added recipe
        /// so that you an increment the count and add a new one
        /// </summary>
        /// <returns></returns>
        public static int GetPKeyofMostRecientlyAddedRecipeFromDB()
        {
            string sql = "SELECT Max(RecipeID) FROM Recipes";
            int id = 0;

            Dictionary<string, object> RecipeIDFromDB = new Dictionary<string, object>
            {
                {"@RecipeID", id },
            };

            List<int> RecipeID = SqliteDataAccess.LoadData<int>(sql, RecipeIDFromDB);

            if (RecipeID.Count != 0)
                return RecipeID[0];

            return -1;
        }


        /// <summary>
        /// Get the users ID from the DB
        /// </summary>
        /// <param name="user">The user name</param>
        /// <returns></returns>
        public static int GetUserIDUserFromDatabase(string user)
        {
            int retInt = -1;
            string sql = "select ID from Users Where  Name='" + user + "'";

            UserDBModel userModel = new UserDBModel();

            Dictionary<string, object> userFromDB = new Dictionary<string, object>
            {
                {"@ID", userModel.ID },
            };

            List<UserDBModel> userDBModels = SqliteDataAccess.LoadData<UserDBModel>(sql, userFromDB);
            try
            {
                if (userDBModels[0] != null)
                {
                    retInt = userDBModels[0].ID;
                }
            }
            catch (Exception e)
            {
                return -1;
            }

            return retInt;
        }

        /// <summary>
        /// Lodes the user-specific information from the DB, adaptation because UWP doesn't allow secure strings
        /// </summary>
        /// <param name="PasswordSecureString">users password as a secure string</param>
        /// <param name="user">the username</param>
        /// <returns>The user ID that is held in the DB</returns>
        //public static string LoadUserFromDatabase(SecureString PasswordSecureString, string user)
        public static int LoadUserFromDatabase(string PasswordString, string user)
        {
            //string user = SelectedComboBoxItem;
            //SecureString password = PasswordSecureString;
            string sql = "select * from Users Where  Name='" + user + "'";

            UserDBModel userModel = new UserDBModel();

            Dictionary<string, object> userFromDB = new Dictionary<string, object>
           {
                {"@Name", userModel.Name },
                {"@ID", userModel.ID },
                {"@Password", userModel.Password },
           };

            List<UserDBModel> userDBModels = SqliteDataAccess.LoadData<UserDBModel>(sql, userFromDB);

            if (userDBModels[0] != null)
            {
                byte[] bytePassword = PasswordHashing.CalculateHash(ConvertingStringToByteArray.ConvertStringToByteArray(PasswordString));

                //do the two passwords match?
                if (PasswordHashing.SequenceEquals(bytePassword, userDBModels[0].Password) == true)
                {
                    //PasswordSecureString.Clear();
                    PasswordString = null;
                    //return userDBModels[0].Name.ToString();
                    return userDBModels[0].ID;
                }
                else
                {
                     //new Windows.UI.Popups.MessageDialog("Invalid Password!").ShowAsync();
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the User's Saved Recipes from the DB and sends them to the TreeViewModel
        /// </summary>
        public static List<RecipeRecordModel> LoadUserDataByID(string username, int userID)
        {
            string UsersIDInDB = userID.ToString();

            Dictionary<string, object> dictionaryUserRecipesFromDB = new Dictionary<string, object>
            {
                 {"@UserID", UsersIDInDB },
            };

            //Get the stored recipies and connect that to the TreeView
            string sql = "SELECT * FROM Recipes WHERE UserID = @UserID";
            List<RecipeRecordDBModel> recipeDBModels = SqliteDataAccess.LoadData<RecipeRecordDBModel>(sql, dictionaryUserRecipesFromDB);
            List<RecipeRecordModel> recipeModels = new List<RecipeRecordModel>();

            foreach (var record in recipeDBModels)
            {
                RecipeRecordModel RRC = new RecipeRecordModel(record.StringOfIngredientForListFromDB, record.StringOfDirectionsForListFromDB);
                RRC.Title = record.Title;
                if(record.Author.Length > 0)
                RRC.Author = record.Author;
                RRC.TypeAsInt = record.TypeAsInt;
                RRC.RecipeDBID = record.RecipeID;
                recipeModels.Add(RRC);
            }

            return recipeModels;
        }

        /// <summary>
        /// Populates the combobox pulldown with the users in the DB
        /// </summary>
        public static void LoadUsersFromDatabase(ObservableCollection<string> ListOfUserAccountsInDB)
        {
            ListOfUserAccountsInDB.Clear();

            string sql = "select Name from Users";
            UserDBModel userModel = new UserDBModel();

            Dictionary<string, object> usersFromDB = new Dictionary<string, object>
            {
                {"@Name", userModel.Name }
            };

            List<UserDBModel> userDBModels = SqliteDataAccess.LoadData<UserDBModel>(sql, usersFromDB);

            if (userDBModels.Count > 0)
            {
                for (int count = 0; count < userDBModels.Count; count++)
                {
                    ListOfUserAccountsInDB.Add(userDBModels[count].Name);
                }
            }
        }

        /// <summary>
        /// Saves a new user to the DB
        /// </summary>
        public static void SaveUserToDatabase(string NewAccountName, string UserName, byte[] bytePassword, byte[] bytePasswordCheck)
        {
            //byte[] bytePassword = PasswordHashing.CalculateHash(SecureStringToByteArray.ConvertSecureStringToByteArray(NewPasswordSecureString));
            //byte[] bytePasswordCheck = PasswordHashing.CalculateHash(SecureStringToByteArray.ConvertSecureStringToByteArray(NewConfirmPasswordSecureString));

            //Get the new users information from the UI
            UserDBModel userModel = new UserDBModel();
            userModel.Password = bytePassword;
            userModel.Name = NewAccountName.ToLower();

            string sqlInsertIntoUsers = "insert into Users (Name, Password) " +
                "values (@Name, @Password)";


            //Put the new users information into a dictionary which will be part of the SQL query
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Name", userModel.Name },
                {"@Password", userModel.Password },
            };

            //save user to User table in DB
            SqliteDataAccess.UpdateData(sqlInsertIntoUsers, parameters);

            //Reset the "Create Password" section to empty strings.
            NewAccountName = "";

            int userID = GetUserIDUserFromDatabase(userModel.Name);
            //check that we have a valid ID returned to us.
            if (userID != -1)
                LoadUserDataByID(UserName, userID);

        }

    }
}
