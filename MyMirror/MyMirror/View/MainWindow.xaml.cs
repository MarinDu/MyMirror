namespace MyMirror.View
{
    using MyMirror.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using WingetContract.Enum;
    using Common.Annimations;

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<WidgetPositionEnum, bool> _widgetsVisibility;
        private readonly int _annimationDuration;

        public MainWindow()
        {
            _annimationDuration = 300;

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
            OnShowElementEvent(null, new ShowElementEventArgs(WidgetPositionEnum.Top, false));

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
                                RightWidget.BeginAnimation(MarginProperty, args.Show ? ThicknessAnimationFactory.GetRightInAnimation(RightWidget.ActualWidth, _annimationDuration) :
                                    ThicknessAnimationFactory.GetRightOutAnimation(RightWidget.ActualWidth, _annimationDuration));
                                break;
                            }
                        case WidgetPositionEnum.Left:
                            {
                                LeftWidget.BeginAnimation(MarginProperty, args.Show ? ThicknessAnimationFactory.GetLeftInAnimation(LeftWidget.ActualWidth, _annimationDuration) :
                                    ThicknessAnimationFactory.GetLeftOutAnimation(LeftWidget.ActualWidth, _annimationDuration));
                                break;
                            }
                        case WidgetPositionEnum.Bot:
                            {
                                BotWidget.BeginAnimation(MarginProperty, args.Show ? ThicknessAnimationFactory.GetBotInAnimation(BotWidget.ActualHeight, _annimationDuration) :
                                    ThicknessAnimationFactory.GetBotOutAnimation(BotWidget.ActualHeight, _annimationDuration));
                                break;
                            } 
                        case WidgetPositionEnum.Top:
                            {
                                TopWidget.BeginAnimation(MarginProperty, args.Show ? ThicknessAnimationFactory.GetTopInAnimation(TopWidget.ActualHeight, _annimationDuration) :
                                    ThicknessAnimationFactory.GetTopOutAnimation(TopWidget.ActualHeight, _annimationDuration));
                                break;
                            }
                    }
                }));
            }
        }

    }
}
