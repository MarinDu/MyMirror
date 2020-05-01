// -----------------------------------------------------------------------
// <copyright file="TimeModel.cs">
//
// </copyright>
// <summary>Contains Time widget model</summary>
// -----------------------------------------------------------------------

namespace TimeWidget.Model
{
    using Common.Settings;
    using TimeWidget.Properties;

    /// <summary>
    /// Contains Time widget model
    /// </summary>
    public class TimeModel
    {
        #region Properties
        /// <summary>
        /// Gets windget settings
        /// </summary>
        public SettingsManager<TimeSettings> SettingsManager { get; internal set; }

        #endregion


        #region Contructor

        /// <summary>
        /// Default constructeur
        /// </summary>
        public TimeModel()
        {
            SettingsManager = new SettingsManager<TimeSettings>();
            SettingsManager.Initialize(Resources.SettingsFileName);
        }

        #endregion
    }
}
