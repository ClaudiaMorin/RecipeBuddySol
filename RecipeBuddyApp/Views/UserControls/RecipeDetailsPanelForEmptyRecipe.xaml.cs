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
            DataContext = WebViewModel.Instance.recipePanelForWebCopy;
            

            ComboBoxMeasure1Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure2Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure3Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure4Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure5Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure6Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure7Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure8Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure9Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure10Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure11Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure12Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure13Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure14Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure15Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure16Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure17Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure18Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure19Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure20Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure21Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure22Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure23Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure24Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure25Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure26Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure27Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure28Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure29Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure30Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure31Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure32Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure33Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure34Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure35Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure36Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure37Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure38Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure39Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure40Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure41Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure42Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure43Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure44Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure45Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure46Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure47Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure48Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure49Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
            ComboBoxMeasure50Types.ItemsSource = WebViewModel.Instance.recipePanelForWebCopy.MeasureTypes;
        }
    }
}
