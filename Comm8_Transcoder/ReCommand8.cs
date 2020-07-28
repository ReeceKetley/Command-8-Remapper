using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReCommand8.Device_Classes;
using ReCommand8.MidiOutput;
using Message = SimpleTCP.Message;

namespace ReCommand8
{
    class ReCommand8
    {
        private Command8 _command8 = new Command8();
        private readonly ReCommand8Midi _output = new ReCommand8Midi();

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        public ReCommand8()
        {
            _command8.EncoderChangedUp += _command8_EncoderChangedUp;
            _command8.EncoderChangedDown += _command8_EncoderChangedDown;
            _command8.ButtonPressed += _command8_ButtonPressed;
            _command8.FaderChanged += _command8_FaderChanged;
            _output.NewMessageReceived += _output_NewMessageReceived;
        }

        private void _command8_EncoderChangedDown(object sender, EncoderChangedDownEvent e)
        {
            if (e.Track == 1)
            {
                _output.SendMidiData(new byte[]{176, 10, 63});
            }
            if (e.Track == 2)
            {
                _output.SendMidiData(new byte[] { 176, 11, 63 });
            }
            if (e.Track == 3)
            {
                _output.SendMidiData(new byte[] { 176, 12, 63 });
            }
            if (e.Track == 4)
            {
                _output.SendMidiData(new byte[] { 176, 13, 63 });
            }
            if (e.Track == 5)
            {
                _output.SendMidiData(new byte[] { 176, 14, 63 });
            }
            if (e.Track == 6)
            {
                _output.SendMidiData(new byte[] { 176, 15, 63 });
            }
            if (e.Track == 7)
            {
                _output.SendMidiData(new byte[] { 176, 16, 63 });
            }
            if (e.Track == 8)
            {
                _output.SendMidiData(new byte[] { 176, 17, 63 });
            }
        } // TODO: Fix these

        private void _command8_EncoderChangedUp(object sender, EncoderChangedUpEvent e)
        {
            if (e.Track == 1)
            {
                _output.SendMidiData(new byte[] { 176, 10, 65 });
            }
            if (e.Track == 2)
            {
                _output.SendMidiData(new byte[] { 176, 11, 65 });
            }
            if (e.Track == 3)
            {
                _output.SendMidiData(new byte[] { 176, 12, 65 });
            }
            if (e.Track == 4)
            {
                _output.SendMidiData(new byte[] { 176, 13, 65 });
            }
            if (e.Track == 5)
            {
                _output.SendMidiData(new byte[] { 176, 14, 65 });
            }
            if (e.Track == 6)
            {
                _output.SendMidiData(new byte[] { 176, 15, 65 });
            }
            if (e.Track == 7)
            {
                _output.SendMidiData(new byte[] { 176, 16, 65 });
            }
            if (e.Track == 8)
            {
                _output.SendMidiData(new byte[] { 176, 17, 65 });
            }
        } // TODO: Fix these

        private void _output_NewMessageReceived(object sender, byte[] e)
        {
            CheckAndSetFaderPostion(e);
        }

        private void CheckAndSetFaderPostion(byte[] e)
        {
            if (e[0] == 224)
            {
                _command8.SetFader(1, e[2]);
            }
            if (e[0] == 225)
            {
                _command8.SetFader(2, e[2]);
            }
            if (e[0] == 226)
            {
                _command8.SetFader(3, e[2]);
            }
            if (e[0] == 227)
            {
                _command8.SetFader(4, e[2]);
            }
            if (e[0] == 228)
            {
                _command8.SetFader(5, e[2]);
            }
            if (e[0] == 229)
            {
                _command8.SetFader(6, e[2]);
            }
            if (e[0] == 230)
            {
                _command8.SetFader(7, e[2]);
            }
            if (e[0] == 231)
            {
                _command8.SetFader(8, e[2]);
            }
        }

