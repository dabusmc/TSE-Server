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
            packet.WriteInt(2_147_483_647);
            packet.WriteFloat(10.5f);
            packet.WriteLong(9_223_372_036_854_775_807);
            packet.WriteShort(-32_768);
            packet.WriteBool(true);
            packet.WriteString("Hello, World!");

            // NOTE: This line is temporary and is only done to ensure that m_Readable is set appropriately.
            // When actually sending packets, this will be done on the senders side and then the packet
            // will be reconstructed on the other side, therefore using the constructor Packet(byte[] data) and
            // not requiring this line to set m_Readable
            packet.ToArray();

            // NOTE: The data MUST be read back in the same order it was written
            int id = packet.ReadInt();
            Console.WriteLine(id);
            int i = packet.ReadInt();
            Console.WriteLine(i);
            float f = packet.ReadFloat();
            Console.WriteLine(f);
            long l = packet.ReadLong();
            Console.WriteLine(l);
            short s = packet.ReadShort();
            Console.WriteLine(s);
            bool b = packet.ReadBool();
            Console.WriteLine(b);
            string str = packet.ReadString();
            Console.WriteLine(str);

            Console.Read();
        }
    }
}
