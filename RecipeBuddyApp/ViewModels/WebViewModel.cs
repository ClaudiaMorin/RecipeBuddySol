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

namespace RecipeBuddy.ViewModels
{
    public sealed class WebViewModel : ObservableObject
    {
        private static readonly WebViewModel instance = new WebViewModel();
        public RecipeDisplayModel mainRecipeCardModel;
        public RecipeListModel listOfRecipeCards;
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
            SetUpComboBox();
            mainRecipeCardModel = new RecipeDisplayModel();
            recipePanelForWebCopy = new RecipePanelForWebCopy();
            //currentLink = MainRecipeCardModel.Link;
            canSelectOpenEntry = true;
            canSelectCancel = true;
            currentLink = null;
            CmdRemove = new ICommandViewModel<WebViewModel>(Action => RemoveRecipe(), canCallActionFunc => CanSelectRemove);
            CmdOpenEntry = new ICommandViewModel<WebViewModel>(Action => OpenKeepRecipePanel(), canCallActionFunc => CanSelectOpenEntry);
            
            CmdSaveButton = new RBRelayCommand(action = () => SaveEntry(), funcBool = () => CanSelectSave);
            CmdCancelButton = new RBRelayCommand(action = () => CancelEntry(), funcBool = () => CanSelectCancel);
            CmdSelectedItemChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => CanSelectRemove);
        }

        public void RemoveRecipe()
        {
            if (listOfRecipeCards.ListCount > 0 && listOfRecipeCards.CurrentCardIndex != -1)
            {
                listOfRecipeCards.Remove(listOfRecipeCards.CurrentCardIndex);

                if (listOfRecipeCards.CurrentCardIndex > 0)
                {
                    listOfRecipeCards.CurrentCardIndex = listOfRecipeCards.CurrentCardIndex - 1;
                    ShowSpecifiedEntry(listOfRecipeCards.CurrentCardIndex);
                    //need to reset the starting index of the "borrow recipe" incase it was removed
                    EditViewModel.Instance.IndexOfComboBoxItem = 0;
                }
                else if (listOfRecipeCards.ListCount > 0)
                {
                    listOfRecipeCards.CurrentCardIndex = listOfRecipeCards.ListCount - 1;
                    ShowSpecifiedEntry(listOfRecipeCards.CurrentCardIndex);
                    //need to reset the starting index of the "borrow recipe" incase it was removed
                    EditViewModel.Instance.IndexOfComboBoxItem = 0;
                }
                //the last element in the list has been removed so now we need to go back to blank screen
                else
                {
                    //selectViewMainRecipeCardModel.CopyRecipeCardModel(new RecipeCardModel());
                    //EmptyIngredientQuanityRow();
                }

                return;
            }
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
        /// sets up the first entry in the dropdown list and the initial index.
        /// </summary>
        private void SetUpComboBox()
        {
            indexOfComboBoxItem = 0;
            listOfRecipeCards = new RecipeListModel();
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

            //If we have nothing in the list we will show the first entry
            if (listOfRecipeCards.ListCount == 1)
            {
                listOfRecipeCards.CurrentCardIndex = 0;
                //CurrentCardTitle = recipeBlurbCard.Title;
                CurrentLink = new Uri(recipeCard.Link);
                EditViewModel.Instance.IndexOfComboBoxItem = 0;
            }
            else
            {
                listOfRecipeCards.CurrentCardIndex = listOfRecipeCards.ListCount - 1;
            }
            ShowSpecifiedEntry(listOfRecipeCards.CurrentCardIndex);
        }

        /// <summary>
        /// For use with the ComboBox navigation
        /// </summary>
        /// <param name="title">title of the recipe we are looking for</param>
        public void ShowSpecifiedEntry(string title)
        {
            ShowSpecifiedEntry(listOfRecipeCards.GetEntryIndex(title));
        }

        /// <summary>
        /// For use with the ComboBox navigation
        /// </summary>
        /// <param name="index"></param>
        public void ShowSpecifiedEntry(int index)
        {
            if (index < listOfRecipeCards.ListCount && index > -1)
            {
                listOfRecipeCards.CurrentCardIndex = index;
                mainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(listOfRecipeCards.GetEntry(index));
                ChangeRecipe(listOfRecipeCards.CurrentCardIndex);
                NumXRecipesIndex = "0";
                IndexOfComboBoxItem = index;
            }
        }

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

        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Basket page
        /// </summary>
        /// <param name="e"></param>
        internal void ChangeRecipeFromComboBox(SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                RecipeRecordModel recipeRecordModelFromChangedEventArgs = e.AddedItems[0] as RecipeRecordModel;

                if (recipeRecordModelFromChangedEventArgs != null)
                {
                    if (listOfRecipeCards.SettingCurrentIndexByTitle(recipeRecordModelFromChangedEventArgs.Title) != -1)
                    {
                        //RecipeCardModel recipeCardModel = new RecipeCardModel(listOfRecipeCards.GetCurrentEntry());
                        NumXRecipesIndex = "0";
                        mainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(listOfRecipeCards.GetCurrentEntry());
                        CurrentLink = mainRecipeCardModel.Link;
                        recipePanelForWebCopy.LoadRecipeCardModel(mainRecipeCardModel);
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
                        ChangeRecipe(recipeModel.Title);
                        mainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(recipeModel);
                        CurrentLink = mainRecipeCardModel.Link;
                        NumXRecipesIndex = "0";
                        recipePanelForWebCopy.LoadRecipeCardModel(mainRecipeCardModel);
                    }
                }
            }
        }

        #region ICommand, Properties, CanSelect

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        private bool canSelectRemove;
        public bool CanSelectRemove
        {
            get
            {
                if (string.Compare(mainRecipeCardModel.Title.ToLower(), "search for your next recipe find!") == 0)
                    return false;
                else
                    return true;
            }

            set { SetProperty(ref canSelectRemove, value); }
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

        private Uri currentLink;
        public Uri CurrentLink
        {
            get { return currentLink; }
            set { SetProperty(ref currentLink, value); }
        }

        private int indexOfComboBoxItem;
        public int IndexOfComboBoxItem
        {
            get { return indexOfComboBoxItem; }
            set { SetProperty(ref indexOfComboBoxItem, value); }
        }

        //private int currentType;
        //public int CurrentType
        //{
        //    get { return currentType; }
        //    set { SetProperty(ref currentType, value); }
        //}

        //private string typeComboBoxVisibility;
        //public string TypeComboBoxVisibility
        //{
        //    get { return typeComboBoxVisibility; }
        //    set { SetProperty(ref typeComboBoxVisibility, value); }
        //}

        private string numXRecipesIndex;
        public string NumXRecipesIndex
        {
            get { return numXRecipesIndex; }
            set { SetProperty(ref numXRecipesIndex, value); }
        }

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
