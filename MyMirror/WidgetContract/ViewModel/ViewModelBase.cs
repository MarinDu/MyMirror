namespace WingetContract.ViewModel
{
    using System.Collections.Generic;
    using System.Windows;

    public abstract class ViewModelBase : ObservableObject
    {
        private const int AvailableFonSize = 100;

        private const float SizeCoef = 0.01f;

        public static Dictionary<float, float> SizeDict { get; private set; }

        public static double XCenter { get; private set; }
        public static double YCenter { get; private set; }

        protected void CreateSizeDictionnary()
        {
            if (SizeDict == null)
            {
                float height = (float)Application.Current.MainWindow.ActualHeight;

                // Create new dictionnary
                Dictionary <float, float> dic = new Dictionary<float, float>();

                for (int i = 1; i < AvailableFonSize + 1; i++)
                {
                    dic.Add(i, 16 / 9 * SizeCoef * i * height);
                }

                SizeDict = dic;

                XCenter = SystemParameters.PrimaryScreenWidth / 2;
                YCenter = SystemParameters.PrimaryScreenHeight / 2;
            }
        }
    }
}
