using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Lobbies
{
    class LobbyPool
    {
        private static Dictionary<int, Lobby> m_Pool;

        public static void Init(int poolSize)
        {
            m_Pool = new Dictionary<int, Lobby>();

            for(int i = 0; i < poolSize; i++)
            {
                Lobby l = new Lobby(i);
                int id = l.GetID();

                m_Pool.Add(id, l);
            }
        }

        public static int GetNextAvailableLobby()
        {
            for(int i = 0; i < m_Pool.Count; i++)
            {
                if (!m_Pool[i].IsFull())
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindLobbyWithClient(int clientID)
        {
            for(int i = 0; i < m_Pool.Count; i++)
            {
                if (m_Pool[i].ClientInLobby(clientID))
                {
                    return i;
                }
            }

            return -1;
        }

        public static Lobby GetLobbyFromID(int id)
        {
            return m_Pool[id];
        }
    }
}
