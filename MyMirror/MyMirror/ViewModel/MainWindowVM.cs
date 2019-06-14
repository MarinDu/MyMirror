
namespace MyMirror.ViewModel
{
    using MyMirror.Model;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using WingetContract;
    using WingetContract.Enum;
    using WingetContract.ViewModel;

    internal class MainWindowVM : ViewModelBase
    {
        #region Properties

        public Dictionary<int, UserControl> Widgets
        {
            get =>_widgets;
            set => Set(ref _widgets, value);
        }

        
        public UserControl CenterWinget
        {
            get => _centerWidget;
            set => Set(ref _centerWidget, value);
        }

        /*public ClickCircle ClickCircle
        {
            get => _clickCircle;
            set => Set(ref _clickCircle, value);
        }*/

        public ICommand TestButtonCommand { get; set; }

        public ICommand MainWindowLoadedCommand { get; set; }
       
        #endregion


        #region Private members

        private MainModel _mainModel;

        private Dictionary<int, UserControl> _widgets;

        
        private UserControl _centerWidget;

        //private ClickCircle _clickCircle;

        #endregion


        #region Contructor

        public MainWindowVM()
        {
            TestButtonCommand = new RelayCommand(TestButton);
            MainWindowLoadedCommand = new RelayCommand(MainWindowLoaded);

            //ClickCircle = new ClickCircle();
        }

        #endregion


        #region Public functions

        #endregion


        #region Private functions

        private void TestButton(object obj)
        {
            Console.WriteLine("test button");
        }

        private void MainWindowLoaded(object obj)
        {
            CreateSizeDictionnary();

            _mainModel = new MainModel();
            _mainModel.LoadWinget();

            _mainModel.LoadInput();
            _mainModel.ScreenInput.ScreenInputEvent += OnScreenInputEvent;

            LoadWinget();
        }

        private void LoadWinget()
        {
            CenterWinget = null;

            Widgets = new Dictionary<int, UserControl>();

            foreach (IWidget winget in _mainModel.WingetList)
            {
                Widgets[(int)winget.WingetPosition] = winget.ReduceWinget;
                winget.Initialize();
            }
            NotifyPropertyChanged(nameof(Widgets));
        }

        private void OnScreenInputEvent(object sender, ScreenInputEventArg e)
        {
            if(e.Exit)
            {
                CenterWinget = null;
            }
            else if (CenterWinget == null)
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
                                CenterWinget = winget.FullWinget;
                                CenterWinget.DataContext = vmSave;
                            }));
                        }
                        else
                        {
                            ShowWinget(clickPos);
                        }
                    }
                }
            }
        }

        private void ShowWinget(WidgetPositionEnum clickPos)
        {

        }

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
