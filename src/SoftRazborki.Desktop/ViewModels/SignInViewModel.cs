namespace SoftRazborki.Desktop.ViewModels
{
    using Prism.Commands;
    using System.ComponentModel.DataAnnotations;

    internal class SignInViewModel : ValidatableViewModel
    {

        private string email;
        private bool rememberMe;

        public SignInViewModel()
        {
            SignInCommand = new DelegateCommand<string>(SignIn, CanSignIn)
                .ObservesProperty(() => Email);
        }

        [Required]
        [EmailAddress]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetProperty(ref email, value);
                _ = Validate(ref email);
            }
        }

        public bool RememberMe
        {
            get
            {
                return rememberMe;
            }
            set
            {
                SetProperty(ref rememberMe, value);
            }
        }

        public DelegateCommand<string> SignInCommand { get; private set; }

        private bool CanSignIn(string password)
        {
            return !HasErrors && !string.IsNullOrEmpty(Email);
        }

        private void SignIn(string password)
        {

        }


    }
}
