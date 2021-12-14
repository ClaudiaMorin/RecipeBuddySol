using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Scrapers;
using RecipeBuddy.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RecipeBuddy.ViewModels
{
    public class RecipePanelForWebCopy : ObservableObject
    {
        public RecipeDisplayModel recipeCardModel;

        Action action;
        Func<bool> funcBool;

        /// <summary>
        /// URLList is used by the Scraper and GenerateSearchResults to pull the URL of all the found recipes 
        /// </summary>
        public RecipePanelForWebCopy()
        {
            recipeCardModel = new RecipeDisplayModel();
        }

        /// <summary>
        /// This manages both the RecipeCardView as well as the list of RecipeCards were pulled from the specific website
        /// </summary>
        /// <param name="type"></param>
        public RecipePanelForWebCopy(RecipeRecordModel recipeBlurbModel)
        {
            //For Threading callbacks.  
            Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView();

            recipeCardModel = new RecipeDisplayModel(recipeBlurbModel);
            CanSelectSave = false;
            CanSelectCancel = true;
            CmdSaveButton = new RBRelayCommand(action = () => SaveEntry(), funcBool = () => CanSelectSave);
            CmdCancelDialogButton = new RBRelayCommand(action = () => CancelEntry(), funcBool = () => CanSelectCancel);
        }

        /// <summary>
        /// Creates are recipeCardModel out of a recipeModel
        /// </summary>
        public void LoadRecipeCardModel(RecipeDisplayModel recipeModel)
        {
            //For Threading callbacks.  
            Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView();

            recipeCardModel.CopyRecipeDisplayModel(recipeModel);
            CanSelectSave = true;
            CanSelectCancel = true;
            CmdSaveButton = new RBRelayCommand(action = () => SaveEntry(), funcBool = () => CanSelectSave);
            CmdCancelDialogButton = new RBRelayCommand(action = () => CancelEntry(), funcBool = () => CanSelectCancel);
        }

        public void ClearRecipeEntry()
        {
            recipeCardModel = new RecipeDisplayModel();
            CanSelectSave= false;
            CanSelectCancel = false;
        }

        public void CancelEntry()
        {
            ClearRecipeEntry();
            //WebViewModel.Instance.CloseKeepRecipePanel();
        }

        /// <summary>
        /// This does the actual Entry Saving and can be called from either the overwrite? dialog or the CheckEntrySave function
        /// </summary>
        /// <param name="recipeCard">This is an object that is then cast back to a RecipeCardModel to satisfy the ICommandInterface</param>
        //public async Task SaveEntry(Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView)
        public void SaveEntry()
        {
            recipeCardModel.SaveEditsToARecipe();
            DataBaseAccessorsForRecipeManager.SaveRecipeToDatabase(UserViewModel.Instance.UsersIDInDB, recipeCardModel, UserViewModel.Instance.UsersIDInDB);
            MainNavTreeViewModel.Instance.AddRecipeModelsToTreeView(new RecipeRecordModel(recipeCardModel), true);
            
            ClearRecipeEntry();
            //MainNavTreeViewModel.Instance.DessertRecipes.ItemExpanded = true;
            //MainNavTreeViewModel.Instance.SavedCakeRecipes.ItemExpanded = true;
            //MainWindowViewModel.Instance.mainTreeViewNav.AddRecipeToTreeView(recipeCardModel);
        }

        #region Properties and ICommand functions 


        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        private bool canSelectSave;
        public bool CanSelectSave
        {
            get { return canSelectSave; }
            set { SetProperty(ref canSelectSave, value); }
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

        public RBRelayCommand CmdSaveButton
        {
            get;
            private set;
        }

        public RBRelayCommandObj CmdSaveDialog
        {
            get;
            private set;
        }

        public RBRelayCommand CmdCancelDialogButton
        {
            get;
            private set;
        }

        #endregion
    }
}
