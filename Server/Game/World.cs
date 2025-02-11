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

        public World()
        {
            LevelManager.Init();

            m_LevelIndex = 0;
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
    }
}
