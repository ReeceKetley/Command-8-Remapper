using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ReCommand8
{
    class PacketBuilder
    {
        public byte[] CreateCommand(CommandType type, int id, int data)
        {
            if (type == CommandType.Button)
            {

            }

            return new byte[1] {1};
        }

    }
}
