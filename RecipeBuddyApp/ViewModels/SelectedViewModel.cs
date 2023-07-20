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
using System.Linq;
using Microsoft.Xaml.Interactivity;

namespace RecipeBuddy.ViewModels
{
    public sealed class SelectedViewModel : ObservableObject
    {
        private static readonly SelectedViewModel instance = new SelectedViewModel();
        Action ActionNoParams;
        Action<SelectionChangedEventArgs> actionWithEventArgs;
        Func<bool> FuncBool;


        public string QuantitySelectedAsString;
        public int QuantitySelectedAsInt;
        public List<string> IngredientQuantityShift;

        static SelectedViewModel()
        { }

        public static SelectedViewModel Instance
        {
            get { return instance; }
        }

        private SelectedViewModel()
        {
            QuantitySelectedAsString = "1";
            QuantitySelectedAsInt = 1;

            noLoadedRecipeHeight = "Auto";
            loadedRecipeHeight = "0";
            canSelectCopy = true;
            canSelectSave = false;

            listOfIngredientQuantitySetters = new List<Action<string>>();
            LoadListSettersWithActionDelegatesForIngredientQuantities();

            IngredientQuantityShift = new List<string>();

            CmdSelectedQuantityChanged = new RelayCommand<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeQuantityFromComboBox(e), canCallActionFunc => CanSelect);
            //CmdCopy = new RelayCommand(ActionNoParams = () => Copy());
            CmdCopy = new RelayCommandRaiseCanExecute(ActionNoParams = () => Copy(), FuncBool = () => canSelectCopy);
            CmdSave = new RelayCommandRaiseCanExecute(ActionNoParams = () => SaveRecipeEdits(), FuncBool = () => canSelectSave);
        }


        /// <summary>
        /// For use when a user logs out of his/her account
        /// </summary>
        public void ResetViewModel()
        {
            NoLoadedRecipeHeight = "50";
            LoadedRecipeHeight = "0";
        }

        /// <summary>
        /// updates the display to the newly selected recipe and updates the list of Edit textboxes so that the 
        /// user can edit the ingredients and we can check it before it is submitted.
        /// </summary>
        /// <param name="recipeCardModel">RecipeCardModel</param>
        public void UpdateRecipeEntry(RecipeRecordModel recipeModel)
        {
            NoLoadedRecipeHeight = "0";
            LoadedRecipeHeight = "Auto";

            RecipeEditsViewModel.Instance.LoadRecipeEntry(recipeModel);
            
            UpdateQuantityCalc();

            //CanSelectSave = canSelectSave;
        }

        private void ChangeQuantityFromComboBox(SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)e.AddedItems[0];
            QuantitySelectedAsString = comboBoxItem.Content.ToString().Trim().Substring(0, 1);
            //get the multipler and parse it to an int
            bool success = Int32.TryParse(QuantitySelectedAsString, out QuantitySelectedAsInt);
            
