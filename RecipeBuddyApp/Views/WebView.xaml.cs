using Windows.UI.Xaml.Controls;
using RecipeBuddy.ViewModels;
using RecipeBuddy.Core.Models;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for Selected.xaml
    /// </summary>
    public partial class WebView : Page
    {
        public WebView()
        {
            InitializeComponent();
            MasterStackPanel.DataContext = WebViewModel.Instance;
            WebControl.DataContext = WebViewModel.Instance;
            SavedRecipesTreeView.DataContext = MainNavTreeViewModel.Instance;
            //SavedRecipesTreeView.DataContext = MainWindowViewModel.Instance.mainTreeViewNav;
            RecipeDetailsForEdit.DataContext = WebViewModel.Instance.recipePanelForWebCopy.recipeCardModel;
            TypesInComboBox.ItemsSource = MainNavTreeViewModel.Instance.CatagoryTypes;

            //TextBoxRecipe.DataContext = 
            //Wires the combobox to the list of recipes
            RecipesInComboBox.ItemsSource = SearchViewModel.Instance.listOfRecipeCards.RecipesList;
        }
    }
}
