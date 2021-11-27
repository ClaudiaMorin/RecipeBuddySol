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

            DataContext = SearchViewModel.Instance;

            ///Search1Panel Panel Setup
            Title1.DataContext = SearchViewModel.Instance.recipePanelForSearch1;
            ListControls1.DataContext = SearchViewModel.Instance.recipePanelForSearch1.listOfRecipeBlurbModel;
            DetailsPanelView1.DataContext = SearchViewModel.Instance.recipePanelForSearch1;
            btnBack1.DataContext = SearchViewModel.Instance.recipePanelForSearch1;
            btnNext1.Command = SearchViewModel.Instance.recipePanelForSearch1.CmdNextButton;
            btnSelect1.Command = SearchViewModel.Instance.recipePanelForSearch1.CmdSelectButton;

            ///Search2Panel Panel Setup
            Title2.DataContext = SearchViewModel.Instance.recipePanelForSearch2;
            ListControls2.DataContext = SearchViewModel.Instance.recipePanelForSearch2.listOfRecipeBlurbModel;
            DetailsPanelView2.DataContext = SearchViewModel.Instance.recipePanelForSearch2;
            btnBack2.Command = SearchViewModel.Instance.recipePanelForSearch2.CmdBackButton;
            btnNext2.Command = SearchViewModel.Instance.recipePanelForSearch2.CmdNextButton;
            btnSelect2.Command = SearchViewModel.Instance.recipePanelForSearch2.CmdSelectButton;

            ///Search3Panel Panel Setup
            Title3.DataContext = SearchViewModel.Instance.recipePanelForSearch3;
            ListControls3.DataContext = SearchViewModel.Instance.recipePanelForSearch3.listOfRecipeBlurbModel;
            DetailsPanelView3.DataContext = SearchViewModel.Instance.recipePanelForSearch3;
            btnBack3.Command = SearchViewModel.Instance.recipePanelForSearch3.CmdBackButton;
            btnNext3.Command = SearchViewModel.Instance.recipePanelForSearch3.CmdNextButton;
            btnSelect3.Command = SearchViewModel.Instance.recipePanelForSearch3.CmdSelectButton;

            
        }
    }
}

