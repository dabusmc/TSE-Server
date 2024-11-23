using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class TCPConnection
    {
        public TcpClient Socket { get; private set; }

        private readonly int m_ID;
        private NetworkStream m_Stream;
        private Packet m_ReceivedData;
        private byte[] m_ReceiveBuffer;

        public TCPConnection(int id)
        {
            m_ID = id;
        }

        public void Connect(TcpClient socket)
        {
            Socket = socket;
            Socket.ReceiveBufferSize = Constants.DATA_BUFFER_SIZE;
            Socket.SendBufferSize = Constants.DATA_BUFFER_SIZE;

            m_Stream = Socket.GetStream();

            m_ReceivedData = new Packet();
            m_ReceiveBuffer = new byte[Constants.DATA_BUFFER_SIZE];

            m_Stream.BeginRead(m_ReceiveBuffer, 0, Constants.DATA_BUFFER_SIZE, ReceiveCallback, null);
            ServerSend.Welcome(m_ID, "Welcome to the game!");
        }

        public void SendData(Packet packet)
        {
            try
            {
                if(Socket != null)
                {
                    m_Stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error sending data to client {m_ID} via TCP: {e}");
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = m_Stream.EndRead(result);
                if (byteLength <= 0)
                {
                    // TODO: disconnect
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(m_ReceiveBuffer, data, byteLength);

                m_ReceivedData.Reset(HandleData(data));
                m_Stream.BeginRead(m_ReceiveBuffer, 0, Constants.DATA_BUFFER_SIZE, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving TCP data: {e}");

                // TODO: disconnect
            }
        }

        private bool HandleData(byte[] data)
        {
            int packetLength = 0;
            m_ReceivedData.SetBytes(data);

            if(m_ReceivedData.LengthLeft() >= 4)
            {
                packetLength = m_ReceivedData.ReadInt();
                if(packetLength <= 0)
                {
                    return true;
                }
            }

            while (packetLength > 0 && packetLength <= m_ReceivedData.LengthLeft())
            {
                byte[] packetBytes = m_ReceivedData.Read(packetLength);

                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using(Packet packet = new Packet(packetBytes))
                    {
                        int packetID = packet.ReadInt();
                        Server.PacketHandlers[packetID](m_ID, packet);
                    }
                });

                packetLength = 0;
                if(m_ReceivedData.LengthLeft() >= 4)
                {
                    packetLength = m_ReceivedData.ReadInt();
                    if(packetLength <= 0)
                    {
                        return true;
                    }
                }
            }

            return packetLength <= 1;
        }
    }
}
