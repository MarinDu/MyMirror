

namespace TramWinget
{
    using System;
    using System.Windows.Controls;
    using WingetContract;
    using WingetContract.Enum;

    public class TramWinget : IWidget
    {
        public string WingetName => throw new NotImplementedException();

        public WidgetPositionEnum WingetPosition => throw new NotImplementedException();

        public UserControl ReduceWinget => throw new NotImplementedException();

        public UserControl FullWinget => throw new NotImplementedException();

        public event EventHandler OnInitializeEnd;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
