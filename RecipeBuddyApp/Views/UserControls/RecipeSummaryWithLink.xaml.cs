using System.Diagnostics;
using RecipeBuddy.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for RecipeSummaryWithLink.xaml
    /// </summary>
    public partial class RecipeSummaryWithLink : UserControl
    {
        public RecipeSummaryWithLink()
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
