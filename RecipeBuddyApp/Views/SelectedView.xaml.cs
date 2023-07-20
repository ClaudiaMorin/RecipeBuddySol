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

            SaveBtn.DataContext = SelectedViewModel.Instance;
            MakeCopyBtn.DataContext = SelectedViewModel.Instance;
            TitleGrid.DataContext = RecipeEditsViewModel.Instance;
            RecipeGrid.DataContext = SelectedViewModel.Instance;
            
            MasterStackPanel.DataContext = RecipeEditsViewModel.Instance;
        }
    }
}
