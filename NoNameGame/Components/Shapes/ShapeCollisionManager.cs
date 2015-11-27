using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using NoNameGame.Extensions;
using NoNameGame.Helpers;

namespace NoNameGame.Components.Shapes
{
    public class ShapeCollisionManager
    {
        public static Vector2 GetCollisionSolvingVector(AABBShape aabbShapeA, AABBShape aabbShapeB)
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
                return new Vector2(0, 1) * penetration;
            else
                return new Vector2(1, 0) * penetration;

            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(CircleShape circleShapeA, CircleShape circleShapeB)
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

        public static Vector2 GetCollisionSolvingVector(OBBShape obbShapeA, OBBShape obbShapeB)
        {
            // Achsen für das OBB B
            List<Vector2> obbAxisA = new List<Vector2>();
            obbAxisA.Add(obbShapeA.Top);
            obbAxisA[0].Normalize();
            obbAxisA.Add(obbShapeA.Right);
            obbAxisA[1].Normalize();
            // Achsen für das OBB A
            List<Vector2> obbAxisB = new List<Vector2>();
            obbAxisB.Add(obbShapeB.Top);
            obbAxisB[0].Normalize();
            obbAxisB.Add(obbShapeB.Right);
            obbAxisB[1].Normalize();

            return calculateSAT(obbAxisA, obbAxisB, obbShapeA.Vertices, obbShapeB.Vertices);
        }

        public static Vector2 GetCollisionSolvingVector(AABBShape aabbShape, CircleShape circleShape)
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
                    return normaleVector * (radius - distance);
                else
                    return -normaleVector * (radius - distance);
            }

            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(AABBShape aabbShape, OBBShape obbShape)
        {
            // Standardachsen für das AABB
            List<Vector2> aabbAxis = new List<Vector2>();
            aabbAxis.Add(new Vector2(0, 1));
            aabbAxis.Add(new Vector2(1, 0));
            // Achsen für das OBB
            List<Vector2> obbAxis = new List<Vector2>();
            obbAxis.Add(obbShape.Top);
            obbAxis[0].Normalize();
            obbAxis.Add(obbShape.Right);
            obbAxis[1].Normalize();

            return calculateSAT(aabbAxis, obbAxis, aabbShape.Vertices, obbShape.Vertices);
        }

        public static Vector2 GetCollisionSolvingVector(OBBShape obbShape, CircleShape circleShape)
        {
            //TODO
            return Vector2.Zero;
        }

        public static Vector2 GetCollisionSolvingVector(CircleShape circleShape, OBBShape obbShape)
        {
            return GetCollisionSolvingVector(obbShape, circleShape);
        }
        
        public static Vector2 GetCollisionSolvingVector(OBBShape obbShape, AABBShape aabbShape)
        {
            return GetCollisionSolvingVector(aabbShape, obbShape);
        }

        public static Vector2 GetCollisionSolvingVector(CircleShape circelShape, AABBShape aabbShape)
        {
            return GetCollisionSolvingVector(aabbShape, circelShape);
        }


        private static Vector2 calculateSAT(List<Vector2> axisListA, List<Vector2> axisListB, List<Vector2> verticesA, List<Vector2> verticesB)
        {
            Tuple<Vector2, float> firstAxisOverlap = calculateOverlapAxis(axisListA, verticesA, verticesB);
            if(firstAxisOverlap.Item1 == Vector2.Zero)
                return Vector2.Zero;
            Tuple<Vector2, float> secondAxisOverlap = calculateOverlapAxis(axisListB, verticesA, verticesB);
            if(secondAxisOverlap.Item1 == Vector2.Zero)
                return Vector2.Zero;

            // Zurückprojektion damit man eindeutige x und y Werte hat
            if(firstAxisOverlap.Item2 < secondAxisOverlap.Item2)
                return new Vector2(Vector2.Dot(firstAxisOverlap.Item1, new Vector2(1, 0)),
                                   Vector2.Dot(firstAxisOverlap.Item1, new Vector2(0, 1))) 
                       * firstAxisOverlap.Item2;
            else
                return new Vector2(Vector2.Dot(secondAxisOverlap.Item1, new Vector2(1, 0)),
                                   Vector2.Dot(secondAxisOverlap.Item1, new Vector2(0, 1)))
                       * secondAxisOverlap.Item2;
        }

        private static Tuple<Vector2, float> calculateOverlapAxis(List<Vector2> axisList, List<Vector2> VerticesA, List<Vector2> VerticesB)
        {
            float overlap = float.MaxValue;
            Vector2 smallestAxis = Vector2.Zero;
            foreach(Vector2 axis in axisList)
            {
                Projection aabbProjection = calculateProjectionOnAxis(axis, VerticesA);
                Projection obbProjection = calculateProjectionOnAxis(axis, VerticesB);

                // Testen ob die beiden Projektionen sich überlappen
                if(!Projection.Overlap(aabbProjection, obbProjection))
                    return new Tuple<Vector2, float>(Vector2.Zero, 0f);
                float newOverlap = Projection.GetOverlap(aabbProjection, obbProjection);
                if(newOverlap < overlap)
                {
                    overlap = newOverlap;
                    smallestAxis = axis;
                }
            }
            return new Tuple<Vector2, float>(smallestAxis, overlap);
        }

        private static Projection calculateProjectionOnAxis(Vector2 axis, List<Vector2> vertices)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            foreach(Vector2 vertex in vertices)
            {
                float projection = Vector2.Dot(axis, vertex);
                if(projection < min)
                    min = projection;
                else if(projection > max)
                    max = projection;
            }
            return new Projection(min, max);
        }
    }
}
