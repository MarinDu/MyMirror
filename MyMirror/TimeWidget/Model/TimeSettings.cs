// -----------------------------------------------------------------------
// <copyright file="TimeSettings.cs">
//
// </copyright>
// <summary>Contains class TimeSettings</summary>
// -----------------------------------------------------------------------


namespace TimeWidget.Model
{
    using System;
    using Common.Settings;
    using TimeWidget.Properties;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class TimeSettings : SettingBase
    {
        /// <inheritdoc />
        protected override Type GetResources()
        {
            return typeof(Resources);
        }
    }
}