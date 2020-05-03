// -----------------------------------------------------------------------
// <copyright file="MainSettings.cs">
//
// </copyright>
// <summary>Contains class WeatherSettings</summary>
// -----------------------------------------------------------------------


namespace NewsWidget.Model
{
    using System;
    using Common.Settings;
    using Common.Settings.Items;
    using NewsWidget.Properties;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class NewsSettings : SettingBase
    {
        /// <summary>
        /// Gets or Sets the news pull frequency
        /// </summary>
        public IntegerSettingItem NewsPullFrequency { get; set; }
        
        /// <summary>
        /// Gets or Sets the news refresh frequency
        /// </summary>
        public IntegerSettingItem NewsRefreshFrequency { get; set; }  
        
        /// <summary>
        /// Gets or Sets the news feed url
        /// </summary>
        public StringSettingItem NewsFeedUrl { get; set; }

        /// <inheritdoc />
        protected override Type GetResources()
        {
            return typeof(Resources);
        }
    }
}