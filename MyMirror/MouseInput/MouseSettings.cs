﻿// -----------------------------------------------------------------------
// <copyright file="MouseSettings.cs">
//
// </copyright>
// <summary>Contains class MouseSettings</summary>
// -----------------------------------------------------------------------


namespace MouseInput
{
    using System;
    using System.Collections.Generic;
    using Common.Settings;
    using Common.Settings.Items;
    using global::MouseInput.Properties;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class MouseSettings : SettingBase
    {
        /// <summary>
        /// Gets or Sets the leamotion refresh periode
        /// </summary>
        public IntegerSettingItem RefreshPeriode { get; set; }

        /// <inheritdoc />
        protected override Type GetResources()
        {
            return typeof(Resources);
        }
    }
}