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

        public bool Intersect(AABBShape shape)
        {
            if(shape.Left > Left && shape.Left < Right)
                return true;
            if(shape.Right < Right && shape.Right > Left)
                return true;
            if(shape.Top > Top && shape.Top < Bottom)
                return true;
            if(shape.Bottom < Bottom && shape.Bottom > Top)
                return true;
            if(Left > shape.Left && Left < shape.Right)
                return true;
            if(Right < shape.Right && Right > shape.Left)
                return true;
            if(Top > shape.Top && Top < shape.Bottom)
                return true;
            if(Bottom < shape.Bottom && Bottom > shape.Top)
                return true;
            return false;
        }
    }
}
