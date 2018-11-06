using BusinessLogic.ViewModel.TreeViewItems;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ConsoleApplication.Converters
{
    [ValueConversion(typeof(TreeViewItem), typeof(string))]
    public class TreeViewItemToStringConverter : IValueConverter
    {
        public static TreeViewItemToStringConverter Instance = new TreeViewItemToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();
            string typeString = type == typeof(TreeViewAssembly) ? "Assembly" :
                type == typeof(TreeViewMethod) ? "Method" :
                type == typeof(TreeViewNamespace) ? "Namespace" :
                type == typeof(TreeViewParameter) ? "Parameter" :
                type == typeof(TreeViewProperty) ? "Property" :
                type == typeof(TreeViewType) ? "Type" : "";
            return '['+ typeString + ']';

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}