using Microsoft.Xna.Framework;
using NoNameGame.Components.Shapes;
using System;

namespace NoNameGame.Components.Shapes
{
    public class ShapeCollisionManager
    {
        public static bool Intersects(AABBShape aabbShapeA, AABBShape aabbShapeB)
        {
            bool intersectX = false;
            bool intersectY = false;

            if(aabbShapeB.Left < aabbShapeA.Left && aabbShapeA.Left < aabbShapeB.Right || 
               aabbShapeB.Left < aabbShapeA.Right && aabbShapeA.Right < aabbShapeB.Right)
                intersectX = true;
            if(aabbShapeB.Top < aabbShapeA.Top && aabbShapeA.Top < aabbShapeB.Bottom ||
               aabbShapeB.Top < aabbShapeA.Bottom && aabbShapeA.Bottom < aabbShapeB.Bottom)
                intersectY = true;

            if(aabbShapeA.Left < aabbShapeB.Left && aabbShapeB.Left < aabbShapeA.Right ||
               aabbShapeA.Left < aabbShapeB.Right && aabbShapeB.Right < aabbShapeA.Right)
                intersectX = true;
            if(aabbShapeA.Top < aabbShapeB.Top && aabbShapeB.Top < aabbShapeA.Bottom ||
               aabbShapeA.Top < aabbShapeB.Bottom && aabbShapeB.Bottom < aabbShapeA.Bottom)
                intersectY = true;

            return intersectX && intersectY;
        }

        public static Vector2 GetIntersectionDepths(AABBShape aabbShapeA, AABBShape aabbShapeB)
        {
            Vector2 halfDimensionA = new Vector2(aabbShapeA.Size.X / 2.0f, aabbShapeA.Size.Y / 2.0f);
            Vector2 halfDimensionB = new Vector2(aabbShapeB.Size.X / 2.0f, aabbShapeB.Size.Y / 2.0f);

            Vector2 centerA = new Vector2(aabbShapeA.Left + halfDimensionA.X, aabbShapeA.Top + halfDimensionA.Y);
            Vector2 centerB = new Vector2(aabbShapeB.Left + halfDimensionB.X, aabbShapeB.Top + halfDimensionB.Y);

            Vector2 distance = new Vector2(Math.Abs(centerA.X - centerB.X), Math.Abs(centerA.Y - centerB.Y));
            Vector2 minDistance = halfDimensionA + halfDimensionB;

            Vector2 depth = Vector2.Zero;
            if(distance.X < minDistance.X)
                depth.X = minDistance.X - distance.X;
            if(distance.Y < minDistance.Y)
                depth.Y = minDistance.Y - distance.Y;

            return depth;
        }
    }
}
