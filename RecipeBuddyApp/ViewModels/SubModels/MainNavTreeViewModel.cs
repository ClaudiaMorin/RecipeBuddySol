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

namespace RecipeBuddy.ViewModels
{
    

    public class MainNavTreeViewModel : ObservableObject
    {
        Action<Object> actionRecipeDisplayModel;

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
            CmdSaveDialog = new RBRelayCommandObj(actionRecipeDisplayModel = (RD) => AddRecipeToTreeView((RecipeDisplayModel)RD));
            //CmdExpandTreeViewItem = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => CanSelectRemove);

            DessertRecipes = new RecipeTreeItem("Desserts");
            MainCourseRecipes = new RecipeTreeItem("Main Courses");
            SavedCakeRecipes = new RecipeTreeItem("Cake");
            SavedCandyRecipes = new RecipeTreeItem("Candy");
            SavedCookieRecipes = new RecipeTreeItem("Cookie");
            SavedCustardRecipes = new RecipeTreeItem("Custard");
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

            switch ((Type_Of_Recipe)recipeTypeAsInt)
            {
                case Type_Of_Recipe.Cake:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedCakeRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Candy:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedCandyRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Cookie:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedCookieRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Unknown:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedUnknownRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Custard:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedCustardRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Pastry:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedPastryRecipes.Children, title, recipeTypeAsInt); ;
                    break;

                case Type_Of_Recipe.SoupStew:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedSoupStewRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Poultry:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedPoultryRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Pork:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedPorkRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Beef:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedBeefRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Lamb:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedLambRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Seafood:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedSeafoodRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Salad:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedSaladRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Bread:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedBreadRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.SideDish:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedSideDishRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Tofu:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedTofuRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Dairy:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedDairyRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Eggs:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedEggsRecipes.Children, title, recipeTypeAsInt);
                    break;

