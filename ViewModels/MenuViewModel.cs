using OthelloDesktop.Helpers;
using System.Windows;
using System.Windows.Input;

namespace OthelloDesktop.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel _mainVm;

        public ICommand VsCpuCommand { get; }
        public ICommand VsOtherPlayerCommand { get; }
        public ICommand ExitCommand { get; }

        public MenuViewModel(MainWindowViewModel mainVm)
        {
            _mainVm = mainVm;

            VsCpuCommand = new RelayCommand(_ =>
            {
                _mainVm.CurrentViewModel = new PlayerSetupViewModel(_mainVm, true);
            });

            VsOtherPlayerCommand = new RelayCommand(_ =>
            {
                _mainVm.CurrentViewModel = new PlayerSetupViewModel(_mainVm, false);
            });

            ExitCommand = new RelayCommand(_ =>
            {
                Application.Current.Shutdown();
            });
        }
    }
}
