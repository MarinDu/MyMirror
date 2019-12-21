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

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeatherVM()
        {
            WeatherModel = new WeatherModel();
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
        public void InputClick(int xPos, int yPos)
        {
            /*if (yPos < (int)Application.Current.MainWindow.ActualHeight / 2)
            {
                Scroll += Application.Current.MainWindow.ActualHeight / 2;
            }
            else
            {
                Scroll -= Application.Current.MainWindow.ActualHeight / 2;
            }*/
        }
        #endregion
    }
}