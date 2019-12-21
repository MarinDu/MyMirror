// -----------------------------------------------------------------------
// <copyright file="MainSettings.cs">
//
// </copyright>
// <summary>Contains class MainSettings</summary>
// -----------------------------------------------------------------------

namespace MyMirror.Model
{
    using System.Collections.Generic;
    using Common.Settings;
    using MyMirror.Properties;

    /// <summary>
    /// Manage application main settings
    /// </summary>
    public class MainSettings : ISettingsBase
    {
        /// <summary>
        /// Gets or Sets top left widgets
        /// </summary>
        public SettingItem<string> TopLeftWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public SettingItem<string> TopWidget { get; set; }

        /// <summary>
        /// Gets or Sets top right widgets
        /// </summary>
        public SettingItem<string> TopRightWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public SettingItem<string> LeftWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public SettingItem<string> RightWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public SettingItem<string> BotWidget { get; set; }

        /// <summary>
        /// Gets or Sets sleep widgets
        /// </summary>
        public SettingItem<string> SleepWidget { get; set; }

        /// <inheritdoc />
        public void GenerateDefaultSettings()
        {
            TopRightWidget = new SettingItem<string>
            {
                Name = "TopRightWidget"
            };
            TopWidget = new SettingItem<string>
            {
                Name = "TopWidget"
            };
            TopLeftWidget = new SettingItem<string>
            {
                Name = "TopLeftWidget"
            };
            LeftWidget = new SettingItem<string>
            {
                Name = "LeftWidget"
            };
            RightWidget = new SettingItem<string>
            {
                Name = "RightWidget"
            };
            BotWidget = new SettingItem<string>
            {
                Name = "BotWidget"
            };
            SleepWidget = new SettingItem<string>
            {
                Name = "SleepWidget"
            };
        }

        /// <inheritdoc />
        public List<SettingItem<string>> GetSettingsList()
        {

            List <SettingItem<string>> ret =  new List<SettingItem<string>>
            {
                TopLeftWidget.SettingToString(),
                TopWidget.SettingToString(),
                TopRightWidget.SettingToString(),
                LeftWidget.SettingToString(),
                RightWidget.SettingToString(),
                BotWidget.SettingToString(),
                SleepWidget.SettingToString(),
            };

            foreach(SettingItem<string> setting in ret)
            {
                TranslateSetting(setting);
            }

            return ret;
        }

        /// <inheritdoc />
        public void SetSettingsList(List<SettingItem<string>> settings)
        {
            if(settings?.Count == 7)
            {
                TopLeftWidget = settings[0];
                TopWidget = settings[1];
                TopRightWidget = settings[2];
                LeftWidget = settings[3];
                RightWidget = settings[4];
                BotWidget = settings[5];
                SleepWidget = settings[6];
            }
        }

        /// <summary>
        /// Translate a setting name
        /// </summary>
        /// <param name="setting">Setting</param>
        private void TranslateSetting(SettingItem<string> setting)
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
