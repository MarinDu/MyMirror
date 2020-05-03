// -----------------------------------------------------------------------
// <copyright file="LeapMotionSettings.cs">
//
// </copyright>
// <summary>Contains class LeapMotionSettings</summary>
// -----------------------------------------------------------------------


namespace LeapMotionInput
{
    using System;
    using Common.Settings;
    using Common.Settings.Items;
    using global::LeapMotionInput.Properties;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class LeapMotionSettings : SettingBase
    {
        /// <summary>
        /// Gets or Sets the leamotion refresh periode
        /// </summary>
        public IntegerSettingItem RefreshPeriode { get; set; }

        /// <summary>
        /// Gets or Sets the screen X size
        /// </summary>
        public IntegerSettingItem ScreenX { get; set; }

        /// <summary>
        /// Gets or Sets the screen Y size
        /// </summary>
        public IntegerSettingItem ScreenY { get; set; }

        /// <summary>
        /// Gets or Sets the screen high
        /// </summary>
        public IntegerSettingItem ScreenHeight { get; set; }

        /// <summary>
        /// Gets or Sets the delay between actions
        /// </summary>
        public IntegerSettingItem DelayBetweenActions { get; set; }

        /// <inheritdoc />
        protected override Type GetResources()
        {
            return typeof(Resources);
        }
    }
}