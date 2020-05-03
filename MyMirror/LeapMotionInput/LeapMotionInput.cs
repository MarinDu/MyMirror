// -----------------------------------------------------------------------
// <copyright file="LeapMotionInput.cs">
//
// </copyright>
// <summary>IScreenInput implemtation for LeapMotion</summary>
// -----------------------------------------------------------------------

namespace LeapMotionInput
{
    using System;
    using System.Timers;
    using Leap;
    using System.Threading.Tasks;
    using InputContract;
    using Common.Log;
    using Common.Settings;
    using global::LeapMotionInput.Properties;

    /// <summary>
    /// ScreenInput implemtation for LeapMotion
    /// </summary>
    public class LeapMotionInput : IScreenInput
    {
        #region Events

        /// <inheritdoc />
        public event EventHandler<ScreenInputEventArg> ScreenInputEvent;

        #endregion

        #region Properties

        /// <inheritdoc />
        public string Name => Resources.Name;

        /// <inheritdoc />
        public ISettingsBase Settings => _settingsManager.Settings;

        #endregion

        #region Private membres

        /// <summary>
        /// Update Timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Current cancel state
        /// </summary>
        private InputGestureEnum _gesture;

        /// <summary>
        /// Leap motion controller
        /// </summary>
        private Controller _controller;

        /// <summary>
        /// Last sent click for hysteresis
        /// </summary>
        private DateTime _lastClick;

        /// <summary>
        /// Reference frame for rotation
        /// </summary>
        private Frame _referenceFrame;

        /// <summary>
        /// Input settings manager
        /// </summary>
        private SettingsManager<LeapMotionSettings> _settingsManager;

        #endregion

        #region Contructor

        /// <summary>
        /// DefaultConstructor
        /// </summary>
        public LeapMotionInput()
        {
            _settingsManager = new SettingsManager<LeapMotionSettings>();
            _settingsManager.Initialize(Resources.SettingsFileName);

            new Task(() =>
            {
                try
                {
                    _controller = new Controller();
                    _lastClick = DateTime.Now;

                    _timer = new Timer(_settingsManager.Settings.RefreshPeriode.Value)
                    {
                        AutoReset = false
                    };
                    _timer.Elapsed += OnTimer;
                    _timer.Start();
                }
                catch (Exception ex)
                {
                    LogManager.LogLine(ex.Message);
                }
                _timer?.Start();
            }
            ).Start();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Handles timer event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                Frame frame = _controller.Frame();
                Hand currentHand = GetRightHand(frame);
                Vector tip = GetHandTip(currentHand);

                // Get reference frame get first time with a righthand
                if(_referenceFrame == null && currentHand != null)
                {
                    _referenceFrame = frame;
                }
                else if(_referenceFrame != null && currentHand == null)
                {                
                    // Remove reference frame if no hand is detected
                    _referenceFrame = null;
                }

                float rotation = 0;
                if(_referenceFrame != null && currentHand != null)
                {
                    rotation = currentHand.RotationAngle(_referenceFrame, Vector.ZAxis);
                }

                int screenX = _settingsManager.Settings.ScreenX.Value;
                int screenY = _settingsManager.Settings.ScreenY.Value;
                int screenH = _settingsManager.Settings.ScreenHeight.Value;
                int delayBetweenActions = _settingsManager.Settings.DelayBetweenActions.Value;

                double xPos = tip == null ? 0f : tip.x * screenX / 180f + screenY;
                double yPos = tip == null ? 0f : screenH - tip.y * 4f;

                if (frame.Hands.Count == 2)
                {
                    _gesture = DateTime.Now.Subtract(_lastClick).TotalMilliseconds > delayBetweenActions ? InputGestureEnum.Exit : InputGestureEnum.None;
                }
                else if (_referenceFrame != null && Math.Abs(rotation) > Math.PI / 3)
                {
                    _gesture = rotation > 0 ? InputGestureEnum.RollIn : InputGestureEnum.Rollout;
                }
                else if (tip != null)
                {
                    _gesture = tip.z < 0 && DateTime.Now.Subtract(_lastClick).TotalMilliseconds > delayBetweenActions ? InputGestureEnum.Click : InputGestureEnum.Position;
                }
                else
                {
                    _gesture = InputGestureEnum.None;
                }

                if (_gesture == InputGestureEnum.Click || _gesture == InputGestureEnum.Exit)
                {
                    _lastClick = DateTime.Now;
                }

                ScreenInputEvent?.Invoke(this, new ScreenInputEventArg(xPos, yPos, _gesture));
            }
            catch(Exception ex)
            {
                LogManager.LogLine(ex.Message);
            }
            _timer.Start();
        }

        /// <summary>
        /// Fonction de réupération de la main droite
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private Hand GetRightHand(Frame f)
        {
            Hand ret = null;
            foreach (Hand h in f.Hands)
            {
                if (h.IsRight)
                {
                    ret =  h;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Fonction de réupération de la point e de la main
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private Vector GetHandTip(Hand hand)
        {
            Vector ret = null;

            if(hand != null)
            {
                foreach (Finger f in hand.Fingers)
                {
                    if (ret == null)
                    {
                        ret = f.TipPosition;
                    }
                    else if (ret.z > f.TipPosition.z)
                    {
                        ret = f.TipPosition;
                    }
                }
            }

            return ret;
        }
        #endregion
    }
}
