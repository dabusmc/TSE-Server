using Server.Helper.Math;
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
        }

        /// <summary>
        /// Get all of the constructed LevelObjects
        /// </summary>
        /// <returns>A List of LevelObjects</returns>
        public List<LevelObject> GetObjects()
        {
            return m_Objects;
        }

        /// <summary>
        /// Allows a child class to add a LevelObject to the list of objects
        /// </summary>
        /// <param name="obj">The LevelObject to add</param>
        protected void AddLevelObject(LevelObject obj)
        {
            m_Objects.Add(obj);
        }

        /// <summary>
        /// Constructs the Level
        /// </summary>
        public abstract void Construct();

        /// <summary>
        /// Updates the Level
        /// </summary>
        public abstract void Update();

        public abstract Vector3 GetNextSpawnPoint();
    }
}
