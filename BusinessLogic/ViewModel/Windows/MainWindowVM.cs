using System.Windows.Input;

namespace BusinessLogic.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        public ICommand ClickOpen { get; }
        public IPathFinder PathFinder { get; set; }

        public MainWindowVM()
        {
            ClickOpen = new RelayCommand(Open);
        }

        private void Open()
        {
            //TODO: Implment behavior after open is clicked
        }
    }
}