using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Models;
using Windows.UI.Xaml.Controls;
using RecipeBuddy.Core.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Security.Cryptography;

namespace RecipeBuddy.ViewModels
{
    public class RecipeEditsViewModel : ObservableObject
    {

        private static readonly RecipeEditsViewModel instance = new RecipeEditsViewModel();

        Action ActionNoParams;
        Action<SelectionChangedEventArgs> actionWithEventArgs;
        Action<string> actionWithObject;
        Func<bool> FuncBool;

        public MainNavTreeViewModel mainTreeViewNav;
        public RecipeDisplayModel selectViewMainRecipeCardModel;

        public string QuantitySelectedAsString;
        public int QuantitySelectedAsInt;
        public List<string> IngredientQuantityShift;

        public List<Action<string>> listOfIngredientEditStringsSetters;
        public List<Action<string>> listOfDirectionEditStringsSetters;
        public List<Func<string>> listOfIngredientEditStringsGetters;
        public List<Func<string>> listOfDirectionEditStringsGetters;
        private List<Action<string>> IngredHeightList;
        private List<Action<string>> DirectHeightList;

        static RecipeEditsViewModel()
        { }

        public static RecipeEditsViewModel Instance
        {
            get { return instance; }
        }

        private RecipeEditsViewModel()
        {
            LoadIngredList("0");
            LoadDirectList("0");

            currentTypeFromCombo = 18;
            title = "";
            author = "";

            titleTypeAuthorHeight = "0";
            authorEditString = "";
            titleEditString = "";
            authorMaxLength = "30";
            titleMaxLength = "60";
            ingredMaxLength = "200";
            directMaxLength = "2000";

            selectViewMainRecipeCardModel = new RecipeDisplayModel();
            IngredientQuantityShift = new List<string>();

            CmdSelectedTypeChanged = new RelayCommand<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeTypeFromComboBox(e), canCallActionFunc => CanSelect);
            //CmdSave = new RelayCommandRaiseCanExecute(ActionNoParams = () => SaveRecipeEdits(), FuncBool = () => CanSelectSave);
            CmdUpdate = new RelayCommand<string>(actionWithObject = s => Update(s), canCallActionFunc => CanSelectAlwaysTrue);
            CmdCancel = new RelayCommand<string>(actionWithObject = s => Cancel(s), canCallActionFunc => CanSelectAlwaysTrue);
            CmdLineEdit = new RelayCommand<string>(actionWithObject = s => LineEdit(s), canCallActionFunc => CanSelectAlwaysTrue);
        }

        #region the private strings for ingredients edits- 50 - and directions edit -30- and the List<string> of IngredientValues and DirectionValues

        private string ingredient1Edits = ""; private string ingredient2Edits = ""; private string ingredient3Edits = ""; private string ingredient4Edits = ""; private string ingredient5Edits = ""; private string ingredient6Edits = ""; private string ingredient7Edits = ""; private string ingredient8Edits = ""; private string ingredient9Edits = ""; private string ingredient10Edits = "";
        private string ingredient11Edits = ""; private string ingredient12Edits = ""; private string ingredient13Edits = ""; private string ingredient14Edits = ""; private string ingredient15Edits = ""; private string ingredient16Edits = ""; private string ingredient17Edits = ""; private string ingredient18Edits = ""; private string ingredient19Edits = ""; private string ingredient20Edits = "";
        private string ingredient21Edits = ""; private string ingredient22Edits = ""; private string ingredient23Edits = ""; private string ingredient24Edits = ""; private string ingredient25Edits = ""; private string ingredient26Edits = ""; private string ingredient27Edits = ""; private string ingredient28Edits = ""; private string ingredient29Edits = ""; private string ingredient30Edits = "";
        private string ingredient31Edits = ""; private string ingredient32Edits = ""; private string ingredient33Edits = ""; private string ingredient34Edits = ""; private string ingredient35Edits = ""; private string ingredient36Edits = ""; private string ingredient37Edits = ""; private string ingredient38Edits = ""; private string ingredient39Edits = ""; private string ingredient40Edits = "";
        private string ingredient41Edits = ""; private string ingredient42Edits = ""; private string ingredient43Edits = ""; private string ingredient44Edits = ""; private string ingredient45Edits = ""; private string ingredient46Edits = ""; private string ingredient47Edits = ""; private string ingredient48Edits = ""; private string ingredient49Edits = ""; private string ingredient50Edits = "";

        private string direction1Edits = ""; private string direction2Edits = ""; private string direction3Edits = ""; private string direction4Edits = ""; private string direction5Edits = ""; private string direction6Edits = ""; private string direction7Edits = ""; private string direction8Edits = ""; private string direction9Edits = ""; private string direction10Edits = "";
        private string direction11Edits = ""; private string direction12Edits = ""; private string direction13Edits = ""; private string direction14Edits = ""; private string direction15Edits = ""; private string direction16Edits = ""; private string direction17Edits = ""; private string direction18Edits = ""; private string direction19Edits = ""; private string direction20Edits = "";
        private string direction21Edits = ""; private string direction22Edits = ""; private string direction23Edits = ""; private string direction24Edits = ""; private string direction25Edits = ""; private string direction26Edits = ""; private string direction27Edits = ""; private string direction28Edits = ""; private string direction29Edits = ""; private string direction30Edits = "";
        #endregion


        ///// <summary>
        ///// For use when a user logs out of his/her account
        ///// </summary>
        //public void ResetViewModel()
        //{
        //    //RecipeLoaded = "Collapsed";
        //    //RecipeNotLoaded = "Visible";
        //    //selectViewMainRecipeCardModel.CopyRecipeDisplayModel(new RecipeDisplayModel());
        //    //selectViewMainRecipeCardModel = new RecipeDisplayModel();
        //    //selectViewMainRecipeCardModel.Title = "Select a recipe from the tree to edit!";
        //    //EmptyIngredientQuanityRow();
        //}

