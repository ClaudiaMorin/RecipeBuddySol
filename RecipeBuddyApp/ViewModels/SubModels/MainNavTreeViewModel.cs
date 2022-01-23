using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using RecipeBuddy.Services;
using RecipeBuddy.Views;
using Microsoft.Toolkit.Mvvm.Input;

namespace RecipeBuddy.ViewModels
{
    

    public class MainNavTreeViewModel : ObservableObject
    {
        Action<TreeViewItemInvokedEventArgs> actionTreeViewArg;

        private static readonly MainNavTreeViewModel instance = new MainNavTreeViewModel();
        public static MainNavTreeViewModel Instance
        {
            get { return instance; }
        }

        static MainNavTreeViewModel()
        { }

        private MainNavTreeViewModel()
        {
            RecipeTreeRootNodes = new ObservableCollection<RecipeTreeItem>();
            //CmdSaveDialog = new RBRelayCommandObj(actionRecipeDisplayModel = (RD) => AddRecipeToTreeView((RecipeDisplayModel)RD));
            //CmdAddTreeVeiwItemToSelectList = new RBRelayCommandObj(actionRecipeDisplayModel = (TreeViewArg) => AddSelectedTreeViewItem((TreeViewItemInvokedEventArgs)TreeViewArg));
            CmdAddTreeVeiwItemToSelectList = new RelayCommand<TreeViewItemInvokedEventArgs>(actionTreeViewArg = (args) => AddSelectedTreeViewItem(args));

            //CmdAddTreeVeiwItemToSelectList = new ICommandViewModel<TreeViewItemInvokedEventArgs>(actionWithEventArgs = e => AddSelectedTreeViewItem(e), canCallActionFunc => CanSelectTrue);
            //CmdExpandTreeViewItem = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => CanSelectRemove);

            DessertRecipes = new RecipeTreeItem("Desserts");
            DessertRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            MainCourseRecipes = new RecipeTreeItem("Main Courses");
            DessertRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedCakeRecipes = new RecipeTreeItem("Cake");
            DessertRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedCandyRecipes = new RecipeTreeItem("Candy");
            DessertRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedCookieRecipes = new RecipeTreeItem("Cookie");
            DessertRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedCustardRecipes = new RecipeTreeItem("Custard");
            DessertRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedPastryRecipes = new RecipeTreeItem("Pastry");
            SavedUnknownRecipes = new RecipeTreeItem("Unknown");
            SavedSoupStewRecipes = new RecipeTreeItem("Soup and Stews");
            SavedPorkRecipes = new RecipeTreeItem("Pork");
            SavedDairyRecipes = new RecipeTreeItem("Dairy");
            SavedBeefRecipes = new RecipeTreeItem("Beef");
            SavedEggsRecipes = new RecipeTreeItem("Eggs");
            SavedPoultryRecipes = new RecipeTreeItem("Poultry");
            SavedSeafoodRecipes = new RecipeTreeItem("Seafood");
            SavedLambRecipes = new RecipeTreeItem("Lamb");
            SavedSaladRecipes = new RecipeTreeItem("Salad");
            SavedAppetizerRecipes = new RecipeTreeItem("Appetizer");
            SavedBreadRecipes = new RecipeTreeItem("Bread");
            SavedSideDishRecipes = new RecipeTreeItem("Side Dish");
            SavedTofuRecipes = new RecipeTreeItem("Tofu");

            CreateTreeViewHierarchy(RecipeTreeRootNodes);
            FillCatagoryCollectionForDropDown();

            //Add User Data To Tree
            List<RecipeRecordModel> recipesFromDB =  DataBaseAccessorsForRecipeManager.LoadUserDataByID(UserViewModel.Instance.UserName, UserViewModel.Instance.UsersIDInDB);   
        }

        /// <summary>
        /// TreeViewNodes
        /// </summary>

        public RecipeListModel listOfRecipeCards;

        //public RecipeTreeItem RecipeTreeRoot;
        public RecipeTreeItem DessertRecipes;
        public RecipeTreeItem MainCourseRecipes;

