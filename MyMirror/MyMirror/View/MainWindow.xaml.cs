namespace MyMirror.View
{
    using MyMirror.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media.Animation;
    using WingetContract.Enum;

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<WidgetPositionEnum, bool> _widgetsVisibility;
        private readonly double _sideSize;
        private readonly double _botSide;

        public MainWindow()
        {
            _sideSize = SystemParameters.PrimaryScreenWidth / 4;
            _botSide = SystemParameters.PrimaryScreenHeight / 4;

            _widgetsVisibility = new Dictionary<WidgetPositionEnum, bool>()
            {
                {WidgetPositionEnum.TopLeft, true },
                {WidgetPositionEnum.Top, true },
                {WidgetPositionEnum.TopRight, true },
                {WidgetPositionEnum.Left, true },
                {WidgetPositionEnum.Right, true },
                {WidgetPositionEnum.Bot, true }
            };

            InitializeComponent();

            OnShowElementEvent(null, new ShowElementEventArgs(WidgetPositionEnum.Right, false));
            OnShowElementEvent(null, new ShowElementEventArgs(WidgetPositionEnum.Left, false));
            OnShowElementEvent(null, new ShowElementEventArgs(WidgetPositionEnum.Bot, false));

            ((MainWindowVM)DataContext).ShowElementEvent += OnShowElementEvent;
        }

        private void OnShowElementEvent(object sender, ShowElementEventArgs args)
        {
            if(_widgetsVisibility[args.Position] != args.Show)
            {
                _widgetsVisibility[args.Position] = args.Show;

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    switch (args.Position)
                    {
                        case WidgetPositionEnum.Right:
                            {
                                RightWidget.BeginAnimation( MarginProperty, args.Show ? GetRightInAnimation() : GetRightOutAnimation());
                                break;
                            }
                        case WidgetPositionEnum.Left:
                            {
                                LeftWidget.BeginAnimation(MarginProperty, args.Show ? GetLeftInAnimation() : GetLeftOutAnimation());
                                break;
                            }
                        case WidgetPositionEnum.Bot:
                            {
                                BotWidget.BeginAnimation(MarginProperty, args.Show ? GetBotInAnimation() : GetBotOutAnimation());
                                break;
                            }
                    }
                }));
            }
        }

        private ThicknessAnimation GetRightInAnimation()
        {
            return new ThicknessAnimation
            {
                From = new Thickness(_sideSize, 0, -_sideSize, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            };
        }

        private ThicknessAnimation GetRightOutAnimation()
        {
            return new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(_sideSize, 0, -_sideSize, 0),
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            };
        }

        private ThicknessAnimation GetLeftInAnimation()
        {
            return new ThicknessAnimation
            {
                From = new Thickness(-_sideSize, 0, _sideSize, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            };
        }

        private ThicknessAnimation GetLeftOutAnimation()
        {
            return new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(-_sideSize, 0, _sideSize, 0),
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            };
        }

        private ThicknessAnimation GetBotInAnimation()
        {
            return new ThicknessAnimation
            {
                From = new Thickness(0, _botSide, 0, -_botSide),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            };
        }

        private ThicknessAnimation GetBotOutAnimation()
        {
            return new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, _botSide, 0, -_botSide),
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            };
        }
    }
}
