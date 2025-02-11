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

        public void GenerateWorld()
        {
            Level lvl = LevelManager.GetLevel(m_LevelIndex);
        }
    }
}
