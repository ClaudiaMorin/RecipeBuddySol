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

    }
}
