using AutoFoam.UI.Services.Dialog;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AutoFoam.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogsService _dialogsService;

        private double _inletSpeed;

        public double InletSpeed
        {
            get => _inletSpeed;

            set 
            {
                var a = SetProperty(ref _inletSpeed, value);
            }
        }

        [RelayCommand]
        public async Task SaveProject()
        {
            await _dialogsService.SaveProject();
        }

        private void SetInitialParameters()
        {

        }

        public MainWindowViewModel()
        {
            SetInitialParameters();

            _dialogsService = new DialogsService(this);
        }
    }
}
