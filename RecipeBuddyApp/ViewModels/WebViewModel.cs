using System;
using System.Collections.Generic;
using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.ViewModels
{
    public sealed class WebViewModel : ObservableObject
    {
        private static readonly WebViewModel instance = new WebViewModel();
        public RecipeBlurbModel selectViewMainRecipeCardModel;
        public RecipeBlurbListModel listOfRecipeCardsModel;

        public List<string> IngredientQuantityShift;
        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        public Action<string> actionWithObject;

        static WebViewModel()
        { }

        public static WebViewModel Instance
        {
            get { return instance; }
        }

        private WebViewModel()
        {
            currentType = 0;

            SetUpComboBox();
            selectViewMainRecipeCardModel = new RecipeBlurbModel();
            currentLink = selectViewMainRecipeCardModel.Link;
            IngredientQuantityShift = new List<string>();
            typeComboBoxVisibility = "Collapsed";
            CmdRemove = new ICommandViewModel<WebViewModel>(Action => RemoveRecipe(), canCallActionFunc => CanSelect);
            CmdSave = new ICommandViewModel<WebViewModel>(Action => SaveRecipe(), canCallActionFunc => CanSelectSave);
            CmdEdit = new ICommandViewModel<WebViewModel>(Action => EditRecipe(), canCallActionFunc => CanSelect);
            CmdSelectedItemChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => CanSelect);
        }

        public void RemoveRecipe()
        {
            if (listOfRecipeCardsModel.ListCountOfBlurbs > 0 && listOfRecipeCardsModel.CurrentCardIndex != -1)
            {
                listOfRecipeCardsModel.Remove(listOfRecipeCardsModel.CurrentCardIndex);

                if (listOfRecipeCardsModel.CurrentCardIndex > 0)
                {
                    listOfRecipeCardsModel.CurrentCardIndex = listOfRecipeCardsModel.CurrentCardIndex - 1;
                    ShowSpecifiedEntry(listOfRecipeCardsModel.CurrentCardIndex);
                    //need to reset the starting index of the "borrow recipe" incase it was removed
                    EditViewModel.Instance.IndexOfComboBoxItem = 0;
                }
                else if (listOfRecipeCardsModel.ListCountOfBlurbs > 0)
                {
                    listOfRecipeCardsModel.CurrentCardIndex = listOfRecipeCardsModel.ListCountOfBlurbs - 1;
                    ShowSpecifiedEntry(listOfRecipeCardsModel.CurrentCardIndex);
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
        public void ResetViewModel()
        {
            if (listOfRecipeCardsModel.ListCountOfBlurbs > 0)
                listOfRecipeCardsModel.RemoveAll();

            selectViewMainRecipeCardModel.CopyRecipeBlurbModel(new RecipeBlurbModel());
        }


        //public void LoadWebPages(string URLlink, bool addToList = true)
        //{ 

        //}

        /// <summary>
        /// Saves the recipe to the DB and the TreeView
        /// </summary>
        public void SaveRecipe()
        {
            //selectViewMainRecipeCardModel.SaveEditsToARecipe(selectViewMainRecipeCardModel.Title);
            //int result = MainWindowViewModel.Instance.mainTreeViewNav.AddRecipeToTreeView(selectViewMainRecipeCardModel, true);

            //if (result == 1)
            //{
            //    MainNavTreeViewModel.Instance.RemoveRecipeFromTreeView(selectViewMainRecipeCardModel);
            //    DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(selectViewMainRecipeCardModel.Title, selectViewMainRecipeCardModel.TypeAsInt);
            //}
            //if (result == 1 || result == 2)
            //{
            //    DataBaseAccessorsForRecipeManager.SaveRecipeToDatabase(UserViewModel.Instance.UsersIDInDB, selectViewMainRecipeCardModel);
            //    RemoveRecipe();
            //}
        }

        /// <summary>
        /// ICommand backer for Edit Button on Select page.  Function adds the current recipe to the edit page and removes
        /// it from the MakeItView
        /// </summary>
        public void EditRecipe()
        {
            //EditViewModel.Instance.UpdateRecipe(selectViewMainRecipeCardModel);


            //Check to see if this if from the TreeView or from a generic search
            MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.EditTab;
            RemoveRecipe();
        }

        /// <summary>
        /// ICommand backer for Create New Button on Select page.  Creates an new recipe and takes the user to the edit panel
        /// </summary>
        public void CreateNewRecipe()
        {
            EditViewModel.Instance.CreateNewRecipe();

            //Check to see if this if from the TreeView or from a generic search
            MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.EditTab;
        }

        /// <summary>
        /// sets up the first entry in the dropdown list and the initial index.
        /// </summary>
        private void SetUpComboBox()
        {
            indexOfComboBoxItem = 0;
            listOfRecipeCardsModel = new RecipeBlurbListModel();
        }

        public void AddToListOfRecipeCards(RecipeBlurbModel recipeBlurbCard)
        {
            if (listOfRecipeCardsModel.IsFoundInList(recipeBlurbCard) == true)
            {
                int index = listOfRecipeCardsModel.GetEntryIndex(recipeBlurbCard.Title);
                ShowSpecifiedEntry(index);
                return;
            }

            listOfRecipeCardsModel.AddToBlurbList(recipeBlurbCard);

            //If we have nothing in the list we will show the first entry
            if (listOfRecipeCardsModel.ListCountOfBlurbs == 1)
            {
                listOfRecipeCardsModel.CurrentCardIndex = 0;
                EditViewModel.Instance.IndexOfComboBoxItem = 0;
            }
            else
            {
                listOfRecipeCardsModel.CurrentCardIndex = listOfRecipeCardsModel.ListCountOfBlurbs - 1;
            }
            ShowSpecifiedEntry(listOfRecipeCardsModel.CurrentCardIndex);
        }

        /// <summary>
        /// For use with the ComboBox navigation
        /// </summary>
        /// <param name="title">title of the recipe we are looking for</param>
        public void ShowSpecifiedEntry(string title)
        {
            ShowSpecifiedEntry(listOfRecipeCardsModel.GetEntryIndex(title));
        }

        /// <summary>
        /// For use with the ComboBox navigation
        /// </summary>
        /// <param name="index"></param>
        public void ShowSpecifiedEntry(int index)
        {
            if (index < listOfRecipeCardsModel.ListCountOfBlurbs && index > -1)
            {
                listOfRecipeCardsModel.CurrentCardIndex = index;
                selectViewMainRecipeCardModel.CopyRecipeBlurbModel(listOfRecipeCardsModel.GetEntry(index));
                ChangeRecipe(listOfRecipeCardsModel.CurrentCardIndex);
                NumXRecipesIndex = "0";
                IndexOfComboBoxItem = index;
                //UpdateQuantityCalc();
            }
        }

        /// <summary>
        /// Updates the current card display and updates the edit-textboxes for the ingredient and direction editing
        /// </summary>
        /// <param name="indexOfTitleInComboBox"></param>
        public void ChangeRecipe(int indexOfTitleInComboBox)
        {
            listOfRecipeCardsModel.CurrentCardIndex = indexOfTitleInComboBox;

        }

        /// <summary>
        /// Called by the RecipiesInComboBox_SelectionChanged function to shift the listOfRecipeCards and update the entry
        /// </summary>
        /// <param name="TitleOfRecipe">Title of the new recipe</param>
        public void ChangeRecipe(string TitleOfRecipe)
        {
            int index = listOfRecipeCardsModel.SettingCurrentIndexByTitle(TitleOfRecipe);
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
                RecipeBlurbModel recipeCardModelFromChangedEventArgs = e.AddedItems[0] as RecipeBlurbModel;

                if (recipeCardModelFromChangedEventArgs != null)
                {
                    if (listOfRecipeCardsModel.SettingCurrentIndexByTitle(recipeCardModelFromChangedEventArgs.Title) != -1)
                    {
                        RecipeBlurbModel recipeCardModel = listOfRecipeCardsModel.GetCurrentEntry();
                        NumXRecipesIndex = "0";
                        selectViewMainRecipeCardModel.CopyRecipeBlurbModel(recipeCardModel);
                        CurrentLink = selectViewMainRecipeCardModel.Link;
                    }
                }
            }
            //need to make this a sub to the first if statment because adding a new item to the listbox
            //removes the other which doesn't actually happen but the EventArgs still sends it as e.RemoveItems[0]
            else
            {
                if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                {
                    RecipeBlurbModel recipeCardModel = e.RemovedItems[0] as RecipeBlurbModel;

                    if (recipeCardModel != null)
                    {
                        ChangeRecipe(recipeCardModel.Title);
                        selectViewMainRecipeCardModel.CopyRecipeBlurbModel(recipeCardModel);
                        CurrentLink = selectViewMainRecipeCardModel.Link;
                        NumXRecipesIndex = "0";
                    }
                }
            }
        }

        #region ICommand, Properties, CanSelect

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        public bool CanSelect
        {
            get
            {
                if (string.Compare(selectViewMainRecipeCardModel.Title.ToLower(), "search for your next recipe find!") == 0)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// can't select save until the user is logged in
        /// because there is no access to the DB
        /// </summary>
        public bool CanSelectSave
        {
            get
            {
                if (UserViewModel.Instance.CanSelectLogout == true && selectViewMainRecipeCardModel.Title.Length > 0 &&
                     string.Compare(selectViewMainRecipeCardModel.Title.ToLower(), "search for your next recipe find!") != 0)
                    return true;
                else
                    return false;
            }
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
        public ICommand CmdSave
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Edit button command
        /// </summary>
        public ICommand CmdEdit
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Edit button command
        /// </summary>
        public ICommand CmdNew
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
        public ICommand CmdUpdate
        {
            get;
            private set;
        }

        public ICommand CmdCancel
        {
            get;
            private set;
        }

        public ICommand CmdLineEdit
        {
            get;
            private set;
        }

        private string currentLink;
        public string CurrentLink
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

        private int currentType;
        public int CurrentType
        {
            get { return currentType; }
            set { SetProperty(ref currentType, value); }
        }

        private string typeComboBoxVisibility;
        public string TypeComboBoxVisibility
        {
            get { return typeComboBoxVisibility; }
            set { SetProperty(ref typeComboBoxVisibility, value); }
        }

        private string numXRecipesIndex;
        public string NumXRecipesIndex
        {
            get { return numXRecipesIndex; }
            set { SetProperty(ref numXRecipesIndex, value); }
        }

        #endregion
    }
}
