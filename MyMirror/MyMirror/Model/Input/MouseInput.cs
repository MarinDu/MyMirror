namespace MyMirror.Model.Input
{
    using System;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;

    internal class MouseInput : IScreenInput
    {
        public event EventHandler<ScreenInputEventArg> ScreenInputEvent;

        private Timer _timer;
        private double _currentZ;
        private bool _cancel;

        public MouseInput()
        {
            _currentZ = 5;

            _timer = new Timer(300)
            {
                AutoReset = false
            };
            _timer.Elapsed += OnTimer;
            _timer.Start();
            //Application.Current.MainWindow.MouseWheel += OnMouseWheelEvent;
            Application.Current.MainWindow.MouseDown += OnMouseDownEvent;
            Application.Current.MainWindow.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _cancel = true;
            _timer.Stop();
            OnTimer(null, null);
            _cancel = false;
        }

        private void OnMouseDownEvent(object sender, MouseButtonEventArgs e)
        {
            _currentZ = -1;
            _timer.Stop();
            OnTimer(null, null);
            _currentZ = 1;
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
           ScreenInputEvent?.Invoke(
               this, 
               new ScreenInputEventArg(
                   System.Windows.Forms.Cursor.Position.X,
                   System.Windows.Forms.Cursor.Position.Y,
                   _currentZ,
                   _cancel));
            _timer.Start();
        }

        /*private void OnMouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            _currentZ += e.Delta / 120;
        }*/
    }
}
