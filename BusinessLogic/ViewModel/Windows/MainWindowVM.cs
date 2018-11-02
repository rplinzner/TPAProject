using System.Windows.Input;

namespace BusinessLogic.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        public ICommand ClickOpen { get; }
        public IPathFinder PathFinder { get; set; }

        public MainWindowVM()
        {
            
        }
    }
}