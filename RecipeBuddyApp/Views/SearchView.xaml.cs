using RecipeBuddy.ViewModels;
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
using Windows.UI.Core;
using System;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : Page
    {

        public SearchView()
        {
            InitializeComponent();

            SearchViewItems.DataContext = SearchViewModel.Instance;
            RecipesInComboBox.ItemsSource = SearchViewModel.Instance.listOfRecipeCards.RecipesList;

            ///Search1Panel Panel Setup
            Title1.DataContext = SearchViewModel.Instance.recipePanelForSearch1.RecipeCard;
            ControlsPanel1.DataContext = SearchViewModel.Instance.recipePanelForSearch1;
            ListControls1.DataContext = SearchViewModel.Instance.recipePanelForSearch1.ListOfRecipeModels;

            ///Search2Panel Panel Setup
            Title2.DataContext = SearchViewModel.Instance.recipePanelForSearch2.RecipeCard;
            ControlsPanel2.DataContext = SearchViewModel.Instance.recipePanelForSearch2;
            ListControls2.DataContext = SearchViewModel.Instance.recipePanelForSearch2.ListOfRecipeModels;


            /////Search3Panel Panel Setup
            Title3.DataContext = SearchViewModel.Instance.recipePanelForSearch3.RecipeCard;
            ControlsPanel3.DataContext = SearchViewModel.Instance.recipePanelForSearch3;
            ListControls3.DataContext = SearchViewModel.Instance.recipePanelForSearch3.ListOfRecipeModels;

        }
    }
}

