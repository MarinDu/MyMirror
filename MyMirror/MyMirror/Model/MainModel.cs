namespace MyMirror.Model
{
    using MyMirror.Model.Input;
    using System.Collections.Generic;
    using WingetContract;

    internal class MainModel
    {
        public ICollection<IWidget> WingetList { get; private set; }

        public IScreenInput ScreenInput;

        public void LoadWinget()
        {
            WingetList = WidgetLoader<IWidget>.LoadWingets("Widgets");
        }

        public void LoadInput()
        {
            ScreenInput = new MouseInput();
        }
    }
}