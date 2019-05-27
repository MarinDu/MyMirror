namespace TimeWinget
{
    using MyMirror.ViewModel.Winget;
    using System;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;
    using WingetContract.View;

    public class TimeWinget : IWinget
    {
        public string WingetName => throw new NotImplementedException();

        public WingetPositionEnum WingetPosition => WingetPositionEnum.TopLeft;

        public UserControl ReduceWinget
        {
            get
            {
                _currentWinget = new TimeWingetReduce();
                return _currentWinget;
            }
        }

        public UserControl FullWinget
        {
            get
            {
                _currentWinget = new TimeWingetFull();
                return _currentWinget;
            }
        }

        public event EventHandler OnInitializeEnd;

        private UserControl _currentWinget;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            ((TimeVM)_currentWinget.DataContext).Initialize();
        }
    }
}
