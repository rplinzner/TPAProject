using System.ComponentModel.Composition;
using System.Windows;
using Interfaces;
using Microsoft.Win32;

namespace WPFPathFinder
{
    [Export((typeof(IPathFinder)))]
    public class WpfPathFinder : IPathFinder
    {
        public string FindPath()
        {
            OpenFileDialog of = new OpenFileDialog
            {
                Filter = "Dynamic Library File(*.dll) | *.dll"+ "| XML File(*.xml) | *.xml",
                RestoreDirectory = true
            };
            of.ShowDialog();
            if (of.FileName.Length == 0)
            {
                MessageBox.Show("No Files were selected", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return of.FileName;
        }
        public string SaveToPath()
        {
            SaveFileDialog sf = new SaveFileDialog {
                Filter = "XML File(*.xml) | *.xml",
                RestoreDirectory = true
            };
            sf.ShowDialog();
            if (sf.FileName.Length == 0)
            {
                MessageBox.Show("File has not been saved.", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return sf.FileName;
        }
    }
}