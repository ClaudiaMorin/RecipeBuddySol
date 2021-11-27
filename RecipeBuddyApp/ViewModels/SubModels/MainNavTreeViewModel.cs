using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;

namespace RecipeBuddy.ViewModels
{

    public class MainNavTreeViewModel : ObservableObject
    {

        private static readonly MainNavTreeViewModel instance = new MainNavTreeViewModel();
        public static MainNavTreeViewModel Instance
        {
            get { return instance; }
        }

        static MainNavTreeViewModel()
        { }

        private MainNavTreeViewModel()
        {
            SavedCakeRecipes = new ObservableCollection<RecipeCardTreeItem>();
            cakeExp = false;
            SavedCandyRecipes = new ObservableCollection<RecipeCardTreeItem>();
            candyExp = false;
            SavedCookieRecipes = new ObservableCollection<RecipeCardTreeItem>();
            cookieExp = false;
            SavedCustardRecipes = new ObservableCollection<RecipeCardTreeItem>();
            custardExp = false;
            SavedPastryRecipes = new ObservableCollection<RecipeCardTreeItem>();
            pastryExp = false;
            SavedSoupStewRecipes = new ObservableCollection<RecipeCardTreeItem>();
            soupStewExp = false;
            SavedPorkRecipes = new ObservableCollection<RecipeCardTreeItem>();
            porkExp = false;
            SavedPoultryRecipes = new ObservableCollection<RecipeCardTreeItem>();
            poultryExp = false;
            SavedBeefRecipes = new ObservableCollection<RecipeCardTreeItem>();
            beefExp = false;
            SavedLambRecipes = new ObservableCollection<RecipeCardTreeItem>();
            lambExp = false;
            SavedSeafoodRecipes = new ObservableCollection<RecipeCardTreeItem>();
            seafoodExp = false;
            SavedSaladRecipes = new ObservableCollection<RecipeCardTreeItem>();
            saladExp = false;
            SavedAppetizerRecipes = new ObservableCollection<RecipeCardTreeItem>();
            appetizerExp = false;
            SavedBreadRecipes = new ObservableCollection<RecipeCardTreeItem>();
            breadExp = false;
            SavedSideDishRecipes = new ObservableCollection<RecipeCardTreeItem>();
            sideDishExp = false;
            SavedTofuRecipes = new ObservableCollection<RecipeCardTreeItem>();
            tofuExp = false;
            SavedDairyRecipes = new ObservableCollection<RecipeCardTreeItem>();
            DairyExp = false;
            SavedEggsRecipes = new ObservableCollection<RecipeCardTreeItem>();
            EggsExp = false;
            SavedUnknownRecipes = new ObservableCollection<RecipeCardTreeItem>();
            unknownExp = false;
            mainCourseExp = false;
            DessertExp = false;



            FillCatagoryCollectionForDropDown();
        }

        /// <summary>
        /// Observable collections so it will auto-update when nodes are removed and added
        /// </summary>
        public ObservableCollection<RecipeCardTreeItem> SavedCandyRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedCakeRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedCookieRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedCustardRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedPastryRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedUnknownRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedSoupStewRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedPorkRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedBeefRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedDairyRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedEggsRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedPoultryRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedSeafoodRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedLambRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedSaladRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedAppetizerRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedBreadRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedSideDishRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> SavedTofuRecipes { get; set; }
        public ObservableCollection<RecipeCardTreeItem> AddRemoveItem { get; set; }
        public ObservableCollection<string> CatagoryTypes { get; set; }


        private void FillCatagoryCollectionForDropDown()
        {

            CatagoryTypes = new ObservableCollection<string>()
            {
               "Appetizer","Beef","Bread", "Cake", "Candy", "Cookie", "Custard", "Dairy", "Eggs", "Lamb", "Pastery", "Pork", "Poultry",
                "Salad", "Seafood", "Side Dish", "Soup Stew", "Tofu", "Unknown"
            };
        }

        /// <summary>
        /// Removes all recipes from the tree.
        /// </summary>
        public void ClearTree()
        {
            SavedCakeRecipes.Clear();
            SavedCandyRecipes.Clear();
            SavedCookieRecipes.Clear();
            SavedCustardRecipes.Clear();
            SavedPastryRecipes.Clear();
            SavedPoultryRecipes.Clear();
            SavedPorkRecipes.Clear();
            SavedBeefRecipes.Clear();
            SavedSeafoodRecipes.Clear();
            SavedSoupStewRecipes.Clear();
            SavedLambRecipes.Clear();
            SavedBreadRecipes.Clear();
            SavedSaladRecipes.Clear();
            SavedAppetizerRecipes.Clear();
            SavedSideDishRecipes.Clear();
            SavedUnknownRecipes.Clear();
            SavedTofuRecipes.Clear();
            SavedDairyRecipes.Clear();
            SavedEggsRecipes.Clear();
            MainCourseExp = false;
            DessertExp = false;
        }

