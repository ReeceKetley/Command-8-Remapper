using System;

namespace ReCommand8.Device_Classes
{
    internal interface IEncoder
    {
        event EventHandler<EncoderChangedUpEvent> EncoderChangedUp;
        event EventHandler<EncoderChangedDownEvent> EncoderChangedDown;
        int GetId();
        void SetValue(int value);
        bool IsEncoder(string name);
        void Process(int value);
    }
}