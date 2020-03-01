// -----------------------------------------------------------------------
// <copyright file="SpotifyModel.cs">
//
// </copyright>
// <summary>Contains Spotify widget model</summary>
// -----------------------------------------------------------------------

namespace NewsWidget.Model
{
    using System;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Threading;
    using Common.ViewModel;
    using Common.Log;
    using System.Collections.Generic;

    /// <summary>
    /// Contains Tram widget model
    /// </summary>
    internal class NewsModel : ObservableObject
    {
        #region Properties

        /// <summary>
        /// Title of the news
        /// </summary>
        public string NewsTitle { get => _newsTitle; private set => Set(ref _newsTitle, value); }

        /// <summary>
        /// Text of the news
        /// </summary>
        public string NewsText { get => _newsText; private set => Set(ref _newsText, value); }

        #endregion

        #region Private members

        /// <summary>
        /// Refresh news list timer
        /// </summary>
        private System.Timers.Timer _timer;

        /// <summary>
        /// Switch news timer
        /// </summary>
        private System.Timers.Timer _switchTimer;

        /// <summary>
        /// News list access mutex
        /// </summary>
        private readonly Mutex _accessMutex;

        /// <summary>
        /// List of the news
        /// </summary>
        private List<String> _newsList;

        /// <summary>
        /// Title of the information
        /// </summary>
        private string _newsTitle;

        /// <summary>
        /// Text of the information
        /// </summary>
        private string _newsText;

        /// <summary>
        /// INdex of the current displayed news
        /// </summary>
        private int _currentNewsIndex;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructeur
        /// </summary>
        public NewsModel()
        {
            _timer = new System.Timers.Timer(10000)
            {
                AutoReset = false
            };
            _timer.Elapsed += GetInfo;

            _switchTimer = new System.Timers.Timer(2000)
            {
                AutoReset = false
            };
            _timer.Elapsed += Refresh;

            _accessMutex = new Mutex();
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Performs a first update
        /// </summary>
        public void Initialize()
        {
            new Task(async () =>
            {
                try
                {        
                    LogManager.LogLine("News initialization OK");            
                    GetInfo(null, null);
                    Refresh(null, null);
                }
                catch (Exception ex)
                {
                    LogManager.LogLine("News initialization Error");
                    LogManager.LogLine(ex.Message);
                }
                
            }
            ).Start();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Get informations form API
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Arguments</param>
        private void GetInfo(object s, ElapsedEventArgs e)
        {
            new Task(() =>
            {
                try
                {
                    if (_accessMutex.WaitOne(1000))
                    {
                        _newsList = new List<string>()
                        {
                            DateTime.Now.ToString() + " News test 1",
                            DateTime.Now.ToString() + " News test 2",
                            DateTime.Now.ToString() + " News test 3"
                        };

                        _currentNewsIndex = 0;
                        _accessMutex.ReleaseMutex();
                    }

                    _timer.Start();
                }
                catch (Exception ex)
                {
                    LogManager.LogLine(ex.Message);
                }
            }
            ).Start();
        }
        
        /// <summary>
        /// Updates displayer info
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Arguments</param>
        private void Refresh(object s, ElapsedEventArgs e)
        {
            new Task(() =>
            {
                if(_newsList != null && _newsList.Count > 0)
                {
                    if (_accessMutex.WaitOne(1000))
                    {
                        NewsTitle = "TITLE " + _currentNewsIndex.ToString();
                        NewsText = _newsList[_currentNewsIndex];

                        _currentNewsIndex = (_currentNewsIndex + 1) % _newsList.Count;
                        _accessMutex.ReleaseMutex();
                    }

                }

                _switchTimer.Start();
            }
            ).Start();
        }

        #endregion
    }
}