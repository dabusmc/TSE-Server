using Server.Game.LevelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.Levels
{
    public class TestLevel : Level
    {
        protected override void Construct()
        {
            CollidableQuad floor = new CollidableQuad();
            AddLevelObject(floor);
        }

        public override void Update()
        {
        }
    }
}
