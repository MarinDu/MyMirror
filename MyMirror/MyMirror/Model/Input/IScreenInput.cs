namespace MyMirror.Model.Input
{
    using System;

    internal interface IScreenInput
    {
        event EventHandler<ScreenInputEventArg> ScreenInputEvent;
    }
}
