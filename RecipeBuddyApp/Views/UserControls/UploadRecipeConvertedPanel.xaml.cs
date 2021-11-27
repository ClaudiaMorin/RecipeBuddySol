using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using RecipeBuddy;
using RecipeBuddy.ViewModels;
using RecipeBuddy.ViewModels.Commands;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for UploadRecipeConvertedPanel.xaml
    /// </summary>
    public partial class UploadRecipeConvertedPanel : UserControl
    {
        public UploadRecipeConvertedPanel()
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
