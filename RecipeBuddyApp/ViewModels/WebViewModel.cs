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
using Microsoft.Toolkit.Mvvm.Input;

namespace RecipeBuddy.ViewModels
{

    public sealed class WebViewModel : ObservableObject
    {
        private static readonly WebViewModel instance = new WebViewModel(); 
        public RecipePanelForWebCopy recipePanelForWebCopy;

        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        Action<string> actionWithString;


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
            dropDownOpen = false;
            firstColumnTreeViewVisibility = "Visible";
            recipeEntryVisibility = "Collapsed";
            recipeEntryFromWebVisibility = "Collapsed";
            newRecipeEntryVisibility = "Collapsed";
            mainViewWidth = "3*";
            recipePanelForWebCopy = RecipePanelForWebCopy.Instance;

            CmdRemove = new RelayCommand<string>(actionWithString = s => RemoveRecipe(s));
            CmdOpenEntry = new RelayCommand(action = () => OpenRecipePanel(), funcBool = () => CanSelectTrueIfThereIsARecipe);
            CmdOpenEmptyEntry = new RelayCommand(action = () => OpenEmptyRecipePanel());
            CmdSelectedTypeChanged = new RelayCommand<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeTypeFromComboBox(e), canCallActionFunc => CanSelectTrueIfThereIsARecipe);
            CmdSelectedItemChanged = new RelayCommand<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => CanSelectTrueIfThereIsARecipe);
        }

        /// <summary>
        /// Opens a recipe panel with the title and ingred list so the user can compelete the directions
        /// </summary>
        public void OpenRecipePanel()
        {
            recipePanelForWebCopy.recipeCardModel.UpdateRecipeDisplayFromRecipeRecord(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry());
            FirstColumnTreeViewVisibility = "Collapsed";
            RecipeEntryVisibility = "Visible";
            RecipeEntryFromWebVisibility = "Visible";
            MainViewWidth = "*";
            recipePanelForWebCopy.LoadRecipeCardModel(recipePanelForWebCopy.recipeCardModel);
        }

        /// <summary>
        /// Opens and empty recipe panel
        /// </summary>
        public void OpenEmptyRecipePanel()
        {
            RecipeDisplayModel emptyRecipeCardModel = new RecipeDisplayModel();
            emptyRecipeCardModel.Title = "Recipe Title Here";
            FirstColumnTreeViewVisibility = "Collapsed";
            RecipeEntryVisibility = "Visible";
            NewRecipeEntryVisibility = "Visible";
            MainViewWidth = "*";
            ComboBoxIndexForRecipeType = (int)emptyRecipeCardModel.RecipeType;
            recipePanelForWebCopy.LoadRecipeCardModel(emptyRecipeCardModel);
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
            CurrentLink = recipePanelForWebCopy.recipeCardModel.Link;
        }

        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the the WebPage
        /// This is the same comboBox that is shown on the SearchView pages.
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
                        SearchViewModel.Instance.NumXRecipesIndex = "0";
                        recipePanelForWebCopy.recipeCardModel.UpdateRecipeDisplayFromRecipeRecord(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry());
                        CurrentLink = recipePanelForWebCopy.recipeCardModel.Link;
                    }
                }
            }
        }

        /// <summary>
        /// Linked to the command behind the button to remove a recipe from the combobox
        /// </summary>
        /// <param name="title">Title of the recipe to remove</param>
        public void RemoveRecipe(string title)
        {
            SearchViewModel.Instance.RemoveRecipeFromComboBoxWork(title);
            DropDownOpen = false;
        }

        /// <summary>
        /// For use when a user logs out
        /// </summary>
        public void ResetViewModel()
        {
            recipePanelForWebCopy.ClearRecipeEntry();
            CurrentLink = null;
        }

        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Create/WebView page
        /// This is one list that is shared by both the SearchView and the CreateView
        /// </summary>
        /// <param name="e"></param>
        internal void ChangeRecipeTypeFromComboBox(SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                string type = e.AddedItems[0].ToString();

                for (int index = 0; index < MainNavTreeViewModel.Instance.CatagoryTypes.Count; index++)
                {
                    if (string.Compare(MainNavTreeViewModel.Instance.CatagoryTypes[index].ToString().ToLower(), type.ToLower()) == 0)
                    {
                        recipePanelForWebCopy.recipeCardModel.RecipeType = (Type_Of_Recipe)index;
                    }
                }
            }
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
                if (string.Compare(recipePanelForWebCopy.recipeCardModel.Title.ToLower(), "search for your next recipe find!") == 0)
                {
                    canSelectTrueIfThereIsARecipe = false;
                    return canSelectTrueIfThereIsARecipe;
                }
                else
                    canSelectTrueIfThereIsARecipe = true;
                return canSelectTrueIfThereIsARecipe;
            }

            set { SetProperty(ref canSelectTrueIfThereIsARecipe, value); }
        }

        /// <summary>
        /// property for the Save button command
        /// </summary>
        public RelayCommand CmdOpenEntry
        {
            get;
            private set;
        }

        /// <summary>
        /// property for the Save button command
        /// </summary>
        public RelayCommand CmdOpenEmptyEntry
        {
            get;
            private set;
        }

        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> CmdSelectedItemChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> CmdSelectedTypeChanged
        {
            get;
            private set;
        }

        ///// <summary>
        ///// property for the Quantity combobox change command
        ///// </summary>
        //public RelayCommand<SelectionChangedEventArgs> CmdSelectedQuantityChanged
        //{
        //    get;
        //    private set;
        //}

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

        //private string mainViewNewRecipeWidth;
        //public string MainViewNewRecipeWidth
        //{
        //    get { return mainViewNewRecipeWidth; }
        //    set { SetProperty(ref mainViewNewRecipeWidth, value); }
        //}

        /// <summary>
        /// property for the Remove button command
        /// </summary>
        public RelayCommand<string> CmdRemove
        {
            get;
            private set;
        }

        private bool dropDownOpen;
        public bool DropDownOpen
        {
            get { return dropDownOpen; }
            set { SetProperty(ref dropDownOpen, value); }
        }

        #endregion
    }
}
