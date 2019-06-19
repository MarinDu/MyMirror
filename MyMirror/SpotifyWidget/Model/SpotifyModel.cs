// -----------------------------------------------------------------------
// <copyright file="SpotifyModel.cs">
//
// </copyright>
// <summary>Contains Spotify widget model</summary>
// -----------------------------------------------------------------------

namespace SpotifyWidget.Model
{
    using SpotifyWidget.Properties;
    using System;
    using System.Threading.Tasks;
    using System.Timers;
    using WingetContract.ViewModel;
    using SpotifyAPI.Web;
    using SpotifyAPI.Web.Auth;
    using SpotifyAPI.Web.Enums;
    using SpotifyAPI.Web.Models;
    using System.Threading;

    /// <summary>
    /// Contains Tram widget model
    /// </summary>
    internal class SpotifyModel : ObservableObject
    {
        #region Properties

        /// <summary>
        /// Is music running
        /// </summary>
        public bool IsPlaying { get => _isPlaying; private set => Set(ref _isPlaying, value); }

        /// <summary>
        /// Gets song title
        /// </summary>
        public string SongTitle { get => _songTitle; private set => Set(ref _songTitle, value); }

        /// <summary>
        /// Gets artist name
        /// </summary>
        public string Artist { get => _artist; private set => Set(ref _artist, value); }

        /// <summary>
        /// Gets song duration
        /// </summary>
        public int SongDuration { get => _songDuration; private set => Set(ref _songDuration, value); }

        /// <summary>
        /// Gets song progress
        /// </summary>
        public int SongProgress
        {
            get => _songProgress;
            private set
            {
                Set(ref _songProgress, value);
                NotifyPropertyChanged(nameof(SongProgressPercent));
            }
        }

        /// <summary>
        /// Gets song progress in percent
        /// </summary>
        public float SongProgressPercent { get => _songDuration == 0 ? 0 : (float)_songProgress / (float)_songDuration; }

        #endregion

        #region Private members

        /// <summary>
        /// Refresh timer
        /// </summary>
        private System.Timers.Timer _timer;

        /// <summary>
        /// Is music running
        /// </summary>
        private bool _isPlaying;

        /// <summary>
        /// Song title
        /// </summary>
        private string _songTitle;

        /// <summary>
        /// Artist name
        /// </summary>
        private string _artist;

        /// <summary>
        /// Song duration
        /// </summary>
        private int _songDuration; 

        /// <summary>
        /// Song progress
        /// </summary>
        private int _songProgress;

        /// <summary>
        /// Instance de l'objet web api
        /// </summary>
        private static SpotifyWebAPI _spotifyWebAPI;

        /// <summary>
        /// API access mutex
        /// </summary>
        private readonly Mutex _accessMutex;

        /// <summary>
        /// Expected sound volume
        /// </summary>
        private int _expectedSoundVolume;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructeur
        /// </summary>
        public SpotifyModel()
        {
            _accessMutex = new Mutex();

            _timer = new System.Timers.Timer(1000)
            {
                AutoReset = false
            };
            _timer.Elapsed += Refresh;

        }

        #endregion

        #region Public functions

        /// <summary>
        /// Performs a first update
        /// </summary>
        public void Initialize()
        {
            new Task( async () =>
            {
                try
                {
                    WebAPIFactory webAPIFactory = new WebAPIFactory(
                        Resources.Url,
                        8000,
                        Resources.ClientID,
                        Scope.UserModifyPlaybackState,
                        TimeSpan.FromSeconds(20));
                    try
                    {
                        _spotifyWebAPI = await webAPIFactory.GetWebApi(false);
                    }
                    catch (Exception ex)
                    {
                        SongTitle = Resources.Error;
                        Console.WriteLine(ex.Message);
                    }

                    Refresh(null, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _timer.Start();
            }
            ).Start();  
        }

        /// <summary>
        /// Play or pose music
        /// </summary>
        public void Play()
        {
            IsPlaying = !IsPlaying;

            new Task(() =>
            {
                try
                {
                    if (_accessMutex.WaitOne(1000))
                    {
                        PlaybackContext context = _spotifyWebAPI.GetPlayback();
                        if (context.Device != null)
                        {
                            ErrorResponse err;
                            if (context.IsPlaying)
                            {
                                err = _spotifyWebAPI.PausePlayback();
                            }
                            else
                            {
                                err = _spotifyWebAPI.ResumePlayback();
                            }
                        }
                        _accessMutex.ReleaseMutex();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _accessMutex.ReleaseMutex();
                }
            }).Start();
        }

        /// <summary>
        /// Go to previous music
        /// </summary>
        public void Previous()
        {
            new Task(() =>
            {
                try
                {
                    if (_accessMutex.WaitOne(1000))
                    {
                        ErrorResponse err = _spotifyWebAPI.SkipPlaybackToPrevious();
                        _accessMutex.ReleaseMutex();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _accessMutex.ReleaseMutex();
                }
            }).Start();
        }

        /// <summary>
        /// Go to next music
        /// </summary>
        public void Next()
        {
            new Task(() =>
            {
                try
                {
                    if (_accessMutex.WaitOne(1000))
                    {
                        ErrorResponse err = _spotifyWebAPI.SkipPlaybackToNext();
                        _accessMutex.ReleaseMutex();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _accessMutex.ReleaseMutex();
                }
            }).Start();
        }

        /// <summary>
        /// Set sound
        /// </summary>
        /// <param name="volume">Volume in percent</param>
        public void SetSound(int volume)
        {
            _expectedSoundVolume = volume;
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Updates tram timers
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Arguments</param>
        private void Refresh(object s, ElapsedEventArgs e)
        {
            new Task(() =>
            {
                bool error = false;

                try
                {
                    if (_accessMutex.WaitOne(1))
                    {
                        PlaybackContext context = _spotifyWebAPI.GetPlayback();
                        if (context.Item != null)
                        {
                            SongTitle = context.Item.Name;

                            SongDuration = context.Item.DurationMs / 1000;
                            SongProgress = context.ProgressMs / 1000;

                            IsPlaying = context.IsPlaying;
                            Artist = context.Item.Artists[0]?.Name;

                            if (_spotifyWebAPI.GetDevices()?.Devices[0]?.VolumePercent != _expectedSoundVolume)
                            {
                                _spotifyWebAPI.SetVolume(_expectedSoundVolume);
                            }
                        }

                        _accessMutex.ReleaseMutex();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _accessMutex.ReleaseMutex();
                    error = true;
                }

                // If token is lost, get it again
                if (_spotifyWebAPI == null || error && _spotifyWebAPI.AccessToken == null)
                {
                    Initialize();
                }
                else
                {
                    _timer.Start();
                }
            }
            ).Start();
        }

        #endregion
    }
}