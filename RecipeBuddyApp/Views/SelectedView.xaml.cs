using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RecipeBuddy.ViewModels;
using RecipeBuddy.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for Selected.xaml
    /// </summary>
    public partial class SelectedView : Page
    {
        public SelectedView()
        {

            InitializeComponent();
            //DetailsPanelForSelected.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;
            DetailsPanelForSelected.DataContext = SelectedViewModel.Instance;
           
            TitleGrid.DataContext = SelectedViewModel.Instance;
            Title.DataContext = SelectedViewModel.Instance.selectViewMainRecipeCardModel;



            //DetailsPanelForSelected.NumXComboBox.DataContext = SelectedViewModel.Instance;
            //ControlsForSelectView.DataContext = SelectedViewModel.Instance;
            ////Wires the combobox to the list of recipes 
            //RecipesInComboBox.ItemsSource = SelectedViewModel.Instance.listOfRecipeModel.RecipesList;




        }
    }
}
