using System.Windows.Input;

namespace BusinessLogic.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private string _pathVariable;
        public ICommand ClickOpen { get; }
        public IPathFinder PathFinder { get; set; }

        public string PathVariable
        {
            get => _pathVariable;
            set
            {
                _pathVariable = value;
                OnPropertyChanged(nameof(PathVariable));
            }
        }

        public MainWindowVM()
        {
            ClickOpen = new RelayCommand(Open);
        }

        private void Open()
        {
            PathVariable = PathFinder.FindPath();
        }
    }
}