using System;
using Midi.Devices;

namespace ReCommand8.Device_Classes
{
    internal interface IFader
    {
        event EventHandler<FaderChangedEvent> FaderChangedHandler;
        int GetId();
        bool IsFader(string name);
        void SetFader(IOutputDevice device, int value);
        void SetValue(int value);
    }
}