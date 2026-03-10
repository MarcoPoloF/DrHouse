using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.Converters
{
    public class SelectedItemChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as SelectedItemChangedEventArgs;
            if (eventArgs == null)
                throw new ArgumentException("Expected SelectedItemChangedEventArgs as value", "value");

            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