            UpdateQuantityCalc();
        }
        /// <summary>
        /// Saves the recipe to the DB and the TreeView if the recipe doesn't already exist - as the case with a copy function, or updates
        /// and existing recipe.
        /// </summary>
        public void SaveRecipeEdits()
        {
            RecipeEditsViewModel.Instance.SaveRecipeEdits();
            CanSelectSave = false;
        }

        private string CreateQuantityString(string ingredient)
        {
            string ingredQuantity;
            string measureType = "";

            //Check if the ingredient string is valid
            if (string.IsNullOrEmpty(ingredient) || string.IsNullOrWhiteSpace(ingredient))
                return "";

            else
            {
                if (ingredient.Trim().ToString()[0] == '-' || ingredient.Trim().ToString()[0] == '!')
                    return "";
            }

            //Remove quantity and measure type from the ingredient string!  These are the only values of the ingred string that could possibly
            //have the information that we are looking for, can dump the rest!
            string[] qArray = ingredient.Trim().Split(' ');
            string[] ingredArr = new string[3];
            int count = 0;
            int sourceCount = 0;
            while (count < 3 && sourceCount < qArray.Length)
            {
                if (qArray[sourceCount].Trim().Length > 0)
                {
                    ingredArr[count] = qArray[sourceCount].Trim();
                    count++;
                }
                sourceCount++;

            }

            ingredQuantity = ingredArr[0];
            //first char of the string... test for num will show if we can proceed or have to bail because the ingred string
            //isn't written in a way that will be easy to take apart.
            char testForNum = ingredArr[0][0];

            //if this is a letter we can check Q1 to see if it is a spelled out number, if not we stop here!
            if (char.IsLetter(testForNum) == true)
            {
                //maybe it is a string spelling out the number?
                int val = StringManipulationHelper.TryToConvertWritenNumbersToDigits(ingredQuantity);
                if (val != 0)
                {
                    return (val * QuantitySelectedAsInt).ToString();
                }

                //Can't work with this, bail now.
                return "";
            }

            //Q1 turns out to be a fraction so Q2 could be the measurement type or not
            else if (StringManipulationHelper.IsFraction(ingredArr[0]))
            {
                measureType = StringManipulationHelper.CheckMeasureString(ingredArr[1]);
                    return produceNewQuantity(ingredArr[0], measureType);
            }

            //Still checking for weird strings like 1(4-5 lbs chicken)!
            //Check for when the chars in a word stop being didgets
            else if (char.IsDigit(ingredArr[0][0]))
            {
                int i = 1;
                while(i < ingredArr[0].Length && char.IsDigit(ingredArr[0][i]) == true)
                {
                    i++;
                }

                ingredQuantity = ingredArr[0].Substring(0, i);
            }

            bool q2Fraction = false;
            bool q2MeasureType = false;
            bool q3MeasureType = false;

            if (ingredArr[1].Length != 0 && StringManipulationHelper.IsFraction(ingredArr[1]) == true)
                q2Fraction = true;
            if(q2Fraction == false && StringManipulationHelper.CheckMeasureString(ingredArr[1]) != "")
                q2MeasureType = true;
            if (ingredArr[2].Length != 0 && StringManipulationHelper.CheckMeasureString(ingredArr[2]) != "")
                q3MeasureType = true;

            switch (q2Fraction)
            {
                case true:
                    //q2 is a fraction but we can't figure out the measuretype!
                    if (q3MeasureType == false)
                    {
                        measureType = "";
                        ingredQuantity += " " + ingredArr[1];
                    }
                    else //Q3 holds the measure type, probably
                    {
                        measureType = StringManipulationHelper.CheckMeasureString(ingredArr[2]);
                        if (measureType == "-1")
                            measureType = "";

                        ingredQuantity += " " + ingredArr[1];
                    }
                    break;

                case false:
                    //Q2 is neither a fraction nor a recognized measurement type, we have no measurement type bail
                    if (q2MeasureType == false)
                    {
                        measureType = "";
                    }
                    else //Q2 is a measurement type
                    {
                        measureType = StringManipulationHelper.CheckMeasureString(ingredArr[1]);
                    }
                    break;
            }

            string retstr = produceNewQuantity(ingredQuantity, measureType);

            return retstr;
        }

        /// <summary>
        /// Getting the new Quantity in string form 
        /// </summary>
        /// <param name="ingredient"the original amount of the ingredient></param>
        /// <returns>The multiplied quality in string form</returns>
        private string produceNewQuantity(string ingredQuantity, string measureType)
        {
            
            try
            {
                //If we have factions we send it to another function!
                if (StringManipulationHelper.IsFraction(ingredQuantity))
                {
                    string foo = manageQuantityMeasurementStringForFractions(ingredQuantity, measureType);
                    return foo;
                }
                

                int temp;
                bool res = Int32.TryParse(ingredQuantity.Trim(), out temp);
                if (res == false)
                    return "-1";

                int newQuantity = temp * QuantitySelectedAsInt;
                return StringManipulationHelper.FinalQuantityString(newQuantity, measureType);

            }
            catch (Exception e)
            { }

            //************************************ Can't figure it out!
            return "-1";
        }

        /// <summary>
        /// manages conversion for ingredQuantities with fractions in the string: 1/3 cup or 2 2/3 tablespoon as a example.
        /// </summary>
        /// <param name="ingredQuantity">The ingredient string we know has a fraction</param>
        /// <param name="measureType">the type of measurement used, Cups, Tablespoons, etc</param>
        /// <returns>The converted string for display</returns>
        internal string manageQuantityMeasurementStringForFractions(string ingredQuantity, string measureType)
        {
            double fractionAsDouble = 0;

            if (ingredQuantity.Contains("/"))
            {
                int indexSlash = ingredQuantity.IndexOf('/');
                //add the converted fration string
                fractionAsDouble += StringManipulationHelper.ConvertVulgarFaction(ingredQuantity.Substring(indexSlash - 1, 3));
                if (fractionAsDouble == -1)
                    return "-1";

                //remove the fraction string
                ingredQuantity = ingredQuantity.Substring(0, indexSlash - 1);
            }
            else
            {
                //add the converted fraction string
                fractionAsDouble = StringManipulationHelper.ConvertVulgarFaction(ingredQuantity.Substring(ingredQuantity.Length - 1));

                //remove the fraction string
                ingredQuantity = ingredQuantity.Substring(0, ingredQuantity.Length - 1);
            }

            //At this point the fraction is in the double called fractionAsDouble
            //The remaining part of the ingredQuantity is only whole numbers at this point.
            //Now we convert the whole number to a double so that you can have one single number to mutiply
            //unless there is no whole number in front of the fraction to worry about!
            if (ingredQuantity.Length > 0)
            {
                double tempDouble;
                bool res = double.TryParse(ingredQuantity.Trim(), out tempDouble);
                if (res == false)
                    return "-1";

                fractionAsDouble += tempDouble;
            }

            //Multiply by the QuantitySelectedAsInt on the UI page
            fractionAsDouble = fractionAsDouble * QuantitySelectedAsInt;
            return StringManipulationHelper.FinalQuantityString(fractionAsDouble, measureType);
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
            for (int i = 0; i < RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay.Count && i < 50; i++)
            {
                if (RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[i].Length > 0)
                {
                    string s1 = RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.listOfIngredientStringsForDisplay[i];
                    IngredientQuantityShift.Add(CreateQuantityString(s1));
                }
                else
                {
                    IngredientQuantityShift.Add(CreateQuantityString(""));
                }

            }

            //filling the ingredientQuantiy properties
            for (int IngredientCount = 0; IngredientCount < IngredientQuantityShift.Count; IngredientCount++)
            {
                if (IngredientQuantityShift[IngredientCount].Length > 0)
                {
                    listOfIngredientQuantitySetters[IngredientCount].Invoke(IngredientQuantityShift[IngredientCount]);
                }
                else
                {
                    listOfIngredientQuantitySetters[IngredientCount].Invoke("");
                }
            }
        }


        /// <summary>
        /// makes a copy of the current selectedViewMainRecipeCardMode and adds it to the tree
        /// </summary>
        private void Copy()
        {
            RecipeRecordModel recipeRecordModel = new RecipeRecordModel(RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.Title + " Copy", RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel);

            RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.UpdateRecipeDisplayFromRecipeRecordForCopy(recipeRecordModel);
            RecipeEditsViewModel.Instance.Title = RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.Title;
            RecipeEditsViewModel.Instance.TitleEditString = RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.Title;
            RecipeEditsViewModel.Instance.Author = RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.Author;
            RecipeEditsViewModel.Instance.CurrentTypeFromCombo = RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel.RecipeTypeInt;
            CanSelectSave = true;
            CmdSave.RaiseCanExecuteChanged();
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
                //if (string.Compare(selectViewMainRecipeCardModel.Title.ToLower(), "search for your next recipe find!") == 0)
                //    return false;
                //else
                    return true;
            }


        }

        /// <summary>
        /// can't select Copy if there are any open edits to the recipe
        /// </summary>
        private bool canSelectCopy;
        public bool CanSelectCopy
        {
            set
            {
                canSelectCopy = value;
                CmdCopy.RaiseCanExecuteChanged();
            }
            get
            {
                return canSelectCopy;
            }
        }

        /// <summary>
        /// can't select save until the user is logged in
        /// because there is no access to the DB
        /// </summary>
        private bool canSelectSave;
        public bool CanSelectSave
        {
            set
            {
                canSelectSave = value;
                CmdSave.RaiseCanExecuteChanged();
            }
            get
            {
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
        public RelayCommandRaiseCanExecute CmdSave
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Edit button command
        /// </summary>
        public RelayCommand CmdEdit
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Edit button command
        /// </summary>
        public RelayCommand CmdNew
        {
            get;
            private set;
        }
        /// <summary>
        /// property for the Remove button command
        /// </summary>
        public RelayCommand CmdRemove
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

        ///// <summary>
        ///// Property for the Recipe combobox change command
        ///// </summary>
        //public ICommand CmdSelectedTypeChanged
        //{
        //    get;
        //    private set;
        //}

        public RelayCommandRaiseCanExecute CmdCopy
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

        //private string currentTypeString;
        //public string CurrentTypeString
        //{
        //    get { return currentTypeString; }
        //    private set //because of the listbox I need to set the type property here and trigger the save button.
        //    {
        //        SetProperty(ref currentTypeString, value);
        //    }
        //}

        private string noLoadedRecipeHeight;
        public string NoLoadedRecipeHeight
        {
            get { return noLoadedRecipeHeight; }
            set { SetProperty(ref noLoadedRecipeHeight, value); }
        }

        private string loadedRecipeHeight;
        public string LoadedRecipeHeight
        {
            get { return loadedRecipeHeight; }
            set { SetProperty(ref loadedRecipeHeight, value); }
        }

        private int numXComboBox;
        public int NumXComboBox
        {
            get { return numXComboBox; }
            set { SetProperty(ref numXComboBox, value); }
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
    }
}
