using RecipeBuddy.ViewModels;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
//using System.Windows.Navigation;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for SingleRecipeDetailsPanel.xaml
    /// </summary>
    public partial class RecipeDetailsPanel : UserControl
    {
        public RecipeDetailsPanel()
        {
            InitializeComponent();
        }

        //private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        //{
        //    Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        //    e.Handled = true;
        //}
    }
}
