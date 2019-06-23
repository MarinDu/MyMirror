// -----------------------------------------------------------------------
// <copyright file="WidgetWeather.cs">
//
// </copyright>
// <summary>Contains Tram widget interface implementation</summary>
// -----------------------------------------------------------------------

namespace WeatherWidget
{
    using WeatherWidget.View;
    using WeatherWidget.ViewModel;
    using System;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;

    /// <summary>
    /// Contains Tram widget interface implementation
    /// </summary>
    public class WidgetWeather : IWidget
    {
        #region Properties

        /// <inheritdoc />
        public WidgetPositionEnum WingetPosition => WidgetPositionEnum.Left;

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
        private WeatherVM _dataContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WidgetWeather()
        {
            _fullWidget = new WeatherWidgetFull();
            _rightLeftWidget = new WeatherWidgetReduce();
            _topBotWidget = new WeatherWidgetReduce();

            _dataContext = (WeatherVM)_fullWidget.DataContext;

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