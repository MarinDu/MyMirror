// -----------------------------------------------------------------------
// <copyright file="WidgetNews.cs">
//
// </copyright>
// <summary>Contains news widget interface implementation</summary>
// -----------------------------------------------------------------------


namespace NewsWidget
{
    using NewsWidget.View;
    using NewsWidget.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;

    public class WidgetNews : IWidget
    {
        #region Properties

        /// <inheritdoc />
        public string Name => "News";

        /// <inheritdoc />
        public List<WidgetPositionEnum> WingetPossiblePosition => new List<WidgetPositionEnum>()
        {
            WidgetPositionEnum.Top,
            WidgetPositionEnum.Bot,
        };

        /// <inheritdoc />
        public UserControl RightOrLeftWidget => _rightLeftWidget;

        /// <inheritdoc />
        public UserControl TopOrBotWidget => _topBotWidget;

        /// <inheritdoc />
        public UserControl FullWidget => _fullWidget;

        /// <inheritdoc />
        public bool CanShowOnSleep => false;

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
        private NewsVM _dataContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WidgetNews()
        {
            _fullWidget = new NewsWidgetFull();
            _topBotWidget = new NewsWidgetReduce();
            _rightLeftWidget = new NewsWidgetReduce();

            _dataContext = (NewsVM)_fullWidget.DataContext;

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
            _dataContext.InputClick(xPos, yPos);
        }

        #endregion
    }
}
