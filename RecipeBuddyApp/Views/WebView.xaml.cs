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

            //Wires the combobox to the list of recipes
            RecipesInComboBox.ItemsSource = SearchViewModel.Instance.listOfRecipeCards.RecipesList;
           
        }

    }
}
