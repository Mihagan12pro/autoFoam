using AutoFoam.UI.Services.Dialog;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AutoFoam.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly IDialogsService _dialogsService;

        private readonly Dictionary<string, List<string>> _errors = new();

        private string _inletSpeedText;

        private void SetInitialParameters()
        {
            InletSpeedText = "3";
        }

        private async Task SetInitialParametersAsync()
        {
            InletSpeedText = "3";
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.TryGetValue(propertyName, out var errors))
            {
                errors = new List<string>();
                _errors[propertyName] = errors;
            }

            errors.Add(error);

            ErrorsChanged?.Invoke(
                this,
                new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.Remove(propertyName))
            {
                ErrorsChanged?.Invoke(
                    this,
                    new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private void ValidateDoubleVariable(string propertyName)
        {
            Type type = this.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            var displayNameAttr = propertyInfo.GetCustomAttributes(
                   typeof(DisplayNameAttribute), true)[0];
            
            ClearErrors(propertyName);

            if (propertyInfo != null)
            {
                if (double.TryParse(propertyInfo.GetValue(this).ToString(), out double result))
                {
                    if (result <= 0)
                        AddError(propertyName, $"В поле '{((DisplayNameAttribute)displayNameAttr).DisplayName}' значение должно быть больше нуля!");

                    return;
                }

                AddError(propertyName, $"В поле '{((DisplayNameAttribute)displayNameAttr).DisplayName}' введены некорретные данные!");
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        [DisplayName("Скорость на входе")]
        public string InletSpeedText
        {
            get => _inletSpeedText;

            set 
            {
                if (SetProperty(ref _inletSpeedText, value))
                {
                    ValidateDoubleVariable(nameof(InletSpeedText));
                }
            }
        }

        public bool HasErrors 
            => _errors.Count > 0;

        [RelayCommand]
        public async Task SaveProject()
        {
            await _dialogsService.SaveProject();
        }

        [RelayCommand]
        public async Task Clear()
        {
            await SetInitialParametersAsync();
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
                return Enumerable.Empty<string>();

            return _errors.TryGetValue(propertyName, out var errors)
                ? errors
                : Enumerable.Empty<string>();
        }

        public MainWindowViewModel()
        {
            SetInitialParameters();

            _dialogsService = new DialogsService(this);
        }
    }
}
