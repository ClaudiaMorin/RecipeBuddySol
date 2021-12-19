using System;
using System.Collections.Generic;
using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Core;
using RecipeBuddy.Views;

namespace RecipeBuddy.ViewModels
{
    public sealed class WebViewModel : ObservableObject
    {
        private static readonly WebViewModel instance = new WebViewModel();
        public RecipeDisplayModel mainRecipeCardModel;
        
        public RecipePanelForWebCopy recipePanelForWebCopy;

        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        public Action<string> actionWithObject;

        Action action;
        Func<bool> funcBool;

        static WebViewModel()
        {}

        public static WebViewModel Instance
        {
            get { return instance; }
        }

        private WebViewModel()
        {
            //currentType = 0;
            firstColumnTreeView = "Visible";
            mainViewWidth = "3*";
            mainRecipeCardModel = new RecipeDisplayModel();
            recipePanelForWebCopy = new RecipePanelForWebCopy();
            //currentLink = MainRecipeCardModel.Link;
            canSelectOpenEntry = true;
            canSelectCancel = true;
            
            CmdRemove = new ICommandViewModel<WebViewModel>(Action => SearchViewModel.Instance.RemoveRecipe(), canCallActionFunc => CanSelectTrueIfThereIsARecipe);
            CmdOpenEntry = new ICommandViewModel<WebViewModel>(Action => OpenKeepRecipePanel(), canCallActionFunc => CanSelectOpenEntry);
            CmdSelectedTypeChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeTpeFromComboBox(e), canCallActionFunc => CanSelectTrueIfThereIsARecipe);

