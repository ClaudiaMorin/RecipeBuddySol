using System;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Scrapers;
using RecipeBuddy.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using RecipeBuddy.Services;
using RecipeBuddy.Views;

namespace RecipeBuddy.ViewModels
{
    public class RecipePanelForSearchViewModel : ObservableObject
    {
        public Type_of_Websource type_Of_Source;
        public RecipeListModel listOfRecipeModel;
        public RecipeDisplayModel recipeCard;

        Action ActionShowCurrentEntry;
        Func<bool> FuncBool;
        Action Action;
        /// <summary>
        /// URLList is used by the Scraper and GenerateSearchResults to pull the URL of all the found recipes 
        /// </summary>
        public RecipePanelForSearchViewModel(RecipeListModel listOfRecipeCardsModel)
        {
            listOfRecipeModel = listOfRecipeCardsModel;
            recipeCard = new RecipeDisplayModel();
        }

        /// <summary>
        /// This manages both the RecipeCardView as well as the list of RecipeCards were pulled from the specific website
        /// </summary>
        /// <param name="type"></param>
        public RecipePanelForSearchViewModel(Type_of_Websource type)
        {   
            type_Of_Source = type;
            recipeCard = new RecipeDisplayModel();
            listOfRecipeModel = new RecipeListModel();
            canSelectBack = false;
            canSelectNext = false;
            canSelectSelect = false;
            Recipe_Type = Type_Of_Recipe.Unknown;
            CmdNextButton = new RBRelayCommand(Action = () => ShowNextEntry(), FuncBool = () => CanSelectNext);
            CmdBackButton = new RBRelayCommand(Action = () => ShowPreviousEntry(), FuncBool = () => CanSelectBack);
            CmdSelectButton = new RBRelayCommand(Action = () => AddToSelectedList(), FuncBool = () => CanSelectSelect);
        }

        /// <summary>
        /// This manages a RecipeCardView that isn't specific to any website
        /// </summary>
        public RecipePanelForSearchViewModel()
        {
            type_Of_Source = Type_of_Websource.None;
            listOfRecipeModel = new RecipeListModel();
            recipeCard = new RecipeDisplayModel();
            CanSelectBack = false;
            CanSelectNext = false;
            CanSelectSelect = false;
            CmdNextButton = new RBRelayCommand(Action = () => ShowNextEntry(), FuncBool = () => CanSelectNext);
            CmdBackButton = new RBRelayCommand(Action = () => ShowPreviousEntry(), FuncBool = () => CanSelectBack);
            CmdSelectButton = new RBRelayCommand(Action = () => AddToSelectedList(), FuncBool = () => CanSelectSelect);

            //Action<RequestNavigateEventArgs> hyLink = (RequestNavigateEventArgs arg) => Hyperlink_Navigate(arg);
            //CmdHyperlinkNav = new ICommandViewModel<RequestNavigateEventArgs>(hyLink, canCallActionFunc => CanSelect);
        }

        /// <summary>
        /// This is where the current recipecard is added to a new list of cards held in the BasketViewModel
        /// </summary>
        internal void AddToSelectedList()
        {
            SearchViewModel.Instance.AddToListOfRecipeCards(listOfRecipeModel.GetCurrentEntry());
        }

        /// <summary>
        /// This is where the current recipecard is added to a new list of cards held in the BasketViewModel
        /// </summary>
        //internal void AddToSelectedList()
        //{
        //    RequestNavigateEventArgs args = new RequestNavigateEventArgs(new System.Uri(recipeCardViewModel.Link), )
        //    System.Windows.Navigation.
        //    Hyperlink_Navigate(recipeCardViewModel.Link)
        //    recipeCardViewModel.Link
        //}

        /// <summary>
        /// New Search Clears the list on the Search page
        /// </summary>
        public void ClearRecipeBlurbModelList()
        {
            listOfRecipeModel.ClearList();
        }

        /// <summary>
        /// Shows the current selected entry in the list
        /// </summary>
        public void ShowCurrentEntry()
        {
            UpdateRecipeEntry(listOfRecipeModel.GetCurrentEntry());
        }

