using Midi.Devices;

namespace ReCommand8.Device_Classes
{
    class VuMeter : IVuMeter
    {
        private readonly byte[] _neg42DBytes;
        private readonly byte[] _neg12DBytes;
        private readonly byte[] _neg6DBytes;
        private readonly byte[] _neg3DBytes;
        private readonly byte[] _zeroDBytes;
        private IOutputDevice _midiDevice;

        public VuMeter(byte[] neg42DBytes, byte[] neg12DBytes, byte[] neg6DBytes, byte[] neg3DBytes, byte[] zeroDBytes)
        {
            _neg42DBytes = neg42DBytes;
            _neg12DBytes = neg12DBytes;
            _neg6DBytes = neg6DBytes;
            _neg3DBytes = neg3DBytes;
            _zeroDBytes = zeroDBytes;

        }

        public void SetDevice(IOutputDevice device)
        {
            _midiDevice = device;
        }

        public void UpdateMeter(float level)
        {

        }

    }
}
