// -----------------------------------------------------------------------
// <copyright file="ClickCircle.cs">
//
// </copyright>
// <summary>Click animation</summary>
// -----------------------------------------------------------------------

namespace MyMirror.ViewModel
{
    using Common.ViewModel;

    /// <summary>
    /// Click animation
    /// </summary>
    internal class ClickCircle : ObservableObject
    {
        /// <summary>
        /// Gets circle size
        /// </summary>
        public double Size
        {
            get => _size;
            set
            {
                Set(ref _size, value);
                NotifyPropertyChanged(nameof(XPos));
                NotifyPropertyChanged(nameof(YPos));
            }
        }

        /// <summary>
        /// Gets circle X position
        /// </summary>
        public double XPos
        {
            get => _xPos - Size / 2;
            set => Set(ref _xPos, value);
        }

        /// <summary>
        /// Gets circle Y position
        /// </summary>
        public double YPos
        {
            get => _yPos - Size / 2;
            set => Set(ref _yPos, value);
        }

        /// <summary>
        /// Gets circle Opcacity
        /// </summary>
        public double Opacity
        {
            get => _opacity;
            set => Set(ref _opacity, value);
        }

        /// <summary>
        /// Circle size
        /// </summary>
        double _size;

        /// <summary>
        /// Circle X pos
        /// </summary>
        double _xPos;

        /// <summary>
        /// Circle Y pos
        /// </summary>
        double _yPos;

        /// <summary>
        /// Circle opacity
        /// </summary>
        double _opacity;
    }
}