        /// <summary>
        /// Shows the current selected entry in the list
        /// </summary>
        public void ShowCurrentEntryAndActivateButtons()
        {
            UpdateRecipeEntry(listOfRecipeModel.GetCurrentEntry());

            CanSelectSelect = true;
            CmdSelectButton.RaiseCanExecuteChanged();
            CanSelectBack = true;
            CmdBackButton.RaiseCanExecuteChanged();
            CanSelectNext = true;
            CmdNextButton.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Used by the nextbutton to show the next entry in the list
        /// </summary>
        public void ShowNextEntry()
        {UpdateRecipeEntry(listOfRecipeModel.GetNextEntryInLoop());}

        /// <summary>
        /// Used by the backbutton to show the previous entry in the list
        /// </summary>
        public void ShowPreviousEntry()
        {UpdateRecipeEntry(listOfRecipeModel.GetPreviousEntryInLoop());}


        /// <summary>
        /// Hands off the search to the GenerateSearchResultsList which uses background threading to run the search and update the lists after
        /// first updating the first entry for each panel so that they don't sit blank while the user is waiting for all the results.
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public async Task SearchAndFillList(string searchTerms, Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView)
        {

            ActionShowCurrentEntry = () => ShowCurrentEntryAndActivateButtons();
            int results = await GenerateSearchResultsLists.SearchSitesAndGenerateEntryList(searchTerms, listOfRecipeModel, type_Of_Source, ActionShowCurrentEntry, coreApplicationView);

            if (results == -1)
            {
                Title = "No Results For: " + searchTerms + " on " + type_Of_Source.ToString();
                return;
            }

        }

        //public int SearchAndFillList(string searchTerms)
        //{
        //    ClearRecipeEntry();
        //    ClearRecipeBlurbModelList();
        //    ActionShowCurrentEntryEnableButtons = () => ShowCurrentEntryAndEnableButtons();

        //    int results = GenerateSearchResultsLists.SearchSitesAndGenerateEntryList(searchTerms, listOfRecipeBlurbModel, type_Of_Source, ActionShowCurrentEntryEnableButtons);

        //    CanSelectBack = true;
        //    CmdBackButton.RaiseCanExecuteChanged();
        //    CanSelectNext = true;
        //    CmdNextButton.RaiseCanExecuteChanged();
        //    return results;
        //}

        /// <summary>
        /// Updates the various elements of the RecipeEntry from the next entry in the RecipeEntriesList, triggering the OnPropertyChanged event that is "contextbound" to the UI
        /// Adds the ingredents and Directions dynamically and then deletes them before the new recipe is replacing the old one.
        /// </summary>
        /// <param name="reSource">The new RecipeCard which we will use to overwrite the old values</param>
        private void UpdateRecipeEntry(RecipeRecordModel reSource)
        {
            if (reSource == null)
                return;

            recipeCard.UpdateRecipeDisplayFromRecipeRecord(reSource);
        }

        public void ClearRecipeEntry()
        {
            Description = "";
            Title = ""; 
            Author = ""; 
            Website = Type_of_Websource.None; 
            Link = null; 
            Recipe_Type = Type_Of_Recipe.Unknown;
            CanSelectBack = false;
            CanSelectNext = false;
            CanSelectSelect = false;
            //listOfIngredientStringsForDisplay.Clear();
        }

        //private void Hyperlink_Navigate( RequestNavigateEventArgs arg)
        //{
        //    Process.Start(new ProcessStartInfo(arg.Uri.AbsoluteUri));
        //    arg.Handled = true;
        //}


        #region Delegate functions and ICommand functions 

        public void RemoveRecipe()
        {
            if (listOfRecipeModel.RecipesList.Count > 0)
            {
                listOfRecipeModel.Remove(listOfRecipeModel.CurrentCardIndex);
                if (listOfRecipeModel.CurrentCardIndex > 0)
                    listOfRecipeModel.CurrentCardIndex = listOfRecipeModel.CurrentCardIndex - 1;

                ShowCurrentEntry();
                return;
            }
        }

        public RecipeRecordModel selectRecipe
        {
            get
            {
                if (listOfRecipeModel.RecipesList.Count > 0)
                    return listOfRecipeModel.GetCurrentEntry();

                else
                    return null;
            }
        }

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        private bool canSelectNext;
        public bool CanSelectNext
        {
            get { return canSelectNext; }
            set { SetProperty(ref canSelectNext, value); }
        }

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        private bool canSelectSelect;
        public bool CanSelectSelect
        {
            get { return canSelectSelect; }
            set { SetProperty(ref canSelectSelect, value); }
        }

        private bool canSelectBack;
        public bool CanSelectBack
        {
            get { return canSelectBack; }
            set { SetProperty(ref canSelectBack, value); }
        }
        
        public RBRelayCommand CmdRemoveButton
        {
            get;
            private set;
        }

        public ICommand CmdHyperlinkNav
        {
            get;
            private set;
        }

        public RBRelayCommand CmdSelectButton
        {
            get;
            private set;
        }

        public RBRelayCommand CmdBackButton
        {
            get;
            private set;
        }

        public RBRelayCommand CmdNextButton
        {
            get;
            private set;
        }


        private Type_Of_Recipe type_of_Recipe;
        public Type_Of_Recipe Recipe_Type
        {
            get { return type_of_Recipe; }
            set { SetProperty(ref type_of_Recipe, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private Uri link;
        public Uri Link
        {
            get { return link; }
            set { SetProperty(ref link, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set {SetProperty(ref description, value);}
        }

        private string author;
        public string Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
        }

        private Type_of_Websource website;
        public Type_of_Websource Website
        {
            get { return website; }
            set { SetProperty(ref website, value); }
        }

        #endregion
    }
}
