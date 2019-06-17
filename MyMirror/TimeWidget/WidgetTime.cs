// -----------------------------------------------------------------------
// <copyright file="WidgetTime.cs">
//
// </copyright>
// <summary>Implements widget interface for Time widget</summary>
// -----------------------------------------------------------------------

namespace TimeWidget
{
    using TimeWidget.View;
    using TimeWidget.ViewModel;
    using System;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;

    /// <summary>
    /// Implements widget interface for Time widget
    /// </summary>
    public class WidgetTime : IWidget
    {
        #region Properties

        /// <inheritdoc />
        public WidgetPositionEnum WingetPosition => WidgetPositionEnum.TopLeft;

        /// <inheritdoc />
        public UserControl ReduceWinget
        {
            get
            {
                _currentWinget = new TimeWidgetReduce();
                return _currentWinget;
            }
        }

        /// <inheritdoc />
        public UserControl FullWinget
        {
            get
            {
                _currentWinget = new TimeWidgetFull();
                return _currentWinget;
            }
        }

        #endregion

        #region Private members

        /// <summary>
        /// Current widget
        /// </summary>
        private UserControl _currentWinget;

        #endregion

        #region Public functions

        /// <inheritdoc />
        public void Initialize()
        {
            ((TimeVM)_currentWinget.DataContext).Initialize();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
