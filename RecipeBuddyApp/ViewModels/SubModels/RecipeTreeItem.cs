﻿using System.Windows.Input;
using System.Collections.ObjectModel;
using RecipeBuddy.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Helpers;
using Microsoft.UI.Xaml.Controls;
using RecipeBuddy.Services;
using RecipeBuddy.Views;
using CommunityToolkit.Mvvm.Input;
using System;

namespace RecipeBuddy.ViewModels
{
    public class RecipeTreeItem : ObservableObject
    {
        Action<TreeViewItemInvokedEventArgs> actionTreeViewArg;
        public RecipeTreeItem(string titleTreeItem)
        {
            recipeModelTV = new RecipeRecordModel();
            treeItemTitle = titleTreeItem;
            CmdAddToSelectList = new RelayCommand<RecipeTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            CmdDelete = new RelayCommand<RecipeTreeItem>(Action => DeleteRecipe(), canCallActionFunc => CanSelect);
            CmdContextMenuRequest = new RelayCommand<TreeViewItemInvokedEventArgs>(actionTreeViewArg = (args) => ContextMenuOrNot(args));
            CmdMouseClick = new RelayCommand<RecipeTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            Children = new ObservableCollection<RecipeTreeItem>();
        }
        public RecipeTreeItem(RecipeRecordModel recipeModel)
        {
            recipeModelTV = new RecipeRecordModel(recipeModel);
            treeItemTitle = recipeModelTV.Title;
            CmdAddToSelectList = new RelayCommand<RecipeTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            CmdDelete = new RelayCommand<RecipeTreeItem>(Action => DeleteRecipe(), canCallActionFunc => CanSelect);
            CmdMouseClick = new RelayCommand<RecipeTreeItem>(Action => AddRecipeToSelectList(), canCallActionFunc => CanSelect);
            CmdContextMenuRequest = new RelayCommand<TreeViewItemInvokedEventArgs>(actionTreeViewArg = (args) => ContextMenuOrNot(args));
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

        public void UpdateRecipeEntry(RecipeDisplayModel reSource)
        {
            recipeModelTV.CopyRecipeModel(reSource);
        }

        /// <summary>
        /// Add the recipe to the Borrow list and select that recipe in the combobox as well as 
        /// take the user to the Edit page.
        /// </summary>
        internal void AddRecipeToSelectList()
        {
            if ((recipeModelTV.TypeAsInt != (int)Type_Of_Recipe.Header))
            {
                MainNavTreeViewModel.Instance.AddRecipeToSelect(this);
                NavigationService.Navigate((typeof(SelectedView)));
            }
        }

        /// <summary>
        /// Removes this recipe from treeview 
        /// </summary>
        /// <param name="recipeCard">The recipe to be added</param>
        /// <returns>A bool with true if the save was successful, false if not</returns>
        internal void DeleteRecipe()
        {
            MainNavTreeViewModel.Instance.RemoveRecipeFromTreeView(this);
            DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(recipeModelTV.RecipeDBID);
        }

        internal void ContextMenuOrNot(TreeViewItemInvokedEventArgs arg)
        {
            if (arg.InvokedItem != null && arg.InvokedItem.GetType() == typeof(RecipeTreeItem))
            {
                RecipeTreeItem recipeTreeItem = arg.InvokedItem as RecipeTreeItem;

                if (recipeTreeItem.RecipeModelTV.TypeAsInt == (int)Type_Of_Recipe.Header)
                    return;
            }
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
            get { return recipeModelTV; }
            set { SetProperty(ref recipeModelTV, value); }
        }

        private string treeItemTitle;
        public string TreeItemTitle
        {
            get { return treeItemTitle; }
            set { SetProperty(ref treeItemTitle, value); }
        }

        private bool itemExpanded;
        public bool ItemExpanded
        {
            get { return itemExpanded; }
            set { SetProperty(ref itemExpanded, value); }
        }

        public RelayCommand<TreeViewItemInvokedEventArgs> CmdContextMenuRequest
        {
            get;
            set;
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


