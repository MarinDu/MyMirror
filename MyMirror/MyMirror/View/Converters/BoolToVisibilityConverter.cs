// -----------------------------------------------------------------------
// <copyright file="BoolToVisibilityConverter.cs">
//
// </copyright>
// <summary>Converts bool to visibility</summary>
// -----------------------------------------------------------------------

namespace MyMirror.View.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts bool to visibility
    /// </summary>
    internal class BoolToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}