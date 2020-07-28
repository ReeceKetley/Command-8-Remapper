namespace ReCommand8.Device_Classes
{
    public class FaderChangedEvent
    {
        public int Track;
        public int Value;

        public FaderChangedEvent(int track, int value)
        {
            Track = track;
            Value = value;
        }
    }
}