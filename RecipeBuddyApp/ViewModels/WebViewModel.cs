using System;
using RecipeBuddy.Core.Models;
using Windows.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeBuddy.ViewModels.Commands;

namespace RecipeBuddy.ViewModels
{

    public sealed class WebViewModel : ObservableObject
    {
        private static readonly WebViewModel instance = new WebViewModel(); 
        public RecipePanelForWebCopyOrNew recipePanelForWebCopy;

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
            firstColumnVisibility = "Collapsed";
            recipeEntryVisibility = "Collapsed";
            recipeEntryFromWebVisibility = "Collapsed";
            mainViewWidth = "Auto";
            recipePanelForWebCopy = new RecipePanelForWebCopyOrNew();
            CmdSaveButton = new RelayCommand(action = () => SaveEntry());
            CmdCancelButton = new RelayCommand(action = () => CancelEntry());

            CmdRemove = new RelayCommand<string>(actionWithString = s => RemoveRecipe(s));
            CmdOpenEntry = new RelayCommandRaiseCanExecute(action = () => OpenRecipePanel(), funcBool = () => CanSelectTrueIfThereIsARecipe);
            CmdSelectedTypeChanged = new RelayCommand<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeTypeFromComboBox(e), canCallActionFunc => CanSelectTrueIfThereIsARecipe);
            CmdSelectedItemChanged = new RelayCommand<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => CanSelectTrueIfThereIsARecipe);
        }

        /// <summary>
        /// Opens a recipe panel with the title and ingred list so the user can compelete the directions
        /// </summary>
        public void OpenRecipePanel()
        {
            recipePanelForWebCopy.recipeCardModel.UpdateRecipeDisplayFromRecipeRecord(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry());
            MainViewWidth = "*";
            RecipeEntryVisibility = "Visible";
            RecipeEntryFromWebVisibility = "Visible";
            FirstColumnVisibility = "Collapsed";
            recipePanelForWebCopy.LoadRecipeCardModelAndDirections(recipePanelForWebCopy.recipeCardModel);
        }


        public void SaveEntry()
        {
            int res = recipePanelForWebCopy.SaveEntry();
            if(res == 1)
                CloseKeepRecipePanel();
        }

        public void ClearRecipeEntry()
        {
            recipePanelForWebCopy.ClearRecipeEntry();
        }

        public void CancelEntry()
        {
            ClearRecipeEntry();
            CloseKeepRecipePanel();
        }

        public void CloseKeepRecipePanel()
        {
            RecipeEntryVisibility = "Collapsed";
            RecipeEntryFromWebVisibility = "Collapsed";
            //NewRecipeEntryVisibility = "Collapsed";
            MainViewWidth = "Auto";
            FirstColumnVisibility = "Collapsed";
        }

        /// <summary>
        /// used when programatically shifting from the SearchViewModel to the WebViewModel
        /// we don't need to send anything because both pages use the same recipelist.
        /// </summary>
        public void ChangeRecipeFromModel()
        {
              ComboBoxIndexForRecipeTitle = SearchViewModel.Instance.IndexOfComboBoxItem;
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
                        //we don't update an empty recipe!
                        recipePanelForWebCopy.recipeCardModel.UpdateRecipeDisplayFromRecipeRecord(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry());
                        CurrentLink = new Uri(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry().Link);
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
            CloseKeepRecipePanel();
            SearchViewModel.Instance.RemoveRecipeFromComboBoxWork(title);
            //Empty list ?
            if (SearchViewModel.Instance.listOfRecipeCards.ListCount > 1)
            {
                ComboBoxIndexForRecipeTitle = SearchViewModel.Instance.IndexOfComboBoxItem;
                CurrentLink = new Uri(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry().Link);
            }
            else 
            {
                ComboBoxIndexForRecipeTitle = 0;
                CurrentLink = null;
            }

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
                        recipePanelForWebCopy.recipeCardModel.RecipeTypeInt = index;
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
            get {return canSelectTrueIfThereIsARecipe;}

            set { SetProperty(ref canSelectTrueIfThereIsARecipe, value); }
        }

        /// <summary>
        /// property for the Save button command
        /// </summary>
        public RelayCommandRaiseCanExecute CmdOpenEntry
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

        private int comboBoxIndexForRecipeTitle;
        public int ComboBoxIndexForRecipeTitle
        {
            get { return comboBoxIndexForRecipeTitle; }
            set {
                    SetProperty(ref comboBoxIndexForRecipeTitle, value);
                }
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


        private string firstColumnVisibility;
        public string FirstColumnVisibility
        {
            get { return firstColumnVisibility; }
            set { SetProperty(ref firstColumnVisibility, value); }
        }

        private string mainViewWidth;
        public string MainViewWidth
        {
            get { return mainViewWidth; }
            set { SetProperty(ref mainViewWidth, value); }
        }

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

        public RelayCommand CmdSaveButton
        {
            get;
            private set;
        }

        public RelayCommand CmdCancelButton
        {
            get;
            private set;
        }

        #endregion
    }
}
