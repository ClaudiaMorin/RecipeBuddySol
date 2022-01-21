using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Scrapers;
using Windows.System.Threading.Core;
using System.Threading;
using Windows.Foundation;
using System;
using Windows.UI.Core;
using RecipeBuddy.Views;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Services;
using Microsoft.Toolkit.Mvvm.Input;

namespace RecipeBuddy.ViewModels
{

    public sealed class SearchViewModel : ObservableObject
    {
        public RecipeListModel listOfRecipeCards;
        public RecipePanelForSearchViewModel recipePanelForSearch1;
        public RecipePanelForSearchViewModel recipePanelForSearch2;
        public RecipePanelForSearchViewModel recipePanelForSearch3;
        Action ActionNoParams;
        Func<bool> FuncBool;
        Action<string> actionWithString;

        private static readonly SearchViewModel instance = new SearchViewModel();


        public static SearchViewModel Instance
        {
            get { return instance; }
        }

        private SearchViewModel()
        {
            searchString = "";
            dropDownOpen = false;
            searchButtonTitle = "Search";
            searchEnabled = true;
            webViewEnabled = true;
            cursorType = CoreCursorType.Arrow;
            searchWait = false;
            listOfRecipeCards = new RecipeListModel();
            recipePanelForSearch1 = new RecipePanelForSearchViewModel();
            recipePanelForSearch2 = new RecipePanelForSearchViewModel();
            recipePanelForSearch3 = new RecipePanelForSearchViewModel();
            UpdateSearchWebsources();

            CmdRemove = new RelayCommand<string>(actionWithString = s => RemoveRecipe(s));
            SearchButtonCmd = new RelayCommandRaiseCanExecute(ActionNoParams = () => Search(), FuncBool = () => SearchEnabled);
            WebButtonCmd = new RelayCommandRaiseCanExecute(ActionNoParams = () => GoToWebView(), FuncBool = () => WebViewEnabled);
        }

        /// <summary>
        /// Updates the panel and the Panel map with the new websource
        /// </summary>
        public void UpdateSearchWebsources()
        {   
           recipePanelForSearch1.type_Of_Source = UserViewModel.Instance.PanelMap[0];
           recipePanelForSearch2.type_Of_Source = UserViewModel.Instance.PanelMap[1];
           recipePanelForSearch3.type_Of_Source = UserViewModel.Instance.PanelMap[2];
        }

        /// <summary>
        /// For use when a user logs out
        /// </summary>
        public void ResetViewModel()
        {
            recipePanelForSearch1.ClearRecipeEntry();
            recipePanelForSearch1.ClearRecipeBlurbModelList();
            recipePanelForSearch2.ClearRecipeEntry();
            recipePanelForSearch2.ClearRecipeBlurbModelList();
            recipePanelForSearch3.ClearRecipeEntry();
            recipePanelForSearch3.ClearRecipeBlurbModelList();
            SearchString = "";
        }

        /// <summary>
        /// Call the Cursor change and the buttonTitle Change and then launches the background thread
        /// that fills the panels.
        /// </summary>
        public async Task Search()
        {
            SearchingChangeToUIAndLists();

            //For Threading callbacks.  
            Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView();

            List<Task> TaskListOfSearches = new List<Task>();
            TaskListOfSearches.Add(Task.Run(() => SearchBackground(recipePanelForSearch1, searchString, coreApplicationView)));
            TaskListOfSearches.Add(Task.Run(() => SearchBackground(recipePanelForSearch2, searchString, coreApplicationView)));
            TaskListOfSearches.Add(Task.Run(() => SearchBackground(recipePanelForSearch3, searchString, coreApplicationView)));
            Task t = Task.WhenAll(TaskListOfSearches);

            CursorType = CoreCursorType.Arrow;
            SearchButtonTitle = "Search";
            SearchEnabled = true;
            SearchButtonCmd.RaiseCanExecuteChanged();      
        }

        public void GoToWebView()
        {
            WebViewModel.Instance.ChangeRecipeFromModel();
            NavigationService.Navigate(typeof(Views.WebView));
        }

        /// <summary>
        /// Changes to the UI button to indicate that a search is happening
        /// Clears out lists so that the new search will have a place for the results.
        /// </summary>
        private void SearchingChangeToUIAndLists()
        {
            cursorType = CoreCursorType.Wait;
            SearchButtonTitle = "Searching...";
            SearchEnabled = false;
            SearchButtonCmd.RaiseCanExecuteChanged();

            recipePanelForSearch1.ClearRecipeEntry();
            recipePanelForSearch1.ClearRecipeBlurbModelList();
            recipePanelForSearch2.ClearRecipeEntry();
            recipePanelForSearch2.ClearRecipeBlurbModelList();
            recipePanelForSearch3.ClearRecipeEntry();
            recipePanelForSearch3.ClearRecipeBlurbModelList();
        }

        /// <summary>
        /// Runs a search on a given searchString and reports the results back.
        /// </summary>
        /// <param name="panel">The panel 1-3 that is being used to display the search results</param>
        /// <param name="searchString">The search string used</param>
        /// <param name="coreApplicationView">Threading, this is the executing UI thread that will need to recieve any callbacks or updates to the UI</param>
        /// <returns></returns>
        private async Task SearchBackground(RecipePanelForSearchViewModel panel, string searchString, Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView)
        {
            await panel.SearchAndFillList(searchString, coreApplicationView);
        }

        /// <summary>
        /// Linked to the command behind the button to remove a recipe from the combobox
        /// </summary>
        /// <param name="title">Title of the recipe to remove</param>
        public void RemoveRecipe(string title)
        {
            RemoveRecipeFromComboBoxWork(title);
            DropDownOpen = false;
        }

