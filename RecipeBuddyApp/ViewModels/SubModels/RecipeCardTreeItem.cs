using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Helpers;

namespace RecipeBuddy.ViewModels
{
     public class RecipeCardTreeItem : ObservableObject
    {

        public RecipeCardTreeItem()
        {
            recipeCardModelTV = new RecipeCardModel();
            recipeTitleTreeItem = recipeCardModelTV.Title;
            CmdAddToSelectList = new ICommandViewModel<RecipeCardTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            CmdAddToEdit = new ICommandViewModel<RecipeCardTreeItem>(Action => AddRecipeToEdit(), canCallActionFunc => CanSelect);
            CmdDelete = new ICommandViewModel<RecipeCardTreeItem>(Action => DeleteRecipe(), canCallActionFunc => CanSelect);
            CmdMouseClick = new ICommandViewModel<RecipeCardTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);

        }
        public RecipeCardTreeItem(RecipeCardModel recipeCardModel)
        {
            recipeCardModelTV = new RecipeCardModel(recipeCardModel);
            recipeTitleTreeItem = recipeCardModelTV.Title;
            CmdAddToSelectList = new ICommandViewModel<RecipeCardTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            CmdAddToEdit = new ICommandViewModel<RecipeCardTreeItem>(Action => AddRecipeToEdit(), canCallActionFunc => CanSelect);
            CmdDelete = new ICommandViewModel<RecipeCardTreeItem>(Action => DeleteRecipe(), canCallActionFunc => CanSelect);
            CmdMouseClick = new ICommandViewModel<RecipeCardTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
        }


        /// <summary>
        /// Updates the various elements of the RecipeEntry from the next entry in the RecipeEntriesList, triggering the OnPropertyChanged event that is "contextbound" to the UI
        /// Adds the ingredents and Directions dynamically and then deletes them before the new recipe is replacing the old one.
        /// </summary>
        /// <param name="reSource">The new RecipeCard which we will use to overwrite the old values</param>
        public void UpdateRecipeEntry(RecipeCardTreeItem reSource)
        {
            recipeCardModelTV.CopyRecipeCardModel(reSource.recipeCardModelTV); 
        }


        /// <summary>
        /// Add the recipe to the Borrow list and select that recipe in the combobox as well as 
        /// take the user to the Edit page.
        /// </summary>
        internal void AddRecipeToSelectList()
        {
            MainNavTreeViewModel.Instance.AddRecipeToSelectList(this);
        }


        /// <summary>
        /// Add the recipe to the Edit page
        /// </summary>
        internal void AddRecipeToEdit()
        {
            MainNavTreeViewModel.Instance.AddRecipeToEdit(this);
        }

        /// <summary>
        /// Removes this recipe from treeview 
        /// </summary>
        /// <param name="recipeCard">The recipe to be added</param>
        /// <returns>A bool with true if the save was successful, false if not</returns>
        internal void DeleteRecipe()
        {
            MainNavTreeViewModel.Instance.RemoveRecipeFromTreeView(this);
            DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(this.recipeTitleTreeItem, recipeCardModelTV.TypeAsInt, UserViewModel.Instance.UsersIDInDB);
        }


        /// <summary>
        /// Indicates whether or not we can click the recipe-related button
        /// </summary>
        public bool CanSelect
        {
            get
            {
                if (RecipeModelPropertyTV == null)
                    return false;
                else
                    return true;
            }
        }

        private RecipeCardModel recipeCardModelTV;

        public RecipeCardModel RecipeModelPropertyTV
        {
            get
            { return recipeCardModelTV; }
            set { SetProperty(ref recipeCardModelTV, value); }
        }

        private string recipeTitleTreeItem;

        public string RecipeTitleTreeItem
        {
            get
            { return recipeTitleTreeItem; }
            set { SetProperty(ref recipeTitleTreeItem, value); }
        }

        public ICommand CmdAddToSelectList
        {
            get;
            private set;
        }

        public ICommand CmdAddToEdit
        {
            get;
            private set;
        }

        public ICommand CmdDelete
        {
            get;
            private set;
        }

        public ICommand CmdMouseClick
        {
            get;
            private set;
        }
    }
}


