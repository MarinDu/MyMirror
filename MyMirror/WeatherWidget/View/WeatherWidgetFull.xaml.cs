// -----------------------------------------------------------------------
// <copyright file="WeatherWidgetFull.cs">
//
// </copyright>
// <summary>Contains WeatherWidgetFull class</summary>
// -----------------------------------------------------------------------

namespace WeatherWidget.View
{
    using Common.Annimations;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Logique d'interaction pour WeatherWidgetFull.xaml
    /// </summary>
    public partial class WeatherWidgetFull : UserControl
    {
        /// <summary>
        /// Scroll bar annimator
        /// </summary>
        ScrollBarAnimator _scrollBarAnimator;

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeatherWidgetFull()
        {
            InitializeComponent();
            _scrollBarAnimator = new ScrollBarAnimator(ScrollContainer, Dispatcher);
        }

        /// <summary>
        /// Handles scrollbutton click
        /// </summary>
        /// <param name="xPos">X position of the click</param>
        public void OnScrollClick(int xPos)
        {
            _scrollBarAnimator.ScrollToHorizontalPosition(xPos);
        }
    }
}