        public void RemoveRecipeFromComboBoxWork(string title)
        {
            try
            {
                if (listOfRecipeCards.ListCount > 0 && listOfRecipeCards.CurrentCardIndex != -1)
                {
                    int indexToRemove = listOfRecipeCards.GetEntryIndex(title);

                    if (indexToRemove == listOfRecipeCards.CurrentCardIndex)
                    {
                        if (listOfRecipeCards.CurrentCardIndex != listOfRecipeCards.ListCount - 1)
                        { listOfRecipeCards.CurrentCardIndex++; }
                        else
                        { listOfRecipeCards.CurrentCardIndex = 0; }
                    }
                    listOfRecipeCards.Remove(listOfRecipeCards.GetEntryIndex(title));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// For use with the ComboBox navigation
        /// </summary>
        /// <param name="index"></param>
        public void ShowSpecifiedEntry(int index)
        {
            if (index <listOfRecipeCards.ListCount && index > -1)
            {
                listOfRecipeCards.CurrentCardIndex = index;
                WebViewModel.Instance.recipePanelForWebCopy.recipeCardModel.UpdateRecipeDisplayFromRecipeRecord(listOfRecipeCards.GetEntry(index));
                ChangeRecipe(listOfRecipeCards.CurrentCardIndex);
                NumXRecipesIndex = "0";
                IndexOfComboBoxItem = index;
            }
        }

        public void AddToListOfRecipeCards(RecipeRecordModel recipeCard)
        {
            if (listOfRecipeCards.IsFoundInList(recipeCard) == true)
            {
                int index = listOfRecipeCards.GetEntryIndex(recipeCard.Title);
                ShowSpecifiedEntry(index);
                return;
            }

            listOfRecipeCards.Add(recipeCard);
            //WebViewModel.Instance.CanSelectOpenRecipeEntry = true;

            //If we have nothing in the list we will show the first entry
            if (listOfRecipeCards.ListCount == 1)
            {
                listOfRecipeCards.CurrentCardIndex = 0;
            }
            else
            {
                listOfRecipeCards.CurrentCardIndex = listOfRecipeCards.ListCount - 1;
            }

            ShowSpecifiedEntry(listOfRecipeCards.CurrentCardIndex);
        }

        ///// <summary>
        ///// For use with the ComboBox navigation
        ///// </summary>
        ///// <param name="title">title of the recipe we are looking for</param>
        //public void ShowSpecifiedEntry(string title)
        //{
        //    ShowSpecifiedEntry(listOfRecipeCards.GetEntryIndex(title));
        //}

        /// <summary>
        /// Updates the current card display and updates the edit-textboxes for the ingredient and direction editing
        /// </summary>
        /// <param name="indexOfTitleInComboBox"></param>
        public void ChangeRecipe(int indexOfTitleInComboBox)
        {
            listOfRecipeCards.CurrentCardIndex = indexOfTitleInComboBox;
        }

        /// <summary>
        /// Called by the RecipiesInComboBox_SelectionChanged function to shift the listOfRecipeCards and update the entry
        /// </summary>
        /// <param name="TitleOfRecipe">Title of the new recipe</param>
        public void ChangeRecipe(string TitleOfRecipe)
        {
            int index = listOfRecipeCards.SettingCurrentIndexByTitle(TitleOfRecipe);
            if (index != -1)
            {
                ChangeRecipe(index);
            }
        }


        #region Command and Properties

        private bool searchEnabled;
        public bool SearchEnabled
        {
            get { return searchEnabled; }
            set { SetProperty(ref searchEnabled, value); }
        }

        private bool webViewEnabled;
        public bool WebViewEnabled
        {
            get { return webViewEnabled; }
            set
            {   SetProperty(ref webViewEnabled, value);
                WebButtonCmd.RaiseCanExecuteChanged();
            }
        }


        private CoreCursorType cursorType;
        public CoreCursorType CursorType
        {
            get { return cursorType; }
            set { SetProperty(ref cursorType, value); }
        }


        private bool searchWait;
        public bool SearchWait
        {
            get { return searchWait; }
            set { SetProperty(ref searchWait, value); }
        }

        private string searchButtonTitle;
        public string SearchButtonTitle
        {
            get { return searchButtonTitle; }
            set { SetProperty(ref searchButtonTitle, value); }
        }

        public string searchString;
        public string SearchString
        {
            get { return searchString; }
            set
            {
                SetProperty(ref searchString, value);
                if (SearchString.Length > 0)
                    CanSelectSearch = true;
            }
        }

        private int indexOfComboBoxItem;
        public int IndexOfComboBoxItem
        {
            get { return indexOfComboBoxItem; }
            set { SetProperty(ref indexOfComboBoxItem, value); }
        }

        private string numXRecipesIndex;
        public string NumXRecipesIndex
        {
            get { return numXRecipesIndex; }
            set { SetProperty(ref numXRecipesIndex, value); }
        }

        public RelayCommandRaiseCanExecute SearchButtonCmd
        {
            get;
            private set;
        }

        public RelayCommandRaiseCanExecute WebButtonCmd
        {
            get;
            private set;
        }

        /// <summary>
        /// property for the Remove button command
        /// </summary>
        public RelayCommand<string> CmdRemove
        {
            get;
            private set;
        }

        /// <summary>
        /// used to deactivate the Search capacity if the user hasn't logged in first
        /// </summary>
        private bool canSelectSearch;
        public bool CanSelectSearch
        {
            get
            {
                return canSelectSearch;
            }
            set { SetProperty(ref canSelectSearch, value); }
        }

        private bool dropDownOpen;
        public bool DropDownOpen
        {
            get
            {
                return dropDownOpen;
            }
            set { SetProperty(ref dropDownOpen, value); }
        }

        #endregion

    }

}

