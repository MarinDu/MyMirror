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
        /// Gets or Sets the user name
        /// </summary>
        public SettingItem UserName { get; set; }

        /// <summary>
        /// Gets or Sets top left widgets
        /// </summary>
        public SettingItem TopLeftWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public SettingItem TopWidget { get; set; }

        /// <summary>
        /// Gets or Sets top right widgets
        /// </summary>
        public SettingItem TopRightWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public SettingItem LeftWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public SettingItem RightWidget { get; set; }

        /// <summary>
        /// Gets or Sets top widgets
        /// </summary>
        public SettingItem BotWidget { get; set; }

        /// <summary>
        /// Gets or Sets sleep widgets
        /// </summary>
        public SettingItem SleepWidget { get; set; }

        /// <inheritdoc />
        public void GenerateDefaultSettings()
        {
            UserName = new SettingItem
            {
                Type = PamameterValueType.FieldString,
                Name = nameof(UserName)
            }; 
            TopRightWidget = new SettingItem
            {
                Type = PamameterValueType.ListOfString,
                Name = nameof(TopRightWidget)
            };
            TopWidget = new SettingItem
            {
                Type = PamameterValueType.ListOfString,
                Name = nameof(TopWidget)
            };
            TopLeftWidget = new SettingItem
            {
                Type = PamameterValueType.ListOfString,
                Name = nameof(TopLeftWidget)
            };
            LeftWidget = new SettingItem
            {
                Type = PamameterValueType.ListOfString,
                Name = nameof(LeftWidget)
            };
            RightWidget = new SettingItem
            {
                Type = PamameterValueType.ListOfString,
                Name = nameof(RightWidget)
            };
            BotWidget = new SettingItem
            {
                Type = PamameterValueType.ListOfString,
                Name = nameof(BotWidget)
            };
            SleepWidget = new SettingItem
            {
                Type = PamameterValueType.ListOfString,
                Name = nameof(SleepWidget)
            };
        }

        /// <inheritdoc />
        public List<SettingItem> GetSettingsList()
        {
            List <SettingItem> ret =  new List<SettingItem>
            {
                UserName,
                TopLeftWidget,
                TopWidget,
                TopRightWidget,
                LeftWidget,
                RightWidget,
                BotWidget,
                SleepWidget,
            };

            foreach(SettingItem setting in ret)
            {
                TranslateSetting(setting);
            }

            return ret;
        }

        /// <inheritdoc />
        public void SetSettingsList(List<SettingItem> settings)
        {
            if(settings?.Count == GetSettingsList().Count)
            {
                UserName = settings[0];
                TopLeftWidget = settings[1];
                TopWidget = settings[2];
                TopRightWidget = settings[3];
                LeftWidget = settings[4];
                RightWidget = settings[5];
                BotWidget = settings[6];
                SleepWidget = settings[7];
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
