using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Midi.Devices;
using Midi.Enums;
using Midi.Messages;

namespace ReCommand8.Device_Classes
{
    class Command8
    {
        // Variables
        private readonly Screen _screen;
        private readonly IInputDevice _midiDevice;
        private readonly IOutputDevice _midiOutDevice;
        private readonly IFader _fader1 = new Fader(1, new[] { "16", "56", "32", "24", "48", "40", "8", "0" });
        private readonly IFader _fader2 = new Fader(2, new[] { "25", "49", "41", "17", "33", "57", "9", "ModulationWheel" });
        private readonly IFader _fader3 = new Fader(3, new[] { "2", "26", "50", "Pan", "34", "18", "42", "58" });
        private readonly IFader _fader4 = new Fader(4, new[] { "Expression", "19", "27", "43", "51", "35", "3", "59" });
        private readonly IFader _fader5 = new Fader(5, new[] { "36", "4", "44", "52", "12", "28", "20", "60" });
        private readonly IFader _fader6 = new Fader(6, new[] { "45", "37", "5", "53", "13", "29", "21", "61" });
        private readonly IFader _fader7 = new Fader(7, new[] { "DataEntryLSB", "14", "30", "DataEntryMSB", "46", "22", "54", "62" });
        private readonly IFader _fader8 = new Fader(8, new[] { "39", "15", "23", "47", "Volume", "31", "55", "63" });
        private readonly IEncoder _encoder1 = new Encoder(1, "SustainPedal");
        private readonly IEncoder _encoder2 = new Encoder(2, "65");
        private readonly IEncoder _encoder3 = new Encoder(3, "66");
        private readonly IEncoder _encoder4 = new Encoder(4, "67");
        private readonly IEncoder _encoder5 = new Encoder(5, "68");
        private readonly IEncoder _encoder6 = new Encoder(6, "69");
        private readonly IEncoder _encoder7 = new Encoder(7, "70");
        private readonly IEncoder _encoder8 = new Encoder(8, "71");
        private IVuMeter _vuMeter1 = new VuMeter(new byte[] { 144, 80, 0 }, new byte[] { 144, 88, 0 }, new byte[] { 144, 92, 0 }, new byte[] { 144, 94, 0 }, new byte[] { 144, 127, 0 });

        private int _currentBank = 0;
        // Events
        public event EventHandler<ButtonMap.ButtonCode> ButtonPressed;
        public event EventHandler<FaderChangedEvent> FaderChanged;
        public event EventHandler<EncoderChangedDownEvent> EncoderChangedDown;
        public event EventHandler<EncoderChangedUpEvent> EncoderChangedUp;
        //



        /// <summary>
        /// Private stuff shouldnt be public.
        /// </summary>
        private readonly List<string> _activeButtons = new List<string>();
        private readonly List<IFader> _faders = new List<IFader>();
        private readonly List<IEncoder> _encoders = new List<IEncoder>();

        public Command8()
        {
            Console.WriteLine("Establishing MIDI Connection to Command|8...");
            SetUpFaders();
            SetUpEncoders();
            var devices = Midi.Devices.DeviceManager.InputDevices;
            var outDevices = Midi.Devices.DeviceManager.OutputDevices;
            foreach (var inputDevice in devices)
            {
                if (inputDevice.Name.Contains("Command8") || inputDevice.Name.Contains("Command|8")) // For the differant driver versions.
                {
                    _midiDevice = inputDevice;
                    _midiDevice.ControlChange += MidiDevice_ControlChange;
                    _midiDevice.NoteOn += MidiDevice_NoteOn;
                    Open();
                    break;
                }
            }
            foreach (var outDevice in outDevices)
            {
                if (outDevice.Name.Contains("Command8") || outDevice.Name.Contains("Command|8")) // For the differant driver versions.
                {
                    _midiOutDevice = outDevice;
                    _midiOutDevice.Open();
                    //WakeUp();
                    break;
                }
            }
            //Test();
            _screen = new Screen(_midiOutDevice);
            //Screen.ClearScreen();
            _screen.PrintText("V1.0.3", 0);// TODO: Fix the screen command.
            //Thread.Sleep(5000);
            ActivateLeds();
            FaderTest();
        }

        public void SetFader(int track, int value)
        {
            foreach (var fader in _faders)
            {
                if (fader.GetId() != track) continue;
                fader.SetFader(_midiOutDevice, value);
                return;
            }
        }

        public void SetEncoder(int track, int value)
        {
            foreach (var encoder in _encoders)
            {
                if (encoder.GetId() != track) continue;
                encoder.SetValue(value);
                return;
            }
        }

        public void UpdateScreen(string text, int line)
        {
            _screen.PrintText(text, line);
        }

        public void ClearScreen()
        {
            _screen.ClearScreen();
        }

        public void UpdateVu(int track, int value)
        {
            // TODO: Impliment this once i've decoded the bytes required for the rest of the VU's 
        }

