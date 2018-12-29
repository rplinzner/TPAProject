using System.Windows;
using Interfaces;
using Microsoft.Win32;

namespace WPFPathFinder
{
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
    }
}