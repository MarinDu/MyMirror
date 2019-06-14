
namespace TramWidget
{
    using global::TramWidget.View;
    using global::TramWidget.ViewModel;
    using System;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;

    public class TramWidget : IWidget
    {
        public string WingetName => "Tram Widget";

        public WidgetPositionEnum WingetPosition => WidgetPositionEnum.Right;

        public UserControl ReduceWinget
        {
            get
            {
                _currentWinget = new TramWidgetReduce();
                return _currentWinget;
            }
        }

        public UserControl FullWinget
        {
            get
            {
                _currentWinget = new TramWidgetFull();
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
            ((TramVM)_currentWinget.DataContext).Initialize();
        }
    }
}
