// -----------------------------------------------------------------------
// <copyright file="TimeVM.cs">
//
// </copyright>
// <summary>Contains Time widget view model</summary>
// -----------------------------------------------------------------------

namespace TimeWidget.ViewModel
{
    using System.Timers;
    using System;
    using Common.ViewModel;
    using Common.Settings;
    using TimeWidget.Model;

    /// <summary>
    /// Contains Time widget view model
    /// </summary>
    internal class TimeVM : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets Time Model
        /// </summary>
        public TimeModel TimeModel { get; private set; }

        /// <summary>
        /// Gets current time with hours, minutes and seconds
        /// </summary>
        public string CurrentTimeWithSecString
        {
            get => _currentTimeWithSecString;
            private set => Set(ref _currentTimeWithSecString, value);
        }

        /// <summary>
        /// Gets current time with hours and minutes
        /// </summary>
        public string CurrentTimeString
        {
            get => _currentTimeString;
            private set => Set(ref _currentTimeString, value);
        }

        /// <summary>
        /// Gets seconds angle 
        /// </summary>
        public double SecAngle
        {
            get => _secAngle;
            private set => Set(ref _secAngle, value);
        }

        #endregion

        /// <summary>
        /// Default constructeur
        /// </summary>
        public TimeVM()
        {
            TimeModel = new TimeModel();
        }


        #region Private members

        /// <summary>
        /// Refresh timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Current time with hours, minutes and seconds 
        /// </summary>
        private string _currentTimeWithSecString;

        /// <summary>
        /// Current time with hours and minutes
        /// </summary>
        private string _currentTimeString;

        /// <summary>
        /// Seconds angle
        /// </summary>
        private double _secAngle;

        #endregion

        #region Public functions

        /// <summary>
        /// Initialize timer
        /// </summary>
        public void Initialize()
        {
            _timer = new Timer(999)
            {
                AutoReset = true
            };
            _timer.Elapsed += UptadeTime;
            _timer.Start();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Update time on timer
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Arguments</param>
        private void UptadeTime(object s, ElapsedEventArgs e)
        {
            CurrentTimeWithSecString = DateTime.Now.ToLongTimeString();
            CurrentTimeString = DateTime.Now.ToShortTimeString();
            SecAngle = DateTime.Now.Second * 6;
        }

        #endregion
    }
}