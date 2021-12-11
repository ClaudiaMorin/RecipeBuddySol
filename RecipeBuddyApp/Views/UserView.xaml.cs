
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RecipeBuddy.ViewModels;
using RecipeBuddy.Views;

namespace RecipeBuddy.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Page
    {
        public RecipeTreeView recipeTreeView;
        public UserView()
        {
            try
            {
                InitializeComponent();
                DataContext = UserViewModel.Instance;
                //SavedRecipesTreeView.DataContext = MainNavTreeViewModel.Instance;
            }
            catch (System.Exception e)
            {
                string stuff = "stuf";
            }

        }

        /// <summary>
        /// Password stuff is very hard to move into the ViewModel because there isn't a dependancy property I can tweek and
        /// I don't want to do an end-run around the securestring stuff that would compromise security so it is still in the code-behind.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pBox = sender as PasswordBox;
            //PasswordBoxAttachedProperties.SetEncryptedPassword(pBox, pBox.Password);
        }

        private void btnLogout_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            btnLogout.IsEnabled = (sender as Button).IsEnabled;
        }

        private void btnLogin_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Button b = sender as Button;

            btnLogin.IsEnabled = b.IsEnabled;
        }

        //private void CheckBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;
        //    if (checkBox.IsEnabled == false)
        //    {
        //        string s = "soo";
        //    }
        //}

        /// <summary>
        /// Has no effect on the password securestring that has already been sent and stored, that is set to null in UserViewModel upon login
        /// This simply clears the password textbox on the UI side.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnLogout_IsEnabledChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        //{
        //    Password.Clear();
        //}

        //private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        //{
        //    Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        //    e.Handled = true;
        //}
    }
}
