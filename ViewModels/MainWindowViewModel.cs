namespace OthelloDesktop.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public MainWindowViewModel()
        {
            // Start at the menu
            CurrentViewModel = new MenuViewModel(this);
        }
    }
}
