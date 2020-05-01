// -----------------------------------------------------------------------
// <copyright file="IScreenInput.cs">
//
// </copyright>
// <summary>Interface for mirror input</summary>
// -----------------------------------------------------------------------

namespace InputContract
{
    using Common.Settings;
    using System;

    /// <summary>
    /// Interface for mirror input
    /// </summary>
    public interface IScreenInput
    {
        /// <summary>
        /// Gets widget name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Mirror input event
        /// </summary>
        event EventHandler<ScreenInputEventArg> ScreenInputEvent;

        /// <summary>
        /// Gets input setting manager
        /// </summary>
        ISettingsBase Settings { get; }
    }
}