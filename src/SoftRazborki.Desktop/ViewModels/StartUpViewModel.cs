namespace SoftRazborki.Desktop.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Regions;

    public class StartUpViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private string title;

        public StartUpViewModel(IRegionManager regionManager)
        {
            Title = "Start Up | Sign In or Sign up.";
            this.regionManager = regionManager;
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public DelegateCommand SignInCommand
        {
            get
            {
                return new DelegateCommand(SignIn);
            }
        }

        public DelegateCommand SignUpCommand
        {
            get
            {
                return new DelegateCommand(SignUp);
            }
        }

        private void SignIn()
        {
            regionManager.RequestNavigate(MainWindowViewModel.MainContentRegion, "SignIn");
        }

        private void SignUp()
        {

        }
    }
}
