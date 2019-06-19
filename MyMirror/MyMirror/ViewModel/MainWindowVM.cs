﻿// -----------------------------------------------------------------------
// <copyright file="MainWindowVM.cs">
//
// </copyright>
// <summary>Contains class MainWindowVM</summary>
// -----------------------------------------------------------------------

namespace MyMirror.ViewModel
{
    using MyMirror.Model;
    using MyMirror.Model.Input;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using WingetContract;
    using WingetContract.Enum;
    using WingetContract.ViewModel;

    /// <summary>
    /// View model for the main window
    /// </summary>
    internal class MainWindowVM : ViewModelBase
    {
        #region Event

        /// <summary>
        /// Show element event
        /// </summary>
        public event EventHandler<ShowElementEventArgs> ShowElementEvent;

        #endregion

        #region Properties

        /// <summary>
        /// Gets widgets dictionary
        /// </summary>
        public Dictionary<int, UserControl> Widgets
        {
            get =>_widgets;
            private set => Set(ref _widgets, value);
        }

        /// <summary>
        /// Gets central widget
        /// </summary>
        public UserControl CenterWidget
        {
            get => _centerWidget;
            private  set => Set(ref _centerWidget, value);
        }

        /// <summary>
        /// Gets click circle animation
        /// </summary>
        public ClickCircle ClickCircle
        {
            get => _clickCircle;
            private set => Set(ref _clickCircle, value);
        }

        /// <summary>
        /// Gets sound visibility
        /// </summary>
        public bool SoundVisibility
        {
            get => _soundVisibility;
            private set => Set(ref _soundVisibility, value);
        }
    
        /// <summary>
        /// Gets test button command
        /// </summary>
        public ICommand TestButtonCommand { get; private set; }

        /// <summary>
        /// Gets main window loaded command
        /// </summary>
        public ICommand MainWindowLoadedCommand { get; private set; }

        /// <summary>
        /// Gets main model
        /// </summary>
        public MainModel MainModel
        {
            get => _mainModel;
            private set => Set(ref _mainModel, value);
        }

        #endregion

        #region Private members

        /// <summary>
        /// Main model
        /// </summary>
        private MainModel _mainModel;

        /// <summary>
        /// Widgets dictionary
        /// </summary>
        private Dictionary<int, UserControl> _widgets;

        /// <summary>
        /// Widgets visibility timers
        /// </summary>
        private Dictionary<int, Timer> _wingetVisibilityTimers;

        /// <summary>
        /// Sound visibility timer
        /// </summary>
        private Timer _soundTimer;

        /// <summary>
        /// Sleep mode timer
        /// </summary>
        private Timer _sleepModeTimer;

        /// <summary>
        /// Central widget
        /// </summary>
        private UserControl _centerWidget;

        /// <summary>
        /// Click circle animation
        /// </summary>
        public ClickCircle  _clickCircle;

        /// <summary>
        /// Sound visibility
        /// </summary>
        private bool _soundVisibility;

