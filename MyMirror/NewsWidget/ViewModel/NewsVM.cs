// -----------------------------------------------------------------------
// <copyright file="SpoNewsVMtifyVM.cs">
//
// </copyright>
// <summary>Contains News widget view model</summary>
// -----------------------------------------------------------------------

namespace NewsWidget.ViewModel
{
    using NewsWidget.Model;
    using Common.ViewModel;
    using Common.Settings;

    /// <summary>
    /// Contains Niews widget view model
    /// </summary>
    internal class NewsVM : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets news Model
        /// </summary>
        public NewsModel NewsModel { get; private set; }
        public ISettingsBase Settings { get; internal set; }

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public NewsVM()
        {
            NewsModel = new NewsModel();
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Initialize model
        /// </summary>
        public void Initialize()
        {
            NewsModel.Initialize();
        }

        /// <summary>
        /// Handles inout click
        /// </summary>
        /// <param name="xPos">Click X pos</param>
        /// <param name="yPos">Click Y pos</param>
        public void InputClick(int xPos, int yPos)
        {

        }

        #endregion
    }
}
