// -----------------------------------------------------------------------
// <copyright file="ShowElementEventArgs.cs">
//
// </copyright>
// <summary>Contains class ShowElementEventArgs</summary>
// -----------------------------------------------------------------------

namespace MyMirror.ViewModel
{
    using System;
    using WingetContract.Enum;

    /// <summary>
    /// Events args for element showing
    /// </summary>
    public class ShowElementEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets element position
        /// </summary>
        public WidgetPositionEnum Position { get; set; }

        /// <summary>
        /// Gets or sets a value indication whether or not show element
        /// </summary>
        public bool Show { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="show">Whether or not show element</param>
        public ShowElementEventArgs(WidgetPositionEnum pos, bool show)
        {
            Position = pos;
            Show = show;
        }
    }
}
