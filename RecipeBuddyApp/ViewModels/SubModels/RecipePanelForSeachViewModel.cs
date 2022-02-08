using System;
using RecipeBuddy.ViewModels.Commands;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Scrapers;
using RecipeBuddy.Core.Models;
using System.Threading.Tasks;


namespace RecipeBuddy.ViewModels
{
    public class RecipePanelForSearchViewModel : ObservableObject
    {
        public Type_of_Websource type_Of_Source;

        Action ActionShowCurrentEntry;
        Func<bool> FuncBool;
        Action Action;
        /// <summary>
        /// URLList is used by the Scraper and GenerateSearchResults to pull the URL of all the found recipes 
        /// </summary>
        public RecipePanelForSearchViewModel(RecipeListModel listOfRecipeCardsModel)
        {
            listOfRecipeModels = listOfRecipeCardsModel;
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
            listOfRecipeModels = new RecipeListModel();
            canSelectBack = false;
            canSelectNext = false;
            canSelectSelect = false;
            Recipe_Type = Type_Of_Recipe.Unknown;
            CmdNextButton = new RelayCommandRaiseCanExecute(Action = () => ShowNextEntry(), FuncBool = () => CanSelectNext);
            CmdBackButton = new RelayCommandRaiseCanExecute(Action = () => ShowPreviousEntry(), FuncBool = () => CanSelectBack);
            CmdSelectButton = new RelayCommandRaiseCanExecute(Action = () => AddToSelectedList(), FuncBool = () => CanSelectSelect);
        }

        /// <summary>
        /// This manages a RecipeCardView that isn't specific to any website
        /// </summary>
        public RecipePanelForSearchViewModel()
        {
            type_Of_Source = Type_of_Websource.None;
            listOfRecipeModels= new RecipeListModel();
            recipeCard = new RecipeDisplayModel();
            CanSelectBack = false;
            CanSelectNext = false;
            CanSelectSelect = false;
            CmdNextButton = new RelayCommandRaiseCanExecute(Action = () => ShowNextEntry(), FuncBool = () => CanSelectNext);
            CmdBackButton = new RelayCommandRaiseCanExecute(Action = () => ShowPreviousEntry(), FuncBool = () => CanSelectBack);
            CmdSelectButton = new RelayCommandRaiseCanExecute(Action = () => AddToSelectedList(), FuncBool = () => CanSelectSelect);
        }

        /// <summary>
        /// This is where the current recipecard is added to a new list of cards held in the BasketViewModel
        /// </summary>
        internal void AddToSelectedList()
        {
            SearchViewModel.Instance.AddToListOfRecipeCards(listOfRecipeModels.GetCurrentEntry());
        }

        /// <summary>
        /// New Search Clears the list on the Search page
        /// </summary>
        public void ClearRecipeBlurbModelList()
        {
            listOfRecipeModels.ClearList();
        }

        /// <summary>
        /// Shows the current selected entry in the list
        /// </summary>
        public void ShowCurrentEntry()
        {
            UpdateRecipeEntry(listOfRecipeModels.GetCurrentEntry());
        }

        /// <summary>
        /// Shows the current selected entry in the list
        /// </summary>
        public void ShowCurrentEntryAndActivateButtons()
        {
            UpdateRecipeEntry(listOfRecipeModels.GetCurrentEntry());

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
        {UpdateRecipeEntry(listOfRecipeModels.GetNextEntryInLoop());}

        /// <summary>
        /// Used by the backbutton to show the previous entry in the list
        /// </summary>
        public void ShowPreviousEntry()
        {UpdateRecipeEntry(listOfRecipeModels.GetPreviousEntryInLoop());}


        /// <summary>
        /// Hands off the search to the GenerateSearchResultsList which uses background threading to run the search and update the lists after
        /// first updating the first entry for each panel so that they don't sit blank while the user is waiting for all the results.
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public async Task SearchAndFillList(string searchTerms, Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView)
        {
            //Creating a new list dumps any leftover search results that might be coming in.
            ActionShowCurrentEntry = () => ShowCurrentEntryAndActivateButtons();
            int results = await GenerateSearchResultsLists.SearchSitesAndGenerateEntryList(searchTerms, listOfRecipeModels, type_Of_Source, ActionShowCurrentEntry, coreApplicationView);

            if (results == -1)
            {
                Title = "No Results For: " + searchTerms + " on " + type_Of_Source.ToString();
            }
        }

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
            UpdateRecipeEntry(new RecipeRecordModel());
            CanSelectBack = false;
            CanSelectNext = false;
            CanSelectSelect = false;
        }

        #region Delegate functions and ICommand functions 

        public void RemoveRecipe()
        {
            if (listOfRecipeModels.RecipesList.Count > 0)
            {
                listOfRecipeModels.Remove(listOfRecipeModels.CurrentCardIndex);
                if (listOfRecipeModels.CurrentCardIndex > 0)
                    listOfRecipeModels.CurrentCardIndex = listOfRecipeModels.CurrentCardIndex - 1;

                ShowCurrentEntry();
                return;
            }
        }

        public RecipeRecordModel selectRecipe
        {
            get
            {
                if (listOfRecipeModels.RecipesList.Count > 0)
                    return listOfRecipeModels.GetCurrentEntry();

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

        public RelayCommandRaiseCanExecute CmdSelectButton
        {
            get;
            private set;
        }

        public RelayCommandRaiseCanExecute CmdBackButton
        {
            get;
            private set;
        }

        public RelayCommandRaiseCanExecute CmdNextButton
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

        private RecipeDisplayModel recipeCard;
        public RecipeDisplayModel RecipeCard
        {
            get { return recipeCard; }
            set { SetProperty(ref recipeCard, value); }
        }

        private RecipeListModel listOfRecipeModels;
        public RecipeListModel ListOfRecipeModels
        {
            get { return listOfRecipeModels; }
            set { SetProperty(ref listOfRecipeModels, value); }
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
