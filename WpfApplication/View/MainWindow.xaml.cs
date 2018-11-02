using System.Windows;
using BusinessLogic.ViewModel;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM()
            {
                //TODO: Create Pathfinder for WPF
                PathFinder = null
            };
        }
    }
}
