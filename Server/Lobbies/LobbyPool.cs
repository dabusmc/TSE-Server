using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Lobbies
{
    class LobbyPool
    {
        public static int LobbyCount = 0;
        public static int MaxLobbiesPerPage = 10;
        private static Dictionary<int, Lobby> s_Pool;

        /// <summary>
        /// Initialises the LobbyPool
        /// </summary>
        /// <param name="poolSize">The number of Lobbies to keep in the pool</param>
        public static void Init(int poolSize)
        {
            s_Pool = new Dictionary<int, Lobby>();

            for(int i = 0; i < poolSize; i++)
            {
                Lobby l = new Lobby();
                int id = l.GetID();

                s_Pool.Add(id, l);

                LobbyCount += 1;
            }
        }

        /// <summary>
        /// Gets the next Lobby that has space within it
        /// </summary>
        /// <returns>The ID of the next available Lobby. If none are available: -1</returns>
        public static int GetNextAvailableLobby()
        {
            foreach (int key in s_Pool.Keys)
            {
                if (!s_Pool[key].IsFull())
                {
                    return key;
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds the Lobby that contains a certain client
        /// </summary>
        /// <param name="clientID">The client to search for</param>
        /// <returns>The ID of the Lobby that contains the client. If the client can't be found: -1</returns>
        public static int FindLobbyWithClient(int clientID)
        {
            foreach(int key in s_Pool.Keys)
            {
                if (s_Pool[key].GetClientInLobby(clientID) != -1)
                {
                    return key;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets a certain Lobby from its ID
        /// </summary>
        /// <param name="id">The ID of the Lobby to grab</param>
        /// <returns>The Lobby object if the ID is valid, otherwise null</returns>
        public static Lobby GetLobbyFromID(int id)
        {
            if(s_Pool.ContainsKey(id))
                return s_Pool[id];
            
            return null;
        }
    }
}
