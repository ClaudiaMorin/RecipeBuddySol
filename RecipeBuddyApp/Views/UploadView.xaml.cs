using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RecipeBuddy.ViewModels;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for UploadView.xaml
    /// </summary>
    public partial class UploadView : Page
    {
        public UploadView()
        {
            //DataContext = UploadViewModel.Instance;
            //InitializeComponent();
            //NewRecipePanel.DataContext = UploadViewModel.Instance.recipeCardViewModelForUpload;
            //NewRecipePanel.TypesInComboBox.ItemsSource = MainNavTreeViewModel.Instance.CatagoryTypes;
            //NewRecipePanel.TypesInComboBox.DataContext = UploadViewModel.Instance;
            //NewRecipePanel.btnSave.DataContext = UploadViewModel.Instance;
        }

    }
}
