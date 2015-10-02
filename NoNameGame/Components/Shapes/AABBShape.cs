using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using System;

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
        { get { return body.Position.X - Center.X; } }
        public float Top
        { get { return body.Position.Y - Center.Y; } }
        public float Right
        { get { return body.Position.X + Center.X; } }
        public float Bottom
        { get { return body.Position.Y + Center.Y; } }

        public void LoadContent(Body body, float width, float height, float scale)
        {
            Size = new Vector2(width, height);
            this.scale = scale;
            base.LoadContent(body);
        }

        protected override void OnScaleChange(float newScale)
        {
            Size /= Scale;
            Size *= newScale;
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
