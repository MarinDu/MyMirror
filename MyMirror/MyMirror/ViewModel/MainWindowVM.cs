// -----------------------------------------------------------------------
// <copyright file="MainWindowVM.cs">
//
// </copyright>
// <summary>Contains class MainWindowVM</summary>
// -----------------------------------------------------------------------

namespace MyMirror.ViewModel
{
    using MyMirror.Model;
    using System;
    using System.Collections.Generic;
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
        /// Gets test button command
        /// </summary>
        public ICommand TestButtonCommand { get; private  set; }

        /// <summary>
        /// Gets main window loaded command
        /// </summary>
        public ICommand MainWindowLoadedCommand { get; private  set; }

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
        /// Central widget
        /// </summary>
        private UserControl _centerWidget;

        #endregion

        #region Contructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowVM()
        {
            TestButtonCommand = new RelayCommand(TestButton);
            MainWindowLoadedCommand = new RelayCommand(MainWindowLoaded);
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

            _mainModel = new MainModel();
            _mainModel.LoadWinget();

            _mainModel.LoadInput();
            _mainModel.ScreenInput.ScreenInputEvent += OnScreenInputEvent;

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
            if(e.Exit)
            {
                CenterWidget = null;
            }
            else if (CenterWidget == null)
            {
                WidgetPositionEnum clickPos = GetClickPos(e.XPos, e.YPos);

                foreach (IWidget winget in _mainModel.WingetList)
                {
                    if (winget.WingetPosition == clickPos)
                    {
                        if (e.ZPos < 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ViewModelBase vmSave = _widgets[(int)clickPos].DataContext as ViewModelBase;
                                CenterWidget = winget.FullWinget;
                                CenterWidget.DataContext = vmSave;
                            }));
                        }
                        else
                        {
                            ShowWinget(clickPos, true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Show widget
        /// </summary>
        /// <param name="clickPos">Widget postion</param>
        /// <param name="show">Whether whow widgets</param>
        private void ShowWinget(WidgetPositionEnum clickPos, bool show)
        {
            ShowElementEvent?.Invoke(this, new ShowElementEventArgs(clickPos, show));
            if(show)
            {
                _wingetVisibilityTimers[(int)clickPos].Stop();
                _wingetVisibilityTimers[(int)clickPos].Start();
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

            Console.WriteLine(ret);
            return ret;
        }

        #endregion
    }
}