        public void AddRecipeModelsToTreeView(List<RecipeCardModel> models)
        {
            foreach (RecipeCardModel recipeCard in models)
            {
                AddRecipeToTreeView(recipeCard, false);
            }
        }

        /// <summary>
        /// Digs out the recipe that is in one of the collection lists
        /// </summary>
        /// <param name="collection">This is going to identify one of the collection lists we are supposed to look for our information in</param>
        /// <param name="titleOfRecipe">The title of the recipe that we are looking for</param>
        /// <returns>RecipeCardModle to find or a null RecipeCardModle if the right one couldn't be found</returns>
        public RecipeCardTreeItem FindRecipeInCollection(ObservableCollection<RecipeCardTreeItem> collection, string titleOfRecipe)
        {

            foreach (RecipeCardTreeItem RC in collection)
            {
                if (string.Compare(RC.RecipeModelPropertyTV.Title.ToLower(), titleOfRecipe.ToLower()) == 0)
                {
                    return RC;
                }
            }

            return new RecipeCardTreeItem();
        }

        /// <summary>
        /// Removes a recipe from the treeview 
        /// </summary>
        /// <param name="recipeCard">The recipe to be added</param>
        /// <returns>A bool with true if the save was successful, false if not</returns>
        public bool RemoveRecipeFromTreeView(RecipeCardTreeItem recipeCardModel)
        {
            return RemoveRecipeFromTreeView(recipeCardModel.RecipeModelPropertyTV);
        }