        /// <summary>
        /// updates the display to the newly selected recipe and updates the list of Edit textboxes so that the 
        /// user can edit the ingredients and we can check it before it is submitted.
        /// </summary>
        /// <param name="recipeCardModel">RecipeCardModel</param>
        public void LoadRecipeEntry(RecipeRecordModel recipeModel)
        {
            CloseAllEditBoxes();
            selectViewMainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(recipeModel);
            UpdateEditTextBoxes();
            CurrentTypeString = MainNavTreeViewModel.Instance.CatagoryTypes[selectViewMainRecipeCardModel.RecipeTypeInt];
            CurrentTypeFromCombo = 0;
            Title = selectViewMainRecipeCardModel.Title;
            Author = selectViewMainRecipeCardModel.Author;
            titleTypeAuthorHeight = "0";
            SelectedViewModel.Instance.CanSelectCopy = true;
            SelectedViewModel.Instance.CanSelectSave = false;
        }

        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Basket page
        /// </summary>
        /// <param name="e"></param>
        internal void ChangeRecipeTypeFromComboBox(SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                string type = e.AddedItems[0].ToString();

                for (int index = 0; index < MainNavTreeViewModel.Instance.CatagoryTypes.Count; index++)
                {
                    if (string.Compare(MainNavTreeViewModel.Instance.CatagoryTypes[index].ToString().ToLower(), type.ToLower()) == 0)
                        CurrentTypeFromCombo = index;
                }
            }
        }


        /// <summary>
        /// Saves the recipe to the DB and the TreeView if the recipe doesn't already exist - as the case with a copy function, or updates
        /// and existing recipe.
        /// </summary>
        public void SaveRecipeEdits()
        {
            MainNavTreeViewModel.Instance.AddUpdateMoveRecipe(selectViewMainRecipeCardModel);

            SelectedViewModel.Instance.CanSelectCopy = true;
        }

        /// <summary>
        /// Updates the current card display and updates the edit-textboxes for the ingredient and direction editing
        /// </summary>
        public void UpdateEditTextBoxes()
        {
            TitleEditString = selectViewMainRecipeCardModel.Title;
            AuthorEditString = selectViewMainRecipeCardModel.Author;
            CurrentTypeFromCombo = selectViewMainRecipeCardModel.RecipeTypeInt;

            for (int count = 0; count < 50; count++)
            {
                if (selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[count].Length > 0)
                {
                    listOfIngredientEditStringsSetters[count].Invoke(selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[count]);
                }
                else
                {
                    listOfIngredientEditStringsSetters[count].Invoke("");
                }
            }

            for (int count = 0; count < 30; count++)
            {
                if (selectViewMainRecipeCardModel.listOfDirectionStringsForDisplay[count].Length > 0)
                {
                    listOfDirectionEditStringsSetters[count].Invoke(selectViewMainRecipeCardModel.listOfDirectionStringsForDisplay[count]);
                }
                else
                {
                    listOfDirectionEditStringsSetters[count].Invoke("");
                }
            }

            SelectedViewModel.Instance.CanSelectCopy = false;
        }


        /// <summary>
        /// Called by clickeing the "edit" button
        /// Shifts the line height to expose the hiden Text box 
        /// </summary>
        /// <param name="sender"></param>
        private void LineEdit(object sender)
        {
            string[] parameters = sender.ToString().Split(',');

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "ingredient") == 0)
            {
                int results;
                bool success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }

                IngredHeightList[results - 1].Invoke("Auto");
                return;
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "direction") == 0)
            {
                int results;
                bool success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                DirectHeightList[results - 1].Invoke("Auto");
                return;
            }


            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "editdetails") == 0)
            {
                int results;
                bool success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                TitleTypeAuthorHeight = "30";
            }
        }

        /// <summary>
        /// Called by the update buttons on the line edits, will also update the RecipeDisplayModel
        /// </summary>
        /// <param name="sender">argument sent from the xaml with the command to indicate what we are updating</param>
        private void Update(object sender)
        {
            int results;
            bool success;
            string[] parameters = sender.ToString().Split(',');
            //CanSelectSave = false;

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "ingredient") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }

                IngredHeightList[results - 1].Invoke("0");

                if (string.Compare(listOfIngredientEditStringsGetters[results - 1].Invoke().ToLower(), selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[results - 1].ToLower()) != 0)
                {
                    selectViewMainRecipeCardModel.listOfIngredientSetters[results - 1].Invoke(listOfIngredientEditStringsGetters[results - 1].Invoke());
                    selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[results - 1] = listOfIngredientEditStringsGetters[results - 1].Invoke();

                    SelectedViewModel.Instance.CanSelectSave = true;
                    TitleTypeAuthorHeight = "0";
                }
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "direction") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }

                DirectHeightList[results - 1].Invoke("0");
                string s1 = listOfDirectionEditStringsGetters[results - 1].Invoke().ToLower();
                string s2 = selectViewMainRecipeCardModel.listOfDirectionStringsForDisplay[results - 1].ToLower();

                if (string.Compare(s1, s2) != 0)
                {
                    string st1 = selectViewMainRecipeCardModel.listOfDirectionGetters[results - 1].Invoke();
                    selectViewMainRecipeCardModel.listOfDirectionSetters[results - 1].Invoke(st1);
                    string st2 = listOfDirectionEditStringsGetters[results - 1].Invoke();
                    selectViewMainRecipeCardModel.listOfDirectionSetters[results - 1].Invoke(st2);
                    selectViewMainRecipeCardModel.listOfDirectionStringsForDisplay[results - 1] = listOfDirectionEditStringsGetters[results - 1].Invoke();
                    SelectedViewModel.Instance.CanSelectSave = true;
                    TitleTypeAuthorHeight = "0";
                }
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "titletypeauthor") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }

                if (string.Compare(selectViewMainRecipeCardModel.Author.ToLower(), AuthorEditString.ToLower()) != 0)
                {
                    Author = AuthorEditString;
                    selectViewMainRecipeCardModel.Author = Author;
                    SelectedViewModel.Instance.CanSelectSave = true;
                    TitleTypeAuthorHeight = "0";
                }

                if (selectViewMainRecipeCardModel.RecipeTypeInt != CurrentTypeFromCombo)
                {
                    CurrentTypeString = MainNavTreeViewModel.Instance.CatagoryTypes[CurrentTypeFromCombo];
                    selectViewMainRecipeCardModel.RecipeTypeInt = CurrentTypeFromCombo;
                    SelectedViewModel.Instance.CanSelectSave = true;
                    TitleTypeAuthorHeight = "0";
                }

                //if there is a change we do something, otherwise we don't..  this has to be the first thing that we address.
                //the user cannot be allowed to get passed this if the title isn't unique.
                if (string.Compare(selectViewMainRecipeCardModel.Title.ToLower(), TitleEditString.ToLower()) != 0)
                {
                    bool titleExists = DataBaseAccessorsForRecipeManager.IsRecipeTitleInDB(TitleEditString);
                    if (titleExists == true) //Title clash don't save.
                    {
                        Title = TitleEditString;
                        SelectedViewModel.Instance.CanSelectSave = false;
                        TitleTypeAuthorHeight = "30";
                        Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog("You must have a unique title to save a recipe", "Please rename the recipe you are saving!");
                        dialog.ShowAsync();
                        
                    }
                    else
                    {
                        Title = TitleEditString;
                        selectViewMainRecipeCardModel.Title = Title;
                        SelectedViewModel.Instance.CanSelectSave = true;
                        TitleTypeAuthorHeight = "0";
                    }
                }

                
            }
        }


        /// <summary>
        /// Closes any open edit boxes before the user is shown a new recipe
        /// </summary>
        private void CloseAllEditBoxes()
        {
            TitleTypeAuthorHeight = "0";

            for (int count = 0; count < 50; count++)
            {
                IngredHeightList[count].Invoke("0");
            }
            for (int count = 0; count < 30; count++)
            {
                DirectHeightList[count].Invoke("0");
            }
        }

        private void Cancel(object sender)
        {
            int results;
            bool success;
            string[] parameters = sender.ToString().Split(',');

            if (parameters.Length > 1 && string.Compare(parameters[1].ToString().ToLower().Trim(), "ingredient") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                IngredHeightList[results - 1].Invoke("0");
            }

            if (parameters.Length > 1 && string.Compare(parameters[1].ToString().ToLower().Trim(), "direction") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                DirectHeightList[results - 1].Invoke("0");
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "titletypeauthor") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                TitleTypeAuthorHeight = "0";
            }
        }
        private void LoadIngredList(string val)
        {
            IngredHeightList = new List<Action<string>>();


            IngredHeightList.Add(SetIngred1Height); IngredHeightList.Add(SetIngred2Height); IngredHeightList.Add(SetIngred3Height); IngredHeightList.Add(SetIngred4Height); IngredHeightList.Add(SetIngred5Height);
            IngredHeightList.Add(SetIngred6Height); IngredHeightList.Add(SetIngred7Height); IngredHeightList.Add(SetIngred8Height); IngredHeightList.Add(SetIngred9Height); IngredHeightList.Add(SetIngred10Height);
            IngredHeightList.Add(SetIngred11Height); IngredHeightList.Add(SetIngred12Height); IngredHeightList.Add(SetIngred13Height); IngredHeightList.Add(SetIngred14Height); IngredHeightList.Add(SetIngred15Height);
            IngredHeightList.Add(SetIngred16Height); IngredHeightList.Add(SetIngred17Height); IngredHeightList.Add(SetIngred18Height); IngredHeightList.Add(SetIngred19Height); IngredHeightList.Add(SetIngred20Height);
            IngredHeightList.Add(SetIngred21Height); IngredHeightList.Add(SetIngred22Height); IngredHeightList.Add(SetIngred23Height); IngredHeightList.Add(SetIngred24Height); IngredHeightList.Add(SetIngred25Height);
            IngredHeightList.Add(SetIngred26Height); IngredHeightList.Add(SetIngred27Height); IngredHeightList.Add(SetIngred28Height); IngredHeightList.Add(SetIngred29Height); IngredHeightList.Add(SetIngred30Height);
            IngredHeightList.Add(SetIngred31Height); IngredHeightList.Add(SetIngred32Height); IngredHeightList.Add(SetIngred33Height); IngredHeightList.Add(SetIngred34Height); IngredHeightList.Add(SetIngred35Height);
            IngredHeightList.Add(SetIngred36Height); IngredHeightList.Add(SetIngred37Height); IngredHeightList.Add(SetIngred38Height); IngredHeightList.Add(SetIngred39Height); IngredHeightList.Add(SetIngred40Height);
            IngredHeightList.Add(SetIngred41Height); IngredHeightList.Add(SetIngred42Height); IngredHeightList.Add(SetIngred43Height); IngredHeightList.Add(SetIngred44Height); IngredHeightList.Add(SetIngred45Height);
            IngredHeightList.Add(SetIngred46Height); IngredHeightList.Add(SetIngred47Height); IngredHeightList.Add(SetIngred48Height); IngredHeightList.Add(SetIngred49Height); IngredHeightList.Add(SetIngred50Height);

            foreach (var action in IngredHeightList)
            {
                action.Invoke(val);
            }

            listOfIngredientEditStringsSetters = new List<Action<string>>();

            listOfIngredientEditStringsSetters.Add(SetIngredient1Edits); listOfIngredientEditStringsSetters.Add(SetIngredient2Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient3Edits); listOfIngredientEditStringsSetters.Add(SetIngredient4Edits); listOfIngredientEditStringsSetters.Add(SetIngredient5Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient6Edits); listOfIngredientEditStringsSetters.Add(SetIngredient7Edits); listOfIngredientEditStringsSetters.Add(SetIngredient8Edits); listOfIngredientEditStringsSetters.Add(SetIngredient9Edits); listOfIngredientEditStringsSetters.Add(SetIngredient10Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient11Edits); listOfIngredientEditStringsSetters.Add(SetIngredient12Edits); listOfIngredientEditStringsSetters.Add(SetIngredient13Edits); listOfIngredientEditStringsSetters.Add(SetIngredient14Edits); listOfIngredientEditStringsSetters.Add(SetIngredient15Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient16Edits); listOfIngredientEditStringsSetters.Add(SetIngredient17Edits); listOfIngredientEditStringsSetters.Add(SetIngredient18Edits); listOfIngredientEditStringsSetters.Add(SetIngredient19Edits); listOfIngredientEditStringsSetters.Add(SetIngredient20Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient21Edits); listOfIngredientEditStringsSetters.Add(SetIngredient22Edits); listOfIngredientEditStringsSetters.Add(SetIngredient23Edits); listOfIngredientEditStringsSetters.Add(SetIngredient24Edits); listOfIngredientEditStringsSetters.Add(SetIngredient25Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient26Edits); listOfIngredientEditStringsSetters.Add(SetIngredient27Edits); listOfIngredientEditStringsSetters.Add(SetIngredient28Edits); listOfIngredientEditStringsSetters.Add(SetIngredient29Edits); listOfIngredientEditStringsSetters.Add(SetIngredient30Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient31Edits); listOfIngredientEditStringsSetters.Add(SetIngredient32Edits); listOfIngredientEditStringsSetters.Add(SetIngredient33Edits); listOfIngredientEditStringsSetters.Add(SetIngredient34Edits); listOfIngredientEditStringsSetters.Add(SetIngredient35Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient36Edits); listOfIngredientEditStringsSetters.Add(SetIngredient37Edits); listOfIngredientEditStringsSetters.Add(SetIngredient38Edits); listOfIngredientEditStringsSetters.Add(SetIngredient39Edits); listOfIngredientEditStringsSetters.Add(SetIngredient40Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient41Edits); listOfIngredientEditStringsSetters.Add(SetIngredient42Edits); listOfIngredientEditStringsSetters.Add(SetIngredient43Edits); listOfIngredientEditStringsSetters.Add(SetIngredient44Edits); listOfIngredientEditStringsSetters.Add(SetIngredient45Edits);
            listOfIngredientEditStringsSetters.Add(SetIngredient46Edits); listOfIngredientEditStringsSetters.Add(SetIngredient47Edits); listOfIngredientEditStringsSetters.Add(SetIngredient48Edits); listOfIngredientEditStringsSetters.Add(SetIngredient49Edits); listOfIngredientEditStringsSetters.Add(SetIngredient50Edits);

            listOfIngredientEditStringsGetters = new List<Func<string>>();

            listOfIngredientEditStringsGetters.Add(GetIngredient1Edits); listOfIngredientEditStringsGetters.Add(GetIngredient2Edits); listOfIngredientEditStringsGetters.Add(GetIngredient3Edits); listOfIngredientEditStringsGetters.Add(GetIngredient4Edits); listOfIngredientEditStringsGetters.Add(GetIngredient5Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient6Edits); listOfIngredientEditStringsGetters.Add(GetIngredient7Edits); listOfIngredientEditStringsGetters.Add(GetIngredient8Edits); listOfIngredientEditStringsGetters.Add(GetIngredient9Edits); listOfIngredientEditStringsGetters.Add(GetIngredient10Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient11Edits); listOfIngredientEditStringsGetters.Add(GetIngredient12Edits); listOfIngredientEditStringsGetters.Add(GetIngredient13Edits); listOfIngredientEditStringsGetters.Add(GetIngredient14Edits); listOfIngredientEditStringsGetters.Add(GetIngredient15Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient16Edits); listOfIngredientEditStringsGetters.Add(GetIngredient17Edits); listOfIngredientEditStringsGetters.Add(GetIngredient18Edits); listOfIngredientEditStringsGetters.Add(GetIngredient19Edits); listOfIngredientEditStringsGetters.Add(GetIngredient20Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient21Edits); listOfIngredientEditStringsGetters.Add(GetIngredient22Edits); listOfIngredientEditStringsGetters.Add(GetIngredient23Edits); listOfIngredientEditStringsGetters.Add(GetIngredient24Edits); listOfIngredientEditStringsGetters.Add(GetIngredient25Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient26Edits); listOfIngredientEditStringsGetters.Add(GetIngredient27Edits); listOfIngredientEditStringsGetters.Add(GetIngredient28Edits); listOfIngredientEditStringsGetters.Add(GetIngredient29Edits); listOfIngredientEditStringsGetters.Add(GetIngredient30Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient31Edits); listOfIngredientEditStringsGetters.Add(GetIngredient32Edits); listOfIngredientEditStringsGetters.Add(GetIngredient33Edits); listOfIngredientEditStringsGetters.Add(GetIngredient34Edits); listOfIngredientEditStringsGetters.Add(GetIngredient35Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient36Edits); listOfIngredientEditStringsGetters.Add(GetIngredient37Edits); listOfIngredientEditStringsGetters.Add(GetIngredient38Edits); listOfIngredientEditStringsGetters.Add(GetIngredient39Edits); listOfIngredientEditStringsGetters.Add(GetIngredient40Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient41Edits); listOfIngredientEditStringsGetters.Add(GetIngredient42Edits); listOfIngredientEditStringsGetters.Add(GetIngredient43Edits); listOfIngredientEditStringsGetters.Add(GetIngredient44Edits); listOfIngredientEditStringsGetters.Add(GetIngredient45Edits);
            listOfIngredientEditStringsGetters.Add(GetIngredient46Edits); listOfIngredientEditStringsGetters.Add(GetIngredient47Edits); listOfIngredientEditStringsGetters.Add(GetIngredient48Edits); listOfIngredientEditStringsGetters.Add(GetIngredient49Edits); listOfIngredientEditStringsGetters.Add(GetIngredient50Edits);
        }


    private void LoadDirectList(string val)
    {
        DirectHeightList = new List<Action<string>>();

        DirectHeightList.Add(SetDirect1Height); DirectHeightList.Add(SetDirect2Height); DirectHeightList.Add(SetDirect3Height); DirectHeightList.Add(SetDirect4Height); DirectHeightList.Add(SetDirect5Height);
        DirectHeightList.Add(SetDirect6Height); DirectHeightList.Add(SetDirect7Height); DirectHeightList.Add(SetDirect8Height); DirectHeightList.Add(SetDirect9Height); DirectHeightList.Add(SetDirect10Height);
        DirectHeightList.Add(SetDirect11Height); DirectHeightList.Add(SetDirect12Height); DirectHeightList.Add(SetDirect13Height); DirectHeightList.Add(SetDirect14Height); DirectHeightList.Add(SetDirect15Height);
        DirectHeightList.Add(SetDirect16Height); DirectHeightList.Add(SetDirect17Height); DirectHeightList.Add(SetDirect18Height); DirectHeightList.Add(SetDirect19Height); DirectHeightList.Add(SetDirect20Height);
        DirectHeightList.Add(SetDirect21Height); DirectHeightList.Add(SetDirect22Height); DirectHeightList.Add(SetDirect23Height); DirectHeightList.Add(SetDirect24Height); DirectHeightList.Add(SetDirect25Height);
        DirectHeightList.Add(SetDirect26Height); DirectHeightList.Add(SetDirect27Height); DirectHeightList.Add(SetDirect28Height); DirectHeightList.Add(SetDirect29Height); DirectHeightList.Add(SetDirect30Height);

        foreach (var action in DirectHeightList)
        {
            action.Invoke(val);
        }

        listOfDirectionEditStringsSetters = new List<Action<string>>();

        listOfDirectionEditStringsSetters.Add(SetDirection1Edits); listOfDirectionEditStringsSetters.Add(SetDirection2Edits); listOfDirectionEditStringsSetters.Add(SetDirection3Edits); listOfDirectionEditStringsSetters.Add(SetDirection4Edits); listOfDirectionEditStringsSetters.Add(SetDirection5Edits);
        listOfDirectionEditStringsSetters.Add(SetDirection6Edits); listOfDirectionEditStringsSetters.Add(SetDirection7Edits); listOfDirectionEditStringsSetters.Add(SetDirection8Edits); listOfDirectionEditStringsSetters.Add(SetDirection9Edits); listOfDirectionEditStringsSetters.Add(SetDirection10Edits);
        listOfDirectionEditStringsSetters.Add(SetDirection11Edits); listOfDirectionEditStringsSetters.Add(SetDirection12Edits); listOfDirectionEditStringsSetters.Add(SetDirection13Edits); listOfDirectionEditStringsSetters.Add(SetDirection14Edits); listOfDirectionEditStringsSetters.Add(SetDirection15Edits);
        listOfDirectionEditStringsSetters.Add(SetDirection16Edits); listOfDirectionEditStringsSetters.Add(SetDirection17Edits); listOfDirectionEditStringsSetters.Add(SetDirection18Edits); listOfDirectionEditStringsSetters.Add(SetDirection19Edits); listOfDirectionEditStringsSetters.Add(SetDirection20Edits);
        listOfDirectionEditStringsSetters.Add(SetDirection21Edits); listOfDirectionEditStringsSetters.Add(SetDirection22Edits); listOfDirectionEditStringsSetters.Add(SetDirection23Edits); listOfDirectionEditStringsSetters.Add(SetDirection24Edits); listOfDirectionEditStringsSetters.Add(SetDirection25Edits);
        listOfDirectionEditStringsSetters.Add(SetDirection26Edits); listOfDirectionEditStringsSetters.Add(SetDirection27Edits); listOfDirectionEditStringsSetters.Add(SetDirection28Edits); listOfDirectionEditStringsSetters.Add(SetDirection29Edits); listOfDirectionEditStringsSetters.Add(SetDirection30Edits);

        listOfDirectionEditStringsGetters = new List<Func<string>>();

        listOfDirectionEditStringsGetters.Add(GetDirection1Edits); listOfDirectionEditStringsGetters.Add(GetDirection2Edits); listOfDirectionEditStringsGetters.Add(GetDirection3Edits); listOfDirectionEditStringsGetters.Add(GetDirection4Edits); listOfDirectionEditStringsGetters.Add(GetDirection5Edits);
        listOfDirectionEditStringsGetters.Add(GetDirection6Edits); listOfDirectionEditStringsGetters.Add(GetDirection7Edits); listOfDirectionEditStringsGetters.Add(GetDirection8Edits); listOfDirectionEditStringsGetters.Add(GetDirection9Edits); listOfDirectionEditStringsGetters.Add(GetDirection10Edits);
        listOfDirectionEditStringsGetters.Add(GetDirection11Edits); listOfDirectionEditStringsGetters.Add(GetDirection12Edits); listOfDirectionEditStringsGetters.Add(GetDirection13Edits); listOfDirectionEditStringsGetters.Add(GetDirection14Edits); listOfDirectionEditStringsGetters.Add(GetDirection15Edits);
        listOfDirectionEditStringsGetters.Add(GetDirection16Edits); listOfDirectionEditStringsGetters.Add(GetDirection17Edits); listOfDirectionEditStringsGetters.Add(GetDirection18Edits); listOfDirectionEditStringsGetters.Add(GetDirection19Edits); listOfDirectionEditStringsGetters.Add(GetDirection20Edits);
        listOfDirectionEditStringsGetters.Add(GetDirection21Edits); listOfDirectionEditStringsGetters.Add(GetDirection22Edits); listOfDirectionEditStringsGetters.Add(GetDirection23Edits); listOfDirectionEditStringsGetters.Add(GetDirection24Edits); listOfDirectionEditStringsGetters.Add(GetDirection25Edits);
        listOfDirectionEditStringsGetters.Add(GetDirection26Edits); listOfDirectionEditStringsGetters.Add(GetDirection27Edits); listOfDirectionEditStringsGetters.Add(GetDirection28Edits); listOfDirectionEditStringsGetters.Add(GetDirection29Edits); listOfDirectionEditStringsGetters.Add(GetDirection30Edits);
    }

        #region Properties ICommands and related

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
        /// Will always return True
        /// </summary>
        public bool CanSelectAlwaysTrue
        {
            get
            {
                return true;
            }
        }

        ///// <summary>
        ///// can't select save until the user is logged in
        ///// because there is no access to the DB
        ///// </summary>
        //private bool canSelectSave;
        //public bool CanSelectSave
        //{
        //    set
        //    {
        //        canSelectSave = value;
        //        CmdSave.RaiseCanExecuteChanged();
        //    }
        //    get
        //    {
        //        return canSelectSave;
        //    }
        //}


        /// <summary>
        /// property for the Save button command
        /// </summary>
        //public RelayCommandRaiseCanExecute CmdSave
        //{
        //    get;
        //    private set;
        //}
        ///// <summary>
        ///// property for the Edit button command
        ///// </summary>
        //public RelayCommand CmdEdit
        //{
        //    get;
        //    private set;
        //}
        /// <summary>
        /// property for the Edit button command
        /// </summary>
        //public RelayCommand CmdNew
        //{
        //    get;
        //    private set;
        //}
        ///// <summary>
        /// property for the Remove button command
        /// </summary>
        //public RelayCommand CmdRemove
        //{
        //    get;
        //    private set;
        //}
        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        //public ICommand CmdSelectedItemChanged
        //{
        //    get;
        //    private set;
        //}

        /// <summary>
        /// property for the Quantity combobox change command
        /// </summary>
        public ICommand CmdSelectedQuantityChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public ICommand CmdSelectedTypeChanged
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

        private string currentTypeString;
        public string CurrentTypeString
        {
            get { return currentTypeString; }
            private set //because of the listbox I need to set the type property here and trigger the save button.
            {
                SetProperty(ref currentTypeString, value);
            }
        }

        private int currentTypeFromCombo;
        public int CurrentTypeFromCombo
        {
            get { return currentTypeFromCombo; }
            set //because of the listbox I need to set the type property here and trigger the save button.
            {
                SetProperty(ref currentTypeFromCombo, value);
                CurrentTypeString = MainNavTreeViewModel.Instance.CatagoryTypes[currentTypeFromCombo];
            }
        }


        private string titleTypeAuthorHeight;
        public string TitleTypeAuthorHeight
        {
            get { return titleTypeAuthorHeight; }
            set { SetProperty(ref titleTypeAuthorHeight, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        private string authorEditString;
        public string AuthorEditString
        {
            get { return authorEditString; }
            set { SetProperty(ref authorEditString, value); }
        }

        private string titleEditString;
        public string TitleEditString
        {
            get { return titleEditString; }
            set { SetProperty(ref titleEditString, value); }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
        }

        private string authorMaxLength;
        public string AuthorMaxLength
        {
            get { return authorMaxLength; }
            set { SetProperty(ref authorMaxLength, value); }
        }

        private string titleMaxLength;
        public string TitleMaxLength
        {
            get { return titleMaxLength; }
            set { SetProperty(ref titleMaxLength, value); }
        }

        private string directMaxLength;
        public string DirectMaxLength
        {
            get { return directMaxLength; }
            set { SetProperty(ref directMaxLength, value); }
        }

        private string ingredMaxLength;
        public string IngredMaxLength
        {
            get { return ingredMaxLength; }
            set { SetProperty(ref ingredMaxLength, value); }
        }

        #endregion

        #region rowHeightPropertiesFor Ingredient and Directions

        private string ingred1Height;
    private void SetIngred1Height(string value)
    { Ingred1Height = value; }
    public string Ingred1Height
    {
        get { return ingred1Height; }
        set { SetProperty(ref ingred1Height, value); }
    }

    private string ingred2Height;
    private void SetIngred2Height(string value)
    { Ingred2Height = value; }
    public string Ingred2Height
    {
        get { return ingred2Height; }
        set { SetProperty(ref ingred2Height, value); }
    }

    private string ingred3Height;
    private void SetIngred3Height(string value)
    { Ingred3Height = value; }
    public string Ingred3Height
    {
        get { return ingred3Height; }
        set { SetProperty(ref ingred3Height, value); }
    }

    private string ingred4Height;
    private void SetIngred4Height(string value)
    { Ingred4Height = value; }
    public string Ingred4Height
    {
        get { return ingred4Height; }
        set { SetProperty(ref ingred4Height, value); }
    }

    private string ingred5Height;
    private void SetIngred5Height(string value)
    { Ingred5Height = value; }
    public string Ingred5Height
    {
        get { return ingred5Height; }
        set { SetProperty(ref ingred5Height, value); }
    }

    private string ingred6Height;
    private void SetIngred6Height(string value)
    { Ingred6Height = value; }
    public string Ingred6Height
    {
        get { return ingred6Height; }
        set { SetProperty(ref ingred6Height, value); }
    }

    private string ingred7Height;
    private void SetIngred7Height(string value)
    { Ingred7Height = value; }
    public string Ingred7Height
    {
        get { return ingred7Height; }
        set { SetProperty(ref ingred7Height, value); }
    }

    private string ingred8Height;
    private void SetIngred8Height(string value)
    { Ingred8Height = value; }
    public string Ingred8Height
    {
        get { return ingred8Height; }
        set { SetProperty(ref ingred8Height, value); }
    }

    private string ingred9Height;
    private void SetIngred9Height(string value)
    { Ingred9Height = value; }
    public string Ingred9Height
    {
        get { return ingred9Height; }
        set { SetProperty(ref ingred9Height, value); }
    }

    private string ingred10Height;
    private void SetIngred10Height(string value)
    { Ingred10Height = value; }
    public string Ingred10Height
    {
        get { return ingred10Height; }
        set { SetProperty(ref ingred10Height, value); }
    }

    private string ingred11Height;
    private void SetIngred11Height(string value)
    { Ingred11Height = value; }
    public string Ingred11Height
    {
        get { return ingred11Height; }
        set { SetProperty(ref ingred11Height, value); }
    }

    private string ingred12Height;
    private void SetIngred12Height(string value)
    { Ingred12Height = value; }
    public string Ingred12Height
    {
        get { return ingred12Height; }
        set { SetProperty(ref ingred12Height, value); }
    }

    private string ingred13Height;
    private void SetIngred13Height(string value)
    { Ingred13Height = value; }
    public string Ingred13Height
    {
        get { return ingred13Height; }
        set { SetProperty(ref ingred13Height, value); }
    }

    private string ingred14Height;
    private void SetIngred14Height(string value)
    { Ingred14Height = value; }
    public string Ingred14Height
    {
        get { return ingred14Height; }
        set { SetProperty(ref ingred14Height, value); }
    }

    private string ingred15Height;
    private void SetIngred15Height(string value)
    { Ingred15Height = value; }
    public string Ingred15Height
    {
        get { return ingred15Height; }
        set { SetProperty(ref ingred15Height, value); }
    }

    private string ingred16Height;
    private void SetIngred16Height(string value)
    { Ingred16Height = value; }
    public string Ingred16Height
    {
        get { return ingred16Height; }
        set { SetProperty(ref ingred16Height, value); }
    }

    private string ingred17Height;
    private void SetIngred17Height(string value)
    { Ingred17Height = value; }
    public string Ingred17Height
    {
        get { return ingred17Height; }
        set { SetProperty(ref ingred17Height, value); }
    }

    private string ingred18Height;
    private void SetIngred18Height(string value)
    { Ingred18Height = value; }
    public string Ingred18Height
    {
        get { return ingred18Height; }
        set { SetProperty(ref ingred18Height, value); }
    }

    private string ingred19Height;
    private void SetIngred19Height(string value)
    { Ingred19Height = value; }
    public string Ingred19Height
    {
        get { return ingred19Height; }
        set { SetProperty(ref ingred19Height, value); }
    }

    private string ingred20Height;
    private void SetIngred20Height(string value)
    { Ingred20Height = value; }
    public string Ingred20Height
    {
        get { return ingred20Height; }
        set { SetProperty(ref ingred20Height, value); }
    }

    private string ingred21Height;
    private void SetIngred21Height(string value)
    { Ingred21Height = value; }
    public string Ingred21Height
    {
        get { return ingred21Height; }
        set { SetProperty(ref ingred21Height, value); }
    }

    private string ingred22Height;
    private void SetIngred22Height(string value)
    { Ingred22Height = value; }
    public string Ingred22Height
    {
        get { return ingred22Height; }
        set { SetProperty(ref ingred22Height, value); }
    }

    private string ingred23Height;
    private void SetIngred23Height(string value)
    { Ingred23Height = value; }
    public string Ingred23Height
    {
        get { return ingred23Height; }
        set { SetProperty(ref ingred23Height, value); }
    }

    private string ingred24Height;
    private void SetIngred24Height(string value)
    { Ingred24Height = value; }
    public string Ingred24Height
    {
        get { return ingred24Height; }
        set { SetProperty(ref ingred24Height, value); }
    }

    private string ingred25Height;
    private void SetIngred25Height(string value)
    { Ingred25Height = value; }
    public string Ingred25Height
    {
        get { return ingred25Height; }
        set { SetProperty(ref ingred25Height, value); }
    }

    private string ingred26Height;
    private void SetIngred26Height(string value)
    { Ingred26Height = value; }
    public string Ingred26Height
    {
        get { return ingred26Height; }
        set { SetProperty(ref ingred26Height, value); }
    }

    private string ingred27Height;
    private void SetIngred27Height(string value)
    { Ingred27Height = value; }
    public string Ingred27Height
    {
        get { return ingred27Height; }
        set { SetProperty(ref ingred27Height, value); }
    }

    private string ingred28Height;
    private void SetIngred28Height(string value)
    { Ingred28Height = value; }
    public string Ingred28Height
    {
        get { return ingred28Height; }
        set { SetProperty(ref ingred28Height, value); }
    }

    private string ingred29Height;
    private void SetIngred29Height(string value)
    { Ingred29Height = value; }
    public string Ingred29Height
    {
        get { return ingred29Height; }
        set { SetProperty(ref ingred29Height, value); }
    }

    private string ingred30Height;
    private void SetIngred30Height(string value)
    { Ingred30Height = value; }
    public string Ingred30Height
    {
        get { return ingred30Height; }
        set { SetProperty(ref ingred30Height, value); }
    }

    private string ingred31Height;
    private void SetIngred31Height(string value)
    { Ingred31Height = value; }
    public string Ingred31Height
    {
        get { return ingred31Height; }
        set { SetProperty(ref ingred31Height, value); }
    }

    private string ingred32Height;
    private void SetIngred32Height(string value)
    { Ingred32Height = value; }
    public string Ingred32Height
    {
        get { return ingred32Height; }
        set { SetProperty(ref ingred32Height, value); }
    }

    private string ingred33Height;
    private void SetIngred33Height(string value)
    { Ingred33Height = value; }
    public string Ingred33Height
    {
        get { return ingred33Height; }
        set { SetProperty(ref ingred33Height, value); }
    }

    private string ingred34Height;
    private void SetIngred34Height(string value)
    { Ingred34Height = value; }
    public string Ingred34Height
    {
        get { return ingred34Height; }
        set { SetProperty(ref ingred34Height, value); }
    }

    private string ingred35Height;
    private void SetIngred35Height(string value)
    { Ingred35Height = value; }
    public string Ingred35Height
    {
        get { return ingred35Height; }
        set { SetProperty(ref ingred35Height, value); }
    }

    private string ingred36Height;
    private void SetIngred36Height(string value)
    { Ingred36Height = value; }
    public string Ingred36Height
    {
        get { return ingred36Height; }
        set { SetProperty(ref ingred36Height, value); }
    }

    private string ingred37Height;
    private void SetIngred37Height(string value)
    { Ingred37Height = value; }
    public string Ingred37Height
    {
        get { return ingred37Height; }
        set { SetProperty(ref ingred37Height, value); }
    }

    private string ingred38Height;
    private void SetIngred38Height(string value)
    { Ingred38Height = value; }
    public string Ingred38Height
    {
        get { return ingred38Height; }
        set { SetProperty(ref ingred38Height, value); }
    }

    private string ingred39Height;
    private void SetIngred39Height(string value)
    { Ingred39Height = value; }
    public string Ingred39Height
    {
        get { return ingred39Height; }
        set { SetProperty(ref ingred39Height, value); }
    }

    private string ingred40Height;
    private void SetIngred40Height(string value)
    { Ingred40Height = value; }

    public string Ingred40Height
    {
        get { return ingred40Height; }
        set { SetProperty(ref ingred40Height, value); }
    }

    private string ingred41Height;
    private void SetIngred41Height(string value)
    { Ingred41Height = value; }
    public string Ingred41Height
    {
        get { return ingred41Height; }
        set { SetProperty(ref ingred41Height, value); }
    }

    private string ingred42Height;
    private void SetIngred42Height(string value)
    { Ingred42Height = value; }
    public string Ingred42Height
    {
        get { return ingred42Height; }
        set { SetProperty(ref ingred42Height, value); }
    }

    private string ingred43Height;
    private void SetIngred43Height(string value)
    { Ingred43Height = value; }
    public string Ingred43Height
    {
        get { return ingred43Height; }
        set { SetProperty(ref ingred43Height, value); }
    }

    private string ingred44Height;
    private void SetIngred44Height(string value)
    { Ingred44Height = value; }
    public string Ingred44Height
    {
        get { return ingred44Height; }
        set { SetProperty(ref ingred44Height, value); }
    }

    private string ingred45Height;
    private void SetIngred45Height(string value)
    { Ingred45Height = value; }
    public string Ingred45Height
    {
        get { return ingred45Height; }
        set { SetProperty(ref ingred45Height, value); }
    }

    private string ingred46Height;
    private void SetIngred46Height(string value)
    { Ingred46Height = value; }
    public string Ingred46Height
    {
        get { return ingred46Height; }
        set { SetProperty(ref ingred46Height, value); }
    }

    private string ingred47Height;
    private void SetIngred47Height(string value)
    { Ingred47Height = value; }
    public string Ingred47Height
    {
        get { return ingred47Height; }
        set { SetProperty(ref ingred47Height, value); }
    }

    private string ingred48Height;
    private void SetIngred48Height(string value)
    { Ingred48Height = value; }
    public string Ingred48Height
    {
        get { return ingred48Height; }
        set { SetProperty(ref ingred48Height, value); }
    }

    private string ingred49Height;
    private void SetIngred49Height(string value)
    { Ingred49Height = value; }
    public string Ingred49Height
    {
        get { return ingred49Height; }
        set { SetProperty(ref ingred49Height, value); }
    }

    private string ingred50Height;
    private void SetIngred50Height(string value)
    { Ingred50Height = value; }
    public string Ingred50Height
    {
        get { return ingred50Height; }
        set { SetProperty(ref ingred50Height, value); }
    }

    private string direct1Height;
    private void SetDirect1Height(string value)
    { Direct1Height = value; }
    public string Direct1Height
    {
        get { return direct1Height; }
        set { SetProperty(ref direct1Height, value); }
    }

    private string direct2Height;
    private void SetDirect2Height(string value)
    { Direct2Height = value; }
    public string Direct2Height
    {
        get { return direct2Height; }
        set { SetProperty(ref direct2Height, value); }
    }

    private string direct3Height;
    private void SetDirect3Height(string value)
    { Direct3Height = value; }
    public string Direct3Height
    {
        get { return direct3Height; }
        set { SetProperty(ref direct3Height, value); }
    }

    private string direct4Height;
    private void SetDirect4Height(string value)
    { Direct4Height = value; }
    public string Direct4Height
    {
        get { return direct4Height; }
        set { SetProperty(ref direct4Height, value); }
    }

    private string direct5Height;
    private void SetDirect5Height(string value)
    { Direct5Height = value; }
    public string Direct5Height
    {
        get { return direct5Height; }
        set { SetProperty(ref direct5Height, value); }
    }

    private string direct6Height;
    private void SetDirect6Height(string value)
    { Direct6Height = value; }
    public string Direct6Height
    {
        get { return direct6Height; }
        set { SetProperty(ref direct6Height, value); }
    }

    private string direct7Height;
    private void SetDirect7Height(string value)
    { Direct7Height = value; }
    public string Direct7Height
    {
        get { return direct7Height; }
        set { SetProperty(ref direct7Height, value); }
    }

    private string direct8Height;
    private void SetDirect8Height(string value)
    { Direct8Height = value; }
    public string Direct8Height
    {
        get { return direct8Height; }
        set { SetProperty(ref direct8Height, value); }
    }

    private string direct9Height;
    private void SetDirect9Height(string value)
    { Direct9Height = value; }
    public string Direct9Height
    {
        get { return direct9Height; }
        set { SetProperty(ref direct9Height, value); }
    }

    private string direct10Height;
    private void SetDirect10Height(string value)
    { Direct10Height = value; }
    public string Direct10Height
    {
        get { return direct10Height; }
        set { SetProperty(ref direct10Height, value); }
    }

    private string direct11Height;
    private void SetDirect11Height(string value)
    { Direct11Height = value; }
    public string Direct11Height
    {
        get { return direct11Height; }
        set { SetProperty(ref direct11Height, value); }
    }

    private string direct12Height;
    private void SetDirect12Height(string value)
    { Direct12Height = value; }
    public string Direct12Height
    {
        get { return direct12Height; }
        set { SetProperty(ref direct12Height, value); }
    }

    private string direct13Height;
    private void SetDirect13Height(string value)
    { Direct13Height = value; }
    public string Direct13Height
    {
        get { return direct13Height; }
        set { SetProperty(ref direct13Height, value); }
    }

    private string direct14Height;
    private void SetDirect14Height(string value)
    { Direct14Height = value; }
    public string Direct14Height
    {
        get { return direct14Height; }
        set { SetProperty(ref direct14Height, value); }
    }

    private string direct15Height;
    private void SetDirect15Height(string value)
    { Direct15Height = value; }
    public string Direct15Height
    {
        get { return direct15Height; }
        set { SetProperty(ref direct15Height, value); }
    }

    private string direct16Height;
    private void SetDirect16Height(string value)
    { Direct16Height = value; }
    public string Direct16Height
    {
        get { return direct16Height; }
        set { SetProperty(ref direct16Height, value); }
    }

    private string direct17Height;
    private void SetDirect17Height(string value)
    { Direct17Height = value; }
    public string Direct17Height
    {
        get { return direct17Height; }
        set { SetProperty(ref direct17Height, value); }
    }

    private string direct18Height;
    private void SetDirect18Height(string value)
    { Direct18Height = value; }
    public string Direct18Height
    {
        get { return direct18Height; }
        set { SetProperty(ref direct18Height, value); }
    }

    private string direct19Height;
    private void SetDirect19Height(string value)
    { Direct19Height = value; }
    public string Direct19Height
    {
        get { return direct19Height; }
        set { SetProperty(ref direct19Height, value); }
    }

    private string direct20Height;
    private void SetDirect20Height(string value)
    { Direct20Height = value; }
    public string Direct20Height
    {
        get { return direct20Height; }
        set { SetProperty(ref direct20Height, value); }
    }
    private string direct21Height;
    private void SetDirect21Height(string value)
    { Direct21Height = value; }
    public string Direct21Height
    {
        get { return direct21Height; }
        set { SetProperty(ref direct21Height, value); }
    }

    private string direct22Height;
    private void SetDirect22Height(string value)
    { Direct22Height = value; }
    public string Direct22Height
    {
        get { return direct22Height; }
        set { SetProperty(ref direct22Height, value); }
    }

    private string direct23Height;
    private void SetDirect23Height(string value)
    { Direct23Height = value; }
    public string Direct23Height
    {
        get { return direct23Height; }
        set { SetProperty(ref direct23Height, value); }
    }

    private string direct24Height;
    private void SetDirect24Height(string value)
    { Direct24Height = value; }
    public string Direct24Height
    {
        get { return direct24Height; }
        set { SetProperty(ref direct24Height, value); }
    }

    private string direct25Height;
    private void SetDirect25Height(string value)
    { Direct25Height = value; }
    public string Direct25Height
    {
        get { return direct25Height; }
        set { SetProperty(ref direct25Height, value); }
    }

    private string direct26Height;
    private void SetDirect26Height(string value)
    { Direct26Height = value; }
    public string Direct26Height
    {
        get { return direct26Height; }
        set { SetProperty(ref direct26Height, value); }
    }

    private string direct27Height;
    private void SetDirect27Height(string value)
    { Direct27Height = value; }
    public string Direct27Height
    {
        get { return direct27Height; }
        set { SetProperty(ref direct27Height, value); }
    }

    private string direct28Height;
    private void SetDirect28Height(string value)
    { Direct28Height = value; }
    public string Direct28Height
    {
        get { return direct28Height; }
        set { SetProperty(ref direct28Height, value); }
    }

    private string direct29Height;
    private void SetDirect29Height(string value)
    { Direct29Height = value; }
    public string Direct29Height
    {
        get { return direct29Height; }
        set { SetProperty(ref direct29Height, value); }
    }

    private string direct30Height;
    private void SetDirect30Height(string value)
    { Direct30Height = value; }
    public string Direct30Height
    {
        get { return direct30Height; }
        set { SetProperty(ref direct30Height, value); }
    }

    #endregion



   

    #region 50 Get/Set Properties for the IngredientsEdits

    internal void SetIngredient1Edits(string value)
    { Ingredient1Edits = value; }
    internal string GetIngredient1Edits()
    { return Ingredient1Edits; }
    public string Ingredient1Edits
    {
        get { return ingredient1Edits; }
        set { SetProperty(ref ingredient1Edits, value); }
    }
    internal void SetIngredient2Edits(string value)
    { Ingredient2Edits = value; }
    internal string GetIngredient2Edits()
    { return Ingredient2Edits; }
    public string Ingredient2Edits
    {
        get { return ingredient2Edits; }
        set { SetProperty(ref ingredient2Edits, value); }
    }
    internal void SetIngredient3Edits(string value)
    { Ingredient3Edits = value; }
    internal string GetIngredient3Edits()
    { return Ingredient3Edits; }
    public string Ingredient3Edits
    {
        get { return ingredient3Edits; }
        set { SetProperty(ref ingredient3Edits, value); }
    }

    internal void SetIngredient4Edits(string value)
    { Ingredient4Edits = value; }
    internal string GetIngredient4Edits()
    { return Ingredient4Edits; }
    public string Ingredient4Edits
    {
        get { return ingredient4Edits; }
        set { SetProperty(ref ingredient4Edits, value); }
    }

    internal void SetIngredient5Edits(string value)
    { Ingredient5Edits = value; }
    internal string GetIngredient5Edits()
    { return Ingredient5Edits; }
    public string Ingredient5Edits
    {
        get { return ingredient5Edits; }
        set { SetProperty(ref ingredient5Edits, value); }
    }

    internal void SetIngredient6Edits(string value)
    { Ingredient6Edits = value; }
    internal string GetIngredient6Edits()
    { return Ingredient6Edits; }
    public string Ingredient6Edits
    {
        get { return ingredient6Edits; }
        set { SetProperty(ref ingredient6Edits, value); }
    }

    internal void SetIngredient7Edits(string value)
    { Ingredient7Edits = value; }
    internal string GetIngredient7Edits()
    { return Ingredient7Edits; }
    public string Ingredient7Edits
    {
        get { return ingredient7Edits; }
        set { SetProperty(ref ingredient7Edits, value); }
    }
    internal void SetIngredient8Edits(string value)
    { Ingredient8Edits = value; }
    internal string GetIngredient8Edits()
    { return Ingredient8Edits; }
    public string Ingredient8Edits
    {
        get { return ingredient8Edits; }
        set { SetProperty(ref ingredient8Edits, value); }
    }
    internal void SetIngredient9Edits(string value)
    { Ingredient9Edits = value; }
    internal string GetIngredient9Edits()
    { return Ingredient9Edits; }
    public string Ingredient9Edits
    {
        get { return ingredient9Edits; }
        set { SetProperty(ref ingredient9Edits, value); }
    }
    internal void SetIngredient10Edits(string value)
    { Ingredient10Edits = value; }
    internal string GetIngredient10Edits()
    { return Ingredient10Edits; }
    public string Ingredient10Edits
    {
        get { return ingredient10Edits; }
        set { SetProperty(ref ingredient10Edits, value); }
    }
    internal void SetIngredient11Edits(string value)
    { Ingredient11Edits = value; }
    internal string GetIngredient11Edits()
    { return Ingredient11Edits; }
    public string Ingredient11Edits
    {
        get { return ingredient11Edits; }
        set { SetProperty(ref ingredient11Edits, value); }
    }
    internal void SetIngredient12Edits(string value)
    { Ingredient12Edits = value; }
    internal string GetIngredient12Edits()
    { return Ingredient12Edits; }
    public string Ingredient12Edits
    {
        get { return ingredient12Edits; }
        set { SetProperty(ref ingredient12Edits, value); }
    }
    internal void SetIngredient13Edits(string value)
    { Ingredient13Edits = value; }
    internal string GetIngredient13Edits()
    { return Ingredient13Edits; }
    public string Ingredient13Edits
    {
        get { return ingredient13Edits; }
        set { SetProperty(ref ingredient13Edits, value); }
    }
    internal void SetIngredient14Edits(string value)
    { Ingredient14Edits = value; }
    internal string GetIngredient14Edits()
    { return Ingredient14Edits; }
    public string Ingredient14Edits
    {
        get { return ingredient14Edits; }
        set { SetProperty(ref ingredient14Edits, value); }
    }
    internal void SetIngredient15Edits(string value)
    { Ingredient15Edits = value; }
    internal string GetIngredient15Edits()
    { return Ingredient15Edits; }
    public string Ingredient15Edits
    {
        get { return ingredient15Edits; }
        set { SetProperty(ref ingredient15Edits, value); }
    }
    internal void SetIngredient16Edits(string value)
    { Ingredient16Edits = value; }
    internal string GetIngredient16Edits()
    { return Ingredient16Edits; }
    public string Ingredient16Edits
    {
        get { return ingredient16Edits; }
        set { SetProperty(ref ingredient16Edits, value); }
    }
    internal void SetIngredient17Edits(string value)
    { Ingredient17Edits = value; }
    internal string GetIngredient17Edits()
    { return Ingredient17Edits; }
    public string Ingredient17Edits
    {
        get { return ingredient17Edits; }
        set { SetProperty(ref ingredient17Edits, value); }
    }
    internal void SetIngredient18Edits(string value)
    { Ingredient18Edits = value; }
    internal string GetIngredient18Edits()
    { return Ingredient18Edits; }
    public string Ingredient18Edits
    {
        get { return ingredient18Edits; }
        set { SetProperty(ref ingredient18Edits, value); }
    }
    internal void SetIngredient19Edits(string value)
    { Ingredient19Edits = value; }
    internal string GetIngredient19Edits()
    { return Ingredient19Edits; }
    public string Ingredient19Edits
    {
        get { return ingredient19Edits; }
        set { SetProperty(ref ingredient19Edits, value); }
    }
    internal void SetIngredient20Edits(string value)
    { Ingredient20Edits = value; }
    internal string GetIngredient20Edits()
    { return Ingredient20Edits; }
    public string Ingredient20Edits
    {
        get { return ingredient20Edits; }
        set { SetProperty(ref ingredient20Edits, value); }
    }
    internal void SetIngredient21Edits(string value)
    { Ingredient21Edits = value; }
    internal string GetIngredient21Edits()
    { return Ingredient21Edits; }
    public string Ingredient21Edits
    {
        get { return ingredient21Edits; }
        set { SetProperty(ref ingredient21Edits, value); }
    }
    internal void SetIngredient22Edits(string value)
    { Ingredient22Edits = value; }
    internal string GetIngredient22Edits()
    { return Ingredient22Edits; }
    public string Ingredient22Edits
    {
        get { return ingredient22Edits; }
        set { SetProperty(ref ingredient22Edits, value); }
    }
    internal void SetIngredient23Edits(string value)
    { Ingredient23Edits = value; }
    internal string GetIngredient23Edits()
    { return Ingredient23Edits; }
    public string Ingredient23Edits
    {
        get { return ingredient23Edits; }
        set { SetProperty(ref ingredient23Edits, value); }
    }
    internal void SetIngredient24Edits(string value)
    { Ingredient24Edits = value; }
    internal string GetIngredient24Edits()
    { return Ingredient24Edits; }
    public string Ingredient24Edits
    {
        get { return ingredient24Edits; }
        set { SetProperty(ref ingredient24Edits, value); }
    }
    internal void SetIngredient25Edits(string value)
    { Ingredient25Edits = value; }
    internal string GetIngredient25Edits()
    { return Ingredient25Edits; }
    public string Ingredient25Edits
    {
        get { return ingredient25Edits; }
        set { SetProperty(ref ingredient25Edits, value); }
    }
    internal void SetIngredient26Edits(string value)
    { Ingredient26Edits = value; }
    internal string GetIngredient26Edits()
    { return Ingredient26Edits; }
    public string Ingredient26Edits
    {
        get { return ingredient26Edits; }
        set { SetProperty(ref ingredient26Edits, value); }
    }
    internal void SetIngredient27Edits(string value)
    { Ingredient27Edits = value; }
    internal string GetIngredient27Edits()
    { return Ingredient27Edits; }
    public string Ingredient27Edits
    {
        get { return ingredient27Edits; }
        set { SetProperty(ref ingredient27Edits, value); }
    }
    internal void SetIngredient28Edits(string value)
    { Ingredient28Edits = value; }
    internal string GetIngredient28Edits()
    { return Ingredient28Edits; }
    public string Ingredient28Edits
    {
        get { return ingredient28Edits; }
        set { SetProperty(ref ingredient28Edits, value); }
    }
    internal void SetIngredient29Edits(string value)
    { Ingredient29Edits = value; }
    internal string GetIngredient29Edits()
    { return Ingredient29Edits; }
    public string Ingredient29Edits
    {
        get { return ingredient29Edits; }
        set { SetProperty(ref ingredient29Edits, value); }
    }
    internal void SetIngredient30Edits(string value)
    { Ingredient30Edits = value; }
    internal string GetIngredient30Edits()
    { return Ingredient30Edits; }
    public string Ingredient30Edits
    {
        get { return ingredient30Edits; }
        set { SetProperty(ref ingredient30Edits, value); }
    }
    internal void SetIngredient31Edits(string value)
    { Ingredient31Edits = value; }
    internal string GetIngredient31Edits()
    { return Ingredient31Edits; }
    public string Ingredient31Edits
    {
        get { return ingredient31Edits; }
        set { SetProperty(ref ingredient31Edits, value); }
    }
    internal void SetIngredient32Edits(string value)
    { Ingredient32Edits = value; }
    internal string GetIngredient32Edits()
    { return Ingredient32Edits; }
    public string Ingredient32Edits
    {
        get { return ingredient32Edits; }
        set { SetProperty(ref ingredient32Edits, value); }
    }
    internal void SetIngredient33Edits(string value)
    { Ingredient33Edits = value; }
    internal string GetIngredient33Edits()
    { return Ingredient33Edits; }
    public string Ingredient33Edits
    {
        get { return ingredient33Edits; }
        set { SetProperty(ref ingredient33Edits, value); }
    }
    internal void SetIngredient34Edits(string value)
    { Ingredient34Edits = value; }
    internal string GetIngredient34Edits()
    { return Ingredient34Edits; }
    public string Ingredient34Edits
    {
        get { return ingredient34Edits; }
        set { SetProperty(ref ingredient34Edits, value); }
    }
    internal void SetIngredient35Edits(string value)
    { Ingredient35Edits = value; }
    internal string GetIngredient35Edits()
    { return Ingredient35Edits; }
    public string Ingredient35Edits
    {
        get { return ingredient35Edits; }
        set { SetProperty(ref ingredient35Edits, value); }
    }
    internal void SetIngredient36Edits(string value)
    { Ingredient36Edits = value; }
    internal string GetIngredient36Edits()
    { return Ingredient36Edits; }
    public string Ingredient36Edits
    {
        get { return ingredient36Edits; }
        set { SetProperty(ref ingredient36Edits, value); }
    }
    internal void SetIngredient37Edits(string value)
    { Ingredient37Edits = value; }
    internal string GetIngredient37Edits()
    { return Ingredient37Edits; }
    public string Ingredient37Edits
    {
        get { return ingredient37Edits; }
        set { SetProperty(ref ingredient37Edits, value); }
    }
    internal void SetIngredient38Edits(string value)
    { Ingredient38Edits = value; }
    internal string GetIngredient38Edits()
    { return Ingredient38Edits; }
    public string Ingredient38Edits
    {
        get { return ingredient38Edits; }
        set { SetProperty(ref ingredient38Edits, value); }
    }
    internal void SetIngredient39Edits(string value)
    { Ingredient39Edits = value; }
    internal string GetIngredient39Edits()
    { return Ingredient39Edits; }
    public string Ingredient39Edits
    {
        get { return ingredient39Edits; }
        set { SetProperty(ref ingredient39Edits, value); }
    }
    internal void SetIngredient40Edits(string value)
    { Ingredient40Edits = value; }
    internal string GetIngredient40Edits()
    { return Ingredient40Edits; }
    public string Ingredient40Edits
    {
        get { return ingredient40Edits; }
        set { SetProperty(ref ingredient40Edits, value); }
    }
    internal void SetIngredient41Edits(string value)
    { Ingredient41Edits = value; }
    internal string GetIngredient41Edits()
    { return Ingredient41Edits; }
    public string Ingredient41Edits
    {
        get { return ingredient41Edits; }
        set { SetProperty(ref ingredient41Edits, value); }
    }
    internal void SetIngredient42Edits(string value)
    { Ingredient42Edits = value; }
    internal string GetIngredient42Edits()
    { return Ingredient42Edits; }
    public string Ingredient42Edits
    {
        get { return ingredient42Edits; }
        set { SetProperty(ref ingredient42Edits, value); }
    }
    internal void SetIngredient43Edits(string value)
    { Ingredient43Edits = value; }
    internal string GetIngredient43Edits()
    { return Ingredient43Edits; }
    public string Ingredient43Edits
    {
        get { return ingredient43Edits; }
        set { SetProperty(ref ingredient43Edits, value); }
    }
    internal void SetIngredient44Edits(string value)
    { Ingredient44Edits = value; }
    internal string GetIngredient44Edits()
    { return Ingredient44Edits; }
    public string Ingredient44Edits
    {
        get { return ingredient44Edits; }
        set { SetProperty(ref ingredient44Edits, value); }
    }
    internal void SetIngredient45Edits(string value)
    { Ingredient45Edits = value; }
    internal string GetIngredient45Edits()
    { return Ingredient45Edits; }
    public string Ingredient45Edits
    {
        get { return ingredient45Edits; }
        set { SetProperty(ref ingredient45Edits, value); }
    }
    internal void SetIngredient46Edits(string value)
    { Ingredient46Edits = value; }
    internal string GetIngredient46Edits()
    { return Ingredient46Edits; }
    public string Ingredient46Edits
    {
        get { return ingredient46Edits; }
        set { SetProperty(ref ingredient46Edits, value); }
    }
    internal void SetIngredient47Edits(string value)
    { Ingredient47Edits = value; }
    internal string GetIngredient47Edits()
    { return Ingredient47Edits; }
    public string Ingredient47Edits
    {
        get { return ingredient47Edits; }
        set { SetProperty(ref ingredient47Edits, value); }
    }
    internal void SetIngredient48Edits(string value)
    { Ingredient48Edits = value; }
    internal string GetIngredient48Edits()
    { return Ingredient48Edits; }
    public string Ingredient48Edits
    {
        get { return ingredient48Edits; }
        set { SetProperty(ref ingredient48Edits, value); }
    }
    internal void SetIngredient49Edits(string value)
    { Ingredient49Edits = value; }
    internal string GetIngredient49Edits()
    { return Ingredient49Edits; }
    public string Ingredient49Edits
    {
        get { return ingredient49Edits; }
        set { SetProperty(ref ingredient49Edits, value); }
    }
    internal void SetIngredient50Edits(string value)
    { Ingredient50Edits = value; }
    internal string GetIngredient50Edits()
    { return Ingredient50Edits; }
    public string Ingredient50Edits
    {
        get { return ingredient50Edits; }
        set { SetProperty(ref ingredient50Edits, value); }
    }
    #endregion

    #region 30 Get/Set Properties for the DirectionsEdits

    //Directions - 30 Get/Set and accessor set for the Action delegates

    internal void SetDirection1Edits(string value)
    { Direction1Edits = value; }
    internal string GetDirection1Edits()
    { return Direction1Edits; }
    public string Direction1Edits
    {
        get { return direction1Edits; }
        set { SetProperty(ref direction1Edits, value); }
    }

    internal void SetDirection2Edits(string value)
    { Direction2Edits = value; }
    internal string GetDirection2Edits()
    { return Direction2Edits; }
    public string Direction2Edits
    {
        get { return direction2Edits; }
        set
        { SetProperty(ref direction2Edits, value); }
    }

    internal void SetDirection3Edits(string value)
    { Direction3Edits = value; }
    internal string GetDirection3Edits()
    { return Direction3Edits; }
    public string Direction3Edits
    {
        get { return direction3Edits; }
        set { SetProperty(ref direction3Edits, value); }
    }

    internal void SetDirection4Edits(string value)
    { Direction4Edits = value; }
    internal string GetDirection4Edits()
    { return Direction4Edits; }
    public string Direction4Edits
    {
        get { return direction4Edits; }
        set { SetProperty(ref direction4Edits, value); }
    }

    internal void SetDirection5Edits(string value)
    { Direction5Edits = value; }
    internal string GetDirection5Edits()
    { return Direction5Edits; }
    public string Direction5Edits
    {
        get { return direction5Edits; }
        set { SetProperty(ref direction5Edits, value); }
    }
    internal void SetDirection6Edits(string value)
    { Direction6Edits = value; }
    internal string GetDirection6Edits()
    { return Direction6Edits; }
    public string Direction6Edits
    {
        get { return direction6Edits; }
        set { SetProperty(ref direction6Edits, value); ; }
    }
    internal void SetDirection7Edits(string value)
    { Direction7Edits = value; }
    internal string GetDirection7Edits()
    { return Direction7Edits; }
    public string Direction7Edits
    {
        get { return direction7Edits; }
        set { SetProperty(ref direction7Edits, value); }
    }
    internal void SetDirection8Edits(string value)
    { Direction8Edits = value; }
    internal string GetDirection8Edits()
    { return Direction8Edits; }
    public string Direction8Edits
    {
        get { return direction8Edits; }
        set { SetProperty(ref direction8Edits, value); }
    }
    internal void SetDirection9Edits(string value)
    { Direction9Edits = value; }
    internal string GetDirection9Edits()
    { return Direction9Edits; }
    public string Direction9Edits
    {
        get { return direction9Edits; }
        set { SetProperty(ref direction9Edits, value); }
    }
    internal void SetDirection10Edits(string value)
    { Direction10Edits = value; }
    internal string GetDirection10Edits()
    { return Direction10Edits; }
    public string Direction10Edits
    {
        get { return direction10Edits; }
        set { SetProperty(ref direction10Edits, value); }
    }
    internal void SetDirection11Edits(string value)
    { Direction11Edits = value; }
    internal string GetDirection11Edits()
    { return Direction11Edits; }
    public string Direction11Edits
    {
        get { return direction11Edits; }
        set { SetProperty(ref direction11Edits, value); }
    }
    internal void SetDirection12Edits(string value)
    { Direction12Edits = value; }
    internal string GetDirection12Edits()
    { return Direction12Edits; }
    public string Direction12Edits
    {
        get { return direction12Edits; }
        set { SetProperty(ref direction12Edits, value); }
    }
    internal void SetDirection13Edits(string value)
    { Direction13Edits = value; }
    internal string GetDirection13Edits()
    { return Direction13Edits; }
    public string Direction13Edits
    {
        get { return direction13Edits; }
        set { SetProperty(ref direction13Edits, value); }
    }
    internal void SetDirection14Edits(string value)
    { Direction14Edits = value; }
    internal string GetDirection14Edits()
    { return Direction14Edits; }
    public string Direction14Edits
    {
        get { return direction14Edits; }
        set { SetProperty(ref direction14Edits, value); }
    }
    internal void SetDirection15Edits(string value)
    { Direction15Edits = value; }
    internal string GetDirection15Edits()
    { return Direction15Edits; }
    public string Direction15Edits
    {
        get { return direction15Edits; }
        set { SetProperty(ref direction15Edits, value); }
    }
    internal void SetDirection16Edits(string value)
    { Direction16Edits = value; }
    internal string GetDirection16Edits()
    { return Direction16Edits; }
    public string Direction16Edits
    {
        get { return direction16Edits; }
        set { SetProperty(ref direction16Edits, value); }
    }
    internal void SetDirection17Edits(string value)
    { Direction17Edits = value; }
    internal string GetDirection17Edits()
    { return Direction17Edits; }
    public string Direction17Edits
    {
        get { return direction17Edits; }
        set { SetProperty(ref direction17Edits, value); }
    }
    internal void SetDirection18Edits(string value)
    { Direction18Edits = value; }
    internal string GetDirection18Edits()
    { return Direction18Edits; }
    public string Direction18Edits
    {
        get { return direction18Edits; }
        set { SetProperty(ref direction18Edits, value); }
    }
    internal void SetDirection19Edits(string value)
    { Direction19Edits = value; }
    internal string GetDirection19Edits()
    { return Direction19Edits; }
    public string Direction19Edits
    {
        get { return direction19Edits; }
        set { SetProperty(ref direction19Edits, value); }
    }
    internal void SetDirection20Edits(string value)
    { Direction20Edits = value; }
    internal string GetDirection20Edits()
    { return Direction20Edits; }
    public string Direction20Edits
    {
        get { return direction20Edits; }
        set { SetProperty(ref direction20Edits, value); }
    }
    internal void SetDirection21Edits(string value)
    { Direction21Edits = value; }
    internal string GetDirection21Edits()
    { return Direction21Edits; }
    public string Direction21Edits
    {
        get { return direction21Edits; }
        set { SetProperty(ref direction21Edits, value); }
    }
    internal void SetDirection22Edits(string value)
    { Direction22Edits = value; }
    internal string GetDirection22Edits()
    { return Direction22Edits; }
    public string Direction22Edits
    {
        get { return direction22Edits; }
        set { SetProperty(ref direction22Edits, value); }
    }
    internal void SetDirection23Edits(string value)
    { Direction23Edits = value; }
    internal string GetDirection23Edits()
    { return Direction23Edits; }
    public string Direction23Edits
    {
        get { return direction23Edits; }
        set { SetProperty(ref direction23Edits, value); }
    }
    internal void SetDirection24Edits(string value)
    { Direction24Edits = value; }
    internal string GetDirection24Edits()
    { return Direction24Edits; }
    public string Direction24Edits
    {
        get { return direction24Edits; }
        set { SetProperty(ref direction24Edits, value); }
    }
    internal void SetDirection25Edits(string value)
    { Direction25Edits = value; }
    internal string GetDirection25Edits()
    { return Direction25Edits; }
    public string Direction25Edits
    {
        get { return direction25Edits; }
        set { SetProperty(ref direction25Edits, value); }
    }
    internal void SetDirection26Edits(string value)
    { Direction26Edits = value; }
    internal string GetDirection26Edits()
    { return Direction26Edits; }
    public string Direction26Edits
    {
        get { return direction26Edits; }
        set { SetProperty(ref direction26Edits, value); }
    }
    internal void SetDirection27Edits(string value)
    { Direction27Edits = value; }
    internal string GetDirection27Edits()
    { return Direction27Edits; }
    public string Direction27Edits
    {
        get { return direction27Edits; }
        set { SetProperty(ref direction27Edits, value); }
    }
    internal void SetDirection28Edits(string value)
    { Direction28Edits = value; }
    internal string GetDirection28Edits()
    { return Direction28Edits; }
    public string Direction28Edits
    {
        get { return direction28Edits; }
        set { SetProperty(ref direction28Edits, value); }
    }
    internal void SetDirection29Edits(string value)
    { Direction29Edits = value; }
    internal string GetDirection29Edits()
    { return Direction29Edits; }
    public string Direction29Edits
    {
        get { return direction29Edits; }
        set { SetProperty(ref direction29Edits, value); }
    }
    internal void SetDirection30Edits(string value)
    { Direction30Edits = value; }
    internal string GetDirection30Edits()
    { return Direction30Edits; }
    public string Direction30Edits
    {
        get { return direction30Edits; }
        set { SetProperty(ref direction30Edits, value); }
    }

    #endregion


    //public RecipeDisplayModel SelectViewMainRecipeCardModel
    //{
    //    get { return selectViewMainRecipeCardModel; }
    //    set { SetProperty(ref selectViewMainRecipeCardModel, value); }
    //}

}
}

