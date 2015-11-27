using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using System;
using NoNameGame.Maps;
using NoNameGame.Managers;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(AABBShape))]
    [XmlInclude(typeof(OBBShape))]
    public class AABBShape : Shape
    {        
        public Vector2 Size;
        public Vector2 Center
        { get { return Size / 2; } }
        public float Left
        { get { return position.X - Center.X; } }
        public float Top
        { get { return position.Y - Center.Y; } }
        public float Right
        { get { return position.X + Center.X; } }
        public float Bottom
        { get { return position.Y + Center.Y; } }

        public AABBShape()
        {
            Type = ShapeType.AABB;
        }

        public void LoadContent(Body body, float width, float height, float scale)
        {
            Size = new Vector2(width, height);
            this.scale = scale;
            base.LoadContent(body);
        }

        public void LoadContent(Tile tile, float width, float height, float scale)
        {
            Size = new Vector2(width, height);
            this.scale = scale;
            base.LoadContent(tile);
        }
        
        protected override void OnScaleChange(float newScale)
        {
            Size /= Scale;
            Size *= newScale;
        }
    }
}
