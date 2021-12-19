using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Database;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace RecipeBuddy.ViewModels
{
    public sealed class EditViewModel : ObservableObject
    {
        public RecipeDisplayModel recipeCardViewModelForEdit;
        public RecipeDisplayModel recipeCardViewModelToBorrowFrom;

        public string contextMenuSelectedItemValue;
        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        public Action<RoutedEventArgs> actionWithRoutedEventArgs;
        public Action<string> actionWithItemName;
        public string selectedComboBoxItem;
        

        private static readonly EditViewModel instance = new EditViewModel();
        private int numberOfIngredients;
        private int numberOfDirections;


        private EditViewModel()
        {
            recipeCardViewModelForEdit = new RecipeDisplayModel();
            recipeCardViewModelToBorrowFrom = new RecipeDisplayModel();

            numberOfIngredients = 0;
            numberOfDirections = 0;
            indexOfComboBoxItem = 0;
            borrowWidthSelected = "0";
            currentType = 0;
            typeComboBoxVisibility = "Collapsed";
            closeButtonVisibilty = "Collapsed";
            borrowTextVisibilty = "Visible";
            CmdSelectedTypeChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeTypeFromComboBox(e), canCallActionFunc => CanSelect);
            CmdClear = new ICommandViewModel<EditViewModel>(Action => ClearRecipe(), canCallActionFunc => CanSelect);
            CmdSave = new ICommandViewModel<RecipeDisplayModel>(Action => Save(), canCallActionFunc => CanSelect);
            CmdNew = new ICommandViewModel<RecipeDisplayModel>(Action => CreateNewRecipe(), canCallActionFunc => CanSelectNew);
            CmdRevert = new ICommandViewModel<RecipeTreeItem>(Action => RevertRecipe(), canCallActionFunc => CanSelect);
            //CmdSelectedItemChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeBorrowRecipeFromComboBox(e), canCallActionFunc => CanSelect);
            CmdOpenBorrow = new ICommandViewModel<SelectionChangedEventArgs>(Action => OpenBorrowPanel(), canCallActionFunc => CanSelectBorrow);
            CmdCloseBorrow = new ICommandViewModel<SelectionChangedEventArgs>(Action => CloseBorrowPanel(), canCallActionFunc => CanSelect);
            CmdTypeUpdate = new ICommandViewModel<string>(Action => ChangeVisiblityofTypeComboBox(true), canCallActionFunc => CanSelect);
            CmdCancelUpdate = new ICommandViewModel<string>(Action => ChangeVisiblityofTypeComboBox(false), canCallActionFunc => CanSelect);
        }

        /// <summary>
        /// Updates the Edit Recipe page with a new entry
        /// </summary>
        /// <param name="recipeItem">RecipeCardTreeItem</param>
        public void UpdateRecipe(RecipeTreeItem recipeItem)
        {
            UpdateRecipe(recipeItem.RecipeModelTV);
        }

        /// <summary>
        /// Updates the Edit Recipe page with a new entry
        /// </summary>
        /// <param name="recipeItem">RecipeCardModel</param>
        public void UpdateRecipe(RecipeRecordModel recipeItem)
        {
            recipeCardViewModelForEdit.UpdateRecipeDisplayFromRecipeRecord(recipeItem);
            CurrentType = (Type_Of_Recipe) recipeItem.TypeAsInt;
            NumberOfIngredients = recipeItem.ListOfIngredientStrings.Count;
            NumberOfDirections = recipeItem.ListOfDirectionStrings.Count;
        }

        /// <summary>
        /// For use when a user is logging out, resets the view for the next user.
        /// </summary>
        public void ResetViewModel()
        {
            UpdateRecipe(new RecipeRecordModel());
            IndexOfComboBoxItem = 0;
            CurrentType = recipeCardViewModelForEdit.RecipeType;
            CloseBorrowPanel();
        }

        /// <summary>
        /// Removes the currently displayed recipe from the page
        /// </summary>
        internal void ClearRecipe()
        {
            //nothing to clear here!
            if (string.Compare(recipeCardViewModelForEdit.Title.ToLower(), "search for your next recipe find!") == 0 || recipeCardViewModelForEdit.listOfIngredientStringsForDisplay.Count == 0)
                return;
            recipeCardViewModelForEdit.UpdateRecipeDisplayFromRecipeRecord(new RecipeRecordModel());
            CurrentType = recipeCardViewModelForEdit.RecipeType;
            NumberOfIngredients = 0;
            NumberOfDirections = 0;
            CloseBorrowPanel();
        }

        /// <summary>
        /// ICommand backer for Create New Button on Select page.  Creates an new recipe and takes the user to the edit panel
        /// </summary>
        public void CreateNewRecipe()
        {
            List<string> ingred = new List<string>() { "-Ingredients" };
            List<string> direct = new List<string>() { "-Directions" };

            RecipeRecordModel recipeCardModelForCreate = new RecipeRecordModel(ingred, direct);
            recipeCardModelForCreate.Title = "";
            recipeCardModelForCreate.TypeAsInt = (int)Type_Of_Recipe.Unknown;
            EditViewModel.Instance.CurrentType = Type_Of_Recipe.Unknown;
            //recipeCardModelForCreate.Link = "My Recipe!";

            recipeCardViewModelForEdit.UpdateRecipeDisplayFromRecipeRecord(recipeCardModelForCreate);
            NumberOfIngredients = recipeCardModelForCreate.ListOfIngredientStrings.Count;
            NumberOfDirections = recipeCardModelForCreate.ListOfDirectionStrings.Count;
        }

        /// <summary>
        /// Saves the recipe to the DB and the TreeView
        /// </summary>
        public void Save()
        {
            //nothing to save here!
            if (string.Compare(recipeCardViewModelForEdit.Title.ToLower(), "search for your next recipe find!") == 0 || recipeCardViewModelForEdit.Title.Length == 0)
            {
                Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("You need a Title!");
                return;
            }

            recipeCardViewModelForEdit.SaveEditsToARecipe();
            recipeCardViewModelForEdit.RecipeType = CurrentType;
            int result = MainWindowViewModel.Instance.mainTreeViewNav.AddRecipeToTreeView(recipeCardViewModelForEdit, true);

            if (result == 1)
            {
                MainNavTreeViewModel.Instance.RemoveRecipeFromTreeView(recipeCardViewModelForEdit);
                DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(recipeCardViewModelForEdit.Title, (int)recipeCardViewModelForEdit.RecipeType, UserViewModel.Instance.UsersIDInDB);
            }
            if (result == 1 || result == 2)
            {
                DataBaseAccessorsForRecipeManager.SaveRecipeToDatabase(UserViewModel.Instance.UsersIDInDB, recipeCardViewModelForEdit, UserViewModel.Instance.UsersIDInDB);
                CloseBorrowPanel();
                ClearRecipe();
            }
        }

        /// <summary>
        /// triggers the visibility of the change-type ComboBox
        /// </summary>
        internal void ChangeVisiblityofTypeComboBox(bool visible)
        {
            if (visible == true)
            {
                TypeComboBoxVisibility = "Visible";
                CurrentType = recipeCardViewModelForEdit.RecipeType;
            }
            else
            {
                TypeComboBoxVisibility = "Collapsed";
            }
        }

        /// <summary>
        /// updates the recipe from the saved values, removing all of the unsaved changed the user may have made
        /// </summary>
        internal void RevertRecipe()
        {
            recipeCardViewModelForEdit.SetIngredientAndDirectionProperties();
            CurrentType = recipeCardViewModelForEdit.RecipeType;
        }

        public int NumberOfIngredients
        {
            get { return numberOfIngredients; }
            set { SetProperty(ref numberOfIngredients, value); }


        }

        public int NumberOfDirections
        {
            get { return numberOfDirections; }
            set { SetProperty(ref numberOfDirections, value); }
        }


        ///// <summary>
        ///// This manages changes that come in through the user manipulating the combobox on the Basket page
        ///// </summary>
        ///// <param name="e"></param>
        //internal void ChangeBorrowRecipeFromComboBox(SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems != null && e.AddedItems.Count > 0)
        //    {
        //        RecipeDisplayModel recipeCardModel = e.AddedItems[0] as RecipeDisplayModel;

        //        if (recipeCardModel != null)
        //        {
        //            if (SelectedViewModel.Instance.listOfRecipeModel.SettingCurrentIndexByTitle(recipeCardModel.Title) != -1)
        //            {
        //                recipeCardViewModelToBorrowFrom.UpdateRecipeDisplayFromRecipeRecord(SelectedViewModel.Instance.listOfRecipeModel.RecipesList[IndexOfComboBoxItem]);
        //            }
        //        }
        //    }

        //    //need to make this a sub to the first if statment because adding a new item to the listbox
        //    //removes the other which doesn't actually happen but the EventArgs still sends it as e.RemoveItems[0]
        //    else
        //    {
        //        if (e.RemovedItems != null && e.RemovedItems.Count > 0)
        //        {
        //            RecipeDisplayModel recipeCardModel = e.RemovedItems[0] as RecipeDisplayModel;

        //            if (recipeCardModel != null)
        //            {
        //                if (SelectedViewModel.Instance.listOfRecipeModel.SettingCurrentIndexByTitle(recipeCardModel.Title) != -1)
        //                {
        //                    recipeCardViewModelToBorrowFrom.UpdateRecipeDisplayFromRecipeRecord(SelectedViewModel.Instance.listOfRecipeModel.RecipesList[IndexOfComboBoxItem]);
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Changes the Type of recipe which effects where the recipe is stored and retreved on the Tree View
        /// </summary>
        /// <param name="e"></param>
        internal void ChangeTypeFromComboBox(SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                string type = e.AddedItems[0].ToString();

                for (int index = 0; index < MainNavTreeViewModel.Instance.CatagoryTypes.Count; index++)
                {
                    if (string.Compare(MainNavTreeViewModel.Instance.CatagoryTypes[index].ToString().ToLower(), type.ToLower()) == 0)
                    {
                        CurrentType = (Type_Of_Recipe)index;
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Closes the Borrow-Panel when the user clicks the 'X' button
        /// </summary>
        public void CloseBorrowPanel()
        {
            BorrowWidthSelected = "0";
            CloseButtonVisibilty = "Collapsed";
        }

        /// <summary>
        /// Opens the Borrow-Panel when the user clicks the 'X' button
        /// </summary>
        public void OpenBorrowPanel()
        {
            BorrowWidthSelected = "*";
            CloseButtonVisibilty = "Visible";

            ////need to update to the current card before opening the Borrow-Panel
            //if (SelectedViewModel.Instance.listOfRecipeModel.RecipesList.Count > 0)
            //    recipeCardViewModelToBorrowFrom.UpdateRecipeDisplayFromRecipeRecord(SelectedViewModel.Instance.listOfRecipeModel.RecipesList[IndexOfComboBoxItem]);
        }

        public static EditViewModel Instance
        {
            get { return instance; }
        }

        #region Commands
        public ICommand CmdSave
        {
            get;
            private set;
        }

        public ICommand CmdCloseBorrow
        {
            get;
            private set;
        }
        public ICommand CmdClear
        {
            get;
            private set;
        }

        public ICommand CmdRevert
        {
            get;
            private set;
        }

        public ICommand CmdNew
        {
            get;
            private set;
        }

        public ICommand CmdSelectedItemChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// property or the cancle-update button command
        /// </summary>
        public ICommand CmdCancelUpdate
        {
            get;
            private set;
        }

        public ICommand CmdTypeUpdate
        {
            get;
            private set;
        }

        /// <summary>
        /// property for the Recipe-type combobox change command
        /// </summary>
        public ICommand CmdSelectedTypeChanged
        {
            get;
            private set;
        }

        public ICommand CmdOpenBorrow
        {
            get;
            private set;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.  CanSelectComboBox
        /// </summary>
        public bool CanSelect
        {
            get
            {
                if (string.Compare(recipeCardViewModelForEdit.Title.ToLower(), "search for your next recipe find!") == 0)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, New is always clickable if the user is logged in!
        /// </summary>
        public bool CanSelectNew
        {
            get
            {
                return UserViewModel.Instance.CanSelectLogout;
            }
        }

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.  CanSelectComboBox
        /// </summary>
        public bool CanSelectBorrow
        {
            get
            {
                //if (SelectedViewModel.Instance.listOfRecipeModel.ListCount == 0)
                //    return false;
                //else
                    return true;
            }
        }

        private string typeComboBoxVisibility;
        public string TypeComboBoxVisibility
        {
            get { return typeComboBoxVisibility; }
            set { SetProperty(ref typeComboBoxVisibility, value); }
        }

        private string borrowWidthSelected;
        public string BorrowWidthSelected
        {
            get { return borrowWidthSelected; }
            set { SetProperty(ref borrowWidthSelected, value); }
        }

        private int indexOfComboBoxItem;
        public int IndexOfComboBoxItem
        {
            get { return indexOfComboBoxItem; }
            set { SetProperty(ref indexOfComboBoxItem, value); }
        }

        private string closeButtonVisibilty;
        public string CloseButtonVisibilty
        {
            get { return closeButtonVisibilty; }
            set { SetProperty(ref closeButtonVisibilty, value); }
        }

        private Type_Of_Recipe currentType;
        public Type_Of_Recipe CurrentType
        {
            get { return currentType; }
            set { SetProperty(ref currentType, value); }
        }

        private string borrowTextVisibilty;
        public string BorrowTextVisibilty
        {
            get { return borrowTextVisibilty; }
            set { SetProperty(ref borrowTextVisibilty, value); }
        }

        #endregion
    }


}

