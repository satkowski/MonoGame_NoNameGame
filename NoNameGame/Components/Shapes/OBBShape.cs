using Microsoft.Xna.Framework;
using NoNameGame.Maps;
using System;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(OBBShape))]
    public class OBBShape : AABBShape
    {
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                rotationX = new Vector2((float)Math.Cos(rotation), -(float)Math.Sin(rotation));
                rotationX = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation));
            }
        }

        protected Vector2 rotationX;
        protected Vector2 rotationY;
        public new Vector2 Left
        { get { return Vector2.Transform(new Vector2(base.Left, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))); } }
        public new Vector2 Top
        { get { return Vector2.Transform(new Vector2(0, base.Top), Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))); } }
        public new Vector2 Right
        { get { return Vector2.Transform(new Vector2(base.Right, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))); } }
        public new Vector2 Bottom
        { get { return Vector2.Transform(new Vector2(0, base.Bottom), Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))); } }

        public OBBShape()
        {
            Rotation = 0.0f;
        }
        
        public void LoadContent(Body body, float width, float height, float scale, float rotation)
        {
            Rotation = rotation;
            base.LoadContent(body, width, height, scale);
        }

        public void LoadConent(Tile tile, float width, float height, float scale, float rotation)
        {
            Rotation = rotation;
            base.LoadContent(tile, width, height, scale);
        }
    }
}