        public void SendKey(string key)
        {
            var processlist = Process.GetProcesses();
            var title = "";
            foreach (var process in processlist)
            {
                if (String.IsNullOrEmpty(process.MainWindowTitle)) continue;
                if (process.MainWindowTitle.Contains("Ableton Live"))
                {
                    title = process.MainWindowTitle;
                }
            }

            var zero = IntPtr.Zero;
            for (int i = 0; (i < 60) && (zero == IntPtr.Zero); i++)
            {
                Thread.Sleep(500);
                zero = FindWindow(null, title);
            }

            if (zero == IntPtr.Zero) return;
            SetForegroundWindow(zero);
            SendKeys.SendWait(key);
            SendKeys.Flush();
        } // TODO: Fix Insert 

        private string ByteArrayToString(byte[] bytes)
        {
            if (bytes == null) return "null";
            string joinedBytes = string.Join(", ", bytes.Select(b => b.ToString()));
            return $"{joinedBytes}";
        }


        private void UpdateScreen(string text, int line) // TODO: Fix
        {
            _command8.UpdateScreen(text, line);
        }

        private void UpdateVu(int track, int value) // TODO: Fix
        {
            _command8.UpdateVu(track, value);
        }

        private void _command8_FaderChanged(object sender, FaderChangedEvent e)
        {
            var data = _builder.CreateCommand(CommandType.Fader, e.Track, e.Value);
            switch (e.Track)
            {
                case 1:
                    _output.SendMidiData(new byte[]{224, (byte)e.Value, (byte) e.Value});
                    break;
                case 2:
                    _output.SendMidiData(new byte[] { 225, (byte)e.Value, (byte)e.Value });
                    break;
                case 3:
                    _output.SendMidiData(new byte[] { 226, (byte)e.Value, (byte)e.Value });
                    break;
                case 4:
                    _output.SendMidiData(new byte[] { 227, (byte)e.Value, (byte)e.Value });
                    break;
                case 5:
                    _output.SendMidiData(new byte[] { 228, (byte)e.Value, (byte)e.Value });
                    break;
                case 6:
                    _output.SendMidiData(new byte[] { 229, (byte)e.Value, (byte)e.Value });
                    break;
                case 7:
                    _output.SendMidiData(new byte[] { 230, (byte)e.Value, (byte)e.Value });
                    break;
                case 8:
                    _output.SendMidiData(new byte[] { 231, (byte)e.Value, (byte)e.Value });
                    break;
            }
        }

