// -----------------------------------------------------------------------
// <copyright file="MouseInput.cs">
//
// </copyright>
// <summary>IScreenInput implemtation for mouse and keyboard</summary>
// -----------------------------------------------------------------------

namespace MyMirror.Model.Input
{
    using System;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// IScreenInput implemtation for mouse and keyboard
    /// </summary>
    internal class MouseInput : IScreenInput
    {
        #region Events

        /// <inheritdoc />
        public event EventHandler<ScreenInputEventArg> ScreenInputEvent;

        #endregion

        #region Private members

        /// <summary>
        /// Update Timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Current cancel state
        /// </summary>
        private InputGestureEnum _gesture;

        /// <summary>
        /// Previous mouse position
        /// </summary>
        private System.Drawing.Point _previousPosition;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MouseInput()
        {
            _timer = new Timer(300)
            {
                AutoReset = false
            };
            _timer.Elapsed += OnTimer;
            _timer.Start();

            _previousPosition = System.Windows.Forms.Cursor.Position;

            Application.Current.MainWindow.MouseWheel += OnMouseWheelEvent;
            Application.Current.MainWindow.MouseDown += OnMouseDownEvent;
            Application.Current.MainWindow.KeyDown += OnKeyDown;
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Handles key down event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _timer.Stop();
            _gesture = InputGestureEnum.Exit;
            OnTimer(null, null);
        }

        /// <summary>
        /// Handles mouse down event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnMouseDownEvent(object sender, MouseButtonEventArgs e)
        {
            _timer.Stop();
            _gesture = InputGestureEnum.Click;
            OnTimer(null, null);
        }

        /// <summary>
        /// Handles mouse wheel event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnMouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            _timer.Stop();
            _gesture = e.Delta > 0 ? InputGestureEnum.RollIn : InputGestureEnum.Rollout;
            OnTimer(null, null);
        }

        /// <summary>
        /// Handles timer event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnTimer(object sender, ElapsedEventArgs e)
        {

            _gesture = _gesture == InputGestureEnum.None && !System.Windows.Forms.Cursor.Position.Equals(_previousPosition) ?
                InputGestureEnum.Position : _gesture;

            _previousPosition = System.Windows.Forms.Cursor.Position;

            ScreenInputEvent?.Invoke(
            this,
            new ScreenInputEventArg(
                System.Windows.Forms.Cursor.Position.X,
                System.Windows.Forms.Cursor.Position.Y,
                _gesture));
            _gesture = InputGestureEnum.None;
            _timer.Start();
        }

        #endregion
    }
}
