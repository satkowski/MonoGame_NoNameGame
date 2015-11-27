using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using System;
using NoNameGame.Maps;
using NoNameGame.Managers;
using System.Collections.Generic;

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
        public override List<Vector2> Vertices
        {   get
            {
                List<Vector2> newVertices = new List<Vector2>();
                newVertices.Add(Center + new Vector2(Left, Top));
                newVertices.Add(Center + new Vector2(Right, Top));
                newVertices.Add(Center + new Vector2(Right, Bottom));
                newVertices.Add(Center + new Vector2(Left, Bottom));

                return newVertices;
            }
        }

        public AABBShape()
        {
            Type = ShapeType.AABB;
        }

        protected AABBShape(Vector2 position, Vector2 size, float scale = 1.0f)
            : base(position, scale)
        {
            Type = ShapeType.AABB;
            Size = size;
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

        public AABBShape Clone()
        {
            return new AABBShape(Position, Size, Scale);
        }
    }
}