                case Type_Of_Recipe.Appetizer:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedAppetizerRecipes.Children, title, recipeTypeAsInt);
                    break;
            }

            return SavedSucceeded;
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
        private bool AddRecipeToSpecifiedTreeViewList(ObservableCollection<RecipeDisplayModel> ListToUse, RecipeDisplayModel recipeCardModel, Action<string, Type_Of_Recipe, Type_Of_Recipe_Action, bool> actionAddRemoveList, bool TreeViewItemsExpand)
        {
            for (int i = 0; i < ListToUse.Count; i++)
            {
                if (ListToUse[i].Title == recipeCardModel.Title && recipeCardModel.Title != "Search For Your Recipe")
                {
                    //MessageBoxResult results = MessageBox.Show("Saving this recipe will overwrite the one currently saved. \n\n  If you don't want to do this change the title before saving!", "Do you want to overwite saved recipe!", MessageBoxButton.OKCancel);

                    /*if (results == MessageBoxResult.OK)
                    {
                        Overwrite the recipe
                        ListToUse.Add(recipeCardModel);
                        remove the old recipe and then add the new one
                        actionAddRemoveList(recipeCardModel.Title,(Type_Of_Recipe)recipeCardModel.TypeAsInt, Type_Of_Recipe_Action.Remove, false);
                        Now remove it from the DB
                        DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(recipeCardModel.Title, recipeCardModel.TypeAsInt);

                        actionAddRemoveList(recipeCardModel.Title, (Type_Of_Recipe)recipeCardModel.TypeAsInt, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);
                        return true;
                    }
                    user chose to not save
                    return false;*/
                }
            }

            ListToUse.Add(recipeCardModel);
            //False will add the item to the list
            actionAddRemoveList(recipeCardModel.Title, (Type_Of_Recipe)recipeCardModel.RecipeType, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);

            return true;
        }

        /// <summary>
        /// Dialog that pops up after the user attempts to save 
        /// </summary>
        /// <param name="recipeCard"></param>
        private void ContentDialogOverWriteOrCancel(RecipeDisplayModel recipeCard)
        {
            ContentDialog overWriteOrCancel = new ContentDialog()
            {
                Title = "Overwrite Recipe ?",
                Content = "A recipe with this title already exists, do you want to overwrite it?",
                CloseButtonText = "Cancel",
                PrimaryButtonText = "Overwrite",
                PrimaryButtonCommand = CmdSaveDialog,
                PrimaryButtonCommandParameter = recipeCard
            };
        }

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

        /// <summary>
        /// Checks the entry to be saved to see if that title exists in the catagory underwhich the recipe is going to be saved
        /// </summary>
        public void SaveEntry(RecipeDisplayModel recipeDisplay)
        {
            recipeDisplay.SaveEditsToARecipe();

            int result = MainWindowViewModel.Instance.mainTreeViewNav.AddRecipeToTreeView(recipeDisplay);

            if (result == 0)
            {
                MainWindowViewModel.Instance.mainTreeViewNav.ContentDialogOverWriteOrCancel(recipeDisplay);
            }

            //If the return is 0 we know that it was an overwrite and the dialogbox took care of it already
            if (result == 1)
            {
                //didn't find any conflicts in the first attempt so it has already been saved.
            }
        }

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

            switch ((Type_Of_Recipe)recipeTreeItem.RecipeModelTV.TypeAsInt)
            {
                case Type_Of_Recipe.Cake:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedCakeRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedCakeRecipes.Children.Add(recipeTreeItem);
                    DessertRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedCakeRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Cookie:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedCookieRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedCookieRecipes.Children.Add(recipeTreeItem);
                    DessertRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedCookieRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Candy:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedCandyRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedCandyRecipes.Children.Add(recipeTreeItem);
                    DessertRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedCandyRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Custard:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedCustardRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedCustardRecipes.Children.Add(recipeTreeItem);
                    DessertRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedCustardRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Pastry:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedPastryRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedPastryRecipes.Children.Add(recipeTreeItem);
                    DessertRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedPastryRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.SoupStew:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedSoupStewRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedSoupStewRecipes.Children.Add(recipeTreeItem);
                    SavedSoupStewRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Seafood:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedSeafoodRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedSeafoodRecipes.Children.Add(recipeTreeItem);
                    MainCourseRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedSeafoodRecipes.ItemExpanded = true;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Pork:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedPorkRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedPorkRecipes.Children.Add(recipeTreeItem);
                    MainCourseRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedPorkRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Beef:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedBeefRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedBeefRecipes.Children.Add(recipeTreeItem);
                    MainCourseRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedBeefRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Dairy:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedDairyRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedDairyRecipes.Children.Add(recipeTreeItem);
                    SavedDairyRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Eggs:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedEggsRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedEggsRecipes.Children.Add(recipeTreeItem);
                    MainCourseRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedEggsRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Poultry:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedPoultryRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedPoultryRecipes.Children.Add(recipeTreeItem);
                    MainCourseRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedPoultryRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Lamb:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedLambRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedLambRecipes.Children.Add(recipeTreeItem);
                    MainCourseRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedLambRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Salad:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedSaladRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedSaladRecipes.Children.Add(recipeTreeItem);
                    SavedSaladRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Appetizer:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedAppetizerRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedAppetizerRecipes.Children.Add(recipeTreeItem);
                    SavedAppetizerRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Bread:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedBreadRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedBreadRecipes.Children.Add(recipeTreeItem);
                    SavedBreadRecipes.ItemExpanded = expandTreeViewHeader;
                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.SideDish:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedSideDishRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedSideDishRecipes.Children.Add(recipeTreeItem);
                    SavedSideDishRecipes.ItemExpanded = expandTreeViewHeader;

                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Tofu:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedTofuRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedTofuRecipes.Children.Add(recipeTreeItem);
                    MainCourseRecipes.ItemExpanded = expandTreeViewHeader;
                    SavedTofuRecipes.ItemExpanded = expandTreeViewHeader;


                    returnOverwriteCmd = 1;
                    break;

                case Type_Of_Recipe.Unknown:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeTreeItem, SavedUnknownRecipes.Children);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedUnknownRecipes.Children.Add(recipeTreeItem);
                    SavedUnknownRecipes.ItemExpanded = expandTreeViewHeader;

                    returnOverwriteCmd = 1;
                    break;
            }
            return returnOverwriteCmd;
        }



        /// <summary>
        /// goes through the list looking for the same title that we are trying to add to the list
        /// if found it will return true
        /// </summary>
        /// <param name="recipeCardTreeItem">the recipe we are attempting to add</param>
        /// <param name="List">the treeviewlist we are attempting to add the recipe to</param>
        /// <returns>true if an identical title if found, false otherwise</returns>
        private int CheckIfRecipeAlreadyPresent(RecipeTreeItem recipeCardTreeItem, ObservableCollection<RecipeTreeItem> List)
        {

            foreach (RecipeTreeItem recipe in List)
            {
                if (string.Compare(recipe.TreeItemTitle.ToLower(), recipeCardTreeItem.TreeItemTitle.ToLower()) == 0)
                {
                    return 1;
                }
            }

            return 2; //recipe title doesn't already exists so don't worry about it!
        }

        private ObservableCollection<RecipeTreeItem> StringMapParentToCollection(string parentFromArgument)
        {
            switch (parentFromArgument)
            {
                case "CakeTreeViewHeader":
                    SavedCakeRecipes.ItemExpanded = true;
                    return SavedCakeRecipes.Children;

                case "CandyTreeViewHeader":
                    return SavedCandyRecipes.Children;

                case "CustardTreeViewHeader":
                    return SavedCustardRecipes.Children;

                case "PastryTreeViewHeader":
                    return SavedPastryRecipes.Children;

                case "PorkTreeViewHeader":
                    return SavedPorkRecipes.Children;

                case "LambTreeViewHeader":
                    return SavedLambRecipes.Children;

                case "PoultryTreeViewHeader":
                    return SavedPoultryRecipes.Children;

                case "SeafoodTreeViewHeader":
                    return SavedSeafoodRecipes.Children;

                case "BeefTreeViewHeader":
                    return SavedBeefRecipes.Children;

                case "SaladTreeViewHeader":
                    return SavedSaladRecipes.Children;

                case "BreadTreeViewHeader":
                    return SavedBreadRecipes.Children;

                case "AppitizerTreeViewHeader":
                    return SavedAppetizerRecipes.Children;

                case "TofuTreeViewHeader":
                    return SavedTofuRecipes.Children;

                case "SideDishTreeViewHeader":
                    return SavedSideDishRecipes.Children;

                case "UnknownTreeViewHeader":
                    return SavedUnknownRecipes.Children;
            }

            return null;
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
        internal void AddRecipeToEdit(RecipeTreeItem recipeTreeView)
        {
            EditViewModel.Instance.UpdateRecipe(recipeTreeView.RecipeModelTV);
            MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.EditTab;
        }

        #region ICommand  and other Properties

        [Microsoft.UI.Xaml.CustomAttributes.MUXPropertyChangedCallback(enable =true)]
        public ObservableCollection<RecipeTreeItem> RecipeTreeRootNodes
        {
            [Microsoft.UI.Xaml.CustomAttributes.MUXPropertyChangedCallback(enable =true)]
            get;

            [Microsoft.UI.Xaml.CustomAttributes.MUXPropertyChangedCallback(enable = true)]
            set;
        }

        public RBRelayCommandObj CmdSaveDialog
        {
            get;
            private set;
        }


        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public ICommand CmdExpandTreeViewItem
        {
            get;
            private set;
        }

        #endregion

    }
}



