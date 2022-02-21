using RecipeBuddy.ViewModels;
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
            ICommandSelectTypeChanged.Command = WebViewModel.Instance.CmdSelectedTypeChanged;
            TypesInComboBox.SelectedIndex = 18;
            

            Ingredient1.DataContext = WebViewModel.Instance.recipePanelForWebCopy.recipeCardModel;
            IngredientGrid.DataContext = WebViewModel.Instance.recipePanelForWebCopy;
            DataContext = WebViewModel.Instance.recipePanelForWebCopy.recipeCardModel;
        }
    }
}
