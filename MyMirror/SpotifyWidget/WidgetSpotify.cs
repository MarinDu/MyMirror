// -----------------------------------------------------------------------
// <copyright file="WidgetSpotify.cs">
//
// </copyright>
// <summary>Contains Spotify widget interface implementation</summary>
// -----------------------------------------------------------------------

namespace SpotifyWidget
{
    using SpotifyWidget.View;
    using SpotifyWidget.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;

    /// <summary>
    /// Contains Spotify widget interface implementation
    /// </summary>
    public class WidgetSpotify : IWidget
    {
        #region Properties

        /// <inheritdoc />
        public string Name => "Spotify";

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
        public bool CanShowOnSleep => true;

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
        private SpotifyVM _dataContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WidgetSpotify()
        {
            _fullWidget = new SpotifyWidgetFull();
            _topBotWidget = new SpotifyWidgetReduce();
            _rightLeftWidget = new SpotifyWidgetReduce();

            _dataContext = (SpotifyVM)_fullWidget.DataContext;

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