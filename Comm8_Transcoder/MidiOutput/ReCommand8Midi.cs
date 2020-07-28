using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReCommand8.MidiOutput
{
    class ReCommand8Midi
    {
        public static TeVirtualMIDI port;
        public event EventHandler<byte[]> NewMessageReceived; 
        public ReCommand8Midi()
        {
            TeVirtualMIDI.logging(TeVirtualMIDI.TE_VM_LOGGING_MISC | TeVirtualMIDI.TE_VM_LOGGING_RX | TeVirtualMIDI.TE_VM_LOGGING_TX);
            Guid manufacturer = new Guid("aa4e075f-3504-4aab-9b06-9a4104a91cf0");
            Guid product = new Guid("bb4e075f-3504-4aab-9b06-9a4104a91cf0");
            port = new TeVirtualMIDI("ReCommand8", 65535, TeVirtualMIDI.TE_VM_FLAGS_PARSE_RX, ref manufacturer, ref product);
            Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
            thread.Start();
        }

        public void WorkThreadFunction()
        {

            byte[] command;

            try
            {
                while (true)
                {

                    command = port.getCommand();
                    OnNewMessageReceived(command);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("thread aborting: " + ex.Message);

            }

        }

        public static string byteArrayToString(byte[] ba)
        {

            string hex = BitConverter.ToString(ba);

            return hex.Replace("-", ", 0x");

        }

        public void SendMidiData(byte[] data)
        {
            port.sendCommand(data);
        }

        protected virtual void OnNewMessageReceived(byte[] e)
        {
            NewMessageReceived?.Invoke(this, e);
        }
    }
}
