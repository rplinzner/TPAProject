using System;
using System.CodeDom;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using ViewModel.TreeViewItems;

namespace ConsoleApplication.Converters
{
    [ValueConversion(typeof(TreeViewItem), typeof(Brush))]
    public class TreeViewItemToConsoleColorConverter :IValueConverter
    {
        public static TreeViewItemToConsoleColorConverter Instance = new TreeViewItemToConsoleColorConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();
            return type == typeof(TreeViewAssembly) ? ConsoleColor.Cyan : 
                type == typeof(TreeViewMethod) ? ConsoleColor.DarkMagenta :
                type == typeof(TreeViewNamespace) ? ConsoleColor.Red :
                type == typeof(TreeViewParameter) ? ConsoleColor.Yellow :
                type == typeof(TreeViewProperty) ? ConsoleColor.Green :
                type == typeof(TreeViewType) ? ConsoleColor.Gray :
                ConsoleColor.White;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}