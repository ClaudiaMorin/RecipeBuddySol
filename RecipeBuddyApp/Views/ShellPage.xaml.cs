using System;
using RecipeBuddy.ViewModels;
using Windows.UI.Xaml.Controls;

namespace RecipeBuddy.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);

            //DataContext = MainWindowViewModel.Instance;
            //SavedRecipesTreeRoot.DataContext = MainWindowViewModel.Instance.mainTreeViewNav;
            //TabControl.DataContext = MainWindowViewModel.Instance;
            //SelectedTab.DataContext = SelectedViewModel.Instance;
            //Count.DataContext = WebViewModel.Instance.listOfRecipeCardsModel;
        }
    }
}