        /// <summary>
        /// If sleep mode is activated
        /// </summary>
        private bool _sleepMode;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowVM()
        {
            TestButtonCommand = new RelayCommand(TestButton);
            MainWindowLoadedCommand = new RelayCommand(MainWindowLoaded);

            _soundTimer = new Timer(1000);
            _soundTimer.Elapsed += OnSoundTimer;

            _sleepModeTimer = new Timer(60000);
            _sleepModeTimer.Elapsed += OnSleepModeTimer;

            ClickCircle = new ClickCircle();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Handles test button commands
        /// </summary>
        /// <param name="obj">Parameters</param>
        private void TestButton(object obj)
        {
            Console.WriteLine("test button");
        }

        /// <summary>
        /// Handles main windows loaded event
        /// </summary>
        /// <param name="obj">Parameters</param>
        private void MainWindowLoaded(object obj)
        {
            CreateSizeDictionnary();

            MainModel = new MainModel();
            _mainModel.LoadWinget();

            _mainModel.LoadInput();

            foreach (IScreenInput input in _mainModel.ScreenInputs)
            {
                input.ScreenInputEvent += OnScreenInputEvent;
            }

            Widgets = new Dictionary<int, UserControl>();
            _wingetVisibilityTimers = new Dictionary<int, Timer>();

            LoadWidget();
        }

        /// <summary>
        /// Load widgets
        /// </summary>
        private void LoadWidget()
        {
            CenterWidget = null;
            foreach (IWidget winget in _mainModel.WingetList)
            {
                Widgets[(int)winget.WingetPosition] = winget.ReduceWinget;
                _wingetVisibilityTimers[(int)winget.WingetPosition] = new Timer(500);
                _wingetVisibilityTimers[(int)winget.WingetPosition].Elapsed += OnVisibilityTimer;
                winget.Initialize();
            }

            NotifyPropertyChanged(nameof(Widgets));
        }

        /// <summary>
        /// Handles screen input events
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnScreenInputEvent(object sender, ScreenInputEventArg e)
        {
            ClickCircle.Opacity = 0f;

            // If sleep mode exit : exit
            if(_sleepMode && e.Gesture != InputGestureEnum.None)
            {
                _sleepMode = false;
                CenterWidget = null;
            }

            if (e.Gesture == InputGestureEnum.Exit)
            {
                if (CenterWidget == null)
                {
                    // If no central winget : show everything 3s
                    foreach (WidgetPositionEnum pos in (WidgetPositionEnum[])Enum.GetValues(typeof(WidgetPositionEnum)))
                    {
                        ShowWinget(pos, true, 3000);
                    }
                }
                else
                {
                    // Else exit it
                    CenterWidget = null;
                }
            }
            else if (e.Gesture == InputGestureEnum.Click || e.Gesture == InputGestureEnum.Position)
            {
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)e.XPos, (int)e.YPos);

                ClickCircle.XPos = e.XPos;
                ClickCircle.YPos = e.YPos;
                ClickCircle.Opacity = 1f;
                ClickCircle.Size = e.Gesture == InputGestureEnum.Position ? SizeDict[8] : SizeDict[5];

                if (CenterWidget == null)
                {
                    // Get cursor position
                    WidgetPositionEnum cursorPosition = GetClickPos(e.XPos, e.YPos);

                    // Get associated widget
                    foreach (IWidget winget in _mainModel.WingetList)
                    {
                        if (winget.WingetPosition == cursorPosition)
                        {
                            if (e.Gesture == InputGestureEnum.Click)
                            {
                                // Show widget full version
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    ViewModelBase vmSave = _widgets[(int)cursorPosition].DataContext as ViewModelBase;
                                    CenterWidget = winget.FullWinget;
                                    CenterWidget.DataContext = vmSave;
                                }));
                            }
                            else
                            {
                                // Show widget reduce version
                                ShowWinget(cursorPosition, true);
                            }
                        }
                    }
                }
                else
                {
                    if (e.Gesture == InputGestureEnum.Click)
                    {
                        _mainModel.DoMouseClick();
                    }
                }
            }
            else if (e.Gesture == InputGestureEnum.RollIn || e.Gesture == InputGestureEnum.Rollout)
            {
                SoundVisibility = true;
                _soundTimer.Stop();
                _soundTimer.Start();

                // Manage sound
                List<ISoundManageable> manageableElement = new List<ISoundManageable>();
                foreach (var item in _widgets)
                {
                    if(item.Value.DataContext is ISoundManageable)
                    {
                        manageableElement.Add((ISoundManageable)item.Value.DataContext);
                    }
                }
                _mainModel.AddVolume(e.Gesture == InputGestureEnum.RollIn ? 2 : -2, manageableElement);
            }

            // Manage sleep mode
            if(e.Gesture == InputGestureEnum.None)
            {
                if (!_sleepModeTimer.Enabled)
                {
                    _sleepModeTimer.Start();
                }
            }
            else
            {
                if (_sleepModeTimer.Enabled)
                {
                    _sleepModeTimer.Stop();
                }
            }
        }

        /// <summary>
        /// Show widget
        /// </summary>
        /// <param name="clickPos">Widget postion</param>
        /// <param name="show">Whether whow widgets</param>
        /// <param name="duration">Showing duration</param>
        private void ShowWinget(WidgetPositionEnum clickPos, bool show, double duration = 500)
        {
            if(_wingetVisibilityTimers.ContainsKey((int)clickPos))
            {
                _wingetVisibilityTimers[(int)clickPos].Stop();
                ShowElementEvent?.Invoke(this, new ShowElementEventArgs(clickPos, show));

                if (show)
                {
                    _wingetVisibilityTimers[(int)clickPos].Interval = duration;
                    _wingetVisibilityTimers[(int)clickPos].Start();
                }
            }
        }

        /// <summary>
        /// Handles widgets visibility timer events
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnVisibilityTimer(object sender, ElapsedEventArgs e)
        {
            WidgetPositionEnum pos = WidgetPositionEnum.None;

            foreach (var timer in _wingetVisibilityTimers)
            {
                if(timer.Value == (Timer)sender)
                {
                    pos = (WidgetPositionEnum)timer.Key;
                    break;
                }
            }
            ShowWinget(pos, false);
        }

        /// <summary>
        /// Handles sound visibility timer
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnSoundTimer(object sender, ElapsedEventArgs e)
        {
            SoundVisibility = false;
        }

        /// <summary>
        /// Handles sleep mode timer event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnSleepModeTimer(object sender, ElapsedEventArgs e)
        {
            foreach (IWidget winget in _mainModel.WingetList)
            {
                if (winget.ShowOnSleep)
                {
                    _sleepMode = true;

                    // Show widget full version
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ViewModelBase vmSave = _widgets[(int)winget.WingetPosition].DataContext as ViewModelBase;
                        CenterWidget = winget.FullWinget;
                        CenterWidget.DataContext = vmSave;
                    }));
                    break;
                }
            }
        }

        /// <summary>
        /// Gets wiget position enum from X Y pos
        /// </summary>
        /// <param name="xPos">X os</param>
        /// <param name="yPos">y pos</param>
        /// <returns>WidgetPositionEnum value</returns>
        private WidgetPositionEnum GetClickPos(double xPos, double yPos)
        {
            WidgetPositionEnum ret = WidgetPositionEnum.None;

            if(2 * xPos < XCenter)
            {
                if(3 * yPos < YCenter)
                {
                    ret = WidgetPositionEnum.TopLeft;
                }
                else
                {
                    ret = WidgetPositionEnum.Left;
                }
            }
            else if (xPos > 1.5 * XCenter)
            {
                if (3 * yPos < YCenter)
                {
                    ret = WidgetPositionEnum.TopRight;
                }
                else
                {
                    ret = WidgetPositionEnum.Right;
                }
            }
            else
            {
                if (3 * yPos < YCenter)
                {
                    ret = WidgetPositionEnum.Top;
                }
                else if (yPos > 1.5 * YCenter)
                {
                    ret = WidgetPositionEnum.Bot;
                }
            }

            return ret;
        }

        #endregion
    }
}
