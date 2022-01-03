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
        //public RecipeDetailsPanelForEmptyRecipe recipePanelForNewRecipe;

        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        public Action<string> actionWithString;

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
            firstColumnTreeViewVisibility = "Visible";
            recipeEntryVisibility = "Collapsed";
            recipeEntryFromWebVisibility = "Collapsed";
            newRecipeEntryVisibility = "Collapsed";
            alwaysTrue = true;
            mainViewWidth = "3*";
            mainRecipeCardModel = new RecipeDisplayModel();
            recipePanelForWebCopy = new RecipePanelForWebCopy();
            //recipePanelForNewRecipe = new RecipeDetailsPanelForEmptyRecipe();
            //currentLink = MainRecipeCardModel.Link;
            canSelectOpenEntry = true;
            canSelectCancel = true;
            
            CmdRemove = new ICommandViewModel<string>(actionWithString = s => SearchViewModel.Instance.RemoveRecipe(s), canCallActionFunc => CanSelectTrueIfThereIsARecipe);
            CmdOpenEntry = new ICommandViewModel<WebViewModel>(Action  => OpenKeepRecipePanel(), canCallActionFunc => CanSelectOpenEntry);
            CmdOpenEmptyEntry = new ICommandViewModel<WebViewModel>(Action => OpenKeepEmptyRecipePanel(), canCallActionFunc => CanSelectOpenEntry);
            CmdSelectedTypeChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeTypeFromComboBox(e), canCallActionFunc => CanSelectTrueIfThereIsARecipe);
            CmdSelectedMeasurementChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeMeasurementTypeFromComboBox(e), canCallActionFunc => AlwaysTrue);

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
            recipePanelForWebCopy.recipeCardModel.RecipeType = (Type_Of_Recipe) ComboBoxIndexForRecipeType;
            if (string.Compare(mainRecipeCardModel.Title, "Search for your next recipe find!") == 0)
            {
                //Launch dialog saying that the new recipe must have a title
            }
            recipePanelForWebCopy.SaveEntry();
            CloseKeepRecipePanel();
        }

        /// <summary>
        /// Saves the recipe to the DB and the TreeView
        /// </summary>
        public void OpenKeepRecipePanel()
        {
            mainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry());
            FirstColumnTreeViewVisibility = "Collapsed";
            RecipeEntryVisibility = "Visible";
            RecipeEntryFromWebVisibility = "Visible";
            MainViewWidth = "*";
            recipePanelForWebCopy.LoadRecipeCardModel(mainRecipeCardModel);
            ComboBoxIndexForRecipeType = (int)mainRecipeCardModel.RecipeType;
        }

        /// <summary>
        /// Saves the recipe to the DB and the TreeView
        /// </summary>
        public void OpenKeepEmptyRecipePanel()
        {
            mainRecipeCardModel = new RecipeDisplayModel();
            mainRecipeCardModel.Title = "Recipe Title Here";
            FirstColumnTreeViewVisibility = "Collapsed";
            RecipeEntryVisibility = "Visible";
            //RecipeEntryFromWebVisibility = "Collapsed";
            NewRecipeEntryVisibility = "Visible";
            MainViewWidth = "*";
            recipePanelForWebCopy.LoadRecipeCardModel(mainRecipeCardModel);
            ComboBoxIndexForRecipeType = (int)mainRecipeCardModel.RecipeType;
        }

        public void CloseKeepRecipePanel()
        {
            MainViewWidth = "3*";
            FirstColumnTreeViewVisibility = "Visible";
            NewRecipeEntryVisibility = "Collapsed";
            RecipeEntryFromWebVisibility = "Collapsed";
            RecipeEntryVisibility = "Collapsed";
        }

        /// <summary>
        /// used when programatically shifting from the SearchViewModel to the WebViewModel
        /// we don't need to send anything because both pages use the same recipelist.
        /// </summary>
        public void ChangeRecipeFromModel()
        {
            ComboBoxIndexForRecipeTitle = SearchViewModel.Instance.IndexOfComboBoxItem;
            CurrentLink = mainRecipeCardModel.Link;
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
        internal void ChangeMeasurementTypeFromComboBox(SelectionChangedEventArgs e)
        {
            recipePanelForWebCopy.recipeCardModel.RecipeType = (Type_Of_Recipe)ComboBoxIndexForRecipeType;
        }

        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Basket page
        /// </summary>
        /// <param name="e"></param>
        internal void ChangeRecipeTypeFromComboBox(SelectionChangedEventArgs e)
        {
            recipePanelForWebCopy.recipeCardModel.RecipeType = (Type_Of_Recipe)ComboBoxIndexForRecipeType;
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
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        private bool alwaysTrue;
        public bool AlwaysTrue
        {
            get
            {
                return alwaysTrue;
            }

            set { SetProperty(ref alwaysTrue, value); }
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
        /// property for the Save button command
        /// </summary>
        public ICommand CmdOpenEmptyEntry
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
        /// Property for the Recipe combobox change command
        /// </summary>
        public ICommand CmdSelectedMeasurementChanged
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

        private int comboBoxIndexForRecipeType;
        public int ComboBoxIndexForRecipeType
        {
            get { return comboBoxIndexForRecipeType; }
            set { SetProperty(ref comboBoxIndexForRecipeType, value); }
        }

        private int comboBoxIndexForRecipeTitle;
        public int ComboBoxIndexForRecipeTitle
        {
            get { return comboBoxIndexForRecipeTitle; }
            set { SetProperty(ref comboBoxIndexForRecipeTitle, value); }
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

        private string recipeEntryFromWebVisibility;
        public string RecipeEntryFromWebVisibility
        {
            get { return recipeEntryFromWebVisibility; }
            set { SetProperty(ref recipeEntryFromWebVisibility, value); }
        }

        private string recipeEntryVisibility;
        public string RecipeEntryVisibility
        {
            get { return recipeEntryVisibility; }
            set { SetProperty(ref recipeEntryVisibility, value); }
        }

        private string newRecipeEntryVisibility;
        public string NewRecipeEntryVisibility
        {
            get { return newRecipeEntryVisibility; }
            set { SetProperty(ref newRecipeEntryVisibility, value); }
        }


        private string firstColumnTreeViewVisibility;
        public string FirstColumnTreeViewVisibility
        {
            get { return firstColumnTreeViewVisibility; }
            set { SetProperty(ref firstColumnTreeViewVisibility, value); }
        }

        private string mainViewWidth;
        public string MainViewWidth
        {
            get { return mainViewWidth; }
            set { SetProperty(ref mainViewWidth, value); }
        }

        private string mainViewNewRecipeWidth;
        public string MainViewNewRecipeWidth
        {
            get { return mainViewNewRecipeWidth; }
            set { SetProperty(ref mainViewNewRecipeWidth, value); }
        }

        #endregion
    }
}
