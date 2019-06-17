// -----------------------------------------------------------------------
// <copyright file="WidgetLoader.cs">
//
// </copyright>
// <summary>Contains class MainModel</summary>
// -----------------------------------------------------------------------

namespace MyMirror.Model
{
    using MyMirror.Model.Input;
    using System.Collections.Generic;
    using WingetContract;

    /// <summary>
    /// Contains application main model
    /// </summary>
    internal class MainModel
    {
        /// <summary>
        /// Gets widget list
        /// </summary>
        public ICollection<IWidget> WingetList { get; private set; }

        /// <summary>
        /// Gets screen input
        /// </summary>
        public IScreenInput ScreenInput { get; private set; }

        /// <summary>
        /// Load widgets
        /// </summary>
        public void LoadWinget()
        {
            WingetList = WidgetLoader<IWidget>.LoadWingets("Widgets");
        }

        /// <summary>
        /// Load input
        /// </summary>
        public void LoadInput()
        {
            ScreenInput = new MouseInput();
        }
    }
}