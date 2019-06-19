// -----------------------------------------------------------------------
// <copyright file="SpotifyVM.cs">
//
// </copyright>
// <summary>Contains Spotify widget view model</summary>
// -----------------------------------------------------------------------

namespace SpotifyWidget.ViewModel
{
    using SpotifyWidget.Model;
    using System.Windows.Input;
    using WingetContract.ViewModel;
    using System;
    using WingetContract;

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
        
        /// <summary>
        /// Gets return button command
        /// </summary>
        public ICommand ReturnButtonCommand { get; private set; }

        /// <summary>
        /// Gets play button command
        /// </summary>
        public ICommand PlayButtonCommand { get; private set; }

        /// <summary>
        /// Gets next button command
        /// </summary>
        public ICommand NextButtonCommand { get; private set; }

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SpotifyVM()
        {
            SpotifyModel = new SpotifyModel();

            ReturnButtonCommand = new RelayCommand(ReturnButton);
            PlayButtonCommand = new RelayCommand(PlayButton);
            NextButtonCommand = new RelayCommand(NextButton);
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

        #endregion

        #region Private function 

        /// <summary>
        /// Handles next button command
        /// </summary>
        /// <param name="obj">Command parameter</param>
        private void NextButton(object obj)
        {
            SpotifyModel.Next();
        }

        /// <summary>
        /// Handles next button command
        /// </summary>
        /// <param name="obj">Command parameter</param>
        private void PlayButton(object obj)
        {
            SpotifyModel.Play();
        }

        /// <summary>
        /// Handles next button command
        /// </summary>
        /// <param name="obj">Command parameter</param>
        private void ReturnButton(object obj)
        {
            SpotifyModel.Previous();
        }

        #endregion

    }
}