        public RecipeTreeItem SavedCakeRecipes;
        public RecipeTreeItem SavedCandyRecipes;
        public RecipeTreeItem SavedCookieRecipes;
        public RecipeTreeItem SavedCustardRecipes;
        public RecipeTreeItem SavedPastryRecipes;
        public RecipeTreeItem SavedUnknownRecipes;
        public RecipeTreeItem SavedSoupStewRecipes;
        public RecipeTreeItem SavedPorkRecipes;
        public RecipeTreeItem SavedDairyRecipes;
        public RecipeTreeItem SavedBeefRecipes;
        public RecipeTreeItem SavedEggsRecipes;
        public RecipeTreeItem SavedPoultryRecipes;
        public RecipeTreeItem SavedSeafoodRecipes;
        public RecipeTreeItem SavedLambRecipes;
        public RecipeTreeItem SavedSaladRecipes;
        public RecipeTreeItem SavedAppetizerRecipes;
        public RecipeTreeItem SavedBreadRecipes;
        public RecipeTreeItem SavedSideDishRecipes;
        public RecipeTreeItem SavedTofuRecipes;

        public ObservableCollection<RecipeTreeItem> AddRemoveItem { get; set; }
        public ObservableCollection<string> CatagoryTypes { get; set; }

        private void FillCatagoryCollectionForDropDown()
        {

            CatagoryTypes = new ObservableCollection<string>()
            {
               "Appetizer","Beef","Bread", "Cake", "Candy", "Cookie", "Custard", "Dairy", "Eggs", "Lamb", "Pastery", "Pork", "Poultry",
                "Salad", "Seafood", "Side Dish", "Soup Stew", "Tofu", "Unknown"
            };
        }

        public void CreateTreeViewHierarchy(ObservableCollection<RecipeTreeItem> RecipeTreeRoot)
        {
            DessertRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            MainCourseRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedCakeRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedCandyRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedCookieRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedCustardRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedPastryRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;

            SavedUnknownRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedSoupStewRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedPorkRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedDairyRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedBeefRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedEggsRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedPoultryRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;

            SavedSeafoodRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedLambRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedSaladRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedAppetizerRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedBreadRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedSideDishRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;
            SavedTofuRecipes.RecipeModelTV.TypeAsInt = (int)Type_Of_Recipe.Header;

            RecipeTreeRoot.Add(SavedAppetizerRecipes);
            RecipeTreeRoot.Add(SavedBreadRecipes);
            RecipeTreeRoot.Add(SavedDairyRecipes);
            RecipeTreeRoot.Add(DessertRecipes);
            RecipeTreeRoot.Add(MainCourseRecipes);
            RecipeTreeRoot.Add(SavedSaladRecipes);
            RecipeTreeRoot.Add(SavedSideDishRecipes);
            RecipeTreeRoot.Add(SavedSoupStewRecipes);
            RecipeTreeRoot.Add(SavedUnknownRecipes);

            DessertRecipes.Children.Add(SavedCakeRecipes);
            DessertRecipes.Children.Add(SavedCandyRecipes);
            DessertRecipes.Children.Add(SavedCookieRecipes);
            DessertRecipes.Children.Add(SavedCustardRecipes);
            DessertRecipes.Children.Add(SavedPastryRecipes);

            MainCourseRecipes.Children.Add(SavedBeefRecipes);
            MainCourseRecipes.Children.Add(SavedPorkRecipes);
            MainCourseRecipes.Children.Add(SavedPoultryRecipes);
            MainCourseRecipes.Children.Add(SavedLambRecipes);
            MainCourseRecipes.Children.Add(SavedEggsRecipes);
            MainCourseRecipes.Children.Add(SavedSeafoodRecipes);
            MainCourseRecipes.Children.Add(SavedTofuRecipes);

            //MainCourseRecipes.ItemExpanded = true;
            //DessertRecipes.ItemExpanded = true;
        }

        /// <summary>
        /// Removes all recipes from the tree.
        /// </summary>
        public void ClearTree()
        {
            SavedCakeRecipes.Children.Clear();
            SavedCandyRecipes.Children.Clear();
            SavedCookieRecipes.Children.Clear();
            SavedCustardRecipes.Children.Clear();
            SavedPastryRecipes.Children.Clear();
            SavedPoultryRecipes.Children.Clear();
            SavedPorkRecipes.Children.Clear();
            SavedBeefRecipes.Children.Clear();
            SavedSeafoodRecipes.Children.Clear();
            SavedSoupStewRecipes.Children.Clear();
            SavedLambRecipes.Children.Clear();
            SavedBreadRecipes.Children.Clear();
            SavedSaladRecipes.Children.Clear();
            SavedAppetizerRecipes.Children.Clear();
            SavedSideDishRecipes.Children.Clear();
            SavedUnknownRecipes.Children.Clear();
            SavedTofuRecipes.Children.Clear();
            SavedDairyRecipes.Children.Clear();
            SavedEggsRecipes.Children.Clear();
            MainCourseRecipes.ItemExpanded = false;
            DessertRecipes.ItemExpanded = false;
        }

