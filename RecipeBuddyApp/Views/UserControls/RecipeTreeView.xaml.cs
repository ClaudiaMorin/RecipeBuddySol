﻿using RecipeBuddy.Core;
using RecipeBuddy.ViewModels;
using System.Windows;
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
using System;

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

        private void TreeViewItem_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            TreeViewItem treeViewItem = sender as TreeViewItem;
            RecipeTreeItem recipeTreeItem = (RecipeTreeItem)treeViewItem.DataContext;

            if (treeViewItem.ContextFlyout == null && recipeTreeItem.RecipeModelTV.TypeAsInt != 19)
            {
                treeViewItem.ContextFlyout = TreeViewItemMenu;
                treeViewItem.ContextFlyout.ShowAt((FrameworkElement)sender);
            }
        }
    }
}
