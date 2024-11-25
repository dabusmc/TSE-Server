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

        /// <summary>
        /// Gets the ID of the Lobby
        /// </summary>
        /// <returns>The integer ID for this Lobby</returns>
        public int GetID()
        {
            return m_ID;
        }

        /// <summary>
        /// Checks if the Lobby has reached maximum capacity
        /// </summary>
        /// <returns>True if the Lobby is full, otherwise False</returns>
        public bool IsFull()
        {
            return m_ConnectedClients.Count >= Constants.MAX_PLAYERS_PER_LOBBY;
        }

        /// <summary>
        /// Connects a client to this Lobby
        /// </summary>
        /// <param name="clientID">The client to connect</param>
        /// <returns>True if the client was connected successfully, otherwise false</returns>
        public bool ConnectClient(int clientID)
        {
            if (IsFull()) return false;

            m_ConnectedClients.Add(clientID);
            return true;
        }

        /// <summary>
        /// Disconnects a client from this Lobby (if they are in this Lobby)
        /// </summary>
        /// <param name="clientID">The client to disconnect</param>
        public void DisconnectClient(int clientID)
        {
            if(m_ConnectedClients.Contains(clientID))
            {
                m_ConnectedClients.Remove(clientID);
            }
        }

        /// <summary>
        /// Checks if a client is in this Lobby
        /// </summary>
        /// <param name="clientID">The client to check for</param>
        /// <returns>True if the client is in the Lobby, otherwise False</returns>
        public bool ClientInLobby(int clientID)
        {
            return m_ConnectedClients.Contains(clientID);
        }

        /// <summary>
        /// Gets the number of connected clients to this Lobby
        /// </summary>
        /// <returns>The number of connected clients</returns>
        public int Count()
        {
            return m_ConnectedClients.Count;
        }

        /// <summary>
        /// Gets a client's ID from the index into the m_ConnectedClients list
        /// </summary>
        /// <param name="index">The index into the list</param>
        /// <returns>The client's ID if the index is valid, otherwise -1</returns>
        public int GetClientInLobby(int index)
        {
            if(index >= 0 && index < m_ConnectedClients.Count)
            {
                return m_ConnectedClients[index];
            }

            return -1;
        }
    }
}
