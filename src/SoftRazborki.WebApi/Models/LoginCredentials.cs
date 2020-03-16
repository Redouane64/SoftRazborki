namespace SoftRazborki.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentValidation;

    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginCredentialsValidator : AbstractValidator<LoginCredentials>
    {
        public LoginCredentialsValidator()
        {
            this.RuleFor(m => m.Username)
                .NotEmpty();

            this.RuleFor(m => m.Password)
                .NotEmpty();
        }
    }
}
