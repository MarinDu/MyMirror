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
        public WidgetPositionEnum WingetPosition => WidgetPositionEnum.Bot;

        /// <inheritdoc />
        public UserControl ReduceWinget
        {
            get
            {
                _currentWinget = new SpotifyWidgetReduce();
                return _currentWinget;
            }
        }

        /// <inheritdoc />
        public UserControl FullWinget
        {
            get
            {
                _currentWinget = new SpotifyWidgetFull();
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
            ((SpotifyVM)_currentWinget.DataContext).Initialize();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}