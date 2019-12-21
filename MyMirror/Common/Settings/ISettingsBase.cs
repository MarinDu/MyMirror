// -----------------------------------------------------------------------
// <copyright file="ISettingsBase.cs">
//
// </copyright>
// <summary>Contains interface ISettingsBase</summary>
// -----------------------------------------------------------------------

namespace Common.Settings
{
    using System.Collections.Generic;

    /// <summary>
    /// Base class for settings
    /// </summary>
    public interface ISettingsBase
    {
        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Return all settings</returns>
        List<SettingItem<string>> GetSettingsList();

        /// <summary>
        /// Sets all settings
        /// </summary>
        /// <param name="settings"></param>
        void SetSettingsList(List<SettingItem<string>> settings);

        /// <summary>
        /// Set settings to defaut values
        /// </summary>
        void GenerateDefaultSettings();
    }
}
