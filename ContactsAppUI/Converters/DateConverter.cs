using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ContactsAppUI
{
    /// <summary>
    /// Класс конвертера даты для отображения на текстовом поле.
    /// </summary>
    public class DateConverter : IValueConverter
    {
        /// <summary>
        /// Метод конвертации.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Метод обратной конвертации.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
