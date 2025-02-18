using Server.Game.LevelObjects;
using Server.Helper;
using Server.Helper.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.Levels
{
    public class TestLevel : Level
    {
        private bool m_FirstSpawnUsed;

        public override void Construct()
        {
            m_FirstSpawnUsed = false;

            CollidableQuad ground = new CollidableQuad();
            ground.Position = new Vector3(0.0f, -2.5f, 0.0f);
            ground.Scale = new Vector3(10.0f, 0.4f, 1.0f);
            AddLevelObject(ground);
        }

        public override void Update()
        {
        }

        public override Vector3 GetNextSpawnPoint()
        {
            if(!m_FirstSpawnUsed)
            {
                m_FirstSpawnUsed = true;
                return new Vector3(-3.5f, -1.0f, 0.0f);
            }
            else
            {
                m_FirstSpawnUsed = false;
                return new Vector3(3.5f, -1.0f, 0.0f);
            }
        }
    }
}
