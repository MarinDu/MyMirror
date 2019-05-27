namespace MyMirror.Model
{
    using System.Collections.Generic;
    using WingetContract;

    public class MainModel
    {
        public ICollection<IWinget> WingetList { get; private set; }

        public void LoadWinget()
        {
            WingetList = WingetLoader<IWinget>.LoadWingets("Wingets");
        }
    }
}
