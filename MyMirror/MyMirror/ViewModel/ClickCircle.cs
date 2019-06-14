namespace MyMirror.ViewModel
{
    using System;
    using System.Timers;
    using WingetContract.ViewModel;

    internal class ClickCircle : ViewModelBase
    {
        public double Size
        {
            get => _size;
            private set
            {
                Set(ref _size, value);
                NotifyPropertyChanged(nameof(XPos));
                NotifyPropertyChanged(nameof(YPos));
            }
        }

        public double XPos
        {
            get => _xPos - Size / 2;
            private set => Set(ref _xPos, value);
        }

        public double YPos
        {
            get => _yPos - Size / 2;
            private set => Set(ref _yPos, value);
        }

        public double Opacity
        {
            get => _opacity;
            private set => Set(ref _opacity, value);
        }

        double _size;
        double _xPos;
        double _yPos;
        double _zPos;
        double _opacity;

        private const double _opacityTimeToZero = 1; //s

        private Timer m_updateTimer;

        public ClickCircle()
        {
            Size = 0;
            XPos = 0;
            XPos = 0;
            Opacity = 0;

            m_updateTimer = new Timer(50)
            {
                AutoReset = false
            };
            m_updateTimer.Elapsed += OnTimeUpdate;
        }

        public void SetPos(double x, double y, double z)
        {
            if(x != _xPos || y != _yPos || z != _zPos)
            {
                Opacity = 1;
                _zPos = z;

                if (_zPos < 0 )
                {
                    Size = SizeDict[5];
                }
                else if (_zPos > 10)
                {
                    Size = SizeDict[20];
                }
                else
                {
                    Size = SizeDict[20] * _zPos / 10 + SizeDict[5];
                }

                XPos = x;
                YPos = y;
                

                Console.WriteLine("x=" + x + " y=" + y + " z=" + z);
            }

            if(!m_updateTimer.Enabled)
            {
                m_updateTimer.Start();
            }
        }

        private void OnTimeUpdate(object sender, ElapsedEventArgs e)
        {
            double delta = m_updateTimer.Interval / 1000 * _opacityTimeToZero;

            if (_opacity > delta)
            {
                Opacity -= delta;
                m_updateTimer.Start();
            }
            else
            {
                Opacity = 0;
            }
        }
    }
}
