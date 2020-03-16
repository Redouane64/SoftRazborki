namespace SoftRazborki.Desktop.ViewModels
{

    using Prism.Mvvm;
    using Prism.Regions;
    using SoftRazborki.Desktop.Views;

    public class MainWindowViewModel : BindableBase
    {
        public static string MainContentRegionName
        {
            get
            {
                return "MainContentRegionName";
            }
        }

        private string title;

        private bool isSignedIn = false;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            Title = "SoftRazborki";

            if (!isSignedIn)
            {
                regionManager.RegisterViewWithRegion(MainContentRegionName, typeof(SignIn));
            }
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}
