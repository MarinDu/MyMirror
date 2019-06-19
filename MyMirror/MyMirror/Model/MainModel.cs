// -----------------------------------------------------------------------
// <copyright file="WidgetLoader.cs">
//
// </copyright>
// <summary>Contains class MainModel</summary>
// -----------------------------------------------------------------------

namespace MyMirror.Model
{
    using MyMirror.Model.Input;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Controls;
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
        public ICollection<IWidget> WingetList { get; private set; }

        /// <summary>
        /// Gets screen input
        /// </summary>
        public List<IScreenInput> ScreenInputs { get; private set; }

        /// <summary>
        /// Load widgets
        /// </summary>
        public void LoadWinget()
        {
            WingetList = WidgetLoader<IWidget>.LoadWingets("Widgets");
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
            //ScreenInputs = new List<IScreenInput>() { new LeapMotionInput() , new MouseInput() };
            ScreenInputs = new List<IScreenInput>() { new LeapMotionInput() };
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
            _soundLevel = 0;
            AddVolume(30);
        }

        #region Import function for clicking

        /// <summary>
        /// Imported mouse click function
        /// </summary>
        /// <param name="dwFlags">Click flag</param>
        /// <param name="dx">X position</param>
        /// <param name="dy">Y position</param>
        /// <param name="cButtons"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        /// <summary>
        /// Performs a mouse right click
        /// </summary>
        public void DoMouseClick()
        {
            mouse_event(0x02 | 0x04, (uint)System.Windows.Forms.Cursor.Position.X, (uint)System.Windows.Forms.Cursor.Position.Y, 0, 0);
        }

        #endregion

        #region Manage Soung

        /// <summary>
        /// Add volume
        /// </summary>
        /// <param name="delta">Volume to add, in percent</param>
        /// <param name="soundManagableElements">Sound manageable wingets list </param>
        public void AddVolume(int delta, List<ISoundManageable> soundManagableElements = null)
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

            if(soundManagableElements != null)
            {
                foreach (var widget in soundManagableElements)
                {
                    widget.SetSoundVolume(_soundLevel);
                }
            }    
        }

        #endregion
    }
}