        private void Test()
        {


        }
        public string ByteArrayToString(byte[] bytes)
        {
            if (bytes == null) return "null";
            string joinedBytes = string.Join(", ", bytes.Select(b => b.ToString()));
            return $"new byte[] {{ {joinedBytes} }}";
        }


        private void ActivateLeds()
        {
            Console.WriteLine("Activating LEDS...");
            var pitchValues = Enum.GetValues(typeof(Pitch));
            foreach (Pitch pitchValue in pitchValues)
            {
                for (int j = 60; j < 80; j++)
                {
                    _midiOutDevice.SendNoteOn(Channel.Channel1, pitchValue, j);
                }
            }
        }

        private void FaderTest()
        {
            Console.WriteLine("Testing Faders...");
            Enum.TryParse("Channel" + 1, out Channel selectedChannel);
            Console.WriteLine("Fader Test:");
            foreach (var fader in _faders)
            {
                for (int j = 0; j < 127; j++)
                {
                    fader.SetFader(_midiOutDevice, j);
                }
            }
            foreach (var fader in _faders)
            {
                var x = 127;
                for (int j = 0; j < 127; j++)
                {
                    x = x - 1;
                    fader.SetFader(_midiOutDevice, x);
                }
            }
            Thread.Sleep(100);
            foreach (var fader in _faders)
            {
                fader.SetFader(_midiOutDevice, 95);
            }
        }

        private void SetUpEncoders()
        {
            _encoders.Add(_encoder1);
            _encoders.Add(_encoder2);
            _encoders.Add(_encoder3);
            _encoders.Add(_encoder4);
            _encoders.Add(_encoder5);
            _encoders.Add(_encoder6);
            _encoders.Add(_encoder7);
            _encoders.Add(_encoder8);
            foreach (var encoder in _encoders)
            {
                encoder.EncoderChangedDown += Encoder_EncoderChangedDown;
                encoder.EncoderChangedUp += Encoder_EncoderChangedUp;
            }
        }

        private void Encoder_EncoderChangedUp(object sender, EncoderChangedUpEvent e)
        {
            OnEncoderChangedUp(e);
        }

        private void Encoder_EncoderChangedDown(object sender, EncoderChangedDownEvent e)
        {
            OnEncoderChangedDown(e);
        }

        private void EncoderOnEncoderChangedUp(object sender, EncoderChangedUpEvent e)
        {
            OnEncoderChangedUp(e);
        }
        private void SetUpFaders()
        {
            _faders.Add(_fader1);
            _faders.Add(_fader2);
            _faders.Add(_fader3);
            _faders.Add(_fader4);
            _faders.Add(_fader5);
            _faders.Add(_fader6);
            _faders.Add(_fader7);
            _faders.Add(_fader8);
            foreach (var fader in _faders)
            {
                fader.FaderChangedHandler += Fader_FaderChangedHandler;
            }
        }

        private void Fader_FaderChangedHandler(object sender, FaderChangedEvent e)
        {
            OnFaderChanged(e);
        }

        private void MidiDevice_NoteOn(NoteOnMessage msg)
        {
            if (msg.Pitch == Pitch.FNeg1 && msg.Velocity < 72)
            {
                return; // Filters out fader notes
            }

            var buttonCode = ButtonMap.ToButton(_midiOutDevice, msg.Pitch.ToString(), msg.Velocity);
            if (buttonCode == ButtonMap.ButtonCode.UNDEFINED)
            {
                return;
            }

            OnButtonPressed(buttonCode);
        }

        private void MidiDevice_ControlChange(ControlChangeMessage msg)
        {
            TrySetEncoders(msg);
            TrySetFaders(msg);
        }

        private void TrySetFaders(ControlChangeMessage msg)
        {
            foreach (var fader in _faders)
            {
                if (fader.IsFader(msg.Control.ToString()))
                {
                    fader.SetValue(msg.Value);
                }
            }
        }

        private void TrySetEncoders(ControlChangeMessage msg)
        {
            foreach (var encoder in _encoders)
            {
                if (encoder.IsEncoder(msg.Control.ToString()))
                {
                    encoder.Process(msg.Value);
                }
            }
        }

        private void Open()
        {
            try
            {
                Console.WriteLine("Command|8 Located.");
                _midiDevice.Open();
                _midiDevice.StartReceiving(null);
            }
            catch
            {
                Console.WriteLine("Failed to connect to Command8.");
            }
        }

        protected virtual void OnButtonPressed(ButtonMap.ButtonCode e)
        {
            ButtonPressed?.Invoke(this, e);
        }

        protected virtual void OnFaderChanged(FaderChangedEvent e)
        {
            FaderChanged?.Invoke(this, e);
        }

  
        protected virtual void OnEncoderChangedUp(EncoderChangedUpEvent e)
        {
            EncoderChangedUp?.Invoke(this, e);
        }

        protected virtual void OnEncoderChangedDown(EncoderChangedDownEvent e)
        {
            EncoderChangedDown?.Invoke(this, e);
        }
    }
}
