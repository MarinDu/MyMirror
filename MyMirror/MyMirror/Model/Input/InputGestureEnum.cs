﻿// -----------------------------------------------------------------------
// <copyright file="InputGestureEnum.cs">
//
// </copyright>
// <summary>Screen input gestures enumerator</summary>
// -----------------------------------------------------------------------

namespace MyMirror.Model.Input
{
    /// <summary>
    /// Screen input gestures enumerator
    /// </summary>
    internal enum InputGestureEnum
    {
        /// <summary>
        /// Click gesture
        /// </summary>
        Click,

        /// <summary>
        /// Positive roll gesture
        /// </summary>
        RollIn,

        /// <summary>
        /// Negative roll gesture
        /// </summary>
        Rollout,

        /// <summary>
        /// Exit gesture
        /// </summary>
        Exit,

        /// <summary>
        /// Simple position Change
        /// </summary>
        Position,

        /// <summary>
        /// No gesture
        /// </summary>
        None
    }
}
