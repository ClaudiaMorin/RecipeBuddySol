
using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Security;
using System;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Scrapers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Views;
using RecipeBuddy.Services;
using Microsoft.UI.Xaml.Controls;

namespace RecipeBuddy.ViewModels
{
    public sealed class UserViewModel : ObservableObject
    {
        public ObservableCollection<string> ListOfUserAccountsInDB { get; set; }
        public int checkBoxEnabledCount = 0;
        public int checkBoxEnabledCountNewUser = 0;
        public bool loggedin;
        public List<Type_of_Websource> PanelMap;

        Action ActionNoParams;
        Func<bool> FuncBool;

        private static readonly UserViewModel instance = new UserViewModel();
        public static UserViewModel Instance
        {
            get { return instance; }
        }

        private UserViewModel()
        {
            ListOfUserAccountsInDB = new ObservableCollection<string>();
            DataBaseAccessorsForRecipeManager.LoadUsersFromDatabase(ListOfUserAccountsInDB);
            PanelMap = new List<Type_of_Websource>();

            allRecipesCheckBoxChecked = false;
            allRecipesCheckBoxEnabled = true;

            epicuriousCheckBoxChecked = false;
            EpicuriousCheckBoxEnabled = true;

            foodAndWineCheckBoxChecked = false;
            FoodAndWineCheckBoxEnabled = true;

            foodNetworkCheckBoxChecked = false;
            FoodNetworkCheckBoxEnabled = true;

            southernLivingCheckBoxChecked = false;
            SouthernLivingCheckBoxEnabled = true;

            tastyCheckBoxChecked = false;
            TastyCheckBoxEnabled = true;

            foodAndWineCheckBoxChecked = false;
            FoodAndWineCheckBoxEnabled = true;

            passwordBoxEnabled = "true";
            PasswordString = "";
            canSelectLogin = false;
            canSelectLogout = false;
            canSelectCreateUser = false;
            loggedin = false;
            comboBoxIndexOfUserFromDB = 0;

            CmdLogoutBtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => LogOut(), FuncBool = () => CanSelectLogout);
            CmdLoginBtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => SetUpUser(), FuncBool = () => CanSelectLogin);
            CmdCreateUserbtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => SetUpNewUser(), FuncBool = () => CanSelectCreateUser);
        }


        /// <summary>
        /// We take in the users password and then set up the users account and load the tree-view with saved recipes if the
        /// account passwords match.
        /// </summary>
        public void SetUpUser()
        {
            string AccountName = ListOfUserAccountsInDB[ComboBoxIndexOfUserFromDB];

            UsersIDInDB = DataBaseAccessorsForRecipeManager.LoadUserFromDatabase(PasswordString, ListOfUserAccountsInDB[ComboBoxIndexOfUserFromDB]);
            if (AccountName.Length > 0)
            {
                SetUpSearchWebsources(false);
                loggedin = true;
                CanSelectLogout = true;
                CanSelectLogin = false;
                PasswordBoxEnabled = "false";
                //Set Up TreeView
                List<RecipeRecordModel> recipeRecords = DataBaseAccessorsForRecipeManager.LoadUserDataByID(AccountName, UsersIDInDB);
                MainNavTreeViewModel.Instance.AddRecipeModelsToTreeViewAsPartOfInitialSetup(recipeRecords);
                NavigationService.Navigate(typeof(SearchView));
            }
            else //user password didm't match
            {
                //PasswordSecureString = null;
            }
            PasswordString = "";
        }

        public void LogOut()
        {
            MainNavTreeViewModel.Instance.ClearTree();
            UsersIDInDB = -1;
            AccountName = "";
            UserName = "";
            NewAccountName = "";
            
            EpicuriousCheckBoxChecked = false;
            EpicuriousCheckBoxEnabled = true;

            AllRecipesCheckBoxChecked = false;
            AllRecipesCheckBoxEnabled = true;

            FoodNetworkCheckBoxChecked = false;
            FoodNetworkCheckBoxEnabled = true;

            SouthernLivingCheckBoxChecked = false;
            SouthernLivingCheckBoxEnabled = true;

            TastyCheckBoxChecked = false;
            TastyCheckBoxEnabled = true;

            FoodAndWineCheckBoxChecked = false;
            FoodAndWineCheckBoxEnabled = true;

            SearchViewModel.Instance.ResetViewModel();

            PanelMap.Clear();
            loggedin = false;
            CanSelectLogin = false;
            CanSelectLogout = false;
            PasswordBoxEnabled = "true"; 
        }


        public void SetUpNewUser()
        {
            SaveUser();
            //SetUpSearchWebsources(true);
            DataBaseAccessorsForRecipeManager.LoadUsersFromDatabase(ListOfUserAccountsInDB);
            ComboBoxIndexOfUserFromDB = ListOfUserAccountsInDB.Count - 1;
        }

        /// <summary>
        /// Saves a new user to the DB
        /// </summary>
        //private void SaveUser(string NewAccountName, string NewEmail, string UserName, SecureString NewPasswordSecureString, SecureString NewConfirmPasswordSecureString)
        private void SaveUser()
        {
            byte[] bytePassword = PasswordHashing.CalculateHash(ConvertingStringToByteArray.ConvertStringToByteArray(NewPasswordString));
            byte[] bytePasswordCheck = PasswordHashing.CalculateHash(ConvertingStringToByteArray.ConvertStringToByteArray(NewConfirmPasswordString));

            //Validate the account information that has been entered
            if (ValidateNewAccount(NewAccountName, bytePassword, bytePasswordCheck) == false)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("There is a problem with your account!");
                return;
            }

            DataBaseAccessorsForRecipeManager.SaveUserToDatabase(NewAccountName, UserName, bytePassword, bytePasswordCheck);

            //Reset the "Create Password" section to empty strings.
            NewAccountName = "";
            NewPasswordString = "";
            NewConfirmPasswordString = "";
        }

        /// <summary>
        /// Some account validation rules for the new account items
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        private bool ValidateNewAccount(string name, byte[] password, byte[] confirmPassword)
        {
            if (name.Length < 3)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("The user name is too short.");
                NewAccountName = "";
                return false;
            }

            foreach (string s in ListOfUserAccountsInDB)
            {
                if (string.Compare(s.ToLower(), name.ToLower()) == 0)
                {
                    Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("The user name is taken");
                    newAccountName = "";
                    return false;
                }
            }

            if (password.Length < 6)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("password must be at least 6 characters.");
                //NewPasswordSecureString.Clear();
                NewPasswordString = "";
                return false;
            }

            return PasswordHashing.SequenceEquals(password, confirmPassword);
        }

        //Used to flip the Login button on and off.
        private void ToggleLogin()
        {
            if (PasswordString != null && PasswordString.Length > 6 && checkBoxEnabledCount == 3 && loggedin == false)
                CanSelectLogin = true;
            else
                CanSelectLogin = false;
        }

        //Used to flip the Login button on and off.
        private void ToggleCreateUser()
        {
            if (newAccountName == null || newConfirmPasswordString == null || newPasswordString == null)
            {
                CanSelectCreateUser = false;
                return;
            }


            if (newAccountName.Length < 3 || newConfirmPasswordString.Length < 6 || newPasswordString.Length < 6)
            {
                CanSelectCreateUser = false;
                return;
            }

            if (string.Compare(newConfirmPasswordString, newPasswordString) != 0)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Passwords must match.");
                CanSelectCreateUser = false;
                return;
            }

            else
                CanSelectCreateUser = true;
        }

        #region properties, private strings, and ICommands


        private string passwordBoxEnabled;
        public string PasswordBoxEnabled
        {
            get { return passwordBoxEnabled; }
            set { SetProperty(ref passwordBoxEnabled, value);}
        }

        private int usersIDInDB;
        public int UsersIDInDB
        {
            get { return usersIDInDB; }
            set { SetProperty(ref usersIDInDB, value);}
        }

        private string accountName;
        public string AccountName
        {
            get { return accountName; }
            set { SetProperty(ref accountName, value);}
        }


        private string userName;
        public string UserName
        {
            get { return userName; }
            set {SetProperty(ref userName, value);}
        }

        private string newAccountName;
        public string NewAccountName
        {
            get { return newAccountName; }
            set
            {
                SetProperty(ref newAccountName, value);
                ToggleCreateUser();
            }
        }

        private String passwordString;
        public String PasswordString
        {
            get { return passwordString; }
            set
            {
                SetProperty(ref passwordString, value);
                //Check if we can login now.
                ToggleLogin();
            }
        }

        private String newPasswordString;
        public String NewPasswordString
        {
            get { return newPasswordString; }
            set
            {
                SetProperty(ref newPasswordString, value);
                ToggleCreateUser();
            }
        }

        private String newConfirmPasswordString;
        public String NewConfirmPasswordString
        {
            get { return newConfirmPasswordString; }
            set
            {
                SetProperty(ref newConfirmPasswordString, value);
                ToggleCreateUser();
            }
        }

        /// <summary>
        /// SecureString doesn't work with UWP so until I figure out a workaround this is dead!
        /// </summary>

        private int comboBoxIndexOfUserFromDB;
        public int ComboBoxIndexOfUserFromDB
        {
            get { return comboBoxIndexOfUserFromDB; }
            set
            {
                if (comboBoxIndexOfUserFromDB == value)
                    return;

                SetProperty(ref comboBoxIndexOfUserFromDB, value);
            }
        }

        public RelayCommandRaiseCanExecute CmdLoginBtn
        {
            get;
            private set;
        }

        public RelayCommandRaiseCanExecute CmdLogoutBtn
        {
            get;
            private set;
        }

        public RelayCommandRaiseCanExecute CmdCreateUserbtn
        {
            get;
            private set;
        }


        /// <summary>
        /// The user needs to have a valid Account name as well as an email address and the passwords have to match
        /// </summary>
        public bool canSelectCreateUser;
        public bool CanSelectCreateUser
        {
            get { return canSelectCreateUser; }

            set
            {
                SetProperty(ref canSelectCreateUser, value);
                CmdCreateUserbtn.RaiseCanExecuteChanged();
            }
        }


        /// <summary>
        /// This enables and disables the "CanSelect" part of the Icommand-Login
        /// </summary>
        private bool canSelectLogin;
        public bool CanSelectLogin
        {
            get { return canSelectLogin; }
            set
            {
                bool temp = CanSelectLogin;
                SetProperty(ref canSelectLogin, value);

                if(canSelectLogin != temp)
                CmdLoginBtn.RaiseCanExecuteChanged();
            }
        }
        

        /// <summary>
        /// This enables and disables the "CanSelect" part of the Icommand-Logout
        /// </summary>
        private bool canSelectLogout;
        public bool CanSelectLogout
        {
            get { return canSelectLogout; }
            set
            {
                SetProperty(ref canSelectLogout, value);
                CmdLogoutBtn.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region CheckBox properties and functions to establish the 3 websources the user is going to use

        /// <summary>
        /// The guts of what allows the user to select only 3 websources no matter how many options are presented
        /// </summary>
        /// <param name="value">true if the user is selecting the checkbox, false if they are unselecting it</param>
        private void CheckBoxEnabledShift(bool value)
        {
            if (value == true)
                checkBoxEnabledCount++;
            else
                checkBoxEnabledCount--;

            if (checkBoxEnabledCount == 3)
            {
                if (allRecipesCheckBoxChecked == true) 
                    AllRecipesCheckBoxEnabled = true;
                else
                    AllRecipesCheckBoxEnabled = false;

                if (epicuriousCheckBoxChecked == true)
                    EpicuriousCheckBoxEnabled = true;
                else
                    EpicuriousCheckBoxEnabled = false;

                if (foodNetworkCheckBoxChecked == true)
                    FoodNetworkCheckBoxEnabled = true;
                else
                    FoodNetworkCheckBoxEnabled = false;

                if (southernLivingCheckBoxChecked == true)
                    SouthernLivingCheckBoxEnabled = true;
                else
                    SouthernLivingCheckBoxEnabled = false;

                if (tastyCheckBoxChecked == true)
                    TastyCheckBoxEnabled = true;
                else
                    TastyCheckBoxEnabled = false;

                if (foodAndWineCheckBoxChecked == true)
                    foodAndWineCheckBoxEnabled = true;
                else
                    FoodAndWineCheckBoxEnabled = false;

                //Check to see if the user can log in now?
                ToggleLogin();
            }

            if (checkBoxEnabledCount < 3)
            {
                AllRecipesCheckBoxEnabled = true;
                EpicuriousCheckBoxEnabled = true;
                FoodNetworkCheckBoxEnabled = true;
                SouthernLivingCheckBoxEnabled = true;
                TastyCheckBoxEnabled = true;
                FoodAndWineCheckBoxEnabled = true;
                ToggleLogin();
            }
        }

        private void SetUpSearchWebsources(bool newUser)
        {
            if (newUser == false)
            {
                if (allRecipesCheckBoxChecked == true)
                    PanelMap.Add(Type_of_Websource.AllRecipes);

                if (epicuriousCheckBoxChecked == true)
                    PanelMap.Add(Type_of_Websource.Epicurious);             

                if (foodNetworkCheckBoxChecked == true)
                    PanelMap.Add(Type_of_Websource.FoodNetwork);
             
                if (southernLivingCheckBoxChecked == true)
                    PanelMap.Add(Type_of_Websource.SouthernLiving);             

                if (tastyCheckBoxChecked == true)
                    PanelMap.Add(Type_of_Websource.Tasty);

                if (foodAndWineCheckBoxChecked == true)
                    PanelMap.Add(Type_of_Websource.FoodAndWine);
            }
        }

        #endregion

        #region checkbox properties

        private bool allRecipesCheckBoxChecked;
        public bool AllRecipesCheckBoxChecked
        {
            get { return allRecipesCheckBoxChecked; }
            set
            {
                SetProperty(ref allRecipesCheckBoxChecked, value);
                CheckBoxEnabledShift(value);
            }
        }

        private bool allRecipesCheckBoxEnabled;
        public bool AllRecipesCheckBoxEnabled
        {
            get { return allRecipesCheckBoxEnabled; }
            set
            {
                SetProperty(ref allRecipesCheckBoxEnabled, value);
            }
        }

        private bool epicuriousCheckBoxChecked;
        public bool EpicuriousCheckBoxChecked
        {
            get { return epicuriousCheckBoxChecked; }
            set
            {
                SetProperty(ref epicuriousCheckBoxChecked, value);
                CheckBoxEnabledShift(value);
            }
        }

        private bool epicuriousCheckBoxEnabled;
        public bool EpicuriousCheckBoxEnabled
        {
            get { return epicuriousCheckBoxEnabled; }
            set
            {
                SetProperty(ref epicuriousCheckBoxEnabled, value);
            }
        }

        private bool foodNetworkCheckBoxChecked;
        public bool FoodNetworkCheckBoxChecked
        {
            get { return foodNetworkCheckBoxChecked; }
            set
            {
                SetProperty(ref foodNetworkCheckBoxChecked, value);
                CheckBoxEnabledShift(value);
            }
        }

        private bool foodNetworkCheckBoxEnabled;
        public bool FoodNetworkCheckBoxEnabled
        {
            get { return foodNetworkCheckBoxEnabled; }
            set
            {
                SetProperty(ref foodNetworkCheckBoxEnabled, value);
            }
        }

        private bool southernLivingCheckBoxChecked;
        public bool SouthernLivingCheckBoxChecked
        {
            get { return southernLivingCheckBoxChecked; }
            set
            {
                SetProperty(ref southernLivingCheckBoxChecked, value);
                CheckBoxEnabledShift(value);
            }
        }

        private bool southernLivingCheckBoxEnabled;
        public bool SouthernLivingCheckBoxEnabled
        {
            get { return southernLivingCheckBoxEnabled; }
            set
            {
                SetProperty(ref southernLivingCheckBoxEnabled, value);
            }
        }

        private bool tastyCheckBoxChecked;
        public bool TastyCheckBoxChecked
        {
            get { return tastyCheckBoxChecked; }
            set
            {
                SetProperty(ref tastyCheckBoxChecked, value);
                CheckBoxEnabledShift(value);
            }
        }

        private bool tastyCheckBoxEnabled;
        public bool TastyCheckBoxEnabled
        {
            get { return tastyCheckBoxEnabled; }
            set
            {
                SetProperty(ref tastyCheckBoxEnabled, value);
            }
        }

        private bool foodAndWineCheckBoxChecked;
        public bool FoodAndWineCheckBoxChecked
        {
            get { return foodAndWineCheckBoxChecked; }
            set
            {
                SetProperty(ref foodAndWineCheckBoxChecked, value);
                CheckBoxEnabledShift(value);
            }
        }

        private bool foodAndWineCheckBoxEnabled;
        public bool FoodAndWineCheckBoxEnabled
        {
            get { return foodAndWineCheckBoxEnabled; }
            set
            {
                SetProperty(ref foodAndWineCheckBoxEnabled, value);
            }
        }

        public object Navigate { get; private set; }

        #endregion

    }
}
