namespace TimeWidget.ViewModel
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

        #endregion

        #region Private members

        private Timer _timer;

        private string _currentTimeWithSecString;

        private string _currentTimeString;

        private double _secAngle;

        #endregion

        #region Public functions

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

        private void UptadeTime(object s, ElapsedEventArgs e)
        {
            CurrentTimeWithSecString = DateTime.Now.ToLongTimeString();
            CurrentTimeString = DateTime.Now.ToShortTimeString();
            SecAngle = DateTime.Now.Second * 6;
        }

        #endregion
    }
}