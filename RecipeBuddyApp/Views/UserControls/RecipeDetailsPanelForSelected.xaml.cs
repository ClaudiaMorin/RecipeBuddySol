using RecipeBuddy.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for RecipeDetailsPanelForMakeIt.xaml
    /// </summary>
    public partial class RecipeDetailsPanelForSelected : UserControl
    {

        public RecipeDetailsPanelForSelected()
        {
            InitializeComponent();
            TypesInComboBox.ItemsSource = MainNavTreeViewModel.Instance.CatagoryTypes;
            Author.DataContext = SelectedViewModel.Instance.SelectViewMainRecipeCardModel;
            //EditRowType.DataContext = SelectedViewModel.Instance;
            RecipeType.DataContext = SelectedViewModel.Instance.SelectViewMainRecipeCardModel;

            Ingred1.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred2.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred3.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred4.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred5.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred6.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred7.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred8.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred9.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred10.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred11.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred12.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred13.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred14.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred15.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred16.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred17.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred18.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred19.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred20.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred21.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred22.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred23.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred24.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred25.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred26.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred27.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred28.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred29.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred30.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred31.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred32.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred33.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred34.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred35.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred36.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred37.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred38.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred39.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred40.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred41.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred42.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred43.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred44.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred45.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred46.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred47.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred48.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred49.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Ingred50.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;

            Dir1.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir2.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir3.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir4.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir5.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir6.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir7.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir8.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir9.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir10.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir11.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir12.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir13.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir14.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir15.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir16.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir17.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir18.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir19.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir20.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir21.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir22.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir23.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir24.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir25.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir26.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir27.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir28.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir29.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            Dir30.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
        }
    }
}
