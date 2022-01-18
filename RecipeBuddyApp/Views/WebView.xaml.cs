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
            Save.DataContext = WebViewModel.Instance.recipePanelForWebCopy;
            Cancel.DataContext = WebViewModel.Instance.recipePanelForWebCopy;
            MasterStackPanel.DataContext = WebViewModel.Instance;
            WebControl.DataContext = WebViewModel.Instance;
            SavedRecipesTreeView.DataContext = MainNavTreeViewModel.Instance;

            //Wires the combobox to the list of recipes
            RecipesInComboBox.DataContext = SearchViewModel.Instance;
            RecipesInComboBox.ItemsSource = SearchViewModel.Instance.listOfRecipeCards.RecipesList;
        }
    }
}
