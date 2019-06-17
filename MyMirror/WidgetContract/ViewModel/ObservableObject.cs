// -----------------------------------------------------------------------
// <copyright file="ObservableObject.cs">
//
// </copyright>
// <summary>Contains ObservableObject class</summary>
// -----------------------------------------------------------------------

namespace WingetContract.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Provide functions for data binding
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChanged notify
        /// </summary>
        /// <param name="prop">Property handled</param>
        public void NotifyPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Modify variable value if necessary and notify a property changed event
        /// </summary>
        /// <typeparam name="T">Type of the variable</typeparam>
        /// <param name="field">Variable to set</param>
        /// <param name="value">New value of the variable</param>
        /// <param name="propertyName">Name of the changed property</param>
        protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }
    }
}
