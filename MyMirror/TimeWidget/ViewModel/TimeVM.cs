// -----------------------------------------------------------------------
// <copyright file="TimeVM.cs">
//
// </copyright>
// <summary>Contains Time widget view model</summary>
// -----------------------------------------------------------------------

namespace TimeWidget.ViewModel
{
    using Common.ViewModel;
    using TimeWidget.Model;

    /// <summary>
    /// Contains Time widget view model
    /// </summary>
    internal class TimeVM : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets Time Model
        /// </summary>
        public TimeModel TimeModel { get; private set; }

        #endregion

        #region Default constructor

        /// <summary>
        /// Default constructeur
        /// </summary>
        public TimeVM()
        {
            TimeModel = new TimeModel();
        }

        #endregion
    }
}