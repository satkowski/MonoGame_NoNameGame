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

        public static bool Intersects(CircleShape circleShapeA, CircleShape circleShapeB)
        {
            Vector2 distanceVector = circleShapeB.Center - circleShapeA.Center;
            float radius = circleShapeA.Radius + circleShapeB.Radius;
            radius *= radius;

            if(distanceVector.LengthSquared() <= radius)
                return true;
            return false;
        }

        public static Vector2 GetIntersectionDepths(AABBShape aabbShapeA, AABBShape aabbShapeB)
        {
            Vector2 centerA = new Vector2(aabbShapeA.Left + aabbShapeA.Center.X, aabbShapeA.Top + aabbShapeA.Center.Y);
            Vector2 centerB = new Vector2(aabbShapeB.Left + aabbShapeB.Center.X, aabbShapeB.Top + aabbShapeB.Center.Y);

            Vector2 distance = new Vector2(Math.Abs(centerA.X - centerB.X), Math.Abs(centerA.Y - centerB.Y));
            Vector2 minDistance = aabbShapeA.Center + aabbShapeB.Center;

            Vector2 depth = Vector2.Zero;
            if(distance.X < minDistance.X)
                depth.X = minDistance.X - distance.X;
            if(distance.Y < minDistance.Y)
                depth.Y = minDistance.Y - distance.Y;

            return depth;
        }

        public Vector2 GetNormalVector_AABB(Vector2 velocity, Vector2 penetration)
        {
            Vector2 normaleVector = Vector2.Zero;

            if(velocity.X < 0 && penetration.X != 0)
                normaleVector.X = 1;
            else if(velocity.X > 0 && penetration.X != 0)
                normaleVector.X = -1;

            if(velocity.Y < 0 && penetration.Y != 0)
                normaleVector.Y = 1;
            else if(velocity.Y > 0 && penetration.Y != 0)
                normaleVector.Y = -1;

            if(normaleVector.X != 0 && normaleVector.Y != 0)
            {
                if(penetration.Y <= penetration.X)
                    normaleVector.X = 0;
                else
                    normaleVector.Y = 0;
            }

            return normaleVector;
        }
    }
}
