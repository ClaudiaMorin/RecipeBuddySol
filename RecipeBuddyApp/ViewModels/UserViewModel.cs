
using RecipeBuddy.ViewModels.Commands;
using System.Collections.ObjectModel;
using System;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Scrapers;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Views;
using RecipeBuddy.Services;
using CommunityToolkit.Mvvm.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using System.Linq;

namespace RecipeBuddy.ViewModels
{
    public sealed class UserViewModel : ObservableObject
    {
        public ObservableCollection<string> ListOfUserAccountsInDB { get; set; }
        public int checkBoxEnabledCount = 0;
        public int checkBoxEnabledCountNewUser = 0;
        public bool loggedin;
        public List<Type_of_Websource> PanelMap;
        Action<KeyRoutedEventArgs> actionWithKeyEventArgs;

        Action ActionNoParams;
        Action<RoutedEventArgs> TypedEventHandler;
        Func<bool> FuncBool;

        private static readonly UserViewModel instance = new UserViewModel();
        public static UserViewModel Instance
        {
            get { return instance; }
        }

        private UserViewModel()
        {
            CmdEnterKeyDown = new RelayCommand<KeyRoutedEventArgs>(actionWithKeyEventArgs = e => EnterKeyDown(e));
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
            loginNewUser = false;

            CmdLogoutBtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => LogOut(), FuncBool = () => CanSelectLogout);
            CmdLoginBtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => LogIn(), FuncBool = () => CanSelectLogin);
            CmdCreateUserbtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => SetUpNewUser(), FuncBool = () => CanSelectCreateUser);
            CmdNewUserLooseFocus = new RelayCommand<RoutedEventArgs>(TypedEventHandler = (a) => ToggleCreateUser(a));
        }


        /// <summary>
        /// We take in the users password and then set up the users account and load the tree-view with saved recipes if the
        /// account passwords match. 
        /// </summary>
        public void LogIn()
        {
            string AccountName = ListOfUserAccountsInDB[ComboBoxIndexOfUserFromDB];

            if (loginNewUser == false)
            {
                UsersIDInDB = DataBaseAccessorsForRecipeManager.LoadUserFromDatabase(PasswordString, AccountName);
                if (AccountName.Length > 0 && UsersIDInDB != -1)
                {
                    SetUpSearchWebsources(false);
                    loggedin = true;
                    CanSelectLogout = true;
                    CanSelectLogin = false;
                    PasswordBoxEnabled = "false";
                    //Set Up TreeView
                    List<RecipeRecordModel> recipeRecords = DataBaseAccessorsForRecipeManager.LoadUserDataByID(UsersIDInDB);
                    MainNavTreeViewModel.Instance.AddRecipeModelsToTreeViewAsPartOfInitialSetup(recipeRecords);
                    NavigationService.Navigate(typeof(SearchView));

                }
                else //user password didm't match
                {
                    Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Password Incorrect!  Casing counts!");
                    dialog.ShowAsync();
                }
            }
            else //this is a new user without any records, we already have set the UserDBID
            {
                SetUpSearchWebsources(false);
                loggedin = true;
                CanSelectLogout = true;
                CanSelectLogin = false;
                PasswordBoxEnabled = "false";
                PasswordString = "";
                loginNewUser = false;
                NavigationService.Navigate(typeof(SearchView));
            }

        }

        public void LogOut()
        {
            MainNavTreeViewModel.Instance.ClearTree();
            UsersIDInDB = -1;
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

            checkBoxEnabledCount = 0;
            SearchViewModel.Instance.ResetViewModel();
            SelectedViewModel.Instance.ResetViewModel();
            WebViewModel.Instance.ResetViewModel();

            PanelMap.Clear();
            loggedin = false;
            CanSelectLogin = false;
            CanSelectLogout = false;
            PasswordBoxEnabled = "true"; 
        }

        /// <summary>
        /// Saves a new user to the DB
        /// </summary>
        private void SetUpNewUser()
        {
            Windows.UI.Popups.MessageDialog dialog;
            

            //Is that name already taken?  If so the user needs to give us another one!
            for (int count = 0; count < ListOfUserAccountsInDB.Count; count++)
            {
                if (string.Compare(ListOfUserAccountsInDB[count].ToLower(), NewAccountName.ToLower()) == 0)
                {
                    NewAccountName = "";
                    NewPasswordString = "";
                    NewConfirmPasswordString = "";
                    dialog = new Windows.UI.Popups.MessageDialog("That User Name is taken.");
                    dialog.ShowAsync();
                    return;
                }
            }

            //Validate passwords
            if (NewPasswordString.SequenceEqual(newConfirmPasswordString) == false)
            {
                NewPasswordString = "";
                NewConfirmPasswordString = "";
                dialog = new Windows.UI.Popups.MessageDialog("Passwords don't match");
                dialog.ShowAsync();
                return;
            }

            byte[] bytePassword = PasswordHashing.CalculateHash(ConvertingStringToByteArray.ConvertStringToByteArray(NewPasswordString));
            UsersIDInDB = DataBaseAccessorsForRecipeManager.SaveUserToDatabase(NewAccountName, bytePassword);


            ListOfUserAccountsInDB.Add(NewAccountName);
            ComboBoxIndexOfUserFromDB = ListOfUserAccountsInDB.Count - 1;

            dialog = new Windows.UI.Popups.MessageDialog("User: " + NewAccountName + " created, now pick sites to search!");
            dialog.ShowAsync();
            PasswordString = NewPasswordString;
            PasswordBoxEnabled = "false";
            //Reset the "Create Password" section to empty strings.
            NewAccountName = "";
            NewPasswordString = "";
            NewConfirmPasswordString = "";
            loginNewUser = true;
        }


        /// <summary>
        /// Used to flip the Login button on and off.
        /// </summary>
        private void ToggleLogin()
        {
            if (PasswordString != null && PasswordString.Length >= 6 && checkBoxEnabledCount == 3 && loggedin == false)
                CanSelectLogin = true;
            else
                CanSelectLogin = false;
        }

        //Used to flip the Login button on and off.
        private void ToggleCreateUser(RoutedEventArgs args = null)
        {

           if (newAccountName == null || newConfirmPasswordString == null || newPasswordString == null)
            {
                CanSelectCreateUser = false;
                return;
            }

            if ( newAccountName.Length < 3 || newConfirmPasswordString.Length < 6 || newPasswordString.Length < 6 )
            {
                CanSelectCreateUser = false;
                return;
            }

            else
                CanSelectCreateUser = true;
        }

        #region properties, private strings, and ICommands

        private bool loginNewUser;

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

        public RelayCommand<RoutedEventArgs> CmdNewUserLooseFocus
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

                SearchViewModel.Instance.UpdateSearchWebsources();
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

        /// <summary>
        /// Allows the enter key to automatically target the search function
        /// </summary>
        /// <param name="args"></param>
        internal void EnterKeyDown(KeyRoutedEventArgs args)
        {
            switch (args.Key)
            {
                case Windows.System.VirtualKey.Enter:
                    if (canSelectLogin == true)
                        LogIn();
                    else if (canSelectCreateUser == true)
                        SetUpNewUser();
                    break;
                default:
                    break;
            }
        }

        public RelayCommand<KeyRoutedEventArgs> CmdEnterKeyDown
        {
            get;
            private set;
        }

    }
}
