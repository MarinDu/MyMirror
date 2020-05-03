// -----------------------------------------------------------------------
// <copyright file="SpotifySettings.cs">
//
// </copyright>
// <summary>Contains class SpotifySettings</summary>
// -----------------------------------------------------------------------


namespace SpotifyWidget.Model
{
    using System;
    using Common.Settings;
    using Common.Settings.Items;
    using SpotifyWidget.Properties;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class SpotifySettings : SettingBase
    {
        /// <summary>
        /// Gets or Sets the cient Id
        /// </summary>
        public StringSettingItem ClientId { get; set; }

        /// <inheritdoc />
        protected override Type GetResources()
        {
            return typeof(Resources);
        }
    }
}