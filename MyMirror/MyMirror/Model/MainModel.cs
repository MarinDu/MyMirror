// -----------------------------------------------------------------------
// <copyright file="WidgetLoader.cs">
//
// </copyright>
// <summary>Contains class MainModel</summary>
// -----------------------------------------------------------------------

namespace MyMirror.Model
{
    using InputContract;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using WingetContract;
    using WingetContract.ViewModel;

    /// <summary>
    /// Contains application main model
    /// </summary>
    internal class MainModel : ObservableObject
    {
        /// <summary>
        /// Gets widget list
        /// </summary>
        public ICollection<IWidget> WidgetList { get; private set; }

        /// <summary>
        /// Gets screen input
        /// </summary>
        public ICollection<IScreenInput> ScreenInputs { get; private set; }

        /// <summary>
        /// Load widgets
        /// </summary>
        public void LoadWinget()
        {
            WidgetList = PluginLoader<IWidget>.LoadPlugins("Widgets", "*Widget.dll");
            AddVolume(0);
        }

        /// <summary>
        /// Gets sound level from 0 to 100
        /// </summary>
        public int SoundLevel
        {
            get => _soundLevel;
            private set => Set(ref _soundLevel, value);
        }

        /// <summary>
        /// Load input
        /// </summary>
        public void LoadInput()
        {
            ScreenInputs = PluginLoader<IScreenInput>.LoadPlugins("Inputs", "*Input.dll");
        }

        /// <summary>
        /// Current audio level
        /// </summary>
        private int _soundLevel;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainModel()
        {
            _soundLevel = 30;
        }

        #region Manage Sound

        /// <summary>
        /// Add volume
        /// </summary>
        /// <param name="delta">Volume to add, in percent</param>
        public void AddVolume(int delta)
        {
            int lvl = _soundLevel + delta;
            lvl = lvl > 100 ? 100 : lvl;
            lvl = lvl < 0 ? 0 : lvl;
            SoundLevel = lvl;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/nircmd.exe",
                Arguments = "setsysvolume " + ((int)(0xFFFF * (float)_soundLevel / 100f)).ToString(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process processTemp = new Process
            {
                StartInfo = startInfo
            };

            try
            {
                processTemp.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (IWidget widget in WidgetList)
            {
                (widget.FullWidget.DataContext as ISoundManageable)?.SetSoundVolume(_soundLevel);
            }
  
        }

        #endregion
    }
}