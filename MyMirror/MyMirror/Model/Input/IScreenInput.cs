// -----------------------------------------------------------------------
// <copyright file="IScreenInput.cs">
//
// </copyright>
// <summary>Interface for mirror input</summary>
// -----------------------------------------------------------------------

namespace MyMirror.Model.Input
{
    using System;

    /// <summary>
    /// Interface for mirror input
    /// </summary>
    internal interface IScreenInput
    {
        /// <summary>
        /// Mirror input event
        /// </summary>
        event EventHandler<ScreenInputEventArg> ScreenInputEvent;
    }
}
