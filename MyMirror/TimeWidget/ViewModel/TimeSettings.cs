// -----------------------------------------------------------------------
// <copyright file="TimeSettings.cs">
//
// </copyright>
// <summary>Contains class TimeSettings</summary>
// -----------------------------------------------------------------------


namespace TimeWidget.Model
{
    using System.Collections.Generic;
    using Common.Settings;
    using TimeWidget.Properties;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class TimeSettings : ISettingsBase
    {
        /// <summary>
        /// Gets or Sets the user name
        /// </summary>
        public SettingItem UserName { get; set; }

        /// <summary>
        /// Gets or Sets top left widgets
        /// </summary>
        public SettingItem TopLeftWidget { get; set; }

        /// <inheritdoc />
        public void GenerateDefaultSettings()
        {
            UserName = new SettingItem
            {
                Type = PamameterValueType.FieldString,
                Name = nameof(UserName)
            };
            TopLeftWidget = new SettingItem
            {
                Type = PamameterValueType.ListOfString,
                Name = nameof(TopLeftWidget)
            };
        }

        /// <inheritdoc />
        public List<SettingItem> GetSettingsList()
        {
            List<SettingItem> ret = new List<SettingItem>
            {
                UserName,
                TopLeftWidget
            };

            foreach (SettingItem setting in ret)
            {
                TranslateSetting(setting);
            }

            return ret;
        }

        /// <inheritdoc />
        public void SetSettingsList(List<SettingItem> settings)
        {
            if (settings?.Count == GetSettingsList().Count)
            {
                UserName = settings[0];
                TopLeftWidget = settings[1];
            }
        }

        /// <summary>
        /// Translate a setting name
        /// </summary>
        /// <param name="setting">Setting</param>
        private void TranslateSetting(SettingItem setting)
        {
            try
            {
                setting.Translation = typeof(Resources).GetProperty(setting.Name).GetValue(null) as string ?? string.Empty;
            }
            catch
            {
                setting.Translation = string.Empty;
            }
        }
    }
}