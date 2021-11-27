using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using RecipeBuddy.ViewModels;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for RecipeDetailsPanelTextBoxCopyOnly.xaml
    /// </summary>
    public partial class RecipeDetailsPanelTextBoxCopyOnly : UserControl
    {
        public object ContextMenuSender;
        public RecipeDetailsPanelTextBoxCopyOnly()
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
