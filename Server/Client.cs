using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Client
    {
        public int ID { get; private set; }
        public TCPConnection TCP { get; private set; }

        public Client(int id)
        {
            ID = id;
            TCP = new TCPConnection(id);
        }
    }
}
