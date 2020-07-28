using System;
using System.Linq;
using Midi.Devices;
using Midi.Enums;

namespace ReCommand8.Device_Classes
{
    internal class Fader : IFader
    {
        public event EventHandler<FaderChangedEvent> FaderChangedHandler;
        public readonly string[] MapStrings;
        public int Value = 0;
        public int Track = 0;

        public Fader(int track, string[] mapStrings)
        {
            Track = track;
            MapStrings = mapStrings;
        }

        public int GetId()
        {
            return Track;
        }

        public void SetValue(int value)
        {
            Value = value;
            OnFaderChangedHandler(new FaderChangedEvent(Track, Value));
        }


        public void SetFader(IOutputDevice device, int value)
        {
            if (value > 127 || value < 0)
            {
                return;
            }

            if (Value != value)
            {
                Value = value;
            }
            OnFaderChangedHandler(new FaderChangedEvent(Track, Value));
            device.SendControlChange(Channel.Channel1, (Control) Convert.ToInt32(MapStrings[1]), value);
        }

        public bool IsFader(string name)
        {
            return (MapStrings.Contains(name));
        }

        protected virtual void OnFaderChangedHandler(FaderChangedEvent e)
        {
            FaderChangedHandler?.Invoke(this, e);
        }
    }
}
