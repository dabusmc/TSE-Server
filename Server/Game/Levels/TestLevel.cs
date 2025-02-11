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
        public override void Construct()
        {
            int count = RND.Random.Next(10, 20);

            for (int i = 0; i < count; i++)
            {
                CollidableQuad quad = new CollidableQuad();

                Vector4 color = new Vector4(RND.RandomFloat(), RND.RandomFloat(), RND.RandomFloat(), 1.0f);

                Vector3 position = new Vector3(RND.RandomFloat(-7.5f, 7.5f), RND.RandomFloat(-3.0f, 3.0f), 0.0f);

                float scaleFactor = RND.RandomFloat(0.5f, 2.0f);
                Vector3 scale = new Vector3(scaleFactor, scaleFactor, 0.0f);

                int spriteRenderer = quad.GetComponentOfType(ObjectComponentType.SpriteRenderer);
                ((SpriteRendererData)quad.GetObjectComponents()[spriteRenderer].Data).Color = color;
                quad.Position = position;
                quad.Scale = scale;

                AddLevelObject(quad);
            }
        }

        public override void Update()
        {
        }
    }
}
