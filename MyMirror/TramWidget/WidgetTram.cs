﻿// -----------------------------------------------------------------------
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
        public UserControl RightOrLeftWidget => _rightLeftWidget;

        /// <inheritdoc />
        public UserControl TopOrBotWidget => _topBotWidget;

        /// <inheritdoc />
        public UserControl FullWidget => _fullWidget;

        /// <inheritdoc />
        public bool ShowOnSleep => true;

        #endregion

        #region Private members

        /// <summary>
        /// Full widgets
        /// </summary>
        private UserControl _fullWidget;

        /// <summary>
        /// Reduce widgets
        /// </summary>
        private UserControl _rightLeftWidget;

        /// <summary>
        /// Reduce widgets
        /// </summary>
        private UserControl _topBotWidget;

        /// <summary>
        /// Widgets view model
        /// </summary>
        private TramVM _dataContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WidgetTram()
        {
            _fullWidget = new TramWidgetFull();
            _rightLeftWidget = new TramWidgetReduce();
            _topBotWidget = new TramWidgetReduce();

            _dataContext = (TramVM)_fullWidget.DataContext;

            _topBotWidget.DataContext = _dataContext;
            _rightLeftWidget.DataContext = _dataContext;
        }

        #endregion

        #region Public functions

        /// <inheritdoc />
        public void Initialize()
        {
            _dataContext.Initialize();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void InputClick(int xPos, int yPos)
        {
        }

        #endregion
    }
}