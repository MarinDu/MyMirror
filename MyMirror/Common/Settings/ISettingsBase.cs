 // -----------------------------------------------------------------------
// <copyright file="ISettingsBase.cs">
//
// </copyright>
// <summary>Contains interface ISettingsBase</summary>
// -----------------------------------------------------------------------

namespace Common.Settings
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base class for settings
    /// </summary>
    public interface ISettingsBase
    {
        /// <summary>
        /// Event raised when settings are updated
        /// </summary>
        event EventHandler<EventArgs> SettingsUpdated;

        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Return all settings</returns>
        List<ISettingItemBase> GetSettingsList();

        /// <summary>
        /// Sets all settings
        /// </summary>
        /// <param name="settings"></param>
        void SetSettingsList(List<ISettingItemBase> settings);

        /// <summary>
        /// Set settings to defaut values
        /// </summary>
        void GenerateDefaultSettings();
    }
}
