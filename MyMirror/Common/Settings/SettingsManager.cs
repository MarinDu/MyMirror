// -----------------------------------------------------------------------
// <copyright file="SettingsManager.cs">
//
// </copyright>
// <summary>Contains class SettingsManager</summary>
// -----------------------------------------------------------------------

namespace Common.Settings
{
    using Common.Log;
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Setting maanger
    /// </summary>
    public class SettingsManager<T> where T : class, ISettingsBase,  new()
    {
        /// <summary>
        /// Gets settings
        /// </summary>
        public T Settings { get; private set; }

        /// <summary>
        /// Settings path
        /// </summary>
        private string _settingsPath;

        /// <summary>
        /// Initialize settings with path
        /// </summary>
        /// <param name="filename">File name</param>
        public void Initialize(string filename)
        {
            string friendlyName = AppDomain.CurrentDomain.FriendlyName;
            string assemblyName = friendlyName.Substring(0, friendlyName.Length - ".exe".Length);
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            _settingsPath = Path.Combine(folder, assemblyName, "Settings", filename);

            //Create file if it does not exists
            if (!File.Exists(_settingsPath))
            {
                Settings = new T();
                Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath));
                File.Create(_settingsPath).Close();
                Settings.GenerateDefaultSettings();
                Save();
            }

            // Load main file
            try
            {
                using (StreamReader stream = new StreamReader(_settingsPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    XmlTextReader reader = new XmlTextReader(stream) { DtdProcessing = DtdProcessing.Prohibit };
                    Settings = serializer.Deserialize(reader) as T;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogLine(ex.Message);
            }

            Settings.SettingsUpdated += OnSettingsUpdated;
        }

        /// <summary>
        /// Handles settings updated events
        /// </summary>
        private void OnSettingsUpdated(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Save settings in configuration file
        /// </summary>
        public void Save()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_settingsPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, Settings);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogLine(ex.Message);
            }
        }
    }
}
