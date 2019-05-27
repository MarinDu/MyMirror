namespace MyMirror.ViewModel.Winget
{
    using System.Timers;
    using System;
    using WingetContract.ViewModel;

    internal class TimeVM : ViewModelBase
    {
        #region Properties

        public string CurrentTimeWithSecString
        {
            get => _currentTimeWithSecString;
            set => Set(ref _currentTimeWithSecString, value);
        }

        public string CurrentTimeString
        {
            get => _currentTimeString;
            set => Set(ref _currentTimeString, value);
        }

        public double SecAngle
        {
            get => _secAngle;
            set => Set(ref _secAngle, value);
        }

        public bool FullScreenMode
        {
            get => _fullScreenMode;
            set
            {
                _fullScreenMode = value;
                m_timer.Interval = _fullScreenMode ? 100 : 500;
            }
        }

        #endregion

        #region Private members

        private Timer m_timer;

        private string _currentTimeWithSecString;

        private string _currentTimeString;

        private double _secAngle;

        private bool _fullScreenMode;
        #endregion

        #region Public functions

        public void Initialize()
        {
            m_timer = new Timer(100)
            {
                AutoReset = false
            };
            m_timer.Elapsed += UptadeTime;
            UptadeTime(null, null);
        }

        #endregion

        #region Private functions

        private void UptadeTime(object s, ElapsedEventArgs e)
        {
            CurrentTimeWithSecString = DateTime.Now.ToLongTimeString();
            CurrentTimeString = DateTime.Now.ToShortTimeString();
            SecAngle = DateTime.Now.Second * 6;
            m_timer.Start();
        }

        #endregion
    }
}
