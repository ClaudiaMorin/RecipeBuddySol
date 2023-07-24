using System;
using RecipeBuddy.Core.Models;
using Windows.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeBuddy.ViewModels.Commands;
using RecipeBuddy.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace RecipeBuddyApp.ViewModels
{
    public sealed class BlankEntryModel : ObservableObject
    {
        Action action;
        private static readonly BlankEntryModel instance = new BlankEntryModel();
        public RecipePanelForWebCopyOrNew recipePanelForNew;

        static BlankEntryModel()
        { }
        public static BlankEntryModel Instance
        {
            get { return instance; }
        }

        private BlankEntryModel()
        {
            recipePanelForNew = new RecipePanelForWebCopyOrNew();
            recipePanelForNew.recipeCardModel.Title = "Recipe Title Here";
            CmdSaveButton = new RelayCommand(action = () => SaveEntry());
            CmdCancelButton = new RelayCommand(action = () => CancelEntry());
        }

        public void EmptyRecipePanel()
        {
            recipePanelForNew.LoadRecipeCardModelAndDirections(new RecipeDisplayModel());
            recipePanelForNew.recipeCardModel.Title = "Recipe Title Here";
            recipePanelForNew.recipeCardModel.RecipeTypeInt = 18;
        }


        public void SaveEntry()
        {
            recipePanelForNew.SaveEntry();
            EmptyRecipePanel();
        }

        public void CancelEntry()
        {
            EmptyRecipePanel();
        }


        public RelayCommand CmdSaveButton
        {
            get;
            private set;
        }

        public RelayCommand CmdCancelButton
        {
            get;
            private set;
        }


        //#region setting up all of the IngredientQuantity Properties

        //string ingred1Quant;
        //public string Ingred1Quant
        //{
        //    get { return ingred1Quant; }
        //    set { SetProperty(ref ingred1Quant, value); }
        //}

        //string ingred2Quant;
        //public string Ingred2Quant
        //{
        //    get { return ingred2Quant; }
        //    set { SetProperty(ref ingred2Quant, value); }
        //}

        //string ingred3Quant;
        //public string Ingred3Quant
        //{
        //    get { return ingred3Quant; }
        //    set { SetProperty(ref ingred3Quant, value); }
        //}

        //string ingred4Quant;
        //public string Ingred4Quant
        //{
        //    get { return ingred4Quant; }
        //    set { SetProperty(ref ingred4Quant, value); }
        //}

        //string ingred5Quant;
        //public string Ingred5Quant
        //{
        //    get { return ingred5Quant; }
        //    set { SetProperty(ref ingred5Quant, value); }
        //}

        //string ingred6Quant;
        //public string Ingred6Quant
        //{
        //    get { return ingred6Quant; }
        //    set { SetProperty(ref ingred6Quant, value); }
        //}

        //string ingred7Quant;
        //public string Ingred7Quant
        //{
        //    get { return ingred7Quant; }
        //    set { SetProperty(ref ingred7Quant, value); }
        //}

        //string ingred8Quant;
        //public string Ingred8Quant
        //{
        //    get { return ingred8Quant; }
        //    set { SetProperty(ref ingred8Quant, value); }
        //}

        //string ingred9Quant;
        //public string Ingred9Quant
        //{
        //    get { return ingred9Quant; }
        //    set { SetProperty(ref ingred9Quant, value); }
        //}

        //string ingred10Quant;
        //public string Ingred10Quant
        //{
        //    get { return ingred10Quant; }
        //    set { SetProperty(ref ingred10Quant, value); }
        //}

        //string ingred11Quant;
        //public string Ingred11Quant
        //{
        //    get { return ingred11Quant; }
        //    set { SetProperty(ref ingred11Quant, value); }
        //}

        //string ingred12Quant;
        //public string Ingred12Quant
        //{
        //    get { return ingred12Quant; }
        //    set { SetProperty(ref ingred12Quant, value); }
        //}

        //string ingred13Quant;
        //public string Ingred13Quant
        //{
        //    get { return ingred13Quant; }
        //    set { SetProperty(ref ingred13Quant, value); }
        //}

        //string ingred14Quant;
        //public string Ingred14Quant
        //{
        //    get { return ingred14Quant; }
        //    set { SetProperty(ref ingred14Quant, value); }
        //}

        //string ingred15Quant;
        //public string Ingred15Quant
        //{
        //    get { return ingred15Quant; }
        //    set { SetProperty(ref ingred15Quant, value); }
        //}

        //string ingred16Quant;
        //public string Ingred16Quant
        //{
        //    get { return ingred16Quant; }
        //    set { SetProperty(ref ingred16Quant, value); }
        //}

        //string ingred17Quant;
        //public string Ingred17Quant
        //{
        //    get { return ingred17Quant; }
        //    set { SetProperty(ref ingred17Quant, value); }
        //}

        //string ingred18Quant;
        //public string Ingred18Quant
        //{
        //    get { return ingred18Quant; }
        //    set { SetProperty(ref ingred18Quant, value); }
        //}

        //string ingred19Quant;
        //public string Ingred19Quant
        //{
        //    get { return ingred19Quant; }
        //    set { SetProperty(ref ingred19Quant, value); }
        //}

        //string ingred20Quant;
        //public string Ingred20Quant
        //{
        //    get { return ingred20Quant; }
        //    set { SetProperty(ref ingred20Quant, value); }
        //}

        //string ingred21Quant;
        //public string Ingred21Quant
        //{
        //    get { return ingred21Quant; }
        //    set { SetProperty(ref ingred21Quant, value); }
        //}

        //string ingred22Quant;
        //public string Ingred22Quant
        //{
        //    get { return ingred22Quant; }
        //    set { SetProperty(ref ingred22Quant, value); }
        //}

        //string ingred23Quant;
        //public string Ingred23Quant
        //{
        //    get { return ingred23Quant; }
        //    set { SetProperty(ref ingred23Quant, value); }
        //}

        //string ingred24Quant;
        //public string Ingred24Quant
        //{
        //    get { return ingred24Quant; }
        //    set { SetProperty(ref ingred24Quant, value); }
        //}

        //string ingred25Quant;
        //public string Ingred25Quant
        //{
        //    get { return ingred25Quant; }
        //    set { SetProperty(ref ingred25Quant, value); }
        //}

        //string ingred26Quant;
        //public string Ingred26Quant
        //{
        //    get { return ingred26Quant; }
        //    set { SetProperty(ref ingred26Quant, value); }
        //}

        //string ingred27Quant;
        //public string Ingred27Quant
        //{
        //    get { return ingred27Quant; }
        //    set { SetProperty(ref ingred27Quant, value); }
        //}

        //string ingred28Quant;
        //public string Ingred28Quant
        //{
        //    get { return ingred28Quant; }
        //    set { SetProperty(ref ingred28Quant, value); }
        //}

        //string ingred29Quant;
        //public string Ingred29Quant
        //{
        //    get { return ingred29Quant; }
        //    set { SetProperty(ref ingred29Quant, value); }
        //}

        //string ingred30Quant;
        //public string Ingred30Quant
        //{
        //    get { return ingred30Quant; }
        //    set { SetProperty(ref ingred30Quant, value); }
        //}

        //string ingred31Quant;
        //public string Ingred31Quant
        //{
        //    get { return ingred31Quant; }
        //    set { SetProperty(ref ingred31Quant, value); }
        //}

        //string ingred32Quant;
        //public string Ingred32Quant
        //{
        //    get { return ingred32Quant; }
        //    set { SetProperty(ref ingred32Quant, value); }
        //}

        //string ingred33Quant;
        //public string Ingred33Quant
        //{
        //    get { return ingred33Quant; }
        //    set { SetProperty(ref ingred32Quant, value); }
        //}

        //string ingred34Quant;
        //public string Ingred34Quant
        //{
        //    get { return ingred34Quant; }
        //    set { SetProperty(ref ingred34Quant, value); }
        //}

        //string ingred35Quant;
        //public string Ingred35Quant
        //{
        //    get { return ingred35Quant; }
        //    set { SetProperty(ref ingred35Quant, value); }
        //}

        //string ingred36Quant;
        //public string Ingred36Quant
        //{
        //    get { return ingred36Quant; }
        //    set { SetProperty(ref ingred36Quant, value); }
        //}

        //string ingred37Quant;
        //public string Ingred37Quant
        //{
        //    get { return ingred37Quant; }
        //    set { SetProperty(ref ingred37Quant, value); }
        //}

        //string ingred38Quant;
        //public string Ingred38Quant
        //{
        //    get { return ingred38Quant; }
        //    set { SetProperty(ref ingred38Quant, value); }
        //}

        //string ingred39Quant;
        //public string Ingred39Quant
        //{
        //    get { return ingred39Quant; }
        //    set { SetProperty(ref ingred39Quant, value); }
        //}

        //string ingred40Quant;
        //public string Ingred40Quant
        //{
        //    get { return ingred40Quant; }
        //    set { SetProperty(ref ingred40Quant, value); }
        //}

        //string ingred41Quant;
        //public string Ingred41Quant
        //{
        //    get { return ingred41Quant; }
        //    set { SetProperty(ref ingred41Quant, value); }
        //}

        //string ingred42Quant;
        //public string Ingred42Quant
        //{
        //    get { return ingred42Quant; }
        //    set { SetProperty(ref ingred42Quant, value); }
        //}

        //string ingred43Quant;
        //public string Ingred43Quant
        //{
        //    get { return ingred43Quant; }
        //    set { SetProperty(ref ingred43Quant, value); }
        //}

        //string ingred44Quant;
        //public string Ingred44Quant
        //{
        //    get { return ingred44Quant; }
        //    set { SetProperty(ref ingred44Quant, value); }
        //}

        //string ingred45Quant;
        //public string Ingred45Quant
        //{
        //    get { return ingred45Quant; }
        //    set { SetProperty(ref ingred45Quant, value); }
        //}

        //string ingred46Quant;
        //public string Ingred46Quant
        //{
        //    get { return ingred46Quant; }
        //    set { SetProperty(ref ingred46Quant, value); }
        //}

        //string ingred47Quant;
        //public string Ingred47Quant
        //{
        //    get { return ingred47Quant; }
        //    set { SetProperty(ref ingred47Quant, value); }
        //}

        //string ingred48Quant;
        //public string Ingred48Quant
        //{
        //    get { return ingred48Quant; }
        //    set { SetProperty(ref ingred48Quant, value); }
        //}

        //string ingred49Quant;
        //public string Ingred49Quant
        //{
        //    get { return ingred49Quant; }
        //    set { SetProperty(ref ingred49Quant, value); }
        //}

        //string ingred50Quant;
        //public string Ingred50Quant
        //{
        //    get { return ingred50Quant; }
        //    set { SetProperty(ref ingred50Quant, value); }
        //}

        //#endregion
    }
}
