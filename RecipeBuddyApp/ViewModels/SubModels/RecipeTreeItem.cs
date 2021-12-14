using System.Windows.Input;
using System.Collections.ObjectModel;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Helpers;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace RecipeBuddy.ViewModels
{
    public class RecipeTreeItem : ObservableObject
    {

        public RecipeTreeItem(string titleTreeItem)
        {
            recipeModelTV = new RecipeRecordModel();
            treeItemTitle = titleTreeItem;
            CmdAddToSelectList = new ICommandViewModel<RecipeTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            CmdAddToEdit = new ICommandViewModel<RecipeTreeItem>(Action => AddRecipeToEdit(), canCallActionFunc => CanSelect);
            CmdDelete = new ICommandViewModel<RecipeTreeItem>(Action => DeleteRecipe(), canCallActionFunc => CanSelect);
            CmdMouseClick = new ICommandViewModel<RecipeTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            Children = new ObservableCollection<RecipeTreeItem>();
        }
        public RecipeTreeItem(RecipeRecordModel recipeModel)
        {
            recipeModelTV = new RecipeRecordModel(recipeModel);
            treeItemTitle = recipeModelTV.Title;
            CmdAddToSelectList = new ICommandViewModel<RecipeTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            CmdAddToEdit = new ICommandViewModel<RecipeTreeItem>(Action => AddRecipeToEdit(), canCallActionFunc => CanSelect);
            CmdDelete = new ICommandViewModel<RecipeTreeItem>(Action => DeleteRecipe(), canCallActionFunc => CanSelect);
            CmdMouseClick = new ICommandViewModel<RecipeTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            Children = new ObservableCollection<RecipeTreeItem>();
        }


        /// <summary>
        /// Updates the various elements of the RecipeEntry from the next entry in the RecipeEntriesList, triggering the OnPropertyChanged event that is "contextbound" to the UI
        /// Adds the ingredents and Directions dynamically and then deletes them before the new recipe is replacing the old one.
        /// </summary>
        /// <param name="reSource">The new RecipeCard which we will use to overwrite the old values</param>
        public void UpdateRecipeEntry(RecipeTreeItem reSource)
        {
            recipeModelTV.CopyRecipeModel(reSource.recipeModelTV); 
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
            DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(this.treeItemTitle, recipeModelTV.TypeAsInt, UserViewModel.Instance.UsersIDInDB);
        }


        /// <summary>
        /// Indicates whether or not we can click the recipe-related button
        /// </summary>
        public bool CanSelect
        {
            get
            {
                if (RecipeModelTV == null)
                    return false;
                else
                    return true;
            }
        }

        public ObservableCollection<RecipeTreeItem> Children { get; set; }

        private RecipeRecordModel recipeModelTV;
        public RecipeRecordModel RecipeModelTV
        {
            get
            { return recipeModelTV; }
            set { recipeModelTV = value; }
        }

        private string treeItemTitle;
        public string TreeItemTitle
        {
            get
            { return treeItemTitle; }
            set { treeItemTitle = value; }
        }

        private bool itemExpanded;
        public bool ItemExpanded
        {
            get { return itemExpanded; }
            //set { itemExpanded = value; }
            set { SetProperty(ref itemExpanded, value); }
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


