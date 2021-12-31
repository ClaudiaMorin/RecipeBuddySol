using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.Globalization;
using System.Windows.Input;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.Core.Models;
using Windows.UI.Xaml.Controls;
using RecipeBuddy.Core.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace RecipeBuddy.ViewModels
{
    public sealed class SelectedViewModel : ObservableObject
    {
        private static readonly SelectedViewModel instance = new SelectedViewModel();
        Action ActionNoParams;
        Func<bool> FuncBool;

        public RecipeDisplayModel selectViewMainRecipeCardModel;
        //public RecipeListModel listOfRecipeModel;

        public string QuantitySelectedAsString;
        public int QuantitySelectedAsInt;
        public List<string> IngredientQuantityShift;
        public List<Action<string>> listOfIngredientEditStringsSetters;
        public List<Action<string>> listOfDirectionEditStringsSetters;
        public List<Func<string>> listOfIngredientEditStringsGetters;
        public List<Func<string>> listOfDirectionEditStringsGetters;
        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        public Action<string> actionWithObject;
        private List<Action<string>> IngredHeightList;
        private List<Action<string>> DirectHeightList;

        static SelectedViewModel()
        { }

        public static SelectedViewModel Instance
        {
            get { return instance; }
        }

        private SelectedViewModel()
        {

            LoadIngredList("0");
            LoadDirectList("0");
            QuantitySelectedAsString = "1";
            QuantitySelectedAsInt = 1;
            currentType = 0;
            titleEditHeight = "0";
            titleEditString = "";
            listOfIngredientQuantitySetters = new List<Action<string>>();

            //SetUpComboBox();
            LoadListSettersWithActionDelegatesForIngredientQuantities();
            selectViewMainRecipeCardModel = new RecipeDisplayModel();
            //listOfRecipeModel = new RecipeListModel();
            IngredientQuantityShift = new List<string>();
            typeComboBoxVisibility = "Collapsed";
            //CmdRemove = new RBRelayCommand(ActionNoParams = () => RemoveRecipe(), FuncBool = () => CanSelect);
            CmdSave = new RBRelayCommand(ActionNoParams = () => SaveRecipe(), FuncBool = () => CanSelectSave);
            CmdNew = new RBRelayCommand(ActionNoParams = () => CreateNewRecipe(), FuncBool = () => CanSelectNew);
            CmdEdit = new RBRelayCommand(ActionNoParams = () => EditRecipe(), FuncBool = () => CanSelect);
            //CmdSelectedItemChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeRecipeFromComboBox(e), canCallActionFunc => CanSelect);
            CmdSelectedQuantityChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeQuantityFromComboBox(e), canCallActionFunc => CanSelect);

            CmdUpdate = new ICommandViewModel<string>(actionWithObject = s => Update(s), canCallActionFunc => CanSelectAlwaysTrue);
            CmdCancel = new ICommandViewModel<string>(actionWithObject = s => Cancel(s), canCallActionFunc => CanSelectAlwaysTrue);
            CmdLineEdit = new ICommandViewModel<string>(actionWithObject = s => LineEdit(s), canCallActionFunc => CanSelectAlwaysTrue);

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

        //public void RemoveRecipe()
        //{
        //    if (listOfRecipeModel.ListCount > 0 && listOfRecipeModel.CurrentCardIndex != -1)
        //    {
        //        listOfRecipeModel.Remove(listOfRecipeModel.CurrentCardIndex);

        //        if (listOfRecipeModel.CurrentCardIndex > 0)
        //        {
        //            listOfRecipeModel.CurrentCardIndex = listOfRecipeModel.CurrentCardIndex - 1;
        //            ShowSpecifiedEntry(listOfRecipeModel.CurrentCardIndex);
        //            //need to reset the starting index of the "borrow recipe" incase it was removed
        //            EditViewModel.Instance.IndexOfComboBoxItem = 0;
        //        }
        //        else if (listOfRecipeModel.ListCount > 0)
        //        {
        //            listOfRecipeModel.CurrentCardIndex = listOfRecipeModel.ListCount - 1;
        //            ShowSpecifiedEntry(listOfRecipeModel.CurrentCardIndex);
        //            //need to reset the starting index of the "borrow recipe" incase it was removed
        //            EditViewModel.Instance.IndexOfComboBoxItem = 0;
        //        }
        //        //the last element in the list has been removed so now we need to go back to blank screen
        //        else
        //        {
        //            selectViewMainRecipeCardModel.CopyRecipeDisplayModel(new RecipeDisplayModel());
        //            EmptyIngredientQuanityRow();
        //        }

        //        return;
        //    }
        //}

        /// <summary>
        /// For use when a user logs out of his/her account
        /// </summary>
        public void ResetViewModel()
        {
            //if (listOfRecipeModel.ListCount > 0)
            //    listOfRecipeModel.RemoveAll();

            selectViewMainRecipeCardModel.CopyRecipeDisplayModel(new RecipeDisplayModel());
            EmptyIngredientQuanityRow();
        }
        /// <summary>
        /// updates the display to the newly selected recipe and updates the list of Edit textboxes so that the 
        /// user can edit the ingredients and we can check it before it is submitted.
        /// </summary>
        /// <param name="recipeCardModel">RecipeCardModel</param>
        public void UpdateRecipeEntry(RecipeRecordModel recipeModel)
        {
            selectViewMainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(recipeModel);
            UpdateEditTextBoxes();
            CurrentType = (int)selectViewMainRecipeCardModel.RecipeType;
            UpdateQuantityCalc();
        }

        /// <summary>
        /// updates the display to the newly selected recipe and updates the list of Edit textboxes so that the 
        /// user can edit the ingredients and we can check it before it is submitted.
        /// </summary>
        /// <param name="recipeCardModel">RecipeCardTreeItem</param>
        public void UpdateRecipe(RecipeTreeItem recipeTreeItem)
        {
            UpdateRecipeEntry(recipeTreeItem.RecipeModelTV);
        }

        /// <summary>
        /// Saves the recipe to the DB and the TreeView
        /// </summary>
        public void SaveRecipe()
        {
            selectViewMainRecipeCardModel.SaveEditsToARecipe(selectViewMainRecipeCardModel.Title);
            int result = MainWindowViewModel.Instance.mainTreeViewNav.AddRecipeToTreeView(selectViewMainRecipeCardModel, true);

            if (result == 1)
            {
                MainNavTreeViewModel.Instance.RemoveRecipeFromTreeView(selectViewMainRecipeCardModel);
                DataBaseAccessorsForRecipeManager.DeleteRecipeFromDatabase(selectViewMainRecipeCardModel.Title, (int)selectViewMainRecipeCardModel.RecipeType, UserViewModel.Instance.UsersIDInDB);
            }
            if (result == 1 || result == 2)
            {
                DataBaseAccessorsForRecipeManager.SaveRecipeToDatabase(UserViewModel.Instance.UsersIDInDB, selectViewMainRecipeCardModel, UserViewModel.Instance.UsersIDInDB);
                //RemoveRecipe();
            }
        }

        /// <summary>
        /// ICommand backer for Edit Button on Select page.  Function adds the current recipe to the edit page and removes
        /// it from the MakeItView
        /// </summary>
        public void EditRecipe()
        {
            //EditViewModel.Instance.UpdateRecipe(selectViewMainRecipeCardModel);


            //Check to see if this if from the TreeView or from a generic search
            MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.EditTab;
            //RemoveRecipe();
        }

        /// <summary>
        /// ICommand backer for Create New Button on Select page.  Creates an new recipe and takes the user to the edit panel
        /// </summary>
        public void CreateNewRecipe()
        {
            EditViewModel.Instance.CreateNewRecipe();

            //Check to see if this if from the TreeView or from a generic search
            MainWindowViewModel.Instance.SelectedTabIndex = (int)MainWindowViewModel.Tabs.EditTab;
        }

        /// <summary>
        /// sets up the first entry in the dropdown list and the initial index.
        /// </summary>
        //private void SetUpComboBox()
        //{
        //    indexOfComboBoxItem = 0;
        //    listOfRecipeModel = new RecipeListModel();
        //}

        //public void AddToListOfRecipeCards(RecipeRecordModel recipeCard)
        //{
        //    if (listOfRecipeModel.IsFoundInList(recipeCard) == true)
        //    {
        //        int index = listOfRecipeModel.GetEntryIndex(recipeCard.Title);
        //        ShowSpecifiedEntry(index);
        //        return; 
        //    }

        //    listOfRecipeModel.Add(recipeCard);

        //    //If we have nothing in the list we will show the first entry
        //    if (listOfRecipeModel.ListCount == 1)
        //    {
        //        listOfRecipeModel.CurrentCardIndex = 0;
        //        EditViewModel.Instance.IndexOfComboBoxItem = 0;
        //    }
        //    else
        //    {
        //        listOfRecipeModel.CurrentCardIndex = listOfRecipeModel.ListCount - 1;
        //    }
        //    ShowSpecifiedEntry(listOfRecipeModel.CurrentCardIndex);

        //}

        /// <summary>
        /// For use with the ComboBox navigation
        /// </summary>
        /// <param name="title">title of the recipe we are looking for</param>
        //public void ShowSpecifiedEntry(string title)
        //{
        //    ShowSpecifiedEntry(listOfRecipeModel.GetEntryIndex(title));
        //}

        /// <summary>
        /// For use with the ComboBox navigation
        /// </summary>
        /// <param name="index"></param>
        public void ShowSpecifiedEntry(RecipeRecordModel recipeRecordModel)
        {
            selectViewMainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(recipeRecordModel);

            //if (index < listOfRecipeModel.ListCount && index > -1)
            //{
            //    listOfRecipeModel.CurrentCardIndex = index;
            //    selectViewMainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecord(listOfRecipeModel.GetEntry(index));
            //    ChangeRecipe(listOfRecipeModel.CurrentCardIndex);
            //    NumXRecipesIndex = "0";
            //    IndexOfComboBoxItem = index;
            //    UpdateQuantityCalc();
            //}
        }

        /// <summary>
        /// Updates the current card display and updates the edit-textboxes for the ingredient and direction editing
        /// </summary>
        public void UpdateEditTextBoxes()
        {

            for (int count = 0; count < selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay.Count; count++)
            {
                listOfIngredientEditStringsSetters[count].Invoke(selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[count]);
            }

            for (int countDirect = 0; countDirect < selectViewMainRecipeCardModel.listOfDirectionStringsForDisplay.Count; countDirect++)
            {
                listOfDirectionEditStringsSetters[countDirect].Invoke(selectViewMainRecipeCardModel.listOfDirectionStringsForDisplay[countDirect]);
            }
        }

        /// <summary>
        /// Called by the RecipiesInComboBox_SelectionChanged function to shift the listOfRecipeCards and update the entry
        /// </summary>
        /// <param name="TitleOfRecipe">Title of the new recipe</param>
        //public void ChangeRecipe(string TitleOfRecipe)
        //{
        //    int index = listOfRecipeModel.SettingCurrentIndexByTitle(TitleOfRecipe);
        //    if (index != -1)
        //    {
        //        ChangeRecipe(index);
        //    }
        //}

        /// <summary>
        /// Getting the new Quantity in string form 
        /// </summary>
        /// <param name="ingredient"the original amount of the ingredient></param>
        /// <returns>The multiplied quality in string form</returns>
        private string CreateQuantityString(string ingredient)
        {
            string ingredQuantity;

            string measureType = "";
            bool resultsTest;
            float quantityAsfloat;
            int quantityAsInt;

            //Check if the ingredient string is valid
            if (string.IsNullOrEmpty(ingredient) == true || string.IsNullOrWhiteSpace(ingredient) == true)
                return "";
            else
            {
                if (ingredient.Trim().ToString()[0] == '-' || ingredient.Trim().ToString()[0] == '!')
                    return "";
            }

            try
            {
                //Remove quantity from the ingredient string!
                ingredQuantity = ingredient.ToString().Substring(0, ingredient.ToString().IndexOf(' ')).Trim();
                ingredient = ingredient.Substring(ingredient.ToString().IndexOf(' ') + 1).Trim();

                //**************************Check for writen numbers which sometimes happens!
                string converted = (StringManipulationHelper.TryToConvertWritenNumbersToDigits(ingredQuantity));
                if (converted.Length > 0)
                {
                    resultsTest = Int32.TryParse(converted, out quantityAsInt);
                    //We have a regular int here,  so get the measure type and multiply
                    //remove the measureType "word" which should be next.
                    measureType = ingredient.Trim().ToString().Substring(0, ingredient.ToString().IndexOf(' ') + 1);

                    if (resultsTest == true)
                        return StringManipulationHelper.FinalQuantityString((quantityAsInt * QuantitySelectedAsInt), measureType);
                }

                //**************************** Nope.  Check for a vulger fraction and convert ************************

                if (ingredQuantity.Contains("/") == true || ingredQuantity.Contains("½") == true || ingredQuantity.Contains("¼") == true || ingredQuantity.Contains("¾") == true || ingredQuantity.Contains("⅓") == true || ingredQuantity.Contains("⅔") == true)
                {
                    //We know we have a fraction someplace, we just need to figure out where? It will either be at the beginging of the string or at the end
                    //Fraction is at the start of the string so there isn't anything else to worry about.
                    if (ingredQuantity[0].ToString().Contains("/") == true || ingredQuantity[0].ToString().Contains("½") == true || ingredQuantity[0].ToString().Contains("¼") == true || ingredQuantity[0].ToString().Contains("¾") == true || ingredQuantity[0].ToString().Contains("⅓") == true || ingredQuantity[0].ToString().Contains("⅔") == true)
                    {
                        ingredQuantity = StringManipulationHelper.ConvertVulgarFaction(ingredQuantity);
                    }
                    else //The fraction is at the end of the number so we need to add them up after they fraction is converted to a float!
                    {
                        if (ingredQuantity[ingredQuantity.Length - 1].ToString().Contains("/") == true || ingredQuantity[ingredQuantity.Length - 1].ToString().Contains("½") == true || ingredQuantity[ingredQuantity.Length - 1].ToString().Contains("¼") == true || ingredQuantity[ingredQuantity.Length - 1].ToString().Contains("¾") == true || ingredQuantity[ingredQuantity.Length - 1].ToString().Contains("⅓") == true || ingredQuantity[ingredQuantity.Length - 1].ToString().Contains("⅔") == true)
                        {
                            string temp1 = StringManipulationHelper.ConvertVulgarFaction(ingredQuantity[ingredQuantity.Length - 1].ToString());
                            string temp2 = ingredQuantity.Substring(0, ingredQuantity.Length - 1);
                            ingredQuantity = temp2 + temp1;
                        }
                    }
                }

                //remove the measureType "word" which should be next.
                measureType = ingredient.Trim().ToString().Substring(0, ingredient.ToString().IndexOf(' ') + 1);
                resultsTest = Int32.TryParse(ingredQuantity, out quantityAsInt);

                //We have a regular int here, now multiply.
                if (resultsTest == true)
                    return StringManipulationHelper.FinalQuantityString((quantityAsInt * QuantitySelectedAsInt), measureType);


                //************************************ Nope. Maybe a float? Convert and return! ************************************
                resultsTest = float.TryParse(ingredQuantity, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out quantityAsfloat);

                if (resultsTest == true)
                {
                    quantityAsfloat = quantityAsfloat * QuantitySelectedAsInt;
                    return StringManipulationHelper.ConvertFloatToVulgarFaction(quantityAsfloat.ToString()) + " " + StringManipulationHelper.CheckMeasureString(measureType);
                }
            }
            catch (Exception e)
            { }

            //************************************ Can't figure it out!
            return "";
        }

        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Basket page
        /// </summary>
        /// <param name="e"></param>
        //internal void ChangeRecipeFromComboBox(SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems != null && e.AddedItems.Count > 0)
        //    {
        //        RecipeDisplayModel recipeCardModelFromChangedEventArgs = e.AddedItems[0] as RecipeDisplayModel;

        //        if (recipeCardModelFromChangedEventArgs != null)
        //        {
        //            if (listOfRecipeModel.SettingCurrentIndexByTitle(recipeCardModelFromChangedEventArgs.Title) != -1)
        //            {
        //                RecipeRecordModel recipeModel = listOfRecipeModel.GetCurrentEntry();
        //                NumXRecipesIndex = "0";
        //                UpdateRecipeEntry(recipeModel);
        //            }
        //        }
        //    }
        //    //need to make this a sub to the first if statment because adding a new item to the listbox
        //    //removes the other which doesn't actually happen but the EventArgs still sends it as e.RemoveItems[0]
        //    else
        //    {
        //        if (e.RemovedItems != null && e.RemovedItems.Count > 0)
        //        {
        //            RecipeRecordModel recipeModel = e.RemovedItems[0] as RecipeRecordModel;

        //            if (recipeModel != null)
        //            {
        //                ChangeRecipe(recipeModel.Title);
        //                //recipeModel.UpdateRecipeEntry(recipeModel);
        //                NumXRecipesIndex = "0";
        //                UpdateQuantityCalc();
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Responds to the changes in the multiplier ComboBox by getting the value from the ingredient string
        /// Takes the default multiplier which is 1 unless it is changed by the user
        /// </summary>
        /// <param name="multiplierAsString">The multiplier that is passed from the combobox</param>
        private void UpdateQuantityCalc()
        {
            //remove old values if there are any left in the List or the UI
            IngredientQuantityShift = new List<string>();
            EmptyIngredientQuanityRow();

            //Multiply the ingredient quantity and limit us to 50.
            for (int i = 0; i < selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay.Count && i < 50; i++)
            {
                string s1 = selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[i];
                IngredientQuantityShift.Add(CreateQuantityString(s1));
            }

            //filling the ingredientQuantiy properties
            for (int IngredientCount = 0; IngredientCount < IngredientQuantityShift.Count; IngredientCount++)
            {
                listOfIngredientQuantitySetters[IngredientCount].Invoke(IngredientQuantityShift[IngredientCount]);
            }
        }

        /// <summary>
        /// This resets the IngredientQuanityRow to empty
        /// </summary>
        internal void EmptyIngredientQuanityRow()
        {
            for (int IngredientCount = 0; IngredientCount < 50; IngredientCount++)
            {
                listOfIngredientQuantitySetters[IngredientCount].Invoke("");
            }
        }

        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Selected page
        /// </summary>
        /// <param name="e"></param>
        private void ChangeQuantityFromComboBox(SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)e.AddedItems[0];
            QuantitySelectedAsString = comboBoxItem.Content.ToString().Trim().Substring(0, 1);
            //get the multipler and part it to an int
            bool success = Int32.TryParse(QuantitySelectedAsString, out QuantitySelectedAsInt);
            UpdateQuantityCalc();
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
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "direction") == 0)
            {
                int results;
                bool success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                DirectHeightList[results - 1].Invoke("Auto");
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "title") == 0)
            {
                int results;
                bool success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }

                TitleEditHeight = "Auto";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void Update(object sender)
        {
            int results;
            bool success;
            string[] parameters = sender.ToString().Split(',');

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "ingredient") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }

                IngredHeightList[results - 1].Invoke("0");
                selectViewMainRecipeCardModel.listOfIngredientSetters[results - 1].Invoke(listOfIngredientEditStringsGetters[results - 1].Invoke());
                selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[results - 1] = listOfIngredientEditStringsGetters[results - 1].Invoke();
                listOfIngredientQuantitySetters[results - 1].Invoke(CreateQuantityString(selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[results - 1]));
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "direction") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }

                DirectHeightList[results - 1].Invoke("0");
                selectViewMainRecipeCardModel.listOfDirectionSetters[results - 1].Invoke(listOfDirectionEditStringsGetters[results - 1].Invoke());
                selectViewMainRecipeCardModel.listOfDirectionStringsForDisplay[results - 1] = listOfDirectionEditStringsGetters[results - 1].Invoke();
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "title") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }

                TitleEditHeight = "0";
                MainNavTreeViewModel.Instance.ChangedTreeItemTitle(selectViewMainRecipeCardModel.Title, TitleEditString, selectViewMainRecipeCardModel.RecipeType);
                selectViewMainRecipeCardModel.Title = TitleEditString;
            }
        }

        private void Cancel(object sender)
        {
            int results;
            bool success;
            string[] parameters = sender.ToString().Split(',');

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "ingredient") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                IngredHeightList[results - 1].Invoke("0");
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "direction") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                DirectHeightList[results - 1].Invoke("0");
            }

            if (string.Compare(parameters[1].ToString().ToLower().Trim(), "title") == 0)
            {
                success = Int32.TryParse(parameters[0], out results);
                //messed up someplace
                if (success == false)
                { return; }
                TitleEditHeight = "0";
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

        /// <summary>
        /// can't select save until the user is logged in
        /// because there is no access to the DB
        /// </summary>
        private bool canSelectSave;
        public bool CanSelectSave
        {
            set { canSelectSave = value; }
            get
            {
                if (UserViewModel.Instance.CanSelectLogout == true && selectViewMainRecipeCardModel.Title.Length > 0 &&
                     string.Compare(selectViewMainRecipeCardModel.Title.ToLower(), "search for your next recipe find!") != 0)
                {
                    canSelectSave = true;
                }
                else
                {
                    canSelectSave = false;
                }

                CmdSave.RaiseCanExecuteChanged();
                return canSelectSave;
            }
        }

        /// <summary>
        /// can't select save until the user is logged in
        /// because there is no access to the DB
        /// </summary>
        public bool CanSelectNew
        {
            get
            {
                return UserViewModel.Instance.CanSelectLogout;
            }
        }

        /// <summary>
        /// property for the Save button command
        /// </summary>
        public RBRelayCommand CmdSave
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Edit button command
        /// </summary>
        public RBRelayCommand CmdEdit
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Edit button command
        /// </summary>
        public RBRelayCommand CmdNew
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Remove button command
        /// </summary>
        public RBRelayCommand CmdRemove
        {
            get;
            private set;
        }
        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public ICommand CmdSelectedItemChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// property for the Quantity combobox change command
        /// </summary>
        public ICommand CmdSelectedQuantityChanged
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

        private int comboBoxIndexForRecipeMultiplier;
        public int ComboBoxIndexForRecipeMultiplier
        {
            get { return comboBoxIndexForRecipeMultiplier; }
            set { SetProperty(ref comboBoxIndexForRecipeMultiplier, value); }
        }

        private int currentType;
        public int CurrentType
        {
            get { return currentType; }
            set { SetProperty(ref currentType, value); }
        }

        private string typeComboBoxVisibility;
        public string TypeComboBoxVisibility
        {
            get { return typeComboBoxVisibility; }
            set { SetProperty(ref typeComboBoxVisibility, value); }
        }

        private string numXRecipesIndex;
        public string NumXRecipesIndex
        {
            get { return numXRecipesIndex; }
            set { SetProperty(ref numXRecipesIndex, value); }
        }

        private string titleEditHeight;
        public string TitleEditHeight
        {
            get { return titleEditHeight; }
            set { SetProperty(ref titleEditHeight, value); }
        }

        private string titleEditString;
        public string TitleEditString
        {
            get { return titleEditString; }
            set { SetProperty(ref titleEditString, value); }
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

        #region loading the listOfIngredientSetters with Action delegates
        private void LoadListSettersWithActionDelegatesForIngredientQuantities()
        {
            listOfIngredientQuantitySetters.Add(setIngredient1); listOfIngredientQuantitySetters.Add(setIngredient2); listOfIngredientQuantitySetters.Add(setIngredient3); listOfIngredientQuantitySetters.Add(setIngredient4); listOfIngredientQuantitySetters.Add(setIngredient5);
            listOfIngredientQuantitySetters.Add(setIngredient6); listOfIngredientQuantitySetters.Add(setIngredient7); listOfIngredientQuantitySetters.Add(setIngredient8); listOfIngredientQuantitySetters.Add(setIngredient9); listOfIngredientQuantitySetters.Add(setIngredient10);
            listOfIngredientQuantitySetters.Add(setIngredient11); listOfIngredientQuantitySetters.Add(setIngredient12); listOfIngredientQuantitySetters.Add(setIngredient13); listOfIngredientQuantitySetters.Add(setIngredient14); listOfIngredientQuantitySetters.Add(setIngredient15);
            listOfIngredientQuantitySetters.Add(setIngredient16); listOfIngredientQuantitySetters.Add(setIngredient17); listOfIngredientQuantitySetters.Add(setIngredient18); listOfIngredientQuantitySetters.Add(setIngredient19); listOfIngredientQuantitySetters.Add(setIngredient20);
            listOfIngredientQuantitySetters.Add(setIngredient21); listOfIngredientQuantitySetters.Add(setIngredient22); listOfIngredientQuantitySetters.Add(setIngredient23); listOfIngredientQuantitySetters.Add(setIngredient24); listOfIngredientQuantitySetters.Add(setIngredient25);
            listOfIngredientQuantitySetters.Add(setIngredient26); listOfIngredientQuantitySetters.Add(setIngredient27); listOfIngredientQuantitySetters.Add(setIngredient28); listOfIngredientQuantitySetters.Add(setIngredient29); listOfIngredientQuantitySetters.Add(setIngredient30);
            listOfIngredientQuantitySetters.Add(setIngredient31); listOfIngredientQuantitySetters.Add(setIngredient32); listOfIngredientQuantitySetters.Add(setIngredient33); listOfIngredientQuantitySetters.Add(setIngredient34); listOfIngredientQuantitySetters.Add(setIngredient35);
            listOfIngredientQuantitySetters.Add(setIngredient36); listOfIngredientQuantitySetters.Add(setIngredient37); listOfIngredientQuantitySetters.Add(setIngredient38); listOfIngredientQuantitySetters.Add(setIngredient39); listOfIngredientQuantitySetters.Add(setIngredient40);
            listOfIngredientQuantitySetters.Add(setIngredient41); listOfIngredientQuantitySetters.Add(setIngredient42); listOfIngredientQuantitySetters.Add(setIngredient43); listOfIngredientQuantitySetters.Add(setIngredient44); listOfIngredientQuantitySetters.Add(setIngredient45);
            listOfIngredientQuantitySetters.Add(setIngredient46); listOfIngredientQuantitySetters.Add(setIngredient47); listOfIngredientQuantitySetters.Add(setIngredient48); listOfIngredientQuantitySetters.Add(setIngredient49); listOfIngredientQuantitySetters.Add(setIngredient50);
        }
        #endregion

        #region the private strings for ingredientsQuantity- 50


        private string ingredientQuantity1 = ""; private string ingredientQuantity2 = ""; private string ingredientQuantity3 = ""; private string ingredientQuantity4 = ""; private string ingredientQuantity5 = ""; private string ingredientQuantity6 = ""; private string ingredientQuantity7 = ""; private string ingredientQuantity8 = ""; private string ingredientQuantity9 = ""; private string ingredientQuantity10 = "";
        private string ingredientQuantity11 = ""; private string ingredientQuantity12 = ""; private string ingredientQuantity13 = ""; private string ingredientQuantity14 = ""; private string ingredientQuantity15 = ""; private string ingredientQuantity16 = ""; private string ingredientQuantity17 = ""; private string ingredientQuantity18 = ""; private string ingredientQuantity19 = ""; private string ingredientQuantity20 = "";
        private string ingredientQuantity21 = ""; private string ingredientQuantity22 = ""; private string ingredientQuantity23 = ""; private string ingredientQuantity24 = ""; private string ingredientQuantity25 = ""; private string ingredientQuantity26 = ""; private string ingredientQuantity27 = ""; private string ingredientQuantity28 = ""; private string ingredientQuantity29 = ""; private string ingredientQuantity30 = "";
        private string ingredientQuantity31 = ""; private string ingredientQuantity32 = ""; private string ingredientQuantity33 = ""; private string ingredientQuantity34 = ""; private string ingredientQuantity35 = ""; private string ingredientQuantity36 = ""; private string ingredientQuantity37 = ""; private string ingredientQuantity38 = ""; private string ingredientQuantity39 = ""; private string ingredientQuantity40 = "";
        private string ingredientQuantity41 = ""; private string ingredientQuantity42 = ""; private string ingredientQuantity43 = ""; private string ingredientQuantity44 = ""; private string ingredientQuantity45 = ""; private string ingredientQuantity46 = ""; private string ingredientQuantity47 = ""; private string ingredientQuantity48 = ""; private string ingredientQuantity49 = ""; private string ingredientQuantity50 = "";
        #endregion

        /// <summary>
        /// The List of Action delegates that wrap the property-setter for Ingredients
        /// </summary>
        public List<Action<string>> listOfIngredientQuantitySetters;

        #region List of 50 Ingredient setters used by the List of Action<string> listOfIngredientSetters

        private void setIngredient1(string value)
        { IngredientQuantity1 = value; }

        private void setIngredient2(string value)
        { IngredientQuantity2 = value; }

        private void setIngredient3(string value)
        { IngredientQuantity3 = value; }

        private void setIngredient4(string value)
        { IngredientQuantity4 = value; }

        private void setIngredient5(string value)
        { IngredientQuantity5 = value; }

        private void setIngredient6(string value)
        { IngredientQuantity6 = value; }

        private void setIngredient7(string value)
        { IngredientQuantity7 = value; }

        private void setIngredient8(string value)
        { IngredientQuantity8 = value; }

        private void setIngredient9(string value)
        { IngredientQuantity9 = value; }

        private void setIngredient10(string value)
        { IngredientQuantity10 = value; }

        private void setIngredient11(string value)
        { IngredientQuantity11 = value; }

        private void setIngredient12(string value)
        { IngredientQuantity12 = value; }

        private void setIngredient13(string value)
        { IngredientQuantity13 = value; }

        private void setIngredient14(string value)
        { IngredientQuantity14 = value; }

        private void setIngredient15(string value)
        { IngredientQuantity15 = value; }

        private void setIngredient16(string value)
        { IngredientQuantity16 = value; }

        private void setIngredient17(string value)
        { IngredientQuantity17 = value; }

        private void setIngredient18(string value)
        { IngredientQuantity18 = value; }

        private void setIngredient19(string value)
        { IngredientQuantity19 = value; }

        private void setIngredient20(string value)
        { IngredientQuantity20 = value; }

        private void setIngredient21(string value)
        { IngredientQuantity21 = value; }

        private void setIngredient22(string value)
        { IngredientQuantity22 = value; }

        private void setIngredient23(string value)
        { IngredientQuantity23 = value; }

        private void setIngredient24(string value)
        { IngredientQuantity24 = value; }

        private void setIngredient25(string value)
        { IngredientQuantity25 = value; }

        private void setIngredient26(string value)
        { IngredientQuantity26 = value; }

        private void setIngredient27(string value)
        { IngredientQuantity27 = value; }

        private void setIngredient28(string value)
        { IngredientQuantity28 = value; }

        private void setIngredient29(string value)
        { IngredientQuantity29 = value; }

        private void setIngredient30(string value)
        { IngredientQuantity30 = value; }

        private void setIngredient31(string value)
        { IngredientQuantity31 = value; }

        private void setIngredient32(string value)
        { IngredientQuantity32 = value; }

        private void setIngredient33(string value)
        { IngredientQuantity33 = value; }

        private void setIngredient34(string value)
        { IngredientQuantity34 = value; }

        private void setIngredient35(string value)
        { IngredientQuantity35 = value; }

        private void setIngredient36(string value)
        { IngredientQuantity36 = value; }

        private void setIngredient37(string value)
        { IngredientQuantity37 = value; }

        private void setIngredient38(string value)
        { IngredientQuantity38 = value; }

        private void setIngredient39(string value)
        { IngredientQuantity39 = value; }

        private void setIngredient40(string value)
        { IngredientQuantity40 = value; }

        private void setIngredient41(string value)
        { IngredientQuantity41 = value; }

        private void setIngredient42(string value)
        { IngredientQuantity42 = value; }

        private void setIngredient43(string value)
        { IngredientQuantity43 = value; }

        private void setIngredient44(string value)
        { IngredientQuantity44 = value; }

        private void setIngredient45(string value)
        { IngredientQuantity45 = value; }

        private void setIngredient46(string value)
        { IngredientQuantity46 = value; }

        private void setIngredient47(string value)
        { IngredientQuantity47 = value; }

        private void setIngredient48(string value)
        { IngredientQuantity48 = value; }

        private void setIngredient49(string value)
        { IngredientQuantity49 = value; }

        private void setIngredient50(string value)
        { IngredientQuantity50 = value; }

        #endregion

        #region setting up all of the IngredientQuantity Properties
        public string IngredientQuantity1
        {
            get { return ingredientQuantity1; }
            set { SetProperty(ref ingredientQuantity1, value); }
        }

        public string IngredientQuantity2
        {
            get { return ingredientQuantity2; }
            set { SetProperty(ref ingredientQuantity2, value); }
        }

        public string IngredientQuantity3
        {
            get { return ingredientQuantity3; }
            set { SetProperty(ref ingredientQuantity3, value); }
        }

        public string IngredientQuantity4
        {
            get { return ingredientQuantity4; }
            set { SetProperty(ref ingredientQuantity4, value); }
        }

        public string IngredientQuantity5
        {
            get { return ingredientQuantity5; }
            set { SetProperty(ref ingredientQuantity5, value); }
        }

        public string IngredientQuantity6
        {
            get { return ingredientQuantity6; }
            set { SetProperty(ref ingredientQuantity6, value); }
        }

        public string IngredientQuantity7
        {
            get { return ingredientQuantity7; }
            set { SetProperty(ref ingredientQuantity7, value); }
        }

        public string IngredientQuantity8
        {
            get { return ingredientQuantity8; }
            set { SetProperty(ref ingredientQuantity8, value); }
        }

        public string IngredientQuantity9
        {
            get { return ingredientQuantity9; }
            set { SetProperty(ref ingredientQuantity9, value); }
        }

        public string IngredientQuantity10
        {
            get { return ingredientQuantity10; }
            set { SetProperty(ref ingredientQuantity10, value); }
        }

        public string IngredientQuantity11
        {
            get { return ingredientQuantity11; }
            set { SetProperty(ref ingredientQuantity11, value); }
        }

        public string IngredientQuantity12
        {
            get { return ingredientQuantity12; }
            set { SetProperty(ref ingredientQuantity12, value); }
        }

        public string IngredientQuantity13
        {
            get { return ingredientQuantity13; }
            set { SetProperty(ref ingredientQuantity13, value); }
        }

        public string IngredientQuantity14
        {
            get { return ingredientQuantity14; }
            set { SetProperty(ref ingredientQuantity14, value); }
        }

        public string IngredientQuantity15
        {
            get { return ingredientQuantity15; }
            set { SetProperty(ref ingredientQuantity15, value); }
        }

        public string IngredientQuantity16
        {
            get { return ingredientQuantity16; }
            set { SetProperty(ref ingredientQuantity16, value); }
        }

        public string IngredientQuantity17
        {
            get { return ingredientQuantity17; }
            set { SetProperty(ref ingredientQuantity17, value); }
        }

        public string IngredientQuantity18
        {
            get { return ingredientQuantity18; }
            set { SetProperty(ref ingredientQuantity18, value); }
        }

        public string IngredientQuantity19
        {
            get { return ingredientQuantity19; }
            set { SetProperty(ref ingredientQuantity19, value); }
        }

        public string IngredientQuantity20
        {
            get { return ingredientQuantity20; }
            set { SetProperty(ref ingredientQuantity20, value); }
        }

        public string IngredientQuantity21
        {
            get { return ingredientQuantity21; }
            set { SetProperty(ref ingredientQuantity21, value); }
        }

        public string IngredientQuantity22
        {
            get { return ingredientQuantity22; }
            set { SetProperty(ref ingredientQuantity22, value); }
        }

        public string IngredientQuantity23
        {
            get { return ingredientQuantity23; }
            set { SetProperty(ref ingredientQuantity23, value); }
        }

        public string IngredientQuantity24
        {
            get { return ingredientQuantity24; }
            set { SetProperty(ref ingredientQuantity24, value); }
        }

        public string IngredientQuantity25
        {
            get { return ingredientQuantity25; }
            set { SetProperty(ref ingredientQuantity25, value); }
        }

        public string IngredientQuantity26
        {
            get { return ingredientQuantity26; }
            set { SetProperty(ref ingredientQuantity26, value); }
        }

        public string IngredientQuantity27
        {
            get { return ingredientQuantity27; }
            set { SetProperty(ref ingredientQuantity27, value); }
        }

        public string IngredientQuantity28
        {
            get { return ingredientQuantity28; }
            set { SetProperty(ref ingredientQuantity28, value); }
        }

        public string IngredientQuantity29
        {
            get { return ingredientQuantity29; }
            set { SetProperty(ref ingredientQuantity29, value); }
        }

        public string IngredientQuantity30
        {
            get { return ingredientQuantity30; }
            set { SetProperty(ref ingredientQuantity30, value); }
        }

        public string IngredientQuantity31
        {
            get { return ingredientQuantity31; }
            set { SetProperty(ref ingredientQuantity31, value); }
        }

        public string IngredientQuantity32
        {
            get { return ingredientQuantity32; }
            set { SetProperty(ref ingredientQuantity32, value); }
        }

        public string IngredientQuantity33
        {
            get { return ingredientQuantity33; }
            set { SetProperty(ref ingredientQuantity33, value); }
        }

        public string IngredientQuantity34
        {
            get { return ingredientQuantity34; }
            set { SetProperty(ref ingredientQuantity34, value); }
        }

        public string IngredientQuantity35
        {
            get { return ingredientQuantity35; }
            set { SetProperty(ref ingredientQuantity35, value); }
        }

        public string IngredientQuantity36
        {
            get { return ingredientQuantity36; }
            set { SetProperty(ref ingredientQuantity36, value); }
        }

        public string IngredientQuantity37
        {
            get { return ingredientQuantity37; }
            set { SetProperty(ref ingredientQuantity37, value); }
        }

        public string IngredientQuantity38
        {
            get { return ingredientQuantity38; }
            set { SetProperty(ref ingredientQuantity38, value); }
        }

        public string IngredientQuantity39
        {
            get { return ingredientQuantity39; }
            set { SetProperty(ref ingredientQuantity39, value); }
        }

        public string IngredientQuantity40
        {
            get { return ingredientQuantity40; }
            set { SetProperty(ref ingredientQuantity40, value); }
        }

        public string IngredientQuantity41
        {
            get { return ingredientQuantity41; }
            set { SetProperty(ref ingredientQuantity41, value); }
        }

        public string IngredientQuantity42
        {
            get { return ingredientQuantity42; }
            set { SetProperty(ref ingredientQuantity42, value); }
        }

        public string IngredientQuantity43
        {
            get { return ingredientQuantity43; }
            set { SetProperty(ref ingredientQuantity43, value); }
        }

        public string IngredientQuantity44
        {
            get { return ingredientQuantity44; }
            set { SetProperty(ref ingredientQuantity44, value); }
        }

        public string IngredientQuantity45
        {
            get { return ingredientQuantity45; }
            set { SetProperty(ref ingredientQuantity45, value); }
        }

        public string IngredientQuantity46
        {
            get { return ingredientQuantity46; }
            set { SetProperty(ref ingredientQuantity46, value); }
        }

        public string IngredientQuantity47
        {
            get { return ingredientQuantity47; }
            set { SetProperty(ref ingredientQuantity47, value); }
        }

        public string IngredientQuantity48
        {
            get { return ingredientQuantity48; }
            set { SetProperty(ref ingredientQuantity48, value); }
        }

        public string IngredientQuantity49
        {
            get { return ingredientQuantity49; }
            set { SetProperty(ref ingredientQuantity49, value); }
        }

        public string IngredientQuantity50
        {
            get { return ingredientQuantity50; }
            set { SetProperty(ref ingredientQuantity50, value); }
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


        public RecipeDisplayModel SelectViewMainRecipeCardModel
        {
            get { return selectViewMainRecipeCardModel; }
            set { SetProperty(ref selectViewMainRecipeCardModel, value); }
        }
    }
}
