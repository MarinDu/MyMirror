// -----------------------------------------------------------------------
// <copyright file="ScreenInputEventArg.cs">
//
// </copyright>
// <summary>Screen input event arguments</summary>
// -----------------------------------------------------------------------

namespace InputContract
{
    using System;

    /// <summary>
    /// Screen input event arguments
    /// </summary>
    public class ScreenInputEventArg : EventArgs
    {
        /// <summary>
        /// Gets or sets X position
        /// </summary>
        public double XPos { get; set; } 

        /// <summary>
        /// Gets or sets Y position
        /// </summary>
        public double YPos { get; set; }

        /// <summary>
        /// Gest or sets input gesture
        /// </summary>
        public InputGestureEnum Gesture { get; set; }

        /// <summary>
        /// Instatiates screen input event oject
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y positon</param>
        /// <param name="gesture">Input gesture</param>
        public ScreenInputEventArg(double x, double y, InputGestureEnum gesture)
        {
            XPos = x;
            YPos = y;
            Gesture = gesture;
        }
    }
}
