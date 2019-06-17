// -----------------------------------------------------------------------
// <copyright file="TramWidget.cs">
//
// </copyright>
// <summary>Contains Tram widget interface implementation</summary>
// -----------------------------------------------------------------------

namespace TramWidget
{
    using TramWidget.View;
    using TramWidget.ViewModel;
    using System;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;

    /// <summary>
    /// Contains Tram widget interface implementation
    /// </summary>
    public class WidgetTram : IWidget
    {
        #region Properties

        /// <inheritdoc />
        public WidgetPositionEnum WingetPosition => WidgetPositionEnum.Right;

        /// <inheritdoc />
        public UserControl ReduceWinget
        {
            get
            {
                _currentWinget = new TramWidgetReduce();
                return _currentWinget;
            }
        }

        /// <inheritdoc />
        public UserControl FullWinget
        {
            get
            {
                _currentWinget = new TramWidgetFull();
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
            ((TramVM)_currentWinget.DataContext).Initialize();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
