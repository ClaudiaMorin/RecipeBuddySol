using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using System;
//using System.Windows.Controls;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Scrapers;
using RecipeBuddy.Core.Helpers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using Windows.System.Threading.Core;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading;

namespace RecipeBuddy.ViewModels
{

    public sealed class UploadViewModel : ObservableObject
    {
        public RecipeDisplayModel recipeCardViewModelForUpload;
        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        public string fileContents;

        private static readonly UploadViewModel instance = new UploadViewModel();
        public static UploadViewModel Instance
        {
            get { return instance; }
        }

        private UploadViewModel()
        {
            columnTwo = "0";
            currentType = 0;
            CmdSelectedTypeChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeTypeFromComboBox(e), canCallActionFunc => CanSelect);
            UploadButtonCmd = new ICommandViewModel<UploadViewModel>(Action => UploadRecipe(), canCallActionFunc => CanSelectUpload);
            ConvertButtonCmd = new ICommandViewModel<UploadViewModel>(Action => ConvertRecipe(), canCallActionFunc => CanSelectConvert);
            SaveButtonCmd = new ICommandViewModel<UploadViewModel>(Action => SaveRecipe(), canCallActionFunc => CanSelectSave);
            recipeCardViewModelForUpload = new RecipeDisplayModel();
        }

        /// <summary>
        /// Uploads the file, verifies it has the required areas to process into a recipe 
        /// </summary>
        async private Task UploadRecipe()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            
            if (file != null)
            {
                fileContents = await Windows.Storage.FileIO.ReadTextAsync(file);
            }

        }

        /// <summary>
        /// Processes the string and attempts to stuff it into one of our RecipeCardModels
        /// </summary>
        private void ConvertRecipe()
        {
            //not a recipe
            if (uploadRecipeText.Contains("ingredients") != true && uploadRecipeText.Contains("Ingredients") != true)
            {
                new MessageDialog("This doesn't appear to be a recipe!  Please try again.");
                UploadRecipeText = "";
            }

            //recipeCardViewModelForUpload.UpdateRecipeEntry(Scraper.ProcessUploatedRecipe(uploadRecipeText));

            ColumnTwo = "*";
        }

        /// <summary>
        /// Processes the string and attempts to stuff it into one of our RecipeCardModels
        /// </summary>
        private int SaveRecipe()
        {

            recipeCardViewModelForUpload.RecipeType = (Type_Of_Recipe) CurrentType;
            int result = MainWindowViewModel.Instance.mainTreeViewNav.AddRecipeToTreeView(recipeCardViewModelForUpload, true);

            if (result == 1)
            {
                MainNavTreeViewModel.Instance.RemoveRecipeFromTreeView(recipeCardViewModelForUpload);
                DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(recipeCardViewModelForUpload.Title, (int)recipeCardViewModelForUpload.RecipeType, UserViewModel.Instance.UsersIDInDB);
            }
            if (result == 1 || result == 2)
            {
                DataBaseAccessorsForRecipeManager.SaveRecipeToDatabase(UserViewModel.Instance.UsersIDInDB, recipeCardViewModelForUpload, UserViewModel.Instance.UsersIDInDB);
                ClearRecipe();
            }

            return result;
        }

        /// <summary>
        /// Removes the currently displayed recipe from the page
        /// </summary>
        internal void ClearRecipe()
        {
            //recipeCardViewModelForUpload.UpdateRecipeEntry(new RecipeCardModel());
            CurrentType = (int)recipeCardViewModelForUpload.RecipeType;
            ColumnTwo = "0";
        }

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
                        CurrentType = index;
                    }
                }
            }

            return;
        }



        /// <summary>
        /// property for the Recipe-type combobox change command
        /// </summary>
        public ICommand CmdSelectedTypeChanged
        {
            get;
            private set;
        }

        private string columnTwo;
        public string ColumnTwo
        {
            get
            { return columnTwo; }

            set { SetProperty(ref columnTwo, value); }
        }

        private string uploadRecipeText;
        public string UploadRecipeText
        {
            get
            { return uploadRecipeText; }

            set { SetProperty(ref uploadRecipeText, value); }
        }

        public ICommand UploadButtonCmd
        {
            get;
            private set;
        }

        public ICommand ConvertButtonCmd
        {
            get;
            private set;
        }

        public ICommand SaveButtonCmd
        {
            get;
            private set;
        }

        private int currentType;
        public int CurrentType
        {
            get { return currentType; }
            set { SetProperty(ref currentType, value); }
        }

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.  CanSelectComboBox
        /// </summary>
        public bool CanSelect
        {
            get
            {
                if (recipeCardViewModelForUpload != null)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Always True
        /// </summary>
        public bool CanSelectUpload
        {
            get
            {
                 return true;
            }
        }

        /// <summary>
        /// Always True
        /// </summary>
        public bool CanSelectConvert
        {
            get
            {
                if(uploadRecipeText != null && uploadRecipeText.Length > 0)
                return true;

                return false;
            }
        }

        /// <summary>
        /// Always True
        /// </summary>
        public bool CanSelectSave
        {
            get
            {
                if (recipeCardViewModelForUpload.Title.Length > 0 && recipeCardViewModelForUpload.listOfIngredientStringsForDisplay.Count > 0)
                    return true;

                return false;
            }
        }
    }
}
