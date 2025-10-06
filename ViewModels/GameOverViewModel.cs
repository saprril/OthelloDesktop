using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OthelloDesktop.Helpers;

namespace OthelloDesktop.ViewModels
{
    public class GameOverViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel _mainVm;
        public string ResultText { get; }
        public ICommand PlayAgainCommand { get; }
        public ICommand ExitCommand { get; }
        public GameOverViewModel(MainWindowViewModel mainVm, string resultText)
        {
            _mainVm = mainVm;
            ResultText = resultText;

            PlayAgainCommand = new RelayCommand(_ =>
            {
                _mainVm.CurrentViewModel = new MenuViewModel(_mainVm);
            });
            ExitCommand = new RelayCommand(_ =>
            {
                System.Windows.Application.Current.Shutdown();
            });
        }
    }
       

}