        private void _command8_ButtonPressed(object sender, ButtonMap.ButtonCode e)
        {
            switch (e)
            {
                case ButtonMap.ButtonCode.REW:
                    SendButtonPress(new byte[3] { 144, 91, 127 });
                    break;
                case ButtonMap.ButtonCode.PLAY:
                    SendButtonPress(new byte[3] { 144, 94, 127 });
                    break;
                case ButtonMap.ButtonCode.STOP:
                    SendButtonPress(new byte[3] { 144, 93, 127 });
                    break;
                case ButtonMap.ButtonCode.UNDEFINED:
                    _command8 = new Command8();
                    break;
                case ButtonMap.ButtonCode.RTZ:
                    SendButtonPress(new byte[3] { 144, 89, 127 });
                    break;
                case ButtonMap.ButtonCode.FWD:
                    SendButtonPress(new byte[3] { 144, 92, 127 });
                    break;
                case ButtonMap.ButtonCode.REC:
                    SendButtonPress(new byte[3] { 144, 95, 127 });
                    break;
                case ButtonMap.ButtonCode.PLUGIN:
                    SendButtonPress(new byte[3] { 144, 43, 127 });
                    break;
                case ButtonMap.ButtonCode.MIX:

                    break;
                case ButtonMap.ButtonCode.EDIT:
                    break;
                case ButtonMap.ButtonCode.LOOP_PLAY:
                    SendButtonPress(new byte[3] { 144, 87, 127 });
                    break;
                case ButtonMap.ButtonCode.LOOP_REC:
                    SendButtonPress(new byte[3] { 144, 88, 127 });
                    break;
                case ButtonMap.ButtonCode.QUICK_PUNCH:
                    SendButtonPress(new byte[3] { 144, 86, 127 });
                    break;
                case ButtonMap.ButtonCode.MEM_LOCK:
                    break;
                case ButtonMap.ButtonCode.MASTER_FADERS:
                    break;
                case ButtonMap.ButtonCode.DOWN:
                    SendButtonPress(new byte[3] { 144, 97, 127 });
                    break;
                case ButtonMap.ButtonCode.FOCUS:
                    break;
                case ButtonMap.ButtonCode.FLIP:
                    SendButtonPress(new byte[3] { 144, 50, 127 });
                    break;
                case ButtonMap.ButtonCode.LEFT:
                    SendButtonPress(new byte[3] { 144, 98, 127 });
                    break;
                case ButtonMap.ButtonCode.RIGHT:
                    SendButtonPress(new byte[3] { 144, 99, 127 });
                    break;
                case ButtonMap.ButtonCode.FADER_MUTE:
                    break;
                case ButtonMap.ButtonCode.UP:
                    SendButtonPress(new byte[3] { 144, 96, 127 });
                    break;
                case ButtonMap.ButtonCode.BANK:
                    break;
                case ButtonMap.ButtonCode.NUDGE:
                    break;
                case ButtonMap.ButtonCode.ZOOM:
                    SendButtonPress(new byte[3] { 144, 100, 127 });
                    break;
                case ButtonMap.ButtonCode.ENTER:
                    SendButtonPress(new byte[3] { 144, 81, 127 });
                    break;
                case ButtonMap.ButtonCode.UNDO:
                    SendButtonPress(new byte[3] { 144, 76, 127 });
                    break;
                case ButtonMap.ButtonCode.CHANNEL_RECORD:
                    break;
                case ButtonMap.ButtonCode.PAN_MASTER:
                    break;
                case ButtonMap.ButtonCode.CTRL_CLUTCH:
                    break;
                case ButtonMap.ButtonCode.WIN_KEY:
                    break;
                case ButtonMap.ButtonCode.SHIFT_ADD:
                    break;
                case ButtonMap.ButtonCode.ALT:
                    break;
                case ButtonMap.ButtonCode.DEFAULT:
                    break;
                case ButtonMap.ButtonCode.MON:
                    break;
                case ButtonMap.ButtonCode.E:
                    break;
                case ButtonMap.ButtonCode.INSERT:
                    SendKey("^(t)");
                    break;
                case ButtonMap.ButtonCode.D:
                    break;
                case ButtonMap.ButtonCode.SEND:
                    break;
                case ButtonMap.ButtonCode.C:
                    break;
                case ButtonMap.ButtonCode.PAN:
                    break;
                case ButtonMap.ButtonCode.B:
                    break;
                case ButtonMap.ButtonCode.CONSOLE_VIEW:
                    break;
                case ButtonMap.ButtonCode.DISPLAY_MODE:
                    SendKey("{TAB}");
                    break;
                case ButtonMap.ButtonCode.MUTE_1:
                    SendButtonPress(new byte[3] { 144, 16, 127 });
                    break;
                case ButtonMap.ButtonCode.MUTE_2:
                    SendButtonPress(new byte[3] { 144, 17, 127 });
                    break;
                case ButtonMap.ButtonCode.MUTE_3:
                    SendButtonPress(new byte[3] { 144, 18, 127 });
                    break;
                case ButtonMap.ButtonCode.MUTE_4:
                    SendButtonPress(new byte[3] { 144, 19, 127 });
                    break;
                case ButtonMap.ButtonCode.MUTE_5:
                    SendButtonPress(new byte[3] { 144, 20, 127 });
                    break;
                case ButtonMap.ButtonCode.MUTE_6:
                    SendButtonPress(new byte[3] { 144, 21, 127 });
                    break;
                case ButtonMap.ButtonCode.MUTE_7:
                    SendButtonPress(new byte[3] { 144, 22, 127 });
                    break;
                case ButtonMap.ButtonCode.MUTE_8:
                    SendButtonPress(new byte[3] { 144, 23, 127 });
                    break;
                case ButtonMap.ButtonCode.SOLO_1:
                    SendButtonPress(new byte[3] { 144, 8, 127 });
                    break;
                case ButtonMap.ButtonCode.SOLO_2:
                    SendButtonPress(new byte[3] { 144, 9, 127 });
                    break;
                case ButtonMap.ButtonCode.SOLO_3:
                    SendButtonPress(new byte[3] { 144, 10, 127 });
                    break;
                case ButtonMap.ButtonCode.SOLO_4:
                    SendButtonPress(new byte[3] { 144, 11, 127 });
                    break;
                case ButtonMap.ButtonCode.SOLO_5:
                    SendButtonPress(new byte[3] { 144, 12, 127 });
                    break;
                case ButtonMap.ButtonCode.SOLO_6:
                    SendButtonPress(new byte[3] { 144, 13, 127 });
                    break;
                case ButtonMap.ButtonCode.SOLO_7:
                    SendButtonPress(new byte[3] { 144, 14, 127 });
                    break;
                case ButtonMap.ButtonCode.SOLO_8:
                    SendButtonPress(new byte[3] { 144, 15, 127 });
                    break;
                case ButtonMap.ButtonCode.SELECT_1:
                    SendButtonPress(new byte[3] { 144, 24, 127 });
                    break;
                case ButtonMap.ButtonCode.SELECT_2:
                    SendButtonPress(new byte[3] { 144, 25, 127 });
                    break;
                case ButtonMap.ButtonCode.SELECT_3:
                    SendButtonPress(new byte[3] { 144, 26, 127 });
                    break;
                case ButtonMap.ButtonCode.SELECT_4:
                    SendButtonPress(new byte[3] { 144, 27, 127 });
                    break;
                case ButtonMap.ButtonCode.SELECT_5:
                    SendButtonPress(new byte[3] { 144, 28, 127 });
                    break;
                case ButtonMap.ButtonCode.SELECT_6:
                    SendButtonPress(new byte[3] { 144, 29, 127 });
                    break;
                case ButtonMap.ButtonCode.SELECT_7:
                    SendButtonPress(new byte[3] { 144, 30, 127 });
                    break;
                case ButtonMap.ButtonCode.SELECT_8:
                    SendButtonPress(new byte[3] { 144, 31, 127 });
                    break;
                case ButtonMap.ButtonCode.EQ:
                    SendButtonPress(new byte[3] { 144, 44, 127 });
                    break;
                case ButtonMap.ButtonCode.DYNAMICS:
                    SendButtonPress(new byte[3] { 144, 45, 127 });
                    break;
                case ButtonMap.ButtonCode.MIX_INSERT:

                    break;
                case ButtonMap.ButtonCode.PAN_SEND_PRE:
                    SendButtonPress(new byte[3] { 144, 42, 127 });
                    break;
                case ButtonMap.ButtonCode.PAGE_L:
                    SendButtonPress(new byte[3] { 144, 46, 127 });
                    break;
                case ButtonMap.ButtonCode.PAGE_R:
                    SendButtonPress(new byte[3] { 144, 47, 127 });
                    break;
                case ButtonMap.ButtonCode.MASTER_BYPASS:
                    break;
                case ButtonMap.ButtonCode.ESC:
                    SendButtonPress(new byte[3] { 144, 80, 127 });
                    break;
            }
        }

        private void SendButtonPress(byte[] data)
        {
            _output.SendMidiData(data);
            data[2] = 0;
            _output.SendMidiData(data);
        } // TODO: Handle ON/OFF events seprately
    }
}
