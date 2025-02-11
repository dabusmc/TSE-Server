using Server.Helper.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public enum ObjectComponentType
    {
        SpriteRenderer = 0,
        BoxCollider = 1
    }

    public class ObjectComponentData
    {
    }

    public class ObjectComponent
    {
        public ObjectComponentType Type;
        public ObjectComponentData Data;

        public ObjectComponent(ObjectComponentType type, ObjectComponentData data)
        {
            Type = type;
            Data = data;
        }
    }

    public enum Sprite
    {
        Quad = 0
    }

    public class SpriteRendererData : ObjectComponentData
    {
        public Sprite Sprite;
        public Vector4 Color;
        public bool FlipX;
        public bool FlipY;

        public SpriteRendererData(Sprite sprite, Vector4 color, bool flipX, bool flipY)
        {
            Sprite = sprite;
            Color = color;
            FlipX = flipX;
            FlipY = flipY;
        }

        public SpriteRendererData Copy()
        {
            return new SpriteRendererData(Sprite, Color, FlipX, FlipY);
        }
    }

    public class BoxColliderData : ObjectComponentData
    {
        public BoxColliderData Copy()
        {
            return new BoxColliderData();
        }
    }

    public static class ObjectComponents
    {
        public static SpriteRendererData SpriteRendererDefault = new SpriteRendererData(Sprite.Quad, new Vector4(1.0f, 1.0f, 1.0f, 1.0f), false, false);
        public static BoxColliderData BoxColliderDefault = new BoxColliderData();
    }
}