        /// <summary>
        /// Adding a list of recipes to the tree view as part of the initial setup from the DB
        /// </summary>
        /// <param name="models">The list of RecipeRecords</param>
        public void AddRecipeModelsToTreeViewAsPartOfInitialSetup(List<RecipeRecordModel> models)
        {
            foreach (RecipeRecordModel recipeCard in models)
            {
                AddRecipeToTreeViewNoExpand(recipeCard);
            }
        }

        //public async Task AddRecipeModelsToTreeView(RecipeRecordModel models, bool expandHeader = false, Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView = null)
        public void AddRecipeModelsToTreeView(RecipeRecordModel models, bool expandHeader = false)
        {
            AddRecipeToTreeView(models, expandHeader);
        }

        /// <summary>
        /// Digs out the recipe that is in one of the collection lists
        /// </summary>
        /// <param name="collection">This is going to identify one of the collection lists we are supposed to look for our information in</param>
        /// <param name="titleOfRecipe">The title of the recipe that we are looking for</param>
        /// <returns>RecipeCardModle to find or a null RecipeCardModle if the right one couldn't be found</returns>
        public RecipeTreeItem FindRecipeInCollection(ObservableCollection<RecipeTreeItem> collection, string titleOfRecipe)
        {

            foreach (RecipeTreeItem RC in collection)
            {
                if (string.Compare(RC.RecipeModelTV.Title.ToLower(), titleOfRecipe.ToLower()) == 0)
                {
                    return RC;
                }
            }

            return new RecipeTreeItem(titleOfRecipe);
        }

        /// <summary>
        /// Wrapper around the FindRecipeInCollection that allows the user to send in a type instead of identifying the collection
        /// </summary>
        /// <param name="type"></param>
        /// <param name="titleOfRecipe"></param>
        /// <returns></returns>
        public RecipeTreeItem FindRecipeInCollection(Type_Of_Recipe type, string titleOfRecipe)
        {
            RecipeTreeItem recipeTreeNode = GetRecipeParentNodeFromType(type);
            return FindRecipeInCollection(recipeTreeNode.Children, titleOfRecipe);
        }

        public void ChangedTreeItemTitle(string oldTitle, string newTitle, Type_Of_Recipe type_Of_Recipe)
        {
            RecipeTreeItem recipeTreeNode = GetRecipeTreeItem(oldTitle, type_Of_Recipe);
            if (recipeTreeNode != null)
            {
                recipeTreeNode.RecipeModelTV.Title = newTitle;
                recipeTreeNode.TreeItemTitle = newTitle;
            }
        }

        /// <summary>
        /// uses the type_of_Recipe to identify the Parent Header node and then itterates through the children to find one where
        /// the title matches and returns that to the caller
        /// </summary>
        /// <param name="title">The title of the recipe we are looking for</param>
        /// <param name="type_Of_Recipe">The recipe type which gives us the "parent node type"</param>
        /// <returns>The recipe tree Item we are looking for</returns>
        public RecipeTreeItem GetRecipeTreeItem(string title, Type_Of_Recipe type_Of_Recipe)
        {
            RecipeTreeItem recipeTreeNode = GetRecipeParentNodeFromType(type_Of_Recipe);
            RecipeTreeItem recipeTreeItem = null;

            for (int count = 0; count < recipeTreeNode.Children.Count; count++)
            {
                if (string.Compare(recipeTreeNode.Children[count].TreeItemTitle.ToLower(), title.ToLower()) == 0)
                {
                    recipeTreeItem = recipeTreeNode.Children[count];
                    break;
                }
            }

            return recipeTreeItem;
        }

        /// <summary>
        /// Removes a recipe from the treeview 
        /// </summary>
        /// <param name="RecipeTreeItem">The recipe to be removed</param>
        /// <returns>A bool with true if the save was successful, false if not</returns>
        public bool RemoveRecipeFromTreeView(RecipeTreeItem recipeModel)
        {
            return RemoveRecipeFromTreeViewBase(recipeModel.RecipeModelTV.TypeAsInt, recipeModel.RecipeModelTV.Title);
        }

