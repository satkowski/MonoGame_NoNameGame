using Microsoft.Xna.Framework;
using NoNameGame.Components.Shapes;
using System;

namespace NoNameGame.Managers
{
    public class CollisionResolvingManager
    {
        public static bool Intersects(AABBShape aabbShapeA, AABBShape aabbShapeB)
        {
            if(aabbShapeA.Left > aabbShapeB.Left && aabbShapeA.Left < aabbShapeB.Right)
                return true;
            if(aabbShapeA.Right < aabbShapeB.Right && aabbShapeA.Right > aabbShapeB.Left)
                return true;
            if(aabbShapeA.Top > aabbShapeB.Top && aabbShapeA.Top < aabbShapeB.Bottom)
                return true;
            if(aabbShapeA.Bottom < aabbShapeB.Bottom && aabbShapeA.Bottom > aabbShapeB.Top)
                return true;
            if(aabbShapeB.Left > aabbShapeA.Left && aabbShapeB.Left < aabbShapeA.Right)
                return true;
            if(aabbShapeB.Right < aabbShapeA.Right && aabbShapeB.Right > aabbShapeA.Left)
                return true;
            if(aabbShapeB.Top > aabbShapeA.Top && aabbShapeB.Top < aabbShapeA.Bottom)
                return true;
            if(aabbShapeB.Bottom < aabbShapeA.Bottom && aabbShapeB.Bottom > aabbShapeA.Top)
                return true;
            return false;
        }

        public static Vector2 GetIntersectionDepths(AABBShape aabbShapeA, AABBShape aabbShapeB)
        {
            Vector2 halfDimensionA = new Vector2(aabbShapeA.Size.X / 2.0f, aabbShapeA.Size.Y / 2.0f);
            Vector2 halfDimensionB = new Vector2(aabbShapeB.Size.X / 2.0f, aabbShapeB.Size.Y / 2.0f);

            Vector2 centerA = new Vector2(aabbShapeA.Left + halfDimensionA.X, aabbShapeA.Top + halfDimensionA.Y);
            Vector2 centerB = new Vector2(aabbShapeB.Left + halfDimensionB.X, aabbShapeB.Top + halfDimensionB.Y);

            Vector2 distance = centerA - centerB;
            Vector2 minDistance = halfDimensionA + halfDimensionB;

            if(Math.Abs(distance.X) >= minDistance.X || Math.Abs(distance.Y) >= minDistance.Y)
                return Vector2.Zero;

            Vector2 depth;
            depth.X = minDistance.X - Math.Abs(distance.X);
            depth.Y = minDistance.Y - Math.Abs(distance.Y);
            return depth;
        }
    }
}
