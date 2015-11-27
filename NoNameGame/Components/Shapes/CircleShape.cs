using Microsoft.Xna.Framework;
using NoNameGame.Maps;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(CircleShape))]
    public class CircleShape : Shape
    {
        public Vector2 Center
        { get { return position; } }
        public float Radius;
        public override List<Vector2> Vertices
        {
            get
            {
                return new List<Vector2>();
            }
        }

        public CircleShape()
        {
            Type = ShapeType.AABB;
        }

        protected CircleShape(Vector2 position, float radius, float scale = 1.0f)
            : base(position, scale)
        {
            Type = ShapeType.AABB;
            Radius = radius;
            this.scale = scale;
        }


        public void LoadContent(Body body, float radius, float scale)
        {
            Radius = radius;
            this.scale = scale;
            base.LoadContent(body);
        }

        public void LoadContent(Tile tile, float radius, float scale)
        {
            Radius = radius;
            this.scale = scale;
            base.LoadContent(tile);
        }

        protected override void OnScaleChange(float newScale)
        {
            Radius /= Scale;
            Radius *= newScale;
        }

        public CircleShape Clone()
        {
            return new CircleShape(Position, Radius, Scale);
        }
    }
}
