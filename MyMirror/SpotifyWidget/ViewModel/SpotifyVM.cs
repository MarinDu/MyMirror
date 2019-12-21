// -----------------------------------------------------------------------
// <copyright file="SpotifyVM.cs">
//
// </copyright>
// <summary>Contains Spotify widget view model</summary>
// -----------------------------------------------------------------------

namespace SpotifyWidget.ViewModel
{
    using SpotifyWidget.Model;
    using WingetContract;
    using System.Windows;
    using Common.ViewModel;

    /// <summary>
    /// Contains Soptify widget view model
    /// </summary>
    internal class SpotifyVM : ViewModelBase, ISoundManageable
    {
        #region Properties

        /// <summary>
        /// Gets Weather Model
        /// </summary>
        public SpotifyModel SpotifyModel { get; private set; }

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SpotifyVM()
        {
            SpotifyModel = new SpotifyModel();
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Initialize model
        /// </summary>
        public void Initialize()
        {
            SpotifyModel.Initialize();
        }

        /// <inheritdoc />
        public void SetSoundVolume(int volume)
        {
            SpotifyModel.SetSound(volume);
        }

        /// <summary>
        /// Handles inout click
        /// </summary>
        /// <param name="xPos">Click X pos</param>
        /// <param name="yPos">Click Y pos</param>
        public void InputClick(int xPos, int yPos)
        {
            if(yPos < (int)Application.Current.MainWindow.ActualWidth * 3 / 4)
            {
                if (xPos < (int)Application.Current.MainWindow.ActualWidth / 3)
                {
                    SpotifyModel.Previous();
                }
                else if (xPos < (int)Application.Current.MainWindow.ActualWidth * 2 / 3)
                {
                    SpotifyModel.Play();
                }
                else if (xPos < (int)Application.Current.MainWindow.ActualWidth)
                {
                    SpotifyModel.Next();
                }
            }
        }

        #endregion
    }
}