
namespace MyMirror.ViewModel
{
    using MyMirror.Model;
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;
    using WingetContract;
    using WingetContract.Enum;
    using WingetContract.ViewModel;

    internal class MainWindowVM : ViewModelBase
    {
        #region Properties

        public UserControl TopLeftWinget
        {
            get =>_topLeftWinget;
            set => Set(ref _topLeftWinget, value);
        }

        public UserControl CenterWinget
        {
            get => _centerWinget;
            set => Set(ref _centerWinget, value);
        }

        public ICommand TestButtonCommand { get; set; }

        public ICommand MainWindowLoadedCommand { get; set; }

        #endregion


        #region Private members

        private MainModel _mainModel;

        private UserControl _topLeftWinget;

        private UserControl _centerWinget;
        
        #endregion


        #region Contructor

        public MainWindowVM()
        {
            TestButtonCommand = new RelayCommand(TestButton);
            MainWindowLoadedCommand = new RelayCommand(MainWindowLoaded);
        }

        #endregion


        #region Public functions

        #endregion


        #region Private functions

        private void TestButton(object obj)
        {
            Console.WriteLine("test button");

            /*if(CenterWinget == null)
            {
                ViewModelBase vmSave = _topLeftWinget.DataContext as ViewModelBase;
                CenterWinget = new TimeWingetFull
                {
                    DataContext = vmSave
                };
                TopLeftWinget = null;
                ((TimeVM)_centerWinget.DataContext).FullScreenMode = true;
            }
            else
            {
                ViewModelBase vmSave = _centerWinget.DataContext as ViewModelBase;
                TopLeftWinget = new TimeWingetReduce
                {
                    DataContext = vmSave
                };
                CenterWinget = null;
            }*/
        }

        private void MainWindowLoaded(object obj)
        {
            CreateSizeDictionnary();

            _mainModel = new MainModel();
            _mainModel.LoadWinget();

            LoadWinget();
        }

        private void LoadWinget()
        {
            CenterWinget = null;

            foreach(IWinget winget in _mainModel.WingetList)
            {
                if (winget.WingetPosition == WingetPositionEnum.TopLeft)
                {
                    TopLeftWinget = winget.ReduceWinget;
                    winget.Initialize();
                }
            }
        }

        #endregion
    }
}
