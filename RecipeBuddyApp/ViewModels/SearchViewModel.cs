using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Scrapers;
using Windows.System.Threading.Core;
using System.Threading;
using Windows.Foundation;
using System;
using Windows.UI.Core;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RecipeBuddy.ViewModels
{

    public sealed class SearchViewModel : ObservableObject
    {
     
        public RecipePanelForSearchViewModel recipePanelForSearch1;
        public RecipePanelForSearchViewModel recipePanelForSearch2;
        public RecipePanelForSearchViewModel recipePanelForSearch3;
        Action ActionNoParams;
        Func<bool> FuncBool;

        private static readonly SearchViewModel instance = new SearchViewModel();


        public static SearchViewModel Instance
        {
            get { return instance; }
        }

        private SearchViewModel()
        {
            searchString = "";
            searchButtonTitle = "Search";
            searchEnabled = true;
            cursorType = CoreCursorType.Arrow;
            searchWait = false;

            recipePanelForSearch1 = new RecipePanelForSearchViewModel();
            recipePanelForSearch2 = new RecipePanelForSearchViewModel();
            recipePanelForSearch3 = new RecipePanelForSearchViewModel();
            UpdateSearchWebsources();


            SearchButtonCmd = new RBRelayCommand(ActionNoParams = () => Search(), FuncBool = () => SearchEnabled);

        }

        /// <summary>
        /// Updates the panel and the Panel map with the new websource
        /// </summary>
        /// <param name="add">bool, true if adding - false is removing</param>
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
            recipePanelForSearch2.ClearRecipeEntry();
            recipePanelForSearch3.ClearRecipeEntry();
            SearchString = "";
        }

        /// <summary>
        /// Call the Cursor change and the buttonTitle Change and then launches the background thread
        /// that fills the panels.
        /// </summary>
        public async Task Search()
        {
            SearchingChangeToUI();

            recipePanelForSearch1.ClearRecipeEntry();
            recipePanelForSearch1.ClearRecipeBlurbModelList();

            //For Threading callbacks.  
            Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView();

            await Task.Run(() => SearchBackground(recipePanelForSearch1, searchString, coreApplicationView));
            //Task.Run(() => SearchBackground(recipePanelForSearch1, searchString));
            //Task.Run(() => SearchBackground(recipePanelForSearch1, searchString));

            //await Task.Run(() => SearchBackgroundAsync(recipePanelForSearch1, searchString));

            //var TaskListOfSearches = new List<Task>();
            //TaskListOfSearches.Add(Task.Run(() => SearchBackground(recipePanelForSearch1, searchString)));
            //TaskListOfSearches.Add(Task.Run(() => SearchBackground(recipePanelForSearch2, searchString)));
            //TaskListOfSearches.Add(Task.Run(() => SearchBackground(recipePanelForSearch3, searchString)));

            //Task t = Task.WhenAll(TaskListOfSearches);
            //try
            //{
            //    t.Wait();
            //}
            //catch
            //{ }

            //recipePanelForSearch1.CanSelectBack = true;
            //recipePanelForSearch1.CmdBackButton.RaiseCanExecuteChanged();
            //recipePanelForSearch1.CanSelectNext = true;
            //recipePanelForSearch1.CmdNextButton.RaiseCanExecuteChanged();
            //recipePanelForSearch1.CanSelectSelect = true;
            //recipePanelForSearch1.CmdSelectButton.RaiseCanExecuteChanged();

            CursorType = CoreCursorType.Arrow;
            SearchButtonTitle = "Search";
            SearchEnabled = true;
            SearchButtonCmd.RaiseCanExecuteChanged();      
        }

        private void SearchingChangeToUI()
        {
            cursorType = CoreCursorType.Wait;
            SearchButtonTitle = "Searching...";
            SearchEnabled = false;
            SearchButtonCmd.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Calls ReturnSearchResults which also splits off into a main thread to return the first
        /// results of each panel and a background thread to find the rest of the recipes.
        /// </summary>
        private async Task SearchBackground(RecipePanelForSearchViewModel panel, string searchString, Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView)
        {
            await panel.SearchAndFillList(searchString, coreApplicationView);
        }

        /// <summary>
        /// Searches for the results and shows if there is an error
        /// Showing the first result happens via callback in the GenerateSearchResultsList.cs file
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="searchstring"></param>
        //public void ReturnSearchResults(RecipePanelForSearchViewModel panel, string searchstring)
        //{
        //    Task<int> results = panel.SearchAndFillListAsync(searchString);


        //    if (result == -1)
        //    {
        //        panel.Title = "No Results For: " + searchString + " on " + panel.type_Of_Source.ToString();
        //    }

        //}

        #region Command and Properties

        private bool searchEnabled;
        public bool SearchEnabled
        {
            get { return searchEnabled; }
            set { SetProperty(ref searchEnabled, value); }
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

        public RBRelayCommand SearchButtonCmd
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

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        public bool CanSelect
        {
            get
            {
                if (searchString.Length == 0)
                    return false;
                else
                    return true;
            }
        }

        #endregion

    }

}

