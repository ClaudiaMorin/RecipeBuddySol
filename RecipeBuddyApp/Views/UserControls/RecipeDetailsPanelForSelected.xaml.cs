using RecipeBuddy.ViewModels;
using RecipeBuddyApp.ViewModels;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;


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
            TypesInComboBox.DataContext = RecipeEditsViewModel.Instance;
            TypeTextBlock.DataContext = RecipeEditsViewModel.Instance;
            TypeAuthorRow.DataContext = SelectedViewModel.Instance;
            RecipeHeaderGrid.DataContext = RecipeEditsViewModel.Instance;

            IngredQuant1BLK.DataContext = SelectedViewModel.Instance;
            Ingred1.DataContext = RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel;
            Dir1.DataContext = RecipeEditsViewModel.Instance.selectViewMainRecipeCardModel;

            //DataContext = RecipeEditsViewModel.Instance;

        }

    }
}
