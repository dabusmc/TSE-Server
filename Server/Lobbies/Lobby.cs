using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Lobby
    {
        private int m_ID;
        private List<int> m_ConnectedClients;

        public Lobby()
        {
            // TODO: Generate ID
            m_ConnectedClients = new List<int>();
        }

        public Lobby(int id)
        {
            m_ID = id;
            m_ConnectedClients = new List<int>();
        }

        public int GetID()
        {
            return m_ID;
        }

        public bool IsFull()
        {
            return m_ConnectedClients.Count >= Constants.MAX_PLAYERS_PER_LOBBY;
        }

        public bool ConnectClient(int clientID)
        {
            if (IsFull()) return false;

            m_ConnectedClients.Add(clientID);
            return true;
        }

        public void DisconnectClient(int clientID)
        {
            if(m_ConnectedClients.Contains(clientID))
            {
                m_ConnectedClients.Remove(clientID);
            }
        }

        public bool ClientInLobby(int clientID)
        {
            return m_ConnectedClients.Contains(clientID);
        }
    }
}
