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
        public const int DATA_BUFFER_SIZE = 4096;

        public TcpClient Socket { get; private set; }

        private readonly int m_ID;
        private NetworkStream m_Stream;
        private byte[] m_ReceiveBuffer;

        public TCPConnection(int id)
        {
            m_ID = id;
        }

        public void Connect(TcpClient socket)
        {
            Socket = socket;
            Socket.ReceiveBufferSize = DATA_BUFFER_SIZE;
            Socket.SendBufferSize = DATA_BUFFER_SIZE;

            m_Stream = Socket.GetStream();

            m_ReceiveBuffer = new byte[DATA_BUFFER_SIZE];

            m_Stream.BeginRead(m_ReceiveBuffer, 0, DATA_BUFFER_SIZE, ReceiveCallback, null);
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

                // TODO: handle data

                m_Stream.BeginRead(m_ReceiveBuffer, 0, DATA_BUFFER_SIZE, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving TCP data: {e}");

                // TODO: disconnect
            }
        }
    }
}
