// -----------------------------------------------------------------------
// <copyright file="WeatherSettings.cs">
//
// </copyright>
// <summary>Contains class WeatherSettings</summary>
// -----------------------------------------------------------------------

namespace WeatherWidget.Model
{
    using System;
    using Common.Settings;
    using Common.Settings.Items;
    using WeatherWidget.Properties;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class WeatherSettings : SettingBase
    {
        /// <summary>
        /// Gets or Sets the pull periode
        /// </summary>
        public IntegerSettingItem PullPeriode { get; set; }

        /// <summary>
        /// Gets or Sets the api address
        /// </summary>
        public StringSettingItem LinkAddress { get; set; }

        /// <summary>
        /// Gets or Sets use api id
        /// </summary>
        public StringSettingItem ApiId { get; set; }

        /// <summary>
        /// Gets or Sets city id
        /// </summary>
        public StringSettingItem CityId { get; set; }

        /// <inheritdoc />
        protected override Type GetResources()
        {
            return typeof(Resources);
        }
    }
}