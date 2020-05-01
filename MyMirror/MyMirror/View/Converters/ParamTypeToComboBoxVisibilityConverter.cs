// -----------------------------------------------------------------------
// <copyright file="ParamTypeToComboBoxVisibilityConverter.cs">
//
// </copyright>
// <summary>Converts param type to visibility</summary>
// -----------------------------------------------------------------------

namespace MyMirror.View.Converters
{
    using Common.Settings;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts bool to visibility
    /// </summary>
    internal class ParamTypeToComboBoxVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PamameterValueType val = (PamameterValueType)value;
            return val == PamameterValueType.ListOfInteger || val == PamameterValueType.ListOfString ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}