namespace MyMirror.Model
{
    using System;

    public class ScreenInputEventArg : EventArgs
    {
        public double XPos { get; set; } 
        public double YPos { get; set; }
        public double ZPos { get; set; }
        public bool Exit { get; set; }

        public ScreenInputEventArg(double x, double y, double z, bool exit = false)
        {
            XPos = x;
            YPos = y;
            ZPos = z;
            Exit = exit;
        }
    }
}
