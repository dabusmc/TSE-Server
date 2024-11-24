using Server.Data;
using Server.Lobbies;
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
        public ClientData Data { get; private set; }
        public TCPConnection TCP { get; private set; }

        public Client(int id)
        {
            ID = id;
            TCP = new TCPConnection(id);
            Data = new ClientData();
        }

        public void Disconnect()
        {
            int lobby = LobbyPool.FindLobbyWithClient(ID);
            if(lobby != -1)
            {
                LobbyPool.GetLobbyFromID(lobby).DisconnectClient(ID);
                Console.WriteLine($"\"{Data.Username}\" (ID: {ID}) has been removed from lobby {lobby}");
            }

            Console.WriteLine($"\"{Data.Username}\" (ID: {ID}) has disconnected");

            TCP.Disconnect();
        }
    }
}
