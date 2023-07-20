
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

            passwordBoxEnabled = "true";
            passwordString = "";
            canSelectLogin = false;
            canSelectLogout = false;
            canSelectCreateUser = false;

            loggedin = false;
            comboBoxIndexOfUserFromDB = 0;

            CmdLogoutBtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => LogOut(), FuncBool = () => CanSelectLogout);
            CmdLoginBtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => LogIn(), FuncBool = () => CanSelectLogin);
            CmdCreateUserBtn = new RelayCommandRaiseCanExecute(ActionNoParams = () => SetUpNewUser(), FuncBool = () => CanSelectCreateUser);
            CmdLoginBtnLooseFocus = new RelayCommand<RoutedEventArgs>(TypedEventHandler = (a) => ToggleCreateUser(a));
        }


        /// <summary>
        /// We take in the users password and then set up the users account and load the tree-view with saved recipes if the
        /// account passwords match. 
        /// </summary>
        public void LogIn()
        {
            string AccountName = ListOfUserAccountsInDB[ComboBoxIndexOfUserFromDB];


            UsersIDInDB = DataBaseAccessorsForRecipeManager.LoadUserFromDatabase(PasswordString, AccountName);
            if (AccountName.Length > 0 && UsersIDInDB != -1)
            {
                setUpLoggedInUser();
            }
            else //user password didm't match
            {
               Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("Password Incorrect!  Casing counts!");
               dialog.ShowAsync();
            }
        }


        public void LogOut()
        {
            MainNavTreeViewModel.Instance.ClearTree();
            UsersIDInDB = -1;
            UserName = "";
            NewAccountName = "";

            SearchViewModel.Instance.ResetViewModel();
            SelectedViewModel.Instance.ResetViewModel();
            WebViewModel.Instance.ResetViewModel();

            loggedin = false;
            CanSelectLogin = false;
            CanSelectLogout = false;

            CanSelectCreateUser = false;
            PasswordString = "";
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
            //Set up the in the DB
            UsersIDInDB = DataBaseAccessorsForRecipeManager.SaveUserToDatabase(NewAccountName, NewPasswordString);
            ListOfUserAccountsInDB.Add(NewAccountName);
            ComboBoxIndexOfUserFromDB = ListOfUserAccountsInDB.Count - 1;

            setUpLoggedInUser();

        }

        /// <summary>
        /// housekeeping for any user setup after they log in.
        /// </summary>
        private void setUpLoggedInUser()
        {

            //Set Up TreeView from the DB if there are any records
            List<RecipeRecordModel> recipeRecords = DataBaseAccessorsForRecipeManager.LoadUserDataByID(UsersIDInDB);
            MainNavTreeViewModel.Instance.AddRecipeModelsToTreeViewAsPartOfInitialSetup(recipeRecords);
            PasswordBoxEnabled = "false";
            //Reset the "Create Password" section to empty strings.
            NewAccountName = "";
            NewPasswordString = "";
            NewConfirmPasswordString = "";
            PasswordBoxEnabled = "false";
            loggedin = true;
            CanSelectLogout = true;
            CanSelectLogin = false;
            CanSelectCreateUser = false;
            NavigationService.Navigate(typeof(Views.SearchView));
        }


        /// <summary>
        /// Used to flip the Login button on and off.
        /// </summary>
        private void ToggleLogin()
        {
            if (PasswordString != null && PasswordString.Length >= 6 && loggedin == false)
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

            if (newAccountName.Length < 3 || newConfirmPasswordString.Length < 6 || newPasswordString.Length < 6)
            {
                CanSelectCreateUser = false;
                return;
            }

            CanSelectCreateUser = true;
        }


        #region properties, private strings, and ICommands

        //private bool loginNewUser;

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


        public RelayCommandRaiseCanExecute CmdCreateUserBtn
        {
            get;
            private set;
        }

        public RelayCommand<RoutedEventArgs> CmdNewUserLooseFocus
        {
            get;
            private set;
        }

        public RelayCommand<RoutedEventArgs> CmdLoginBtnLooseFocus
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
                CmdCreateUserBtn.RaiseCanExecuteChanged();
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
                SetProperty(ref canSelectLogin, value);
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

        public object Navigate { get; private set; }


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