        /// <summary>
        /// Removes a recipe from the treeview 
        /// </summary>
        /// <param name="RecipeCardModel">The recipe to be removed</param>
        /// <returns>A bool with true if the save was successful, false if not</returns>
        public bool RemoveRecipeFromTreeView(RecipeDisplayModel recipeCardModel)
        {
            return RemoveRecipeFromTreeViewBase((int)recipeCardModel.RecipeType, recipeCardModel.Title);
        }

        /// <summary>
        /// Removes a recipe from the treeview 
        /// </summary>
        /// <param name="RecipeCardModel">The recipe to be removed</param>
        /// <returns>A bool with true if the save was successful, false if not</returns>
        public bool RemoveRecipeFromTreeView(RecipeRecordModel recipeModel)
        {
            return RemoveRecipeFromTreeViewBase(recipeModel.TypeAsInt, recipeModel.Title);
        }

        /// <summary>
        /// Removes a recipe from the treeview 
        /// </summary>
        /// <param name="recipeCard">The recipe to be added</param>
        /// <returns>A bool with true if the save was successful, false if not</returns>
        private bool RemoveRecipeFromTreeViewBase(int recipeTypeAsInt, string title)
        {
            bool SavedSucceeded = false;
            RecipeTreeItem parentTreeNode = GetRecipeParentNodeFromType(recipeTypeAsInt);
            return SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(parentTreeNode.Children, title, recipeTypeAsInt);
        }

        /// <summary>
        /// Uses the recipe type to return the correct parent node
        /// </summary>
        /// <param name="recipeType">The type of a given recipe in an int format</param>
        /// <returns></returns>
        private RecipeTreeItem GetRecipeParentNodeFromType(int recipeType)
        {
            Type_Of_Recipe type = (Type_Of_Recipe)recipeType;
            RecipeTreeItem recipeTreeItem = GetRecipeParentNodeFromType(type);

            return recipeTreeItem;
        }

        /// <summary>
        /// Uses the recipe type to return the correct parent node
        /// </summary>
        /// <param name="recipeType">Type of the recipe in the Type_Of_Recipe enum format</param>
        /// <returns>The parent node to which the recipetype belongs</returns>
        private RecipeTreeItem GetRecipeParentNodeFromType(Type_Of_Recipe recipeType)
        {
            RecipeTreeItem recipeTreeItem = null;

            switch (recipeType)
            {
                case Type_Of_Recipe.Cake:
                    recipeTreeItem = SavedCakeRecipes;
                    break;

                case Type_Of_Recipe.Candy:
                    recipeTreeItem = SavedCandyRecipes;
                    break;

                case Type_Of_Recipe.Cookie:
                    recipeTreeItem = SavedCookieRecipes;
                    break;

                case Type_Of_Recipe.Unknown:
                    recipeTreeItem = SavedUnknownRecipes;
                    break;

                case Type_Of_Recipe.Custard:
                    recipeTreeItem = SavedCustardRecipes;
                    break;

                case Type_Of_Recipe.Pastry:
                    recipeTreeItem = SavedPastryRecipes;
                    break;

                case Type_Of_Recipe.SoupStew:
                    recipeTreeItem = SavedSoupStewRecipes;
                    break;

                case Type_Of_Recipe.Poultry:
                    recipeTreeItem = SavedPoultryRecipes;
                    break;

                case Type_Of_Recipe.Pork:
                    recipeTreeItem = SavedPorkRecipes;
                    break;

                case Type_Of_Recipe.Beef:
                    recipeTreeItem = SavedPorkRecipes;
                    break;

                case Type_Of_Recipe.Lamb:
                    recipeTreeItem = SavedLambRecipes;
                    break;

                case Type_Of_Recipe.Seafood:
                    recipeTreeItem = SavedSeafoodRecipes;
                    break;

                case Type_Of_Recipe.Salad:
                    recipeTreeItem = SavedSaladRecipes;
                    break;

                case Type_Of_Recipe.Bread:
                    recipeTreeItem = SavedBreadRecipes;
                    break;

                case Type_Of_Recipe.SideDish:
                    recipeTreeItem = SavedSideDishRecipes;
                    break;

                case Type_Of_Recipe.Tofu:
                    recipeTreeItem = SavedTofuRecipes;
                    break;

                case Type_Of_Recipe.Dairy:
                    recipeTreeItem = SavedDairyRecipes;
                    break;

                case Type_Of_Recipe.Eggs:
                    recipeTreeItem = SavedEggsRecipes;
                    break;

                case Type_Of_Recipe.Appetizer:
                    recipeTreeItem = SavedAppetizerRecipes;
                    break;
            }

            return recipeTreeItem;
        }


