namespace SoftRazborki.Desktop.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Prism.Mvvm;

    internal abstract class ValidatableViewModel : BindableBase, INotifyDataErrorInfo
    {

        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get
            {
                return _errors.Any();
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.TryGetValue(propertyName, out List<string> errors))
            {
                return errors;
            }

            return null;
        }

        protected bool Validate<T>(ref T property, [CallerMemberName]string propertyName = null)
        {
            ValidationContext context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            List<ValidationResult> result = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(property, context, result))
            {
                _errors.TryAdd(propertyName, result.Select(r => r.ErrorMessage).ToList());

                EventHandler<DataErrorsChangedEventArgs> handler = ErrorsChanged;
                handler?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

                return false;
            }
            else
            {
                _errors.Remove(propertyName);
            }

            return true;
        }
    }
}
