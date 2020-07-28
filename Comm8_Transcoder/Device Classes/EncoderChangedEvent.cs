using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCommand8.Device_Classes
{
    class EncoderChangedUpEvent
    {
        public int Track;
        public int Value;

        public EncoderChangedUpEvent(int track, int value)
        {
            Track = track;
            Value = value;
        }
    }

    class EncoderChangedDownEvent
    {
        public int Track;
        public int Value;

        public EncoderChangedDownEvent(int track, int value)
        {
            Track = track;
            Value = value;
        }
    }
}