        /// <summary>
        /// Removes a recipe from the treeview 
        /// </summary>
        /// <param name="recipeCard">The recipe to be added</param>
        /// <returns>A bool with true if the save was successful, false if not</returns>
        public bool RemoveRecipeFromTreeView(RecipeCardModel recipeCardModel)
        {
            bool SavedSucceeded = false;

            switch ((Type_Of_Recipe)recipeCardModel.TypeAsInt)
            {
                case Type_Of_Recipe.Cake:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedCakeRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Candy:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedCandyRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Cookie:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedCookieRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Unknown:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedUnknownRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Custard:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedCustardRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Pastery:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedPastryRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.SoupStew:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedSoupStewRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Poultry:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedPoultryRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Pork:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedPorkRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Beef:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedBeefRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Lamb:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedLambRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Seafood:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedSeafoodRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Salad:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedSaladRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Bread:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedBreadRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.SideDish:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedSideDishRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Tofu:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedTofuRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Dairy:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedDairyRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Eggs:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedEggsRecipes, recipeCardModel);
                    break;

                case Type_Of_Recipe.Appetizer:
                    SavedSucceeded = RemoveRecipeFromSpecifiedTreeViewList(SavedAppetizerRecipes, recipeCardModel);
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
        private bool AddRecipeToSpecifiedTreeViewList(ObservableCollection<RecipeCardTreeItem> ListToUse, RecipeCardTreeItem recipeCardViewModel, Action<string, Type_Of_Recipe, Type_Of_Recipe_Action, bool> actionAddRemoveList, bool TreeViewItemsExpand)
        {
            for (int i = 0; i < ListToUse.Count; i++)
            {
                if (ListToUse[i].RecipeModelPropertyTV.Title == recipeCardViewModel.RecipeModelPropertyTV.Title && recipeCardViewModel.RecipeModelPropertyTV.Title != "Search For Your Recipe")
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
            actionAddRemoveList(recipeCardViewModel.RecipeModelPropertyTV.Title, (Type_Of_Recipe)recipeCardViewModel.RecipeModelPropertyTV.TypeAsInt, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);

            return true;
        }

        /// <summary>
        /// A private function that actually does the adding to the specified list in the tree view
        /// </summary>
        /// <param name="ListToUse">The preselected list</param>
        /// <param name="recipeCardModel">The RecipeModel we are adding to the preselected list</param>
        /// <param name="action">The specific action keyed to the list</param>
        private bool AddRecipeToSpecifiedTreeViewList(ObservableCollection<RecipeCardModel> ListToUse, RecipeCardModel recipeCardModel, Action<string, Type_Of_Recipe, Type_Of_Recipe_Action, bool> actionAddRemoveList, bool TreeViewItemsExpand)
        {
            for (int i = 0; i < ListToUse.Count; i++)
            {
                if (ListToUse[i].Title == recipeCardModel.Title && recipeCardModel.Title != "Search For Your Recipe")
                {
                    //MessageBoxResult results = MessageBox.Show("Saving this recipe will overwrite the one currently saved. \n\n  If you don't want to do this change the title before saving!", "Do you want to overwite saved recipe!", MessageBoxButton.OKCancel);

                    /*if (results == MessageBoxResult.OK)
                    {
                        //Overwrite the recipe
                        ListToUse.Add(recipeCardModel);
                        //remove the old recipe and then add the new one
                        actionAddRemoveList(recipeCardModel.Title,(Type_Of_Recipe)recipeCardModel.TypeAsInt, Type_Of_Recipe_Action.Remove, false);
                        //Now remove it from the DB
                        DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(recipeCardModel.Title, recipeCardModel.TypeAsInt);

                        actionAddRemoveList(recipeCardModel.Title, (Type_Of_Recipe)recipeCardModel.TypeAsInt, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);
                        return true;
                    }
                    //user chose to not save
                    return false;*/
                }
            }

            ListToUse.Add(recipeCardModel);
            //False will add the item to the list
            actionAddRemoveList(recipeCardModel.Title, (Type_Of_Recipe)recipeCardModel.TypeAsInt, Type_Of_Recipe_Action.Add, TreeViewItemsExpand);

            return true;
        }

        /// <summary>
        /// A private function that actually does the removing from the specified list
        /// </summary>
        /// <param name="ListToUse">The preselected list</param>
        /// <param name="recipeCardModel">The RecipeModel we are removing from the preselected list</param>
        /// <param name="action">The specific action keyed to the list</param>
        private bool RemoveRecipeFromSpecifiedTreeViewList(ObservableCollection<RecipeCardTreeItem> ListToUse, RecipeCardTreeItem recipeCardModel)
        {
            return RemoveRecipeFromSpecifiedTreeViewList(ListToUse, recipeCardModel.RecipeModelPropertyTV);
        }

        /// <summary>
        /// A private function that actually does the removing from the specified list
        /// </summary>
        /// <param name="ListToUse">The preselected list</param>
        /// <param name="recipeCardModel">The RecipeModel we are removing from the preselected list</param>
        /// <param name="action">The specific action keyed to the list</param>
        private bool RemoveRecipeFromSpecifiedTreeViewList(ObservableCollection<RecipeCardTreeItem> ListToUse, RecipeCardModel recipeCardModel)
        {
            for (int i = 0; i < ListToUse.Count; i++)
            {
                if (ListToUse[i].RecipeModelPropertyTV.Title == recipeCardModel.Title && recipeCardModel.Title != "Search For Your Recipe")
                {
                    //Remove the recipe
                    ListToUse.RemoveAt(i);
                    DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(recipeCardModel.Title, recipeCardModel.TypeAsInt, UserViewModel.Instance.UsersIDInDB);
                    return true;
                }
            }

            return false;
        }

        public int AddRecipeToTreeView(RecipeCardModel recipeCardModel, bool expandTreeViewHeader)
        {
            int returnOverwriteCmd = 0;
            //Create a new RecipeCardTreeItem
            RecipeCardTreeItem recipeCardTreeItem = new RecipeCardTreeItem(recipeCardModel);

            switch ((Type_Of_Recipe)recipeCardTreeItem.RecipeModelPropertyTV.TypeAsInt)
            {
                case Type_Of_Recipe.Cake:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedCakeRecipes);
                    if(returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedCakeRecipes.Add(recipeCardTreeItem);
                    DessertExp = true;
                    CakeExp = true;
                    break;

                case Type_Of_Recipe.Cookie:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedCookieRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedCookieRecipes.Add(recipeCardTreeItem);
                    DessertExp = true;
                    CookieExp = true;
                    break;

                case Type_Of_Recipe.Candy:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedCandyRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedCandyRecipes.Add(recipeCardTreeItem);
                    DessertExp = true;
                    CandyExp = true;
                    break;

                case Type_Of_Recipe.Custard:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedCustardRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedCustardRecipes.Add(recipeCardTreeItem);
                    DessertExp = true;
                    CustardExp = true;
                    break;

                case Type_Of_Recipe.Pastery:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedPastryRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedPastryRecipes.Add(recipeCardTreeItem);
                    DessertExp = true;
                    PastryExp = true;
                    break;

                case Type_Of_Recipe.SoupStew:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedSoupStewRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedSoupStewRecipes.Add(recipeCardTreeItem);
                    SoupStewExp = true;
                    break;

                case Type_Of_Recipe.Seafood:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedSeafoodRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedSeafoodRecipes.Add(recipeCardTreeItem);
                    MainCourseExp = true;
                    SeafoodExp = true;
                    break;

                case Type_Of_Recipe.Pork:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedPorkRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedPorkRecipes.Add(recipeCardTreeItem);
                    MainCourseExp = true;
                    PorkExp = true;
                    break;

                case Type_Of_Recipe.Beef:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedBeefRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedBeefRecipes.Add(recipeCardTreeItem);
                    MainCourseExp = true;
                    BeefExp = true;
                    break;

                case Type_Of_Recipe.Dairy:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedDairyRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedDairyRecipes.Add(recipeCardTreeItem);
                    MainCourseExp = true;
                    DairyExp = true;
                    break;

                case Type_Of_Recipe.Eggs:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedEggsRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedEggsRecipes.Add(recipeCardTreeItem);
                    MainCourseExp = true;
                    EggsExp = true;
                    break;

                case Type_Of_Recipe.Poultry:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedPoultryRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedPoultryRecipes.Add(recipeCardTreeItem);
                    MainCourseExp = true;
                    PoultryExp = true;
                    break;

                case Type_Of_Recipe.Lamb:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedLambRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedLambRecipes.Add(recipeCardTreeItem);
                    MainCourseExp = true;
                    LambExp = true;
                    break;

                case Type_Of_Recipe.Salad:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedSaladRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedSaladRecipes.Add(recipeCardTreeItem);
                    SaladExp = true;
                    break;

                case Type_Of_Recipe.Appetizer:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedAppetizerRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedAppetizerRecipes.Add(recipeCardTreeItem);
                    AppetizerExp = true;
                    break;

                case Type_Of_Recipe.Bread:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedBreadRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedBreadRecipes.Add(recipeCardTreeItem);
                    BreadExp = true;
                    break;

                case Type_Of_Recipe.SideDish:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedSideDishRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedSideDishRecipes.Add(recipeCardTreeItem);
                    SideDishExp = true;
                    break;

                case Type_Of_Recipe.Tofu:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedTofuRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedTofuRecipes.Add(recipeCardTreeItem);
                    MainCourseExp = true;
                    TofuExp = true;
                    break;

                case Type_Of_Recipe.Unknown:
                    returnOverwriteCmd = CheckIfRecipeAlreadyPresent(recipeCardTreeItem, SavedUnknownRecipes);
                    if (returnOverwriteCmd == 0)
                        return returnOverwriteCmd;
                    SavedUnknownRecipes.Add(recipeCardTreeItem);
                    UnknownExp = true;
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
        private int CheckIfRecipeAlreadyPresent(RecipeCardTreeItem recipeCardTreeItem, ObservableCollection<RecipeCardTreeItem> List)
        {

            foreach (RecipeCardTreeItem recipe in List)
            {
                if (string.Compare(recipe.RecipeTitleTreeItem.ToLower(), recipeCardTreeItem.RecipeTitleTreeItem.ToLower()) == 0)
                {
                   /* if (MessageBox.Show("Your recipe must have a unique title to be saved here or you will overwrite the existing recipe", "Overwrite Recipe?", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    { return 0; } //recipe title already exists so don't overwrite
                    else
                    { return 1; } //recipe title already exists but do overwrite*/
                }
            }

            return 2; //recipe title doesn't already exists so don't worry about it!
        }

        private ObservableCollection<RecipeCardTreeItem> StringMapParentToCollection(string parentFromArgument)
        {
            switch (parentFromArgument)
            {
                case "CakeTreeViewHeader":
                    return SavedCakeRecipes;

                case "CandyTreeViewHeader":
                    return SavedCandyRecipes;

                case "CustardTreeViewHeader":
                    return SavedCustardRecipes;

                case "PastryTreeViewHeader":
                    return SavedPastryRecipes;

                case "PorkTreeViewHeader":
                    return SavedPorkRecipes;

                case "LambTreeViewHeader":
                    return SavedLambRecipes;

                case "PoultryTreeViewHeader":
                    return SavedPoultryRecipes;

                case "SeafoodTreeViewHeader":
                    return SavedSeafoodRecipes;

                case "BeefTreeViewHeader":
                    return SavedBeefRecipes;

                case "SaladTreeViewHeader":
                    return SavedSaladRecipes;

                case "BreadTreeViewHeader":
                    return SavedBreadRecipes;

                case "AppitizerTreeViewHeader":
                    return SavedAppetizerRecipes;

                case "TofuTreeViewHeader":
                    return SavedTofuRecipes;

                case "SideDishTreeViewHeader":
                    return SavedSideDishRecipes;

                case "UnknownTreeViewHeader":
                    return SavedUnknownRecipes;
            }

            return null;
        }


        /// <summary>
        /// Add the recipe to the Borrow list and select that recipe in the combobox as well as 
        /// take the user to the Edit page.
        /// </summary>
        internal void AddRecipeToSelectList(RecipeCardTreeItem recipeTreeItem)
        {
            MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.EditTab;
            SelectedViewModel.Instance.AddToListOfRecipeCards(recipeTreeItem.RecipeModelPropertyTV);
            MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.SelectedTab;
        }

        /// <summary>
        /// Add the recipe to the Edit page
        /// </summary>
        internal void DeleteRecipe(RecipeCardTreeItem recipeTreeItem)
        {
            RemoveRecipeFromTreeView(recipeTreeItem);
        }

        /// <summary>
        /// Add the recipe to the Edit page
        /// </summary>
        internal void AddRecipeToEdit(RecipeCardTreeItem recipeTreeView)
        {
            EditViewModel.Instance.UpdateRecipe(recipeTreeView.RecipeModelPropertyTV);
            MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.EditTab;
        }

        #region ICommand  and other Properties

        private bool mainCourseExp;
        public bool MainCourseExp
        {
            get { return mainCourseExp; }
            set { SetProperty(ref mainCourseExp, value); }
        }

        private bool dessertExp;
        public bool DessertExp
        {
            get { return dessertExp; }
            set { SetProperty(ref dessertExp, value); }
        }

        private bool cakeExp;
        public bool CakeExp
        {
            get { return cakeExp; }
            set { SetProperty(ref cakeExp, value); }
        }

        private bool cookieExp;
        public bool CookieExp
        {
            get { return cookieExp; }
            set { SetProperty(ref cookieExp, value); }
        }


        private bool candyExp;
        public bool CandyExp
        {
            get { return candyExp; }
            set { SetProperty(ref candyExp, value); }
        }

        private bool custardExp;
        public bool CustardExp
        {
            get { return custardExp; }
            set { SetProperty(ref custardExp, value); }
        }

        private bool pastryExp;
        public bool PastryExp
        {
            get { return pastryExp; }
            set { SetProperty(ref pastryExp, value); }
        }

        private bool soupStewExp;
        public bool SoupStewExp
        {
            get { return soupStewExp; }
            set { SetProperty(ref soupStewExp, value); }
        }

        private bool porkExp;
        public bool PorkExp
        {
            get { return porkExp; }
            set { SetProperty(ref porkExp, value); }
        }

        private bool poultryExp;
        public bool PoultryExp
        {
            get { return poultryExp; }
            set { SetProperty(ref poultryExp, value); }
        }

        private bool beefExp;
        public bool BeefExp
        {
            get { return beefExp; }
            set { SetProperty(ref beefExp, value); }
        }

        private bool lambExp;
        public bool LambExp
        {
            get { return lambExp; }
            set { SetProperty(ref lambExp, value); }
        }

        private bool seafoodExp;
        public bool SeafoodExp
        {
            get { return seafoodExp; }
            set { SetProperty(ref seafoodExp, value); }
        }

        private bool saladExp;
        public bool SaladExp
        {
            get { return saladExp; }
            set { SetProperty(ref saladExp, value); }
        }

        private bool appetizerExp;
        public bool AppetizerExp
        {
            get { return appetizerExp; }
            set { SetProperty(ref appetizerExp, value); }
        }

        private bool sideDishExp;
        public bool SideDishExp
        {
            get { return sideDishExp; }
            set { SetProperty(ref sideDishExp, value); }
        }

        private bool tofuExp;
        public bool TofuExp
        {
            get { return tofuExp; }
            set { SetProperty(ref tofuExp, value); }
        }

        private bool dairyExp;
        public bool DairyExp
        {
            get { return dairyExp; }
            set { SetProperty(ref dairyExp, value); }
        }

        private bool eggsExp;
        public bool EggsExp
        {
            get { return eggsExp; }
            set { SetProperty(ref eggsExp, value); }
        }

        private bool breadExp;
        public bool BreadExp
        {
            get { return breadExp; }
            set { SetProperty(ref breadExp, value); }
        }

        private bool unknownExp;
        public bool UnknownExp
        {
            get { return unknownExp; }
            set { SetProperty(ref unknownExp, value); }
        }

        #endregion

    }
}



