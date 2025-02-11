using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public abstract class Level
    {
        private List<LevelObject> m_Objects;

        public Level()
        {
            m_Objects = new List<LevelObject>();

            Construct();
        }

        public List<LevelObject> GetObjects()
        {
            return m_Objects;
        }

        protected void AddLevelObject(LevelObject obj)
        {
            m_Objects.Add(obj);
        }

        protected abstract void Construct();
        public abstract void Update();
    }
}
