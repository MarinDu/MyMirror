// -----------------------------------------------------------------------
// <copyright file="WidgetLoader.cs">
//
// </copyright>
// <summary>Contains interface for sound manageable widgets/summary>
// -----------------------------------------------------------------------

namespace WingetContract
{
    /// <summary>
    /// Contains interface for sound manageable widgets
    /// </summary>
    public interface ISoundManageable
    {
        /// <summary>
        /// Set widgets sound level
        /// </summary>
        /// <param name="volume">Sound level, from 0 to 100</param>
        void SetSoundVolume(int volume);
    }
}
