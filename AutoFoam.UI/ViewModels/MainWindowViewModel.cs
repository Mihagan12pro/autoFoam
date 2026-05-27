using AutoFoam.UI.Extensions;
using AutoFoam.UI.Models.FlatChannel;
using AutoFoam.UI.Services.Calculations;
using AutoFoam.UI.Services.Dialog;
using AutoFoam.UI.Services.Shell;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AutoFoam.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly IDialogsService _dialogsService;
        private readonly ICalculationService _culculationService;
        private readonly IShellExecuter _shellExecuter;

        private readonly Dictionary<string, List<string>> _errors = new();

        private string _inletSpeedText, _inletWidthText, _channelHeightText, 
            _legHeightText, _triangleHeightText, _triangleBaseText,
            _outletWidthText, _outletLengthText;

        private bool _startCulculationEnabled;

        private void SetInitialParameters()
        {
            InletSpeedText = "3";
            InletWidthText = "50";
            ChannelHeightText = "100";
            LegHeightText = "30";
            TriangleHeightText = "20";
            TriangleBaseText = "20";
            OutletWidthText = "50";
            OutletLengthText = "120";
        }

        private async Task SetInitialParametersAsync()
        {
            InletSpeedText = "3";
            InletWidthText = "50";
            ChannelHeightText = "100";
            LegHeightText = "30";
            TriangleHeightText = "20";
            TriangleBaseText = "20";
            OutletWidthText = "50";
            OutletLengthText = "120";
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

        [DisplayName("Ширина входа")]
        public string InletWidthText
        {
            get => _inletWidthText;

            set
            {
                if (SetProperty(ref _inletWidthText, value))
                {
                    ValidateDoubleVariable(nameof(InletWidthText));
                }
            }
        }

        [DisplayName("Высота канала")]
        public string ChannelHeightText
        {
            get => _channelHeightText;
            set
            {
                if (SetProperty(ref _channelHeightText, value))
                {
                    ValidateDoubleVariable(nameof(ChannelHeightText));
                }
            }
        }

        [DisplayName("Высота ножки")]
        public string LegHeightText
        {
            get => _legHeightText;
            set
            {
                if (SetProperty(ref _legHeightText, value))
                {
                    ValidateDoubleVariable(nameof(LegHeightText));
                }
            }
        }

        [DisplayName("Высота треугольника")]
        public string TriangleHeightText
        {
            get => _triangleHeightText;
            set
            {
                if (SetProperty(ref _triangleHeightText, value))
                {
                    ValidateDoubleVariable(nameof(TriangleHeightText));
                }
            }
        }

        [DisplayName("Основание треугольника")]
        public string TriangleBaseText
        {
            get => _triangleBaseText;
            set
            {
                if (SetProperty(ref _triangleBaseText, value))
                {
                    ValidateDoubleVariable(nameof(TriangleBaseText));
                }
            }
        }

        [DisplayName("Ширина выхода")]
        public string OutletWidthText
        {
            get => _outletWidthText;
            set
            {
                if (SetProperty(ref _outletWidthText, value))
                {
                    ValidateDoubleVariable(nameof(OutletWidthText));
                }
            }
        }

        [DisplayName("Длина выхода")]
        public string OutletLengthText
        {
            get => _outletLengthText;
            set
            {
                if (SetProperty(ref _outletLengthText, value))
                {
                    ValidateDoubleVariable(nameof(OutletLengthText));
                }
            }
        }

        public bool StartCalculationEnabled
        {
            get => _startCulculationEnabled;
            set { SetProperty(ref _startCulculationEnabled, value); }
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

            _shellExecuter.ExecuteClean();
        }

        public async Task StartCalculations()
        {
            FlatChannel flatChannel = new FlatChannel();

            flatChannel.SetValues(this, 
                    nameof(InletSpeedText),
                    
                    nameof(InletWidthText),
                    
                    nameof(ChannelHeightText),
                    
                    nameof(LegHeightText),
                    
                    nameof(TriangleBaseText),
                    
                    nameof(TriangleHeightText),

                    nameof(OutletLengthText),

                    nameof(OutletWidthText)
                );

            var results = new List<ValidationResult>();

            var context = new ValidationContext(flatChannel);

            if (!Validator.TryValidateObject(flatChannel, context, results, true))
            {
                await _dialogsService.ShowErrors(results.ToStrings());

                return;
            }
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
                return Enumerable.Empty<string>();

            StartCalculationEnabled = !HasErrors;

            return _errors.TryGetValue(propertyName, out var errors)
                ? errors
                : Enumerable.Empty<string>();
        }

        public MainWindowViewModel()
        {
            SetInitialParameters();

            _dialogsService = new DialogsService(this);

            _culculationService = new CalculationService();
            
            _shellExecuter = new FlatChannelExecuter();
        }
    }
}
