namespace WingetContract
{
    using System;
    using System.Windows.Controls;
    using WingetContract.Enum;

    public interface IWinget
    {
        string WingetName { get; }
        WingetPositionEnum WingetPosition { get; }

        UserControl ReduceWinget { get; }
        UserControl FullWinget { get; }

        void Dispose();
        void Initialize();
        event EventHandler OnInitializeEnd;
    }
}
