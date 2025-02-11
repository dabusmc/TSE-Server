using Server.Helper.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public class LevelObject
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        public string Name;

        private List<ObjectComponent> m_Components;

        public LevelObject()
        {
            Position = new Vector3();
            Rotation = new Vector3();
            Scale = new Vector3(1.0f, 1.0f, 1.0f);

            m_Components = new List<ObjectComponent>();
        }

        /// <summary>
        /// Allows a child class to add an ObjectComponent
        /// </summary>
        /// <param name="type">The Type of ObjectComponent</param>
        /// <param name="data">The relevant data for that ObjectComponent</param>
        protected void AddComponent(ObjectComponentType type, ObjectComponentData data)
        {
            m_Components.Add(new ObjectComponent(type, data));
        }

        public List<ObjectComponent> GetObjectComponents()
        {
            return m_Components;
        }

        public int GetComponentOfType(ObjectComponentType type)
        {
            for(int i = 0; i < m_Components.Count; i++)
            {
                if (m_Components[i].Type == type)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
