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
        public UserControl ReduceWinget
        {
            get
            {
                _currentWinget = new WeatherWidgetReduce();
                return _currentWinget;
            }
        }

        /// <inheritdoc />
        public UserControl FullWinget
        {
            get
            {
                _currentWinget = new WeatherWidgetFull();
                return _currentWinget;
            }
        }

        /// <inheritdoc />
        public bool ShowOnSleep => false;

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
            ((WeatherVM)_currentWinget.DataContext).Initialize();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}