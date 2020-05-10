// -----------------------------------------------------------------------
// <copyright file="WeatherVM.cs">
//
// </copyright>
// <summary>Contains Weather widget view model</summary>
// -----------------------------------------------------------------------

namespace WeatherWidget.ViewModel
{
    using WeatherWidget.Model;
    using Common.ViewModel;
    using WeatherWidget.View;
    using System.Windows;

    /// <summary>
    /// Contains Tram widget view model
    /// </summary>
    internal class WeatherVM : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets Weather Model
        /// </summary>
        public WeatherModel WeatherModel { get; private set; }
        
        /// <summary>
        /// Gets the visibility of next arow
        /// </summary>
        public bool ShowNextArrow
        {
            get => _showNextArrow;
            private set => Set(ref _showNextArrow, value);
        }

        /// <summary>
        /// Gets the visibility of previous arow
        /// </summary>
        public bool ShowPreviousArrow
        {
            get => _showPreviousArrow;
            private set => Set(ref _showPreviousArrow, value);
        }

        #endregion

        #region Private members

        /// <summary>
        /// Gets the visibilit of next arow
        /// </summary>
        private bool _showNextArrow;

        /// <summary>
        /// Gets the visibilit of previous arow
        /// </summary>
        private bool _showPreviousArrow;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeatherVM()
        {
            WeatherModel = new WeatherModel();
            ShowNextArrow = true;
            ShowPreviousArrow = false;
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Initialize model
        /// </summary>
        public void Initialize()
        {
            WeatherModel.Initialize();
        }

        /// <summary>
        /// Handles inout click
        /// </summary>
        /// <param name="xPos">Click X pos</param>
        /// <param name="yPos">Click Y pos</param>
        /// <param name="fullWidget">Widget ref</param>
        public void InputClick(int xPos, int yPos, WeatherWidgetFull fullWidget)
        {
            double margin = 0.12;
            double targetPos = 0;
   
            if(xPos > Application.Current.MainWindow.ActualWidth * (1 - margin))
            {
                targetPos = 0.3 * Application.Current.MainWindow.ActualWidth;
            }
            else if (xPos < Application.Current.MainWindow.ActualWidth * margin)
            {
                targetPos = -0.3 * Application.Current.MainWindow.ActualWidth;
            }

            if (targetPos != 0)
            {
                targetPos += fullWidget.ScrollContainer.HorizontalOffset;
                fullWidget.OnScrollClick((int)targetPos);
                ShowPreviousArrow = targetPos > 0;
                ShowNextArrow = targetPos < fullWidget.ScrollContainer.ScrollableWidth;
            }      
        }

        #endregion
    }
}