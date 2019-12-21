// -----------------------------------------------------------------------
// <copyright file="IWidget.cs">
//
// </copyright>
// <summary>Contains Tinterface for widgets</summary>
// -----------------------------------------------------------------------

namespace WingetContract
{
    using System.Collections.Generic;
    using System.Windows.Controls;
    using WingetContract.Enum;

    /// <summary>
    /// Contains Tinterface for widgets
    /// </summary>
    public interface IWidget
    {
        /// <summary>
        /// Gets widget name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets widget possible positions
        /// </summary>
        List<WidgetPositionEnum> WingetPossiblePosition { get; }

        /// <summary>
        /// Gets widget right or left version
        /// </summary>
        UserControl RightOrLeftWidget { get; }

        /// <summary>
        /// Gets widget top or bot version
        /// </summary>
        UserControl TopOrBotWidget { get; }

        /// <summary>
        /// Gets widget full version
        /// </summary>
        UserControl FullWidget { get; }

        /// <summary>
        /// Gets a value indicating wheather the widget can be shown on sleep
        /// </summary>
        bool CanShowOnSleep { get; }

        /// <summary>
        /// Initializes widget
        /// </summary>
        void Initialize();

        /// <summary>
        /// Disposes widget
        /// </summary>
        void Dispose();

        /// <summary>
        /// Send click input to windgets
        /// </summary>
        /// <param name="xPos">Click X pos</param>
        /// <param name="yPos">Click Y pos</param>
        void InputClick(int xPos, int yPos);
    }
}
