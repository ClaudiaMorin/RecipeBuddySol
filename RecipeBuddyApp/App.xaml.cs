using System;
using System.IO;
using System.Threading.Tasks;
using RecipeBuddy.Services;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace RecipeBuddy
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;
        private readonly double minW = 1000, minH = 600;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
            InitializeAppEnvironment();
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }

            ApplicationView.PreferredLaunchViewSize = new Size(minW, minH);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(minW, minH));
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private async void InitializeAppEnvironment()
        {
            try
            {
                if (!(await AppHelper.ExistsInStorageFolder(AppHelper.localFolder, "RecipeMangerDB.db")))
                {
                    StorageFile defaultDb = await AppHelper.installedLocation.GetFileAsync("Database\\" + "RecipeManagerDB.db");
                    await defaultDb.CopyAsync(AppHelper.localFolder);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            //return new ActivationService(this, typeof(Views.MainPageView), new Lazy<UIElement>(CreateShell));
            return new ActivationService(this, typeof(Views.UserView), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }

    }

    static class AppHelper
    {
        public static StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
        public static StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        public static async Task<bool> ExistsInStorageFolder(this StorageFolder folder, string fileName)
        {
            try
            {
                await folder.GetFileAsync(fileName);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
    }




}
