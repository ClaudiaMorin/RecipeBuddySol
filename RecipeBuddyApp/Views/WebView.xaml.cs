using Windows.UI.Xaml.Controls;
using RecipeBuddy.ViewModels;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for Selected.xaml
    /// </summary>
    public partial class WebView : Page
    {
        public WebView()
        {
            //InitializeComponent();
            //MasterStackPanel.DataContext = WebViewModel.Instance;
            //Wires the combobox to the list of recipes 
            //RecipesInComboBox.ItemsSource = WebViewModel.Instance.listOfRecipeCardsModel.RecipiesBlurbList;
        }
    }
}
