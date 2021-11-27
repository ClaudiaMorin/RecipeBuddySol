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


namespace RecipeBuddy.ViewModels
{
    public class RecipePanelForSearchViewModel : ObservableObject
    {
        //public RecipeBlurbModel recipeBlurbModel;
        public Type_of_Websource type_Of_Source;
        public RecipeBlurbListModel listOfRecipeBlurbModel;
        Action ActionShowCurrentEntry;
        Func<bool> FuncBool;
        Action Action;
        /// <summary>
        /// URLList is used by the Scraper and GenerateSearchResults to pull the URL of all the found recipes 
        /// </summary>
        public RecipePanelForSearchViewModel(RecipeBlurbListModel listOfRecipeCardsModel)
        {
            listOfRecipeBlurbModel = listOfRecipeCardsModel;
        }

        /// <summary>
        /// This manages both the RecipeCardView as well as the list of RecipeCards were pulled from the specific website
        /// </summary>
        /// <param name="type"></param>
        public RecipePanelForSearchViewModel(Type_of_Websource type)
        {   
            type_Of_Source = type;
            listOfRecipeBlurbModel = new RecipeBlurbListModel();
            description = "";
            title = "";
            Author = "";
            Website = "";
            Link = "";
            canSelectBack = false;
            canSelectNext = false;
            canSelectSelect = false;
            Recipe_Type = Type_Of_Recipe.Unknown;
            CmdNextButton = new RBRelayCommand(Action = () => ShowNextEntry(), FuncBool = () => CanSelectNext);
            CmdBackButton = new RBRelayCommand(Action = () => ShowPreviousEntry(), FuncBool = () => CanSelectBack);
            CmdSelectButton = new RBRelayCommand(Action = () => AddToSelectedList(), FuncBool = () => CanSelectSelect);
            //CmdRemoveButton = new RBRelayCommand(Action = () => RemoveRecipe(), FuncBool = () => CanSelect);
        }

        /// <summary>
        /// This manages a RecipeCardView that isn't specific to any website
        /// </summary>
        public RecipePanelForSearchViewModel()
        {
            type_Of_Source = Type_of_Websource.None;
            description = "Some Description";
            title = "Some Title";
            Author = "Some Author";
            Website = "Some Website";
            Link = "Some link";
            Recipe_Type = Type_Of_Recipe.Unknown;
            listOfRecipeBlurbModel = new RecipeBlurbListModel();
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
            //WebViewModel.Instance.AddToListOfRecipeCards(listOfRecipeBlurbModel.GetCurrentEntry());
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
            listOfRecipeBlurbModel.ClearList();
        }

        /// <summary>
        /// Shows the current selected entry in the list
        /// </summary>
        public void ShowCurrentEntry()
        {
            UpdateRecipeEntry(listOfRecipeBlurbModel.GetCurrentEntry());
        }

        /// <summary>
        /// Shows the current selected entry in the list
        /// </summary>
        public void ShowCurrentEntryAndActivateButtons()
        {
            UpdateRecipeEntry(listOfRecipeBlurbModel.GetCurrentEntry());

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
        {UpdateRecipeEntry(listOfRecipeBlurbModel.GetNextEntryInLoop());}

        /// <summary>
        /// Used by the backbutton to show the previous entry in the list
        /// </summary>
        public void ShowPreviousEntry()
        {UpdateRecipeEntry(listOfRecipeBlurbModel.GetPreviousEntryInLoop());}


        /// <summary>
        /// Hands off the search to the GenerateSearchResultsList which uses background threading to run the search and update the lists after
        /// first updating the first entry for each panel so that they don't sit blank while the user is waiting for all the results.
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public async Task SearchAndFillList(string searchTerms, Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView)
        {

            ActionShowCurrentEntry = () => ShowCurrentEntryAndActivateButtons();
            int results = await GenerateSearchResultsLists.SearchSitesAndGenerateEntryList(searchTerms, listOfRecipeBlurbModel, type_Of_Source, ActionShowCurrentEntry, coreApplicationView);

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
        private void UpdateRecipeEntry(RecipeBlurbModel reSource)
        {
            if (reSource == null)
                return;

            Description = string.Copy(reSource.Description);
            Title = string.Copy(reSource.Title);
            Author = reSource.Author;
            Website = reSource.Website;
            Link = reSource.Link;
            Recipe_Type = reSource.Recipe_Type;
        }

        public void ClearRecipeEntry()
        {
            Description = "";
            Title = ""; 
            Author = ""; 
            Website = ""; 
            Link = ""; 
            Recipe_Type = Type_Of_Recipe.Unknown;
            CanSelectBack = false;
            CanSelectNext = false;
            CanSelectSelect = false;  
        }

        //private void Hyperlink_Navigate( RequestNavigateEventArgs arg)
        //{
        //    Process.Start(new ProcessStartInfo(arg.Uri.AbsoluteUri));
        //    arg.Handled = true;
        //}


        #region Delegate functions and ICommand functions 

        public void RemoveRecipe()
        {
            if (listOfRecipeBlurbModel.RecipiesBlurbList.Count > 0)
            {
                listOfRecipeBlurbModel.Remove(listOfRecipeBlurbModel.CurrentCardIndex);
                if (listOfRecipeBlurbModel.CurrentCardIndex > 0)
                    listOfRecipeBlurbModel.CurrentCardIndex = listOfRecipeBlurbModel.CurrentCardIndex - 1;

                ShowCurrentEntry();
                return;
            }
        }

        public RecipeBlurbModel selectRecipe
        {
            get
            {
                if (listOfRecipeBlurbModel.RecipiesBlurbList.Count > 0)
                    return listOfRecipeBlurbModel.GetCurrentEntry();

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

        private string link;
        public string Link
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

        private string website;
        public string Website
        {
            get { return website; }
            set { SetProperty(ref website, value); }
        }

        //private int listCount;
        //public int ListCount
        //{
        //    get { return listCount; }
        //    set { SetProperty(ref listCount, value); }
        //}

        //private int totalCount;
        //public int TotalCount
        //{
        //    get { return totalCount; }
        //    set { SetProperty(ref totalCount, value); }
        //}

        #endregion
    }
}
