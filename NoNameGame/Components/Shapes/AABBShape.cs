using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using System;
using NoNameGame.Maps;

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

        public override bool Intersects(Shape shape)
        {
            if(shape.GetType().Equals(typeof(AABBShape)))
            {
                AABBShape aabb = (shape as AABBShape);
                if(aabb.Left > Left && aabb.Left < Right)
                    return true;
                if(aabb.Right < Right && aabb.Right > Left)
                    return true;
                if(aabb.Top > Top && aabb.Top < Bottom)
                    return true;
                if(aabb.Bottom < Bottom && aabb.Bottom > Top)
                    return true;
                if(Left > aabb.Left && Left < aabb.Right)
                    return true;
                if(Right < aabb.Right && Right > aabb.Left)
                    return true;
                if(Top > aabb.Top && Top < aabb.Bottom)
                    return true;
                if(Bottom < aabb.Bottom && Bottom > aabb.Top)
                    return true;
            }
            //TODO: Implementiere andere
            return false;
        }

        public override Vector2 GetIntersectionDepth(Shape shape)
        {
            if(shape.GetType().Equals(typeof(AABBShape)))
            {
                AABBShape aabb = (shape as AABBShape);

                Vector2 halfDimensionA = new Vector2(Size.X / 2.0f, Size.Y / 2.0f);
                Vector2 halfDimensionB = new Vector2(aabb.Size.X / 2.0f, aabb.Size.Y / 2.0f);

                Vector2 centerA = new Vector2(Left + halfDimensionA.X, Top + halfDimensionA.Y);
                Vector2 centerB = new Vector2(aabb.Left + halfDimensionB.X, aabb.Top + halfDimensionB.Y);

                Vector2 distance = centerA - centerB;
                Vector2 minDistance = halfDimensionA + halfDimensionB;

                if(Math.Abs(distance.X) >= minDistance.X || Math.Abs(distance.Y) >= minDistance.Y)
                    return Vector2.Zero;

                Vector2 depth;
                depth.X = minDistance.X - Math.Abs(distance.X);
                depth.Y = minDistance.Y - Math.Abs(distance.Y);
                return depth;
            }
            //TODO: Implementiere andere
            return Vector2.Zero;
        }
    }
}
