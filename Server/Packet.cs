using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

        public void WriteInt(int data)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(data));
        }

        public void WriteFloat(float data)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(data));
        }

        public void WriteLong(long data)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(data));
        }

        public void WriteShort(short data)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(data));
        }

        public void WriteBool(bool data)
        {
            m_Buffer.AddRange(BitConverter.GetBytes(data));
        }

        public void WriteString(string data)
        {
            WriteInt(data.Length);
            m_Buffer.AddRange(Encoding.ASCII.GetBytes(data)); // NOTE: This only allows the sending of ASCII characters
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

        public byte[] Read(int length)
        {
            if(m_Buffer.Count > m_ReadPos && m_Buffer.Count > m_ReadPos + length)
            {
                byte[] values = new byte[length];
                Array.Copy(m_Readable, m_ReadPos, values, 0, length);
                m_ReadPos += length;
                return values;
            }
            else
            {
                throw new Exception("Could not read the value of bytes");
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

        public float ReadFloat()
        {
            if(m_Buffer.Count > m_ReadPos)
            {
                float value = BitConverter.ToSingle(m_Readable, m_ReadPos);
                m_ReadPos += 4;
                return value;
            }
            else
            {
                throw new Exception("Could not read the value of float");
            }
        }

        public long ReadLong()
        {
            if(m_Buffer.Count > m_ReadPos)
            {
                long value = BitConverter.ToInt64(m_Readable, m_ReadPos);
                m_ReadPos += 8;
                return value;
            }
            else
            {
                throw new Exception("Could not read the value of long");
            }
        }

        public short ReadShort()
        {
            if (m_Buffer.Count > m_ReadPos)
            {
                short value = BitConverter.ToInt16(m_Readable, m_ReadPos);
                m_ReadPos += 2;
                return value;
            }
            else
            {
                throw new Exception("Could not read the value of short");
            }
        }
        
        public bool ReadBool()
        {
            if (m_Buffer.Count > m_ReadPos)
            {
                bool value = BitConverter.ToBoolean(m_Readable, m_ReadPos);
                m_ReadPos += 1;
                return value;
            }
            else
            {
                throw new Exception("Could not read the value of bool");
            }
        }

        public string ReadString()
        {
            try
            {
                int length = ReadInt();
                string value = Encoding.ASCII.GetString(m_Readable, m_ReadPos, length);
                m_ReadPos += length;
                return value;
            }
            catch
            {
                throw new Exception("Could not read the value of string");
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
