using Midi.Devices;

namespace ReCommand8.Device_Classes
{
    interface IVuMeter
    {
        void UpdateMeter(float level);
        void SetDevice(IOutputDevice device);
    }
}