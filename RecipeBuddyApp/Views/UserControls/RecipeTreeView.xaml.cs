using RecipeBuddy.ViewModels;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace RecipeBuddy.Views
{
    public sealed partial class RecipeTreeView : UserControl
    {
        public ObservableCollection<RecipeTreeItem> RecipeTreeRoot = new ObservableCollection<RecipeTreeItem>();
        public RecipeTreeView()
        {
            this.InitializeComponent();
            SavedRecipesTreeRoot.DataContext = MainNavTreeViewModel.Instance;
        }

        /// <summary>
        /// Creates the context menu based on what the TreeViewItem contains.  The recipe type needs to not be
        /// "19" which is a header-type, not an actual recipe.  The first time this happens the context menu is added to the TreeViewItem node,
        /// the second time it is automatically triggered and doesn't come to this function at all.
        /// </summary>
        /// <param name="sender">The UI element, which is cast to a TreeViewItem type</param>
        /// <param name="args">Not used.</param>
        private void TreeViewItem_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            TreeViewItem treeViewItem = sender as TreeViewItem;
            RecipeTreeItem recipeTreeItem = (RecipeTreeItem)treeViewItem.DataContext;

            if (recipeTreeItem.RecipeModelTV.TypeAsInt != 19)
            {
                treeViewItem.ContextFlyout = TreeViewItemMenu;
                treeViewItem.ContextFlyout.ShowAt((FrameworkElement)sender);
            }
        }
    }
}
