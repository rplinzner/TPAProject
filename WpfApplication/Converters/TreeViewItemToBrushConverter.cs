using System;
using System.CodeDom;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using ViewModel.TreeViewItems;

namespace WpfApplication.Converters
{
    [ValueConversion(typeof(TreeViewItem), typeof(Brush))]
    public class TreeViewItemToBrushConverter :IValueConverter
    {
        public static TreeViewItemToBrushConverter Instance = new TreeViewItemToBrushConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();
            return type == typeof(TreeViewAssembly) ? new SolidColorBrush(Colors.BlueViolet) : 
                type == typeof(TreeViewMethod) ? new SolidColorBrush(Colors.Salmon) :
                type == typeof(TreeViewNamespace) ? new SolidColorBrush(Colors.DarkBlue) :
                type == typeof(TreeViewParameter) ? new SolidColorBrush(Colors.SeaGreen) :
                type == typeof(TreeViewProperty) ? new SolidColorBrush(Colors.OrangeRed) :
                type == typeof(TreeViewType) ? new SolidColorBrush(Colors.Fuchsia) : 
                new SolidColorBrush(Colors.Black);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}