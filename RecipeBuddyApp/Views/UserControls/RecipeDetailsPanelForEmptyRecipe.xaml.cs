using RecipeBuddy.ViewModels;
using RecipeBuddyApp.ViewModels;
using Windows.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RecipeBuddy.Views
{
    public sealed partial class RecipeDetailsPanelForEmptyRecipe : UserControl
    {
        public RecipeDetailsPanelForEmptyRecipe()
        {
            this.InitializeComponent();

            TypesInComboBox.ItemsSource = MainNavTreeViewModel.Instance.CatagoryTypes;

            //All of the Ingredient1 - 50 are keyed to the DataContext of Ingredient1 so leave this.
            //Because rebinding doesn't happen you need to Databind the specific feilds before you do the more
            //general DataContext binding to the entire Grid.
            Ingredient1.DataContext = BlankEntryModel.Instance.recipePanelForNew.recipeCardModel;
            Directions.DataContext = BlankEntryModel.Instance.recipePanelForNew.recipeCardModel;
            //Ingred1Quant.DataContext = BlankEntryModel.Instance.recipePanelForNew;
            MasterStackPanel.DataContext = BlankEntryModel.Instance.recipePanelForNew;

            InitializeComponent();
        }
    }
}
