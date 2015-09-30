using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Components.Shapes
{
    public class AABBShape : Shape
    {
        private Vector2 size;

        public Vector2 Location;
        public float Width
        { get { return size.X; } }
        public float Height
        { get { return size.Y; } }
        public float X
        { get { return Location.X; } }
        public float Y
        { get { return Location.Y; } }
        public Vector2 Center
        { get { return size / 2; } }
        public float Left
        { get { return Location.X; } }
        public float Top
        { get { return Location.Y; } }
        public float Right
        { get { return Location.X + size.X; } }
        public float Bottom
        { get { return Location.Y + size.Y; } }

        public AABBShape(float x, float y, float width, float height)
        {
            Location = new Vector2(x, y);
            size = new Vector2(width, height);
        }

        public override bool Intersect<S>(S shape)
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
    }
}
