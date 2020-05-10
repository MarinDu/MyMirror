
namespace SpotifyWidget.View
{
    using Common.Annimations;
    using System.Windows.Controls;


    /// <summary>
    /// Logique d'interaction pour SpotifyWidgetFull.xaml
    /// </summary>
    public partial class SpotifyWidgetFull : UserControl
    {
        /// <summary>
        /// Scroll bar annimator
        /// </summary>
        ScrollBarAnimator _scrollBarAnimator;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SpotifyWidgetFull()
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
