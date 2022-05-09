
using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.ViewModels
{
    
    public sealed class MainWindowViewModel : ObservableObject
    {

        public enum Tabs { SearchTab, WebViewTab, SelectedTab,  UserTab };


        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        private static readonly MainWindowViewModel instance = new MainWindowViewModel();
        
        public MainNavTreeViewModel mainTreeViewNav;
        
        string basketTabName;
        static int selectedTabIndexInt;

        private MainWindowViewModel()
        {
            try
            {
                selectedTabIndexInt = (int)MainWindowViewModel.Tabs.UserTab;
                titleAndVersion = "RecipeBuddy    version ";
            }
            catch (Exception e)
            {
                string stuff = "stuff";
            }

            //AddVersionNumber();
            //CheckForUpdates();
        }


        /// <summary>
        /// Checking for updates to the app and download them.
        /// </summary>
        /// <returns>manager updates that are pending</returns>
        //private async Task CheckForUpdates()
        //{
        //    using (var manager = new UpdateManager(@"C:\Temp\Releases"))
        //    {
        //        await manager.UpdateApp();
        //    }
        //}

        //private void AddVersionNumber()
        //{
        //    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        //    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        //    TitleAndVersion = TitleAndVersion + versionInfo.FileVersion;
        //}
        public static MainWindowViewModel Instance
        { get { return instance; } }


        #region properties for page controls
        public int SelectedTabIndex
        {
            get { return selectedTabIndexInt; }

            set { SetProperty(ref selectedTabIndexInt, value); }
        }

        public string titleAndVersion;
        public string TitleAndVersion
        {
            get { return titleAndVersion; }

            set { SetProperty(ref titleAndVersion, value); }
        }

        //public  SearchPage;
        //public string TitleAndVersion
        //{
        //    get { return titleAndVersion; }

        //    set
        //    {
        //        titleAndVersion = value;
        //        OnPropertyChanged(TitleAndVersion);
        //    }
        //}

        public string BasketTabName
        {
            get { return basketTabName; }
            set { SetProperty(ref basketTabName, value); }
        }

        #endregion


        /// <summary>
        /// Will always be true.
        /// </summary>
        public bool CanChangeView
        {get{return true;}}

    }
}
