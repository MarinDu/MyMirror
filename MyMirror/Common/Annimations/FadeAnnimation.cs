// <copyright file="WidgetLoader.cs">
//
// </copyright>
// <summary>Contains class MainModel</summary>
// -----------------------------------------------------------------------

namespace Common.Annimations
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;

    public class FadeAnnimation
    {
        /// <summary>
        /// Refresh periode
        /// </summary>
        private const int REFRESH_TIME = 100;

        /// <summary>
        /// Refresh timer
        /// </summary>
        private System.Timers.Timer _timer;

        /// <summary>
        /// Opacity property to manupulate
        /// </summary>
        Action<double> _property;
        
        /// <summary>
        /// Current opacity
        /// </summary>
        double _opacity;  
        
        /// <summary>
        /// Opacity step each timer trigger
        /// </summary>
        double _opacityStep;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property">Fade property to manipulare</param>
        public FadeAnnimation(Action<double> property)
        {
            _property = property;
            _timer = new System.Timers.Timer(REFRESH_TIME);
            _timer.AutoReset = true;
            _timer.Elapsed += OnTimer;
        }

        /// <summary>
        /// Fade in, wait and faid out
        /// </summary>
        /// <param name="fadeDuration">Fade duration (ms)</param>
        /// <param name="displayDuration">Display duration (ms)</param>
        public void FadeInAndOut(int fadeSpeed, int displayDuration)
        {
            new Task(async () =>
            {
                FadeIn(fadeSpeed);
                Thread.Sleep(displayDuration);
                FadeOut(fadeSpeed);
            }
            ).Start();
        }

        /// <summary>
        /// Fade in, wait and faid out
        /// </summary>
        /// <param name="fadeDuration">Fade duration (ms)</param>
        public void FadeIn(int fadeDuration)
        {
            _opacity = 0;
            _opacityStep = (double)REFRESH_TIME / (double)fadeDuration;
            _timer.Start();
        }

        /// <summary>
        /// Fade in, wait and faid out
        /// </summary>
        /// <param name="fadeDuration">Fade duration (ms)</param>
        public void FadeOut(int fadeDuration)
        {
            _opacity = 1;
            _opacityStep = - (double)REFRESH_TIME / (double)fadeDuration;
            _timer.Start();
        }

        /// <summary>
        /// Handles timer events
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            _opacity += _opacityStep;

            if(_opacity <= 0)
            {
                _timer.Stop();
                _opacity = 0;
            } 
            else if(_opacity >= 1)
            {
                _timer.Stop();
                _opacity = 1;
            }
            _property(_opacity);
        }
    }
}
