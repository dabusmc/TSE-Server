using Server.Game.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public class World
    {
        private int m_LevelIndex;

        private Dictionary<int, Player> m_PlayerMap;

        public World()
        {
            LevelManager.Init();

            m_LevelIndex = 0;
            m_PlayerMap = new Dictionary<int, Player>();
        }

        /// <summary>
        /// Get the ID for the current level loaded
        /// </summary>
        /// <returns>The ID for the current loaded level</returns>
        public int GetCurrentLevel()
        {
            return m_LevelIndex;
        }

        /// <summary>
        /// Generates the world using the current Level
        /// </summary>
        public void GenerateWorld()
        {
            Level lvl = LevelManager.GetLevel(m_LevelIndex);
            lvl.Construct();
        }

        public void AddPlayer(int clientID, Player player)
        {
            if(!m_PlayerMap.ContainsKey(clientID))
            {
                m_PlayerMap.Add(clientID, player);
            }
        }

        public void NewPlayer(int clientID)
        {
            AddPlayer(clientID, new Player(clientID));
        }

        public Player GetPlayer(int clientID)
        {
            if(m_PlayerMap.ContainsKey(clientID))
            {
                return m_PlayerMap[clientID];
            }

            return null;
        }

        public void RemovePlayer(int clientID)
        {
            if(m_PlayerMap.ContainsKey(clientID))
            {
                m_PlayerMap.Remove(clientID);
            }
        }
    }
}
