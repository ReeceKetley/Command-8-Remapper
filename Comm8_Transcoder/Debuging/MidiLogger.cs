using System;
using System.Threading;

namespace ReCommand8.Debuging
{
    class MidiLogger
    {
        public static TeVirtualMIDI port;

        public MidiLogger()
        {
            TeVirtualMIDI.logging(TeVirtualMIDI.TE_VM_LOGGING_MISC | TeVirtualMIDI.TE_VM_LOGGING_RX | TeVirtualMIDI.TE_VM_LOGGING_TX);
            Guid manufacturer = new Guid("aa4e075f-3504-4aab-9b06-9a4104a91cf0");
            Guid product = new Guid("bb4e075f-3504-4aab-9b06-9a4104a91cf0");
            port = new TeVirtualMIDI("ReCommand8", 65535, TeVirtualMIDI.TE_VM_FLAGS_PARSE_RX, ref manufacturer, ref product);
            Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
            thread.Start();
        }

        public static void WorkThreadFunction()
        {

            byte[] command;

            try
            {
                while (true)
                {

                    command = port.getCommand();

                    port.sendCommand(command);

                    var arrayToString = byteArrayToString(command);
                    if (arrayToString.Length < 2)
                    {
                        continue;
                    }

                    if (arrayToString == "90, 0x02, 0x4D" || arrayToString == "90, 0x04, 0x0D" || arrayToString == "90, 0x03, 0x0D")
                    {
                        continue;
                    }
                    Console.WriteLine("byteList.Add(new byte[] {0x" + arrayToString + "});");

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



    }
}
