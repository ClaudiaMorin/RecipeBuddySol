using System.Diagnostics;
using Windows.UI.Xaml.Controls;
//using System.Windows.Navigation;
using RecipeBuddy.ViewModels;
using RecipeBuddy.ViewModels.Commands;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for RecipeDetailsPanelForEdit.xaml
    /// </summary>
    public partial class RecipeDetailsPanelForEdit : UserControl
    {
        public RecipeDetailsPanelForEdit()
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
