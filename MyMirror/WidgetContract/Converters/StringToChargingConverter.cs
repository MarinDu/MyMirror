// -----------------------------------------------------------------------
// <copyright file="StringToChargingConverter.cs">
//
// </copyright>
// <summary>Detects null or empty string and covert them to "Charging"</summary>
// -----------------------------------------------------------------------

namespace WingetContract.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using WingetContract.Properties;

    /// <summary>
    /// Detects null or empty string and covert them to "Charging"
    /// </summary>
    public class StringToChargingConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ret = (string)value;
            ret = string.IsNullOrEmpty(ret) ? Resources.ChargingText : ret;

            return ret;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
