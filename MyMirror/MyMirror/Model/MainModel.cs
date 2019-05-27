namespace MyMirror.Model
{
    using System.Collections.Generic;
    using WingetContract;

    public class MainModel
    {
        public ICollection<IWidget> WingetList { get; private set; }

        public void LoadWinget()
        {
            WingetList = WingetLoader<IWidget>.LoadWingets("Widgets");
        }
    }
}
