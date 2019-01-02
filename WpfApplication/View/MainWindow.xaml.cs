using System.Windows;
using Composition;
using ViewModel.Windows;

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
            MainWindowVM vm = new MainWindowVM();
            Compose.Instance.ComposeParts(vm);
            DataContext = vm;

        }
    }
}
