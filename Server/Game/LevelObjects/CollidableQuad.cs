using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.LevelObjects
{
    public class CollidableQuad : LevelObject
    {
        public CollidableQuad() : base()
        {
            AddComponent(ObjectComponentType.SpriteRenderer, ObjectComponents.SpriteRendererDefault.Copy());
            AddComponent(ObjectComponentType.BoxCollider, ObjectComponents.BoxColliderDefault.Copy());

            Name = "Quad";
            Position.Y += 1.0f;
        }
    }
}
