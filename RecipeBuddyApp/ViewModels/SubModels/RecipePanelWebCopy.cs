using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Scrapers;
using RecipeBuddy.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace RecipeBuddy.ViewModels
{
    public class RecipePanelForWebCopy : ObservableObject
    {
        public RecipeDisplayModel recipeCardModel;
        

        Action action;
        public Action<SelectionChangedEventArgs> actionWithEventArgs;
        Func<bool> funcBool;

        private static readonly RecipePanelForWebCopy instance = new RecipePanelForWebCopy();
        public static RecipePanelForWebCopy Instance
        {
            get { return instance; }
        }

        static RecipePanelForWebCopy()
        { }

        /// <summary>
        /// URLList is used by the Scraper and GenerateSearchResults to pull the URL of all the found recipes 
        /// </summary>
        private RecipePanelForWebCopy()
        {
            recipeCardModel = new RecipeDisplayModel();
            measureTypes = new ObservableCollection<string>()
            {
               "---", "Cup(s)","Tablespoon(s)","Teaspoon(s)"
            };
            InitializeDefaultValuesForWebCopy();
            CanSelectSave = false;
            CanSelectCancel = true;
            CmdSaveButton = new RBRelayCommand(action = () => SaveEntryToDB(), funcBool = () => CanSelectSave);
            CmdCancelDialogButton = new RBRelayCommand(action = () => CancelEntry(), funcBool = () => CanSelectCancel);
            //CmdSelectedMeasurementChanged = new ICommandViewModel<SelectionChangedEventArgs>(actionWithEventArgs = e => ChangeMeasurementTypeFromComboBox(e), canCallActionFunc => AlwaysTrue);
        }

        /// <summary>
        /// This manages both the RecipeCardView as well as the list of RecipeCards were pulled from the specific website
        /// </summary>
        /// <param name="type"></param>
        //private RecipePanelForWebCopy(RecipeRecordModel recipeBlurbModel)
        //{
        //    //For Threading callbacks.  
        //    Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView();

        //    InitializeValuesForWebCopy();

        //    recipeCardModel = new RecipeDisplayModel(recipeBlurbModel);
        //    CanSelectSave = false;
        //    CanSelectCancel = true;
        //    CmdSaveButton = new RBRelayCommand(action = () => SaveEntryToDB(), funcBool = () => CanSelectSave);
        //    CmdCancelDialogButton = new RBRelayCommand(action = () => CancelEntry(), funcBool = () => CanSelectCancel);
        //}

        //public void InitializeRecipeCardModel(RecipeRecordModel recipeBlurbModel)
        //{
        //    recipeCardModel = new RecipeDisplayModel(recipeBlurbModel);
        //}



        /// <summary>
        /// Creates are recipeCardModel out of a recipeModel
        /// </summary>
        public void LoadRecipeCardModel(RecipeDisplayModel recipeModel)
        {
            //For Threading callbacks.  
            Windows.ApplicationModel.Core.CoreApplicationView coreApplicationView = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView();

            recipeCardModel.CopyRecipeDisplayModel(recipeModel);
            InitializeDefaultValuesForWebCopy();
            CanSelectSave = true;
            CanSelectCancel = true;
            CmdSaveButton = new RBRelayCommand(action = () => SaveEntryToDB(), funcBool = () => CanSelectSave);
            CmdCancelDialogButton = new RBRelayCommand(action = () => CancelEntry(), funcBool = () => CanSelectCancel);
        }

        public void ClearRecipeEntry()
        {
            LoadRecipeCardModel(new RecipeDisplayModel());
            WebViewModel.Instance.CloseKeepRecipePanel();
            CanSelectSave= false;
            CanSelectCancel = false;
            InitializeDefaultValuesForWebCopy();
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
        public void SaveEntryToDB()
        {
            //This will only do something if their is a value in the quantity field, if not it will simply return here.
            CreateIngredStrings();
            recipeCardModel.SaveEditsToARecipeModel();
            DataBaseAccessorsForRecipeManager.SaveRecipeToDatabase(UserViewModel.Instance.UsersIDInDB, recipeCardModel, UserViewModel.Instance.UsersIDInDB);
            MainNavTreeViewModel.Instance.AddRecipeModelsToTreeView(new RecipeRecordModel(recipeCardModel), true);
            ClearRecipeEntry();
        }


        /// <summary>
        /// This manages changes that come in through the user manipulating the combobox on the Recipe Entry Panel
        /// </summary>
        /// <param name="e"></param>
        //internal void ChangeMeasurementTypeFromComboBox(SelectionChangedEventArgs e)
        //{
        //    //recipePanelForWebCopy.recipeCardModel.RecipeType = (Type_Of_Recipe)ComboBoxIndexForRecipeType;
        //    if (e.AddedItems != null && e.AddedItems.Count > 0)
        //    {
        //        string type = e.AddedItems[0].ToString();

        //        //for (int index = 0; index < MainNavTreeViewModel.Instance.CatagoryTypes.Count; index++)
        //        //{
        //        //    if (string.Compare(RecipePanelForWebCopy.Instance.MeasureTypes[index].ToString().ToLower(), type.ToLower()) == 0)
        //        //    {
        //        //        recipePanelForWebCopy.recipeCardModel.RecipeType = (Type_Of_Recipe)index;
        //        //    }
        //        //}
        //    }
        //}


        /// <summary>
        /// Concatinates the ingredients quantity with the measurement type and the actualy ingredient
        /// </summary>
        private void CreateIngredStrings()
        {

            if (Ingred1Quant.Length > 0)
            { recipeCardModel.Ingredient1 = ManageStringCreation(recipeCardModel.Ingredient1, Ingred1Quant, MeasurementType1); }

            if (Ingred2Quant.Length > 0)
            { recipeCardModel.Ingredient2 = ManageStringCreation(recipeCardModel.Ingredient2, Ingred2Quant, MeasurementType2); }

            if (Ingred3Quant.Length > 0)
            { recipeCardModel.Ingredient3 = ManageStringCreation(recipeCardModel.Ingredient3, Ingred3Quant, MeasurementType3); }

            if (Ingred4Quant.Length > 0)
            { recipeCardModel.Ingredient4 = ManageStringCreation(recipeCardModel.Ingredient4, Ingred4Quant, MeasurementType4); }

            if (Ingred5Quant.Length > 0)
            { recipeCardModel.Ingredient5 = ManageStringCreation(recipeCardModel.Ingredient5, Ingred5Quant, MeasurementType5); }

            if (Ingred6Quant.Length > 0)
            { recipeCardModel.Ingredient6 = ManageStringCreation(recipeCardModel.Ingredient6, Ingred6Quant, MeasurementType6); }

            if (Ingred7Quant.Length > 0)
            { recipeCardModel.Ingredient7 = ManageStringCreation(recipeCardModel.Ingredient7, Ingred7Quant, MeasurementType7); }

            if (Ingred8Quant.Length > 0)
            { recipeCardModel.Ingredient8 = ManageStringCreation(recipeCardModel.Ingredient8, Ingred8Quant, MeasurementType8); }

            if (Ingred9Quant.Length > 0)
            { recipeCardModel.Ingredient9 = ManageStringCreation(recipeCardModel.Ingredient9, Ingred9Quant, MeasurementType9); }

            if (Ingred10Quant.Length > 0)
            { recipeCardModel.Ingredient10 = ManageStringCreation(recipeCardModel.Ingredient10, Ingred10Quant, MeasurementType10); }

            if (Ingred11Quant.Length > 0)
            { recipeCardModel.Ingredient11 = ManageStringCreation(recipeCardModel.Ingredient11, Ingred11Quant, MeasurementType11); }

            if (Ingred12Quant.Length > 0)
            { recipeCardModel.Ingredient12 = ManageStringCreation(recipeCardModel.Ingredient12, Ingred12Quant, MeasurementType12); }

            if (Ingred13Quant.Length > 0)
            { recipeCardModel.Ingredient13 = ManageStringCreation(recipeCardModel.Ingredient13, Ingred13Quant, MeasurementType13); }

            if (Ingred14Quant.Length > 0)
            { recipeCardModel.Ingredient14 = ManageStringCreation(recipeCardModel.Ingredient14, Ingred14Quant, MeasurementType14); }

            if (Ingred15Quant.Length > 0)
            { recipeCardModel.Ingredient15 = ManageStringCreation(recipeCardModel.Ingredient15, Ingred15Quant, MeasurementType15); }

            if (Ingred16Quant.Length > 0)
            { recipeCardModel.Ingredient16 = ManageStringCreation(recipeCardModel.Ingredient16, Ingred16Quant, MeasurementType16); }

            if (Ingred17Quant.Length > 0)
            { recipeCardModel.Ingredient17 = ManageStringCreation(recipeCardModel.Ingredient17, Ingred17Quant, MeasurementType17); }

            if (Ingred18Quant.Length > 0)
            { recipeCardModel.Ingredient18 = ManageStringCreation(recipeCardModel.Ingredient18, Ingred18Quant, MeasurementType18); }

            if (Ingred19Quant.Length > 0)
            { recipeCardModel.Ingredient19 = ManageStringCreation(recipeCardModel.Ingredient19, Ingred19Quant, MeasurementType19); }

            if (Ingred20Quant.Length > 0)
            { recipeCardModel.Ingredient20 = ManageStringCreation(recipeCardModel.Ingredient20, Ingred20Quant, MeasurementType20); }

            if (Ingred21Quant.Length > 0)
            { recipeCardModel.Ingredient21 = ManageStringCreation(recipeCardModel.Ingredient21, Ingred21Quant, MeasurementType21); }

            if (Ingred22Quant.Length > 0)
            { recipeCardModel.Ingredient22 = ManageStringCreation(recipeCardModel.Ingredient22, Ingred22Quant, MeasurementType22); }

            if (Ingred23Quant.Length > 0)
            { recipeCardModel.Ingredient23 = ManageStringCreation(recipeCardModel.Ingredient23, Ingred23Quant, MeasurementType23); }

            if (Ingred24Quant.Length > 0)
            { recipeCardModel.Ingredient24 = ManageStringCreation(recipeCardModel.Ingredient24, Ingred24Quant, MeasurementType24); }

            if (Ingred25Quant.Length > 0)
            { recipeCardModel.Ingredient25 = ManageStringCreation(recipeCardModel.Ingredient25, Ingred25Quant, MeasurementType25); }

            if (Ingred26Quant.Length > 0)
            { recipeCardModel.Ingredient26 = ManageStringCreation(recipeCardModel.Ingredient26, Ingred26Quant, MeasurementType26); }

            if (Ingred27Quant.Length > 0)
            { recipeCardModel.Ingredient27 = ManageStringCreation(recipeCardModel.Ingredient27, Ingred27Quant, MeasurementType27); }

            if (Ingred28Quant.Length > 0)
            { recipeCardModel.Ingredient28 = ManageStringCreation(recipeCardModel.Ingredient28, Ingred28Quant, MeasurementType28); }

            if (Ingred29Quant.Length > 0)
            { recipeCardModel.Ingredient29 = ManageStringCreation(recipeCardModel.Ingredient29, Ingred29Quant, MeasurementType29); }

            if (Ingred30Quant.Length > 0)
            { recipeCardModel.Ingredient30 = ManageStringCreation(recipeCardModel.Ingredient30, Ingred30Quant, MeasurementType30); }

            if (Ingred31Quant.Length > 0)
            { recipeCardModel.Ingredient31 = ManageStringCreation(recipeCardModel.Ingredient31, Ingred31Quant, MeasurementType31); }

            if (Ingred32Quant.Length > 0)
            { recipeCardModel.Ingredient32 = ManageStringCreation(recipeCardModel.Ingredient32, Ingred32Quant, MeasurementType32); }

            if (Ingred33Quant.Length > 0)
            { recipeCardModel.Ingredient33 = ManageStringCreation(recipeCardModel.Ingredient33, Ingred33Quant, MeasurementType33); }

            if (Ingred34Quant.Length > 0)
            { recipeCardModel.Ingredient34 = ManageStringCreation(recipeCardModel.Ingredient34, Ingred34Quant, MeasurementType34); }

            if (Ingred35Quant.Length > 0)
            { recipeCardModel.Ingredient35 = ManageStringCreation(recipeCardModel.Ingredient35, Ingred35Quant, MeasurementType35); }

            if (Ingred36Quant.Length > 0)
            { recipeCardModel.Ingredient36 = ManageStringCreation(recipeCardModel.Ingredient36, Ingred36Quant, MeasurementType36); }

            if (Ingred37Quant.Length > 0)
            { recipeCardModel.Ingredient37 = ManageStringCreation(recipeCardModel.Ingredient37, Ingred37Quant, MeasurementType37); }

            if (Ingred38Quant.Length > 0)
            { recipeCardModel.Ingredient38 = ManageStringCreation(recipeCardModel.Ingredient38, Ingred38Quant, MeasurementType38); }

            if (Ingred39Quant.Length > 0)
            { recipeCardModel.Ingredient39 = ManageStringCreation(recipeCardModel.Ingredient39, Ingred39Quant, MeasurementType39); }

            if (Ingred40Quant.Length > 0)
            { recipeCardModel.Ingredient40 = ManageStringCreation(recipeCardModel.Ingredient40, Ingred40Quant, MeasurementType40); }

            if (Ingred41Quant.Length > 0)
            { recipeCardModel.Ingredient41 = ManageStringCreation(recipeCardModel.Ingredient41, Ingred41Quant, MeasurementType41); }

            if (Ingred42Quant.Length > 0)
            { recipeCardModel.Ingredient42 = ManageStringCreation(recipeCardModel.Ingredient42, Ingred42Quant, MeasurementType42); }

            if (Ingred43Quant.Length > 0)
            { recipeCardModel.Ingredient43 = ManageStringCreation(recipeCardModel.Ingredient43, Ingred43Quant, MeasurementType43); }

            if (Ingred4Quant.Length > 0)
            { recipeCardModel.Ingredient44 = ManageStringCreation(recipeCardModel.Ingredient44, Ingred44Quant, MeasurementType44); }

            if (Ingred45Quant.Length > 0)
            { recipeCardModel.Ingredient45 = ManageStringCreation(recipeCardModel.Ingredient45, Ingred45Quant, MeasurementType45); }

            if (Ingred46Quant.Length > 0)
            { recipeCardModel.Ingredient46 = ManageStringCreation(recipeCardModel.Ingredient46, Ingred46Quant, MeasurementType46); }

            if (Ingred47Quant.Length > 0)
            { recipeCardModel.Ingredient47 = ManageStringCreation(recipeCardModel.Ingredient47, Ingred47Quant, MeasurementType47); }

            if (Ingred48Quant.Length > 0)
            { recipeCardModel.Ingredient48 = ManageStringCreation(recipeCardModel.Ingredient48, Ingred48Quant, MeasurementType48); }

            if (Ingred49Quant.Length > 0)
            { recipeCardModel.Ingredient49 = ManageStringCreation(recipeCardModel.Ingredient49, Ingred49Quant, MeasurementType49); }

            if (Ingred50Quant.Length > 0)
            { recipeCardModel.Ingredient50 = ManageStringCreation(recipeCardModel.Ingredient50, Ingred50Quant, MeasurementType50); }
        }

        private string ManageStringCreation(string ingred, string quantity, int measureIndex)
        {
            string strReturn;

            if (measureIndex == 0)
            {
                strReturn = quantity + " " + ingred;
            }
            else
            {
                strReturn = quantity + " " + measureTypes[measureIndex] + " " + ingred;
            }

            return strReturn;
        }

        private void InitializeDefaultValuesForWebCopy()
        {
            ingred1Quant = ""; ingred2Quant = ""; ingred3Quant = ""; ingred4Quant = ""; ingred5Quant = ""; ingred6Quant = ""; ingred7Quant = ""; ingred8Quant = ""; ingred9Quant = ""; ingred10Quant = "";
            ingred11Quant = ""; ingred12Quant = ""; ingred13Quant = ""; ingred14Quant = ""; ingred15Quant = ""; ingred16Quant = ""; ingred17Quant = ""; ingred18Quant = ""; ingred19Quant = ""; ingred20Quant = "";
            ingred21Quant = ""; ingred22Quant = ""; ingred23Quant = ""; ingred24Quant = ""; ingred25Quant = ""; ingred26Quant = ""; ingred27Quant = ""; ingred28Quant = ""; ingred29Quant = ""; ingred30Quant = "";
            ingred31Quant = ""; ingred32Quant = ""; ingred33Quant = ""; ingred34Quant = ""; ingred35Quant = ""; ingred36Quant = ""; ingred37Quant = ""; ingred38Quant = ""; ingred39Quant = ""; ingred40Quant = "";
            ingred41Quant = ""; ingred42Quant = ""; ingred43Quant = ""; ingred44Quant = ""; ingred45Quant = ""; ingred46Quant = ""; ingred47Quant = ""; ingred48Quant = ""; ingred49Quant = ""; ingred50Quant = "";

            measurementType1 = 0; measurementType2 = 0; measurementType3 = 0; measurementType4 = 0; measurementType5 = 0; measurementType6 = 0; measurementType7 = 0; measurementType8 = 0; measurementType9 = 0; measurementType10 = 0;
            measurementType11 = 0; measurementType12 = 0; measurementType13 = 0; measurementType14 = 0; measurementType15 = 0; measurementType16 = 0; measurementType17 = 0; measurementType18 = 0; measurementType19 = 0; measurementType20 = 0;
            measurementType21 = 0; measurementType22 = 0; measurementType23 = 0; measurementType24 = 0; measurementType25 = 0; measurementType26 = 0; measurementType27 = 0; measurementType28 = 0; measurementType29 = 0; measurementType30 = 0;
            measurementType31 = 0; measurementType32 = 0; measurementType33 = 0; measurementType34 = 0; measurementType35 = 0; measurementType36 = 0; measurementType37 = 0; measurementType38 = 0; measurementType39 = 0; measurementType40 = 0;
            measurementType41 = 0; measurementType42 = 0; measurementType43 = 0; measurementType44 = 0; measurementType45 = 0; measurementType46 = 0; measurementType47 = 0; measurementType48 = 0; measurementType49 = 0; measurementType50 = 0;
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


        private ObservableCollection<string> measureTypes;
        public ObservableCollection<string> MeasureTypes
        {
            get { return measureTypes; }
            set { SetProperty(ref measureTypes, value); }
        }

        public RBRelayCommand CmdSaveButton
        {
            get;
            private set;
        }

        public RBRelayCommand CmdSaveNewEntryButton
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

        /// <summary>
        /// Property for the Recipe combobox change command
        /// </summary>
        public ICommand CmdSelectedMeasurementChanged
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates whether or not we can click the recipe-related button, there needs to be a recipe in the CardView so the 
        /// total list count has to be greater than 0.
        /// </summary>
        private bool alwaysTrue;
        public bool AlwaysTrue
        {
            get
            {
                return alwaysTrue;
            }

            set { SetProperty(ref alwaysTrue, value); }
        }


        #endregion

        #region Ingredient Qantities and Measurement types backing the UI combobox and qantity fields attached to creating an recipe from scratch
        private string ingred1Quant;
        public string Ingred1Quant
        {
            get { return ingred1Quant; }
            set { SetProperty(ref ingred1Quant, value); }
        }

        private int measurementType1;
        public int MeasurementType1
        {
            get { return measurementType1; }
            set { SetProperty(ref measurementType1, value); }
        }

        private string ingred2Quant;
        public string Ingred2Quant
        {
            get { return ingred2Quant; }
            set { SetProperty(ref ingred2Quant, value); }
        }

        private int measurementType2;
        public int MeasurementType2
        {
            get { return measurementType2; }
            set { SetProperty(ref measurementType2, value); }
        }

        private string ingred3Quant;
        public string Ingred3Quant
        {
            get { return ingred3Quant; }
            set { SetProperty(ref ingred3Quant, value); }
        }

        private int measurementType3;
        public int MeasurementType3
        {
            get { return measurementType3; }
            set { SetProperty(ref measurementType3, value); }
        }

        private string ingred4Quant;
        public string Ingred4Quant
        {
            get { return ingred4Quant; }
            set { SetProperty(ref ingred4Quant, value); }
        }

        private int measurementType4;
        public int MeasurementType4
        {
            get { return measurementType4; }
            set { SetProperty(ref measurementType4, value); }
        }

        private string ingred5Quant;
        public string Ingred5Quant
        {
            get { return ingred5Quant; }
            set { SetProperty(ref ingred5Quant, value); }
        }

        private int measurementType5;
        public int MeasurementType5
        {
            get { return measurementType5; }
            set { SetProperty(ref measurementType5, value); }
        }

        private string ingred6Quant;
        public string Ingred6Quant
        {
            get { return ingred6Quant; }
            set { SetProperty(ref ingred6Quant, value); }
        }

        private int measurementType6;
        public int MeasurementType6
        {
            get { return measurementType6; }
            set { SetProperty(ref measurementType6, value); }
        }

        private string ingred7Quant;
        public string Ingred7Quant
        {
            get { return ingred7Quant; }
            set { SetProperty(ref ingred7Quant, value); }
        }

        private int measurementType7;
        public int MeasurementType7
        {
            get { return measurementType7; }
            set { SetProperty(ref measurementType7, value); }
        }

        private string ingred8Quant;
        public string Ingred8Quant
        {
            get { return ingred8Quant; }
            set { SetProperty(ref ingred8Quant, value); }
        }

        private int measurementType8;
        public int MeasurementType8
        {
            get { return measurementType8; }
            set { SetProperty(ref measurementType8, value); }
        }

        private string ingred9Quant;
        public string Ingred9Quant
        {
            get { return ingred9Quant; }
            set { SetProperty(ref ingred9Quant, value); }
        }

        private int measurementType9;
        public int MeasurementType9
        {
            get { return measurementType9; }
            set { SetProperty(ref measurementType9, value); }
        }

        private string ingred10Quant;
        public string Ingred10Quant
        {
            get { return ingred10Quant; }
            set { SetProperty(ref ingred10Quant, value); }
        }

        private int measurementType10;
        public int MeasurementType10
        {
            get { return measurementType10; }
            set { SetProperty(ref measurementType10, value); }
        }

        private string ingred11Quant;
        public string Ingred11Quant
        {
            get { return ingred11Quant; }
            set { SetProperty(ref ingred11Quant, value); }
        }


        private int measurementType11;
        public int MeasurementType11
        {
            get { return measurementType11; }
            set { SetProperty(ref measurementType11, value); }
        }

        private string ingred12Quant;
        public string Ingred12Quant
        {
            get { return ingred12Quant; }
            set { SetProperty(ref ingred12Quant, value); }
        }

        private int measurementType12;
        public int MeasurementType12
        {
            get { return measurementType12; }
            set { SetProperty(ref measurementType12, value); }
        }

        private string ingred13Quant;
        public string Ingred13Quant
        {
            get { return ingred13Quant; }
            set { SetProperty(ref ingred13Quant, value); }
        }

        private int measurementType13;
        public int MeasurementType13
        {
            get { return measurementType13; }
            set { SetProperty(ref measurementType13, value); }
        }

        private string ingred14Quant;
        public string Ingred14Quant
        {
            get { return ingred14Quant; }
            set { SetProperty(ref ingred14Quant, value); }
        }

        private int measurementType14;
        public int MeasurementType14
        {
            get { return measurementType14; }
            set { SetProperty(ref measurementType14, value); }
        }

        private string ingred15Quant;
        public string Ingred15Quant
        {
            get { return ingred15Quant; }
            set { SetProperty(ref ingred15Quant, value); }
        }

        private int measurementType15;
        public int MeasurementType15
        {
            get { return measurementType15; }
            set { SetProperty(ref measurementType15, value); }
        }

        private string ingred16Quant;
        public string Ingred16Quant
        {
            get { return ingred16Quant; }
            set { SetProperty(ref ingred16Quant, value); }
        }

        private int measurementType16;
        public int MeasurementType16
        {
            get { return measurementType16; }
            set { SetProperty(ref measurementType16, value); }
        }

        private string ingred17Quant;
        public string Ingred17Quant
        {
            get { return ingred17Quant; }
            set { SetProperty(ref ingred17Quant, value); }
        }

        private int measurementType17;
        public int MeasurementType17
        {
            get { return measurementType17; }
            set { SetProperty(ref measurementType17, value); }
        }

        private string ingred18Quant;
        public string Ingred18Quant
        {
            get { return ingred18Quant; }
            set { SetProperty(ref ingred18Quant, value); }
        }

        private int measurementType18;
        public int MeasurementType18
        {
            get { return measurementType18; }
            set { SetProperty(ref measurementType18, value); }
        }

        private string ingred19Quant;
        public string Ingred19Quant
        {
            get { return ingred19Quant; }
            set { SetProperty(ref ingred19Quant, value); }
        }

        private int measurementType19;
        public int MeasurementType19
        {
            get { return measurementType19; }
            set { SetProperty(ref measurementType19, value); }
        }

        private string ingred20Quant;
        public string Ingred20Quant
        {
            get { return ingred20Quant; }
            set { SetProperty(ref ingred20Quant, value); }
        }

        private int measurementType20;
        public int MeasurementType20
        {
            get { return measurementType20; }
            set { SetProperty(ref measurementType20, value); }
        }

        private string ingred21Quant;
        public string Ingred21Quant
        {
            get { return ingred21Quant; }
            set { SetProperty(ref ingred21Quant, value); }
        }

        private int measurementType21;
        public int MeasurementType21
        {
            get { return measurementType21; }
            set { SetProperty(ref measurementType21, value); }
        }

        private string ingred22Quant;
        public string Ingred22Quant
        {
            get { return ingred22Quant; }
            set { SetProperty(ref ingred22Quant, value); }
        }

        private int measurementType22;
        public int MeasurementType22
        {
            get { return measurementType22; }
            set { SetProperty(ref measurementType22, value); }
        }

        private string ingred23Quant;
        public string Ingred23Quant
        {
            get { return ingred23Quant; }
            set { SetProperty(ref ingred23Quant, value); }
        }

        private int measurementType23;
        public int MeasurementType23
        {
            get { return measurementType23; }
            set { SetProperty(ref measurementType23, value); }
        }

        private string ingred24Quant;
        public string Ingred24Quant
        {
            get { return ingred24Quant; }
            set { SetProperty(ref ingred24Quant, value); }
        }

        private int measurementType24;
        public int MeasurementType24
        {
            get { return measurementType24; }
            set { SetProperty(ref measurementType24, value); }
        }

        private string ingred25Quant;
        public string Ingred25Quant
        {
            get { return ingred25Quant; }
            set { SetProperty(ref ingred25Quant, value); }
        }

        private int measurementType25;
        public int MeasurementType25
        {
            get { return measurementType25; }
            set { SetProperty(ref measurementType25, value); }
        }

        private string ingred26Quant;
        public string Ingred26Quant
        {
            get { return ingred26Quant; }
            set { SetProperty(ref ingred26Quant, value); }
        }

        private int measurementType26;
        public int MeasurementType26
        {
            get { return measurementType26; }
            set { SetProperty(ref measurementType26, value); }
        }

        private string ingred27Quant;
        public string Ingred27Quant
        {
            get { return ingred27Quant; }
            set { SetProperty(ref ingred27Quant, value); }
        }

        private int measurementType27;
        public int MeasurementType27
        {
            get { return measurementType27; }
            set { SetProperty(ref measurementType27, value); }
        }

        private string ingred28Quant;
        public string Ingred28Quant
        {
            get { return ingred28Quant; }
            set { SetProperty(ref ingred28Quant, value); }
        }

        private int measurementType28;
        public int MeasurementType28
        {
            get { return measurementType28; }
            set { SetProperty(ref measurementType28, value); }
        }

        private string ingred29Quant;
        public string Ingred29Quant
        {
            get { return ingred29Quant; }
            set { SetProperty(ref ingred29Quant, value); }
        }

        private int measurementType29;
        public int MeasurementType29
        {
            get { return measurementType29; }
            set { SetProperty(ref measurementType29, value); }
        }

        private string ingred30Quant;
        public string Ingred30Quant
        {
            get { return ingred30Quant; }
            set { SetProperty(ref ingred30Quant, value); }
        }

        private int measurementType30;
        public int MeasurementType30
        {
            get { return measurementType30; }
            set { SetProperty(ref measurementType30, value); }
        }

        private string ingred31Quant;
        public string Ingred31Quant
        {
            get { return ingred31Quant; }
            set { SetProperty(ref ingred31Quant, value); }
        }

        private int measurementType31;
        public int MeasurementType31
        {
            get { return measurementType31; }
            set { SetProperty(ref measurementType31, value); }
        }

        private string ingred32Quant;
        public string Ingred32Quant
        {
            get { return ingred32Quant; }
            set { SetProperty(ref ingred32Quant, value); }
        }

        private int measurementType32;
        public int MeasurementType32
        {
            get { return measurementType32; }
            set { SetProperty(ref measurementType32, value); }
        }

        private string ingred33Quant;
        public string Ingred33Quant
        {
            get { return ingred33Quant; }
            set { SetProperty(ref ingred33Quant, value); }
        }

        private int measurementType33;
        public int MeasurementType33
        {
            get { return measurementType33; }
            set { SetProperty(ref measurementType33, value); }
        }

        private string ingred34Quant;
        public string Ingred34Quant
        {
            get { return ingred34Quant; }
            set { SetProperty(ref ingred34Quant, value); }
        }

        private int measurementType34;
        public int MeasurementType34
        {
            get { return measurementType34; }
            set { SetProperty(ref measurementType34, value); }
        }

        private string ingred35Quant;
        public string Ingred35Quant
        {
            get { return ingred35Quant; }
            set { SetProperty(ref ingred35Quant, value); }
        }

        private int measurementType35;
        public int MeasurementType35
        {
            get { return measurementType35; }
            set { SetProperty(ref measurementType35, value); }
        }

        private string ingred36Quant;
        public string Ingred36Quant
        {
            get { return ingred36Quant; }
            set { SetProperty(ref ingred36Quant, value); }
        }

        private int measurementType36;
        public int MeasurementType36
        {
            get { return measurementType36; }
            set { SetProperty(ref measurementType36, value); }
        }

        private string ingred37Quant;
        public string Ingred37Quant
        {
            get { return ingred37Quant; }
            set { SetProperty(ref ingred37Quant, value); }
        }

        private int measurementType37;
        public int MeasurementType37
        {
            get { return measurementType37; }
            set { SetProperty(ref measurementType37, value); }
        }

        private string ingred38Quant;
        public string Ingred38Quant
        {
            get { return ingred38Quant; }
            set { SetProperty(ref ingred38Quant, value); }
        }

        private int measurementType38;
        public int MeasurementType38
        {
            get { return measurementType38; }
            set { SetProperty(ref measurementType38, value); }
        }

        private string ingred39Quant;
        public string Ingred39Quant
        {
            get { return ingred39Quant; }
            set { SetProperty(ref ingred39Quant, value); }
        }

        private int measurementType39;
        public int MeasurementType39
        {
            get { return measurementType39; }
            set { SetProperty(ref measurementType39, value); }
        }

        private string ingred40Quant;
        public string Ingred40Quant
        {
            get { return ingred40Quant; }
            set { SetProperty(ref ingred40Quant, value); }
        }

        private int measurementType40;
        public int MeasurementType40
        {
            get { return measurementType40; }
            set { SetProperty(ref measurementType40, value); }
        }

        private string ingred41Quant;
        public string Ingred41Quant
        {
            get { return ingred41Quant; }
            set { SetProperty(ref ingred41Quant, value); }
        }

        private int measurementType41;
        public int MeasurementType41
        {
            get { return measurementType41; }
            set { SetProperty(ref measurementType41, value); }
        }

        private string ingred42Quant;
        public string Ingred42Quant
        {
            get { return ingred42Quant; }
            set { SetProperty(ref ingred42Quant, value); }
        }

        private int measurementType42;
        public int MeasurementType42
        {
            get { return measurementType42; }
            set { SetProperty(ref measurementType42, value); }
        }

        private string ingred43Quant;
        public string Ingred43Quant
        {
            get { return ingred43Quant; }
            set { SetProperty(ref ingred43Quant, value); }
        }

        private int measurementType43;
        public int MeasurementType43
        {
            get { return measurementType43; }
            set { SetProperty(ref measurementType43, value); }
        }

        private string ingred44Quant;
        public string Ingred44Quant
        {
            get { return ingred44Quant; }
            set { SetProperty(ref ingred44Quant, value); }
        }

        private int measurementType44;
        public int MeasurementType44
        {
            get { return measurementType44; }
            set { SetProperty(ref measurementType44, value); }
        }

        private string ingred45Quant;
        public string Ingred45Quant
        {
            get { return ingred45Quant; }
            set { SetProperty(ref ingred45Quant, value); }
        }

        private int measurementType45;
        public int MeasurementType45
        {
            get { return measurementType45; }
            set { SetProperty(ref measurementType45, value); }
        }

        private string ingred46Quant;
        public string Ingred46Quant
        {
            get { return ingred46Quant; }
            set { SetProperty(ref ingred46Quant, value); }
        }

        private int measurementType46;
        public int MeasurementType46
        {
            get { return measurementType46; }
            set { SetProperty(ref measurementType46, value); }
        }

        private string ingred47Quant;
        public string Ingred47Quant
        {
            get { return ingred47Quant; }
            set { SetProperty(ref ingred47Quant, value); }
        }

        private int measurementType47;
        public int MeasurementType47
        {
            get { return measurementType47; }
            set { SetProperty(ref measurementType47, value); }
        }

        private string ingred48Quant;
        public string Ingred48Quant
        {
            get { return ingred48Quant; }
            set { SetProperty(ref ingred48Quant, value); }
        }

        private int measurementType48;
        public int MeasurementType48
        {
            get { return measurementType48; }
            set { SetProperty(ref measurementType48, value); }
        }

        private string ingred49Quant;
        public string Ingred49Quant
        {
            get { return ingred49Quant; }
            set { SetProperty(ref ingred49Quant, value); }
        }

        private int measurementType49;
        public int MeasurementType49
        {
            get { return measurementType49; }
            set { SetProperty(ref measurementType49, value); }
        }

        private string ingred50Quant;
        public string Ingred50Quant
        {
            get { return ingred50Quant; }
            set { SetProperty(ref ingred50Quant, value); }
        }

        private int measurementType50;
        public int MeasurementType50
        {
            get { return measurementType50; }
            set { SetProperty(ref measurementType50, value); }
        }
        #endregion
    }
}
