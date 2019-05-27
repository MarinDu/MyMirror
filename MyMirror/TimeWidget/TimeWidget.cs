namespace TimeWidget
{
    using global::TimeWidget.View;
    using global::TimeWidget.ViewModel;
    using System;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;

    public class TimeWidget : IWidget
    {
        public string WingetName => throw new NotImplementedException();

        public WingetPositionEnum WingetPosition => WingetPositionEnum.TopLeft;

        public UserControl ReduceWinget
        {
            get
            {
                _currentWinget = new TimeWidgetReduce();
                return _currentWinget;
            }
        }

        public UserControl FullWinget
        {
            get
            {
                _currentWinget = new TimeWidgetFull();
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
