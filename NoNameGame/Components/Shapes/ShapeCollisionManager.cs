using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using NoNameGame.Extensions;

namespace NoNameGame.Components.Shapes
{
    public class ShapeCollisionManager
    {
        public static Vector2 GetCollisionSolvingVector(AABBShape aabbShapeA, AABBShape aabbShapeB, Vector2 velocity)
        {
            Vector2 distanceVector = new Vector2(Math.Abs(aabbShapeA.Position.X - aabbShapeB.Position.X), 
                                                 Math.Abs(aabbShapeA.Position.Y - aabbShapeB.Position.Y));
            Vector2 minDistanceVector = aabbShapeA.Center + aabbShapeB.Center;

            Vector2 penetration = Vector2.Zero;
            if(distanceVector.X < minDistanceVector.X)
                penetration.X = minDistanceVector.X - distanceVector.X;
            if(distanceVector.Y < minDistanceVector.Y)
                penetration.Y = minDistanceVector.Y - distanceVector.Y;

            if(penetration.Y <= penetration.X)
            {
                if(velocity.Y < 0 && penetration.Y != 0)
                    return new Vector2(0, 1) * penetration;
                else if(velocity.Y > 0 && penetration.Y != 0)
                    return new Vector2(0, -1) * penetration;
            }
            else
            {
                if(velocity.X < 0 && penetration.X != 0)
                    return new Vector2(1, 0) * penetration;
                else if(velocity.X > 0 && penetration.X != 0)
                    return new Vector2(-1, 0) * penetration;
            }

            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(CircleShape circleShapeA, CircleShape circleShapeB, Vector2 velocity)
        {
            Vector2 distanceVector = circleShapeB.Position - circleShapeA.Position;
            float minDistance = circleShapeA.Radius + circleShapeB.Radius;

            if(distanceVector.LengthSquared() <= minDistance * minDistance)
            {
                float distance = distanceVector.Length();
                if(distance != 0)
                    return (distanceVector / distance) * (minDistance - distance);
                else
                    return new Vector2(1, 0) * circleShapeA.Radius;
            }
            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(OBBShape obbShapeA, OBBShape obbShapeB, Vector2 velocity)
        {
            // TODO
            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(AABBShape aabbShape, CircleShape circleShape, Vector2 velocity)
        {
            Vector2 distanceVector = circleShape.Position - aabbShape.Position;
            Vector2 closestPoint = distanceVector;
            closestPoint.X = MathHelper.Clamp(closestPoint.X, -aabbShape.Center.X, aabbShape.Center.X);
            closestPoint.Y = MathHelper.Clamp(closestPoint.Y, -aabbShape.Center.Y, aabbShape.Center.Y);

            bool inside = false;
            if(distanceVector == closestPoint)
            {
                inside = true;

                if(Math.Abs(distanceVector.X) > Math.Abs(distanceVector.Y))
                {
                    if(closestPoint.X > 0)
                        closestPoint.X = aabbShape.Center.X;
                    else
                        closestPoint.X = -aabbShape.Center.X;
                }
                else
                {
                    if(closestPoint.Y > 0)
                        closestPoint.Y = aabbShape.Center.Y;
                    else
                        closestPoint.Y = -aabbShape.Center.Y;
                }
            }

            Vector2 normaleVector = distanceVector - closestPoint;
            float distance = normaleVector.LengthSquared();
            float radius = circleShape.Radius;

            if(distance <= radius * radius || inside)
            {
                distance = (float)Math.Sqrt(distance);
                normaleVector.Normalize();
                if(inside)
                    return -normaleVector * (radius - distance);
                else
                    return normaleVector * (radius - distance);
            }

            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(AABBShape aabbShape, OBBShape obbShape, Vector2 velocity)
        {
            //TODO
            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(OBBShape obbShape, CircleShape circleShape, Vector2 velocity)
        {
            //TODO
            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(CircleShape circleShape, OBBShape obbShape, Vector2 velocity)
        {
            return GetCollisionSolvingVector(obbShape, circleShape, velocity);
        }
        
        public static Vector2 GetCollisionSolvingVector(OBBShape obbShape, AABBShape aabbShape, Vector2 velocity)
        {
            return GetCollisionSolvingVector(aabbShape, obbShape, velocity);
        }

        public static Vector2 GetCollisionSolvingVector(CircleShape circelShape, AABBShape aabbShape, Vector2 velocity)
        {
            return GetCollisionSolvingVector(aabbShape, circelShape, velocity);
        }
    }
}
