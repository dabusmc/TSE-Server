using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Packet packet = new Packet(12);

            // NOTE: This line is temporary and is only done to ensure that m_Readable is set appropriately.
            // When actually sending packets, this will be done on the senders side and then the packet
            // will be reconstructed on the other side, therefore using the constructor Packet(byte[] data) and
            // not requiring this line to set m_Readable
            packet.ToArray();

            int id = packet.ReadInt();
            Console.WriteLine(id);

            Console.Read();
        }
    }
}
