// -----------------------------------------------------------------------
// <copyright file="IWidget.cs">
//
// </copyright>
// <summary>Contains Tinterface for widgets</summary>
// -----------------------------------------------------------------------

namespace WingetContract
{
    using System.Windows.Controls;
    using WingetContract.Enum;

    /// <summary>
    /// Contains Tinterface for widgets
    /// </summary>
    public interface IWidget
    {
        /// <summary>
        /// Gets widget position
        /// </summary>
        WidgetPositionEnum WingetPosition { get; }

        /// <summary>
        /// Gets widget reduce version
        /// </summary>
        UserControl ReduceWinget { get; }

        /// <summary>
        /// Gets widget full version
        /// </summary>
        UserControl FullWinget { get; }

        /// <summary>
        /// Show on sleep
        /// </summary>
        bool ShowOnSleep { get; }

        /// <summary>
        /// Initializes widget
        /// </summary>
        void Initialize();

        /// <summary>
        /// Disposes widget
        /// </summary>
        void Dispose();
    }
}
