// -----------------------------------------------------------------------
// <copyright file="ManagementWindowVM.cs">
//
// </copyright>
// <summary>Contains class ManagementWindowVM</summary>
// -----------------------------------------------------------------------

namespace MyMirror.ViewModel
{
    using Common.Log;
    using Common.ViewModel;
    using System.Windows;
    using System.Windows.Input;
    using System;
    using MyMirror.View;
    using MyMirror.Model;
    using System.Collections.Generic;
    using MyMirror.Properties;
    using Common.Settings;

    /// <summary>
    /// View model for the management window
    /// </summary>
    internal class ManagementWindowVM : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets exit button command
        /// </summary>
        public ICommand ExitButtonCommand { get; private set; }

        /// <summary>
        /// Gets return button command
        /// </summary>
        public ICommand ReturnButtonCommand { get; private set; }

        /// <summary>
        /// Gets save button command
        /// </summary>
        public ICommand SaveButtonCommand { get; private set; }

        /// <summary>
        /// Gets log button command
        /// </summary>
        public ICommand LogButtonCommand { get; private set; }
        
        /// <summary>
        /// Gets main model
        /// </summary>
        public MainModel MainModel
        {
            get => _mainModel;
            private set => Set(ref _mainModel, value);
        }

        /// <summary>
        /// Tab items list
        /// </summary>
        public List<ParametersTabItem> TabItems
        {
            get => _tabItems;
            private set => Set(ref _tabItems, value);
        }

        #endregion

        #region Private members

        /// <summary>
        /// Main model
        /// </summary>
        private MainModel _mainModel;

        /// <summary>
        /// Tab items list
        /// </summary>
        private List<ParametersTabItem> _tabItems;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ManagementWindowVM()
        {
            ExitButtonCommand = new RelayCommand(ExitButton);
            ReturnButtonCommand = new RelayCommand(ReturnButton);
            SaveButtonCommand = new RelayCommand(SaveButton);
            LogButtonCommand = new RelayCommand(LogButton);
            
            MainModel = MainModel.Instance;

            _mainModel.InitializeMainSettings();
            LoadParameters();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Handles exit command
        /// </summary>
        /// <param name="obj">Parameters</param>
        private void ExitButton(object obj)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Handles exit command
        /// </summary>
        /// <param name="obj">Parameters</param>
        private void ReturnButton(object obj)
        {
            ((Window)obj)?.Close();
        }

        /// <summary>
        /// Handles exit command
        /// </summary>
        /// <param name="obj">Parameters</param>
        private void SaveButton(object obj)
        {
            // Update main parameters
            MainModel.MainSettings.Settings.SetSettingsList(_tabItems[0].Items);

            // Update widgets parameters

            // Update input parameters

            // Save parameters
            MainModel.MainSettings.Save();

            //Restart application
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }    

        /// <summary>
        /// Handles log command
        /// </summary>
        /// <param name="obj">Parameters</param>
        private void LogButton(object obj)
        {
            new LogWindow().ShowDialog();
        }

        /// <summary>
        /// Load parameters
        /// </summary>
        private void LoadParameters()
        {
            List<ParametersTabItem> tabItems = new List<ParametersTabItem>();

            // Add main items
            ParametersTabItem tab = new ParametersTabItem
            {               
                Title = Resources.MainTabName,
                Items = MainModel.MainSettings.Settings.GetSettingsList()
            };
            tabItems.Add(tab);

            // Add widgets parameters

            // Add input parameters

            TabItems = tabItems;
        }

        #endregion
    }
}