        /// <summary>
        /// A private function that actually does the adding to the specified list in the tree view
        /// </summary>
        /// <param name="ListToUse">The preselected list</param>
        /// <param name="recipeCardModel">The RecipeModel we are adding to the preselected list</param>
        /// <param name="action">The specific action keyed to the list</param>
        private bool AddRecipeToSpecifiedTreeViewList(ObservableCollection<RecipeTreeItem> ListToUse, RecipeTreeItem recipeCardViewModel, Action<string, Type_Of_Recipe, Type_Of_Recipe_Action, bool> actionAddRemoveList, bool TreeViewItemsExpand)
        {
            for (int i = 0; i < ListToUse.Count; i++)
            {
                if (ListToUse[i].RecipeModelTV.Title == recipeCardViewModel.RecipeModelTV.Title && recipeCardViewModel.RecipeModelTV.Title != "Search For Your Recipe")
                {
                    //Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("There is a problem with your account!");

                    //MessageBoxResult results = MessageBox.Show("Saving this recipe will overwrite the one currently saved. \n\n  If you don't want to do this change the title before saving!", "Do you want to overwite saved recipe!", MessageBoxButton.OKCancel);

                    //if (results == MessageBoxResult.OK)
                    //{
                    //    //Overwrite the recipe
                    //    ListToUse.Add(recipeCardViewModel);
                    //    //remove the old recipe and then add the new one
                    //    actionAddRemoveList(recipeCardViewModel.RecipeModelPropertyTV.Title, (Type_Of_Recipe)recipeCardViewModel.RecipeModelPropertyTV.TypeAsInt, Type_Of_Recipe_Action.Remove, false);
                    //    //Now remove it from the DB
                    //    DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(recipeCardViewModel.RecipeModelPropertyTV.Title, recipeCardViewModel.RecipeModelPropertyTV.TypeAsInt);

                    //    actionAddRemoveList(recipeCardViewModel.RecipeModelPropertyTV.Title, (Type_Of_Recipe)recipeCardViewModel.RecipeModelPropertyTV.TypeAsInt, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);
                    //    return true;
                    //}
                    ////user chose to not save
                    return false;
                }
            }

            ListToUse.Add(recipeCardViewModel);
            //False will add the item to the list
            actionAddRemoveList(recipeCardViewModel.RecipeModelTV.Title, (Type_Of_Recipe)recipeCardViewModel.RecipeModelTV.TypeAsInt, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);

            return true;
        }

        /// <summary>
        /// A private function that actually does the adding to the specified list in the tree view
        /// </summary>
        /// <param name="ListToUse">The preselected list</param>
        /// <param name="recipeCardModel">The RecipeModel we are adding to the preselected list</param>
        /// <param name="action">The specific action keyed to the list</param>
        //private bool AddRecipeToSpecifiedTreeViewList(ObservableCollection<RecipeDisplayModel> ListToUse, RecipeDisplayModel recipeCardModel, Action<string, Type_Of_Recipe, Type_Of_Recipe_Action, bool> actionAddRemoveList, bool TreeViewItemsExpand)
        //{
        //    for (int i = 0; i < ListToUse.Count; i++)
        //    {
        //        if (ListToUse[i].Title == recipeCardModel.Title && recipeCardModel.Title != "Search For Your Recipe")
        //        {
        //            //MessageBoxResult results = MessageBox.Show("Saving this recipe will overwrite the one currently saved. \n\n  If you don't want to do this change the title before saving!", "Do you want to overwite saved recipe!", MessageBoxButton.OKCancel);

        //            /*if (results == MessageBoxResult.OK)
        //            {
        //                Overwrite the recipe
        //                ListToUse.Add(recipeCardModel);
        //                remove the old recipe and then add the new one
        //                actionAddRemoveList(recipeCardModel.Title,(Type_Of_Recipe)recipeCardModel.TypeAsInt, Type_Of_Recipe_Action.Remove, false);
        //                Now remove it from the DB
        //                DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(recipeCardModel.Title, recipeCardModel.TypeAsInt);

