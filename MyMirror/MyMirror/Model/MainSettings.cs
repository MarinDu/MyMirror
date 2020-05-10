// -----------------------------------------------------------------------
// <copyright file="MainSettings.cs">
//
// </copyright>
// <summary>Contains class MainSettings</summary>
// -----------------------------------------------------------------------

namespace MyMirror.Model
{
    using Common.Settings;
    using Common.Settings.Items;
    using MyMirror.Properties;
    using System;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class MainSettings : SettingBase
    {
        /// <summary>
        /// Gets or Sets the user name
        /// </summary>
        public StringSettingItem UserName { get; set; }

        /// <summary>
        /// Gets or Sets top left widgets
        /// </summary>
        public ListSettingItem TopLeftWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public ListSettingItem TopWidget { get; set; }

        /// <summary>
        /// Gets or Sets top right widgets
        /// </summary>
        public ListSettingItem TopRightWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public ListSettingItem LeftWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public ListSettingItem RightWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public ListSettingItem BotWidget { get; set; }

        /// <summary>
        /// Gets or Sets sleep widgets
        /// </summary>
        public ListSettingItem SleepWidget { get; set; }

        /// <summary>
        /// Gets or Sets sleep timer
        /// </summary>
        public IntegerSettingItem SleepTimer { get; set; }

        /// <inheritdoc />
        protected override Type GetResources()
        {
            return typeof(Resources);
        }
    }
}
