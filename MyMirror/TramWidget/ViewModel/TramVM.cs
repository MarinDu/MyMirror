// -----------------------------------------------------------------------
// <copyright file="TramVM.cs">
//
// </copyright>
// <summary>Contains Tram widget view model</summary>
// -----------------------------------------------------------------------

namespace TramWidget.ViewModel
{
    using TramWidget.Model;
    using WingetContract.ViewModel;

    /// <summary>
    /// Contains Tram widget view model
    /// </summary>
    internal class TramVM : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets Tram Model
        /// </summary>
        public TramModel TramModel { get; private set; }

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TramVM()
        {
            TramModel = new TramModel();
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Initialize model
        /// </summary>
        public void Initialize()
        {
            TramModel.Initialize();
        }

        #endregion
    }
}