        //                actionAddRemoveList(recipeCardModel.Title, (Type_Of_Recipe)recipeCardModel.TypeAsInt, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);
        //                return true;
        //            }
        //            user chose to not save
        //            return false;*/
        //        }
        //    }

        //    ListToUse.Add(recipeCardModel);
        //    //False will add the item to the list
        //    actionAddRemoveList(recipeCardModel.Title, (Type_Of_Recipe)recipeCardModel.RecipeType, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);

        //    return true;
        //}

        /// <summary>
        /// Changes the Type of recipe which effects where the recipe is stored and retreved on the Tree View
        /// </summary>
        /// <param name="e"></param>
        internal void AddSelectedTreeViewItem(TreeViewItemInvokedEventArgs arg)
        {
            if (arg.InvokedItem != null && arg.InvokedItem.GetType() == typeof(RecipeTreeItem))
            {
                RecipeTreeItem recipeTreeItem = arg.InvokedItem as RecipeTreeItem;

                if(recipeTreeItem.RecipeModelTV.TypeAsInt != (int)Type_Of_Recipe.Header)
                SelectedViewModel.Instance.UpdateRecipeEntry(recipeTreeItem.RecipeModelTV);
                NavigationService.Navigate(typeof(SelectedView));
            }
        }

        public void MoveSelectedTreeViewItem(RecipeDisplayModel recipe, Type_Of_Recipe deleteTargetType)
        {
            AddRecipeToTreeView(recipe);
            RecipeTreeItem parent = GetRecipeParentNodeFromType(deleteTargetType);

            for (int i = 0; i < parent.Children.Count; i++)
            {
                if (parent.Children[i].RecipeModelTV.Title == recipe.Title && recipe.Title != "Search For Your Recipe")
                {
                    //Remove the recipe
                    parent.Children.RemoveAt(i);
                }
            }
        }

        ///// <summary>
        ///// Changes the Type of recipe which effects where the recipe is stored and retreved on the Tree View
        ///// </summary>
        ///// <param name="e"></param>
        //internal void AddSelectedTreeViewItem(SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems != null && e.AddedItems.Count > 0)
        //    {
        //        RecipeRecordModel recipeRecordModel = e.AddedItems[0] as RecipeRecordModel;
        //        SelectedViewModel.Instance.UpdateRecipeEntry(recipeRecordModel);
        //    }
        //}

        /// <summary>
        /// Dialog that pops up after the user attempts to save 
        /// </summary>
        /// <param name="recipeCard"></param>
        //private void ContentDialogOverWriteOrCancel(RecipeDisplayModel recipeCard)
        //{
        //    ContentDialog overWriteOrCancel = new ContentDialog()
        //    {
        //        Title = "Overwrite Recipe ?",
        //        Content = "A recipe with this title already exists, do you want to overwrite it?",
        //        CloseButtonText = "Cancel",
        //        PrimaryButtonText = "Overwrite",
        //        PrimaryButtonCommand = CmdSaveDialog,
        //        PrimaryButtonCommandParameter = recipeCard
        //    };
        //}

        /// <summary>
        /// A private function that actually does the removing from the specified list
        /// </summary>
        /// <param name="ListToUse">The preselected list</param>
        /// <param name="recipeModel">The RecipeModel we are removing from the preselected list</param>
        /// <param name="action">The specific action keyed to the list</param>
        private bool RemoveRecipeFromSpecifiedTreeViewList(ObservableCollection<RecipeTreeItem> ListToUse, string title, int recipeTypeAsInt)
        {
            return RemoveRecipeFromTreeViewList(ListToUse, title, recipeTypeAsInt);
        }

        /// <summary>
        /// A private function that actually does the removing from the specified list
        /// </summary>
        /// <param name="ListToUse">The preselected list</param>
        /// <param name="recipeModel">The RecipeModel we are removing from the preselected list</param>
        /// <param name="action">The specific action keyed to the list</param>
        private bool RemoveRecipeFromTreeViewList(ObservableCollection<RecipeTreeItem> ListToUse, string title, int recipeTypeAsInt)
        {
            for (int i = 0; i < ListToUse.Count; i++)
            {
                if (ListToUse[i].RecipeModelTV.Title == title && title != "Search For Your Recipe")
                {
                    //Remove the recipe
                    ListToUse.RemoveAt(i);
                    DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(title, recipeTypeAsInt, UserViewModel.Instance.UsersIDInDB);
                    return true;
                }
            }

            return false;
        }

