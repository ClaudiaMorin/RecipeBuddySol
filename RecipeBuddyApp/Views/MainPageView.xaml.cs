using RecipeBuddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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


namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xam
    /// </summary>
    public partial class MainPageView : Page
    {

        public MainPageView()
        {
            InitializeComponent();
            //DataContext = MainWindowViewModel.Instance;
            
            //TabControl.DataContext = MainWindowViewModel.Instance;
            //SelectedTab.DataContext = SelectedViewModel.Instance;
            //Count.DataContext = WebViewModel.Instance.listOfRecipeCardsModel;
        }
    }
}
