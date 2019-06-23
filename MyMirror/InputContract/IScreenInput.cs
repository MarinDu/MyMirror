// -----------------------------------------------------------------------
// <copyright file="IScreenInput.cs">
//
// </copyright>
// <summary>Interface for mirror input</summary>
// -----------------------------------------------------------------------

namespace InputContract
{
    using System;

    /// <summary>
    /// Interface for mirror input
    /// </summary>
    public interface IScreenInput
    {
        /// <summary>
        /// Mirror input event
        /// </summary>
        event EventHandler<ScreenInputEventArg> ScreenInputEvent;
    }
}