        public void SaveUpdatesToRecipe(RecipeDisplayModel recipeDisplay)
        {
            RecipeTreeItem recipeTreeItem = FindRecipeInCollection(recipeDisplay.RecipeType, recipeDisplay.Title);
            recipeTreeItem.UpdateRecipeEntry(recipeDisplay);
        }

        /// <summary>
        /// Checks the entry to be saved to see if that title exists in the catagory underwhich the recipe is going to be saved
        /// </summary>
        //public void SaveEntry(RecipeDisplayModel recipeDisplay)
        //{
        //    recipeDisplay.SaveEditsToARecipeModel();

        //    int result = MainWindowViewModel.Instance.mainTreeViewNav.AddRecipeToTreeView(recipeDisplay);

        //    if (result == 0)
        //    {
        //        MainWindowViewModel.Instance.mainTreeViewNav.ContentDialogOverWriteOrCancel(recipeDisplay);
        //    }

        //    //If the return is 0 we know that it was an overwrite and the dialogbox took care of it already
        //    if (result == 1)
        //    {
        //        //didn't find any conflicts in the first attempt so it has already been saved.
        //    }
        //}

        /// <summary>
        /// Takes the RecipeCardModel, converts it to a recipeModel and then saves it to the treeview
        /// </summary>
        /// <param name="recipeDisplayModel">The RecipeDisplayModel to be saved</param>
        /// <param name="expandTreeViewHeader">are we expanding the tree header when we add the item?</param>
        /// <returns>0 if we have to overwrite an existing recipe</returns>
        public int AddRecipeToTreeView(RecipeDisplayModel recipeDisplayModel, bool expandTreeViewHeader = true)
        {
            RecipeRecordModel recipeModel = new RecipeRecordModel(recipeDisplayModel);
            return AddRecipeToTreeViewWork(recipeModel, expandTreeViewHeader);
        }

        /// <summary>
        /// Adding a record to the treeview
        /// </summary>
        /// <param name="recipeRecordModel">The RecipeDisplayModel to be saved</param>
        /// <param name="expandTreeViewHeader">are we expanding the tree header when we add the item?</param>
        /// <param name="view"></param>
        /// <returns>0 if we have to overwrite an existing recipe</returns>
        public int AddRecipeToTreeView(RecipeRecordModel recipeRecordModel, bool expandTreeViewHeader = true)
        {
            return AddRecipeToTreeViewWork(recipeRecordModel, expandTreeViewHeader);
        }

        /// <summary>
        /// Takes a RecipeRecordModel and saves it
        /// </summary>
        /// <param name="recipeRecordModel"></param>
        /// <param name="expandTreeViewHeader"></param>
        /// <returns></returns>
        public int AddRecipeToTreeViewNoExpand(RecipeRecordModel recipeRecordModel)
        {
            return  AddRecipeToTreeViewWork(recipeRecordModel);
        }

        /// <summary>
        /// Does the work of adding a recipe to the treeview
        /// </summary>
        /// <param name="recipeModel">the recipeModel record we are adding/param>
        /// <param name="expandTreeViewHeader">default is true, should the node be expanded when the addition happens</param>
        /// <returns></returns>
        private int AddRecipeToTreeViewWork(RecipeRecordModel recipeModel, bool expandTreeViewHeader = false)
        {
            int returnOverwriteCmd = 0;
            //Create a new RecipeCardTreeItem
            RecipeTreeItem recipeTreeItem = new RecipeTreeItem(recipeModel);
            RecipeTreeItem parentTreeNode = GetRecipeParentNodeFromType(recipeTreeItem.RecipeModelTV.TypeAsInt);

            //returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem.TreeItemTitle, recipeTreeItem.RecipeModelTV.TypeAsInt);
            if (CheckIfRecipeAlreadyPresent(recipeTreeItem.TreeItemTitle, recipeTreeItem.RecipeModelTV.TypeAsInt) == true)
                return returnOverwriteCmd;
            parentTreeNode.Children.Add(recipeTreeItem);
            parentTreeNode.ItemExpanded = expandTreeViewHeader;
            returnOverwriteCmd = 1;

            switch ((Type_Of_Recipe)parentTreeNode.RecipeModelTV.TypeAsInt)
            {
                case Type_Of_Recipe.Cake:
                case Type_Of_Recipe.Cookie:
                case Type_Of_Recipe.Candy:
                case Type_Of_Recipe.Custard:
                case Type_Of_Recipe.Pastry:
                    DessertRecipes.ItemExpanded = expandTreeViewHeader;
                    break;

                case Type_Of_Recipe.Seafood:
                case Type_Of_Recipe.Pork:
                case Type_Of_Recipe.Beef:
                case Type_Of_Recipe.Eggs:
                case Type_Of_Recipe.Poultry:
                case Type_Of_Recipe.Lamb:
                case Type_Of_Recipe.Tofu:
                    MainCourseRecipes.ItemExpanded = expandTreeViewHeader;
                    break;
            }

            return returnOverwriteCmd;
        }



