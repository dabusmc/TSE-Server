using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Packet : IDisposable
    {
        private byte[] m_Readable;
        private List<byte> m_Buffer;
        private int m_ReadPos;

        public Packet()
        {
            m_Buffer = new List<byte>();
            m_ReadPos = 0;
        }

        public Packet(int id)
        {
            m_Buffer = new List<byte>();
            m_ReadPos = 0;

            InsertInt(id);
        }

        public Packet(byte[] data)
        {
            m_Buffer = new List<byte>();
            m_ReadPos = 0;

            SetBytes(data);
        }

        #region Write
        public void Write(byte data)
        {
            m_Buffer.Add(data);
        }

        public void Write(byte[] data)
        {
            m_Buffer.AddRange(data);
        }
        #endregion

        #region Read
        public byte Read()
        {
            if(m_Buffer.Count > m_ReadPos)
            {
                byte value = m_Readable[m_ReadPos];
                m_ReadPos += 1;
                return value;
            }
            else
            {
                throw new Exception("Could not read the value of byte");
            }
        }

        public int ReadInt()
        {
            if(m_Buffer.Count > m_ReadPos)
            {
                int value = BitConverter.ToInt32(m_Readable, m_ReadPos);
                m_ReadPos += 4;
                return value;
            }
            else
            {
                throw new Exception("Could not read the value of int");
            }
        }
        #endregion

        #region Utility
        public void SetBytes(byte[] _data)
        {
            Write(_data);
            m_Readable = m_Buffer.ToArray();
        }

        public void WriteLength()
        {
            m_Buffer.InsertRange(0, BitConverter.GetBytes(m_Buffer.Count)); // Insert the buffer length at the start of the packet
        }

        public void InsertInt(int val)
        {
            m_Buffer.InsertRange(0, BitConverter.GetBytes(val)); // Insert the int at the start of the packet
        }

        public byte[] ToArray()
        {
            m_Readable = m_Buffer.ToArray();
            return m_Readable;
        }

        public int Length()
        {
            return m_Buffer.Count;
        }

        public int LengthLeft()
        {
            return Length() - m_ReadPos;
        }
        #endregion

        #region GC
        private bool m_Disposed = false;

        protected virtual void DisposePacket(bool disposing)
        {
            if(!m_Disposed)
            {
                if(disposing)
                {
                    m_Buffer = null;
                    m_Readable = null;
                    m_ReadPos = 0;
                }

                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            DisposePacket(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