            CmdSaveButton = new RBRelayCommand(action = () => SaveEntry(), funcBool = () => CanSelectSave);
            CmdCancelButton = new RBRelayCommand(action = () => CancelEntry(), funcBool = () => CanSelectCancel);
            CmdSelectedItemChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => CanSelectTrueIfThereIsARecipe);
        }

        

        /// <summary>
        /// For use when a user logs out of his/her account
        /// </summary>
        //public void ResetViewModel()
        //{
        //    if (listOfRecipeCardsModel.ListCountOfBlurbs > 0)
        //        listOfRecipeCardsModel.RemoveAll();

        //    MainRecipeCardModel.CopyRecipeBlurbModel(new RecipeBlurbModel());
        //}

        public void CancelEntry()
        {
            recipePanelForWebCopy.CancelEntry();
        }

        public void SaveEntry()
        {
            CloseKeepRecipePanel();
            recipePanelForWebCopy.SaveEntry();
        }

        /// <summary>
        /// Saves the recipe to the DB and the TreeView
        /// </summary>
        public void OpenKeepRecipePanel()
        {
            FirstColumnTreeView = "Collapsed";
            MainViewWidth = "*";
            recipePanelForWebCopy.LoadRecipeCardModel(mainRecipeCardModel);
        }

        public void CloseKeepRecipePanel()
        {
            MainViewWidth = "3*";
            FirstColumnTreeView = "Visible";
        }


        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Web page
        /// </summary>
        /// <param name="e"></param>
        internal void ChangeRecipeFromComboBox(SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                RecipeRecordModel recipeRecordModelFromChangedEventArgs = e.AddedItems[0] as RecipeRecordModel;

                if (recipeRecordModelFromChangedEventArgs != null)
                {
                    if (SearchViewModel.Instance.listOfRecipeCards.SettingCurrentIndexByTitle(recipeRecordModelFromChangedEventArgs.Title) != -1)
                    {
                        //RecipeCardModel recipeCardModel = new RecipeCardModel(listOfRecipeCards.GetCurrentEntry());
                        SearchViewModel.Instance.NumXRecipesIndex = "0";
                        mainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry());
                        CurrentLink = mainRecipeCardModel.Link;
                        //recipePanelForWebCopy.LoadRecipeCardModel(mainRecipeCardModel);
                    }
                }
            }
            //need to make this a sub to the first if statment because adding a new item to the listbox
            //removes the other which doesn't actually happen but the EventArgs still sends it as e.RemoveItems[0]
            else
            {
                if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                {
                    RecipeRecordModel recipeModel = e.RemovedItems[0] as RecipeRecordModel;

                    if (recipeModel != null)
                    {
                        SearchViewModel.Instance.ChangeRecipe(recipeModel.Title);
                        mainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(recipeModel);
                        //SearchViewModel.Instance.CurrentLink = mainRecipeCardModel.Link;
                        SearchViewModel.Instance.NumXRecipesIndex = "0";
                        //recipePanelForWebCopy.LoadRecipeCardModel(mainRecipeCardModel);
                    }
                }
            }
        }

        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Basket page
        /// </summary>
        /// <param name="e"></param>
        internal void ChangeRecipeTpeFromComboBox(SelectionChangedEventArgs e)
        {
            recipePanelForWebCopy.recipeCardModel.RecipeType = (Type_Of_Recipe)CurrentType;
        }

        #region ICommand, Properties, CanSelect

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        private bool canSelectTrueIfThereIsARecipe;
        public bool CanSelectTrueIfThereIsARecipe
        {
            get
            {
                if (string.Compare(mainRecipeCardModel.Title.ToLower(), "search for your next recipe find!") == 0)
                    return false;
                else
                    return true;
            }

            set { SetProperty(ref canSelectTrueIfThereIsARecipe, value); }
        }

        /// <summary>
        /// can't select save until the user is logged in
        /// because there is no access to the DB
        /// </summary>
        ///
        private bool canSelectSave;
        public bool CanSelectSave
        {
            get
            {
                if (UserViewModel.Instance.CanSelectLogout == true && mainRecipeCardModel.Title.Length > 0 && mainRecipeCardModel.Title.Length > 0)
                    return true;
                else
                    return false;
            }

            set { SetProperty(ref canSelectSave, value); }
        }

        private bool canSelectOpenEntry;
        public bool CanSelectOpenEntry
        {
            get
            { return canSelectOpenEntry; }

            set { SetProperty(ref canSelectOpenEntry, value); }
        }

        /// <summary>
        /// can't select save until the user is logged in
        /// because there is no access to the DB
        /// </summary>
        public bool CanSelectNew
        {
            get
            {
                return UserViewModel.Instance.CanSelectLogout;
            }
        }

        /// <summary>
        /// property for the Save button command
        /// </summary>
        public ICommand CmdOpenEntry
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Remove button command
        /// </summary>
        public ICommand CmdRemove
        {
            get;
            private set;
        }
        public RBRelayCommand CmdSaveButton
        {
            get;
            private set;
        }

        public RBRelayCommand CmdCancelButton
        {
            get;
            private set;
        }


        /// <summary>
        /// Always True
        /// </summary>
        private bool canSelectCancel;
        public bool CanSelectCancel
        {
            get { return canSelectCancel; }
            set { SetProperty(ref canSelectCancel, value); }
        }

        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public ICommand CmdSelectedItemChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public ICommand CmdSelectedTypeChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// property for the Quantity combobox change command
        /// </summary>
        public ICommand CmdSelectedQuantityChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// property for the update button command
        /// </summary>
        //public ICommand CmdUpdate
        //{
        //    get;
        //    private set;
        //}

        //public ICommand CmdCancel
        //{
        //    get;
        //    private set;
        //}

        //public ICommand CmdLineEdit
        //{
        //    get;
        //    private set;
        //}

        private int currentType;
        public int CurrentType
        {
            get { return currentType; }
            set { SetProperty(ref currentType, value); }
        }

        /// <summary>
        /// The CurrentCardIndex sets the recipe that has current focus
        /// </summary>
        private Uri currentLink;
        public Uri CurrentLink
        {
            get { return currentLink; }
            set
            {
                SetProperty(ref currentLink, value);
            }
        }


        //private string typeComboBoxVisibility;
        //public string TypeComboBoxVisibility
        //{
        //    get { return typeComboBoxVisibility; }
        //    set { SetProperty(ref typeComboBoxVisibility, value); }
        //}



        private string firstColumnTreeView;
        public string FirstColumnTreeView
        {
            get { return firstColumnTreeView; }
            set { SetProperty(ref firstColumnTreeView, value); }
        }

        private string mainViewWidth;
        public string MainViewWidth
        {
            get { return mainViewWidth; }
            set { SetProperty(ref mainViewWidth, value); }
        }

        #endregion
    }
}