        /// <summary>
        /// goes through the list looking for the same title that we are trying to add to the list
        /// if found it will return true
        /// </summary>
        /// <param name="title">the title of the recipe we are attempting to add</param>
        /// <param name="type">the type of this recipe, we use this to fine the parent node and then access the collection through the children</param>
        /// <returns>true if an identical title if found, false otherwise</returns>
        public bool CheckIfRecipeAlreadyPresent(string title, int type)
        {

            RecipeTreeItem parentNode = MainNavTreeViewModel.instance.GetRecipeParentNodeFromType(type);

            foreach (RecipeTreeItem recipe in parentNode.Children)
            {
                if (string.Compare(title.ToLower(), recipe.TreeItemTitle.ToLower()) == 0)
                {
                    return true;
                }
            }

            return false; //recipe title doesn't already exists so don't worry about it!
        }

        

        /// <summary>
        /// Add the recipe to the Borrow list and select that recipe in the combobox as well as 
        /// take the user to the Edit page.
        /// </summary>
        internal void AddRecipeToSelectList(RecipeTreeItem recipeTreeItem)
        {
            NavigationService.Navigate(typeof(SelectedView));
            SelectedViewModel.Instance.UpdateRecipeEntry(recipeTreeItem.RecipeModelTV);
        }

        /// <summary>
        /// Add the recipe to the Edit page
        /// </summary>
        internal void DeleteRecipe(RecipeTreeItem recipeTreeItem)
        {
            RemoveRecipeFromTreeView(recipeTreeItem);
        }

        /// <summary>
        /// Add the recipe to the Edit page
        /// </summary>
        //internal void AddRecipeToEdit(RecipeTreeItem recipeTreeView)
        //{
        //    EditViewModel.Instance.UpdateRecipe(recipeTreeView.RecipeModelTV);
        //    MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.EditTab;
        //}

        #region ICommand  and other Properties

        [Microsoft.UI.Xaml.CustomAttributes.MUXPropertyChangedCallback(enable =true)]
        public ObservableCollection<RecipeTreeItem> RecipeTreeRootNodes
        {
            [Microsoft.UI.Xaml.CustomAttributes.MUXPropertyChangedCallback(enable =true)]
            get;

            [Microsoft.UI.Xaml.CustomAttributes.MUXPropertyChangedCallback(enable = true)]
            set;
        }

        //public RBRelayCommandObj CmdSaveDialog
        //{
        //    get;
        //    private set;
        //}

        ///// <summary>
        ///// Property for the Recipe combobox change command
        ///// </summary>
        //public RBRelayCommandObj CmdExpandTreeViewItem
        //{
        //    get;
        //    private set;
        //}

        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public ICommand CmdExpandTreeViewItem
        {
            get;
            private set;
        }

        /// <summary>
        /// property for the Quantity combobox change command
        /// </summary>
        //public RBRelayCommand CmdAddTreeVeiwItemToSelectList
        //{
        //    get;
        //    private set;
        //}
        public RelayCommand<TreeViewItemInvokedEventArgs> CmdAddTreeVeiwItemToSelectList
        {
            get;
            private set;
        }


        /// <summary>
        /// Alwasy returns true
        /// </summary>
        private bool canSelectTrue;
        public bool CanSelectTrue
        {
            get
            {
                return true;
            }

            set { SetProperty(ref canSelectTrue, value); }
        }

        #endregion

    }
}



