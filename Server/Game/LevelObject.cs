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
        private Vector3 m_Position;
        private Vector3 m_Rotation;
        private Vector3 m_Scale;

        private List<ObjectComponent> m_Components;

        public LevelObject()
        {
            m_Position = new Vector3();
            m_Rotation = new Vector3();
            m_Scale = new Vector3();

            m_Components = new List<ObjectComponent>();
        }

        protected void AddComponent(ObjectComponentType type, ObjectComponentData data)
        {
            m_Components.Add(new ObjectComponent(type, data));
        }
    }
}
