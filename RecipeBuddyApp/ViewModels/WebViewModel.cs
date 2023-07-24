using System;
using RecipeBuddy.Core.Models;
using Windows.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Helpers;

namespace RecipeBuddy.ViewModels
{

    public sealed class WebViewModel : ObservableObject
    {
        private static readonly WebViewModel instance = new WebViewModel(); 
        public RecipePanelForWebCopyOrNew recipePanelForWebCopy;

        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        Func<bool> FuncBool;
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
            recipeViewWidth = "0";
            recipePanelForWebCopy = new RecipePanelForWebCopyOrNew();
            canSelectGetRecipe = false;
            noLoadedRecipeHeight = "50";
            loadedRecipeHeight = "0";
            CmdSaveButton = new RelayCommand(action = () => SaveEntry());
            CmdCancelButton = new RelayCommand(action = () => CancelEntry());
            CmdRemove = new RelayCommand<string>(actionWithString = s => RemoveRecipe(s));
            CmdOpenEntry = new RelayCommandRaiseCanExecute(action = () => OpenRecipePanel(), FuncBool = () => CanSelectGetRecipe);
            CmdSelectedItemChanged = new RelayCommand<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => true);
        }

        /// <summary>
        /// Opens a recipe panel with the title and ingred list so the user can compelete the directions
        /// </summary>
        public void OpenRecipePanel()
        {
            recipePanelForWebCopy.recipeCardModel.UpdateRecipeDisplayFromRecipeRecord(SearchViewModel.Instance.listOfRecipeCards.GetCurrentEntry());
            RecipeViewWidth = "*";
            recipePanelForWebCopy.LoadRecipeCardModelAndDirections(recipePanelForWebCopy.recipeCardModel);
            CanSelectGetRecipe = false;
        }


        public void SaveEntry()
        {
            int res = recipePanelForWebCopy.SaveEntry();

            if (res == 1)
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
            RecipeViewWidth = "0";
            CanSelectGetRecipe = true;
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
                WebViewModel.Instance.CanSelectGetRecipe = false;
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

        #region ICommand, Properties, CanSelect

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

        private bool canSelectGetRecipe;
        public bool CanSelectGetRecipe
        {
            get { return canSelectGetRecipe; }
            set { SetProperty(ref canSelectGetRecipe, value); }
        }

        private string recipeViewWidth;
        public string RecipeViewWidth
        {
            get { return recipeViewWidth; }
            set { SetProperty(ref recipeViewWidth, value); }
        }

        private string noLoadedRecipeHeight;
        public string NoLoadedRecipeHeight
        {
            get { return noLoadedRecipeHeight; }
            set { SetProperty(ref noLoadedRecipeHeight, value); }
        }

        private string loadedRecipeHeight;
        public string LoadedRecipeHeight
        {
            get { return loadedRecipeHeight; }
            set { SetProperty(ref loadedRecipeHeight, value); }
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
