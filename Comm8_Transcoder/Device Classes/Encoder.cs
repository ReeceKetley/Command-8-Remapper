using System;

namespace ReCommand8.Device_Classes
{
    class Encoder : IEncoder
    {
        public event EventHandler<EncoderChangedUpEvent> EncoderChangedUp; 
        public event EventHandler<EncoderChangedDownEvent> EncoderChangedDown; 
        public string MapString;
        public int Track = 0;
        public int Value = 0;

        public Encoder(int track, string mapString)
        {
            Track = track;
            MapString = mapString;
        }


        public int GetId()
        {
            return Track;
        }

        public void SetValue(int value)
        {
            Value = value;
        }

        public bool IsEncoder(string name)
        {
            if (MapString == name)
            {
                return true;
            };

            return false;
        }

        public void Process(int value)
        {
            switch (value)
            {
                case 65:
                    if (Value == 127)
                    {
                        return;
                    }
                    Value = Value + 1;
                    OnEncoderChangedUpEvent(new EncoderChangedUpEvent(Track, Value));
                    break;
                case 63:
                    if (Value == 0)
                    {
                        return;
                    }
                    Value = Value - 1;
                    OnEncoderChangedDownEvent(new EncoderChangedDownEvent(Track, Value));
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnEncoderChangedUpEvent(EncoderChangedUpEvent e)
        {
            EncoderChangedUp?.Invoke(this, e);
        }

        protected virtual void OnEncoderChangedDownEvent(EncoderChangedDownEvent e)
        {
            EncoderChangedDown?.Invoke(this, e);
        }
    }
}
