using RecipeBuddy.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for RecipeDetailsPanelForMakeIt.xaml
    /// </summary>
    public partial class RecipeDetailsPanelForSelected : UserControl
    {

        public RecipeDetailsPanelForSelected()
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
