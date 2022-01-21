using System;
using System.Windows.Input;
using RecipeBuddy.ViewModels;
using Windows.UI.Xaml.Controls;

namespace RecipeBuddy.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {

        //public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            try
            {
                InitializeComponent();
                DataContext = ShellViewModel.Instance;
                ShellViewModel.Instance.Initialize(shellFrame, navigationView, KeyboardAccelerators);
                cmdInvokeCommand.Command = ShellViewModel.Instance.ItemInvokedCommand;
                cmdLoad.Command = ShellViewModel.Instance.LoadedCommand;

            }
            catch (Exception e)
            {
            }
        }
    }
}
