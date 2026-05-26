using System.Collections;
using System.Collections.Generic;

namespace AutoFoam.UI.ViewModels
{
    internal class ErrorsWindowViewModel : ViewModelBase
    {
        private IEnumerable<string> _errors;

        public IEnumerable<string> Errors
            => _errors;

        public ErrorsWindowViewModel(IEnumerable<string> errors)
        {
            _errors = errors;
        }
    }
}
