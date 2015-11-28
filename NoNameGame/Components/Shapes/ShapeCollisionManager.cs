using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using NoNameGame.Extensions;
using NoNameGame.Helpers;

namespace NoNameGame.Components.Shapes
{
    /// <summary>
    /// Eine statische Klasse, welche alle Berechnungen zu Kollisionauflösung der verschiedenen Shapes beinhaltet.
    /// </summary>
    public class ShapeCollisionManager
    {
        /// <summary>
        /// Kollisionsauflösung zwischen AABB und ABBB
        /// </summary>
        /// <param name="aabbShapeA"></param>
        /// <param name="aabbShapeB"></param>
        /// <returns></returns>
        public static Vector2 GetCollisionSolvingVector(AABBShape aabbShapeA, AABBShape aabbShapeB)
        {
            // Distanzberechnung der AABB
            Vector2 distanceVector = new Vector2(Math.Abs(aabbShapeA.Position.X - aabbShapeB.Position.X), 
                                                 Math.Abs(aabbShapeA.Position.Y - aabbShapeB.Position.Y));
            Vector2 minDistanceVector = aabbShapeA.Center + aabbShapeB.Center;

            // Berechnen der Penetration der beiden AABB
            Vector2 penetration = Vector2.Zero;
            if(distanceVector.X < minDistanceVector.X)
                penetration.X = minDistanceVector.X - distanceVector.X;
            if(distanceVector.Y < minDistanceVector.Y)
                penetration.Y = minDistanceVector.Y - distanceVector.Y;

            // Nur wenn es eine Penetration gab, wird ein Collisionhandling betrieben
            if(penetration.Y <= penetration.X)
                if(penetration.Y != 0)
                    return new Vector2(0, 1) * penetration;
            else
                if(penetration.X != 0)
                    return new Vector2(1, 0) * penetration;

            return Vector2.Zero;
        }

        /// <summary>
        /// Kollisionsauflösung zwischen Circle und Circle
        /// </summary>
        /// <param name="circleShapeA"></param>
        /// <param name="circleShapeB"></param>
        /// <returns></returns>
        public static Vector2 GetCollisionSolvingVector(CircleShape circleShapeA, CircleShape circleShapeB)
        {
            // Distanzberechnung der Circle
            Vector2 distanceVector = circleShapeB.Position - circleShapeA.Position;
            float minDistance = circleShapeA.RadiusScaled + circleShapeB.RadiusScaled;

            // Nur wenn die Distanz kleiner ist, wird eine Collisionhandling betrieben
            if(distanceVector.LengthSquared() <= minDistance * minDistance)
            {
                float distance = distanceVector.Length();
                distanceVector = new Vector2(Math.Abs(distanceVector.X), Math.Abs(distanceVector.Y));
                // Wenn ein Kreis im anderen ist, wir dieser immer um den Radius nach rechts verschoben
                if(distance != 0)
                    return (distanceVector / distance) * (minDistance - distance);
                else
                    return new Vector2(1, 0) * circleShapeA.RadiusScaled;
            }
            return Vector2.Zero;
        }

        /// <summary>
        /// Kollisionsauflösung zwischen OBB und OBB
        /// </summary>
        /// <param name="obbShapeA"></param>
        /// <param name="obbShapeB"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Kollisionsauflösung zwischen AABB und Circle
        /// </summary>
        /// <param name="aabbShape"></param>
        /// <param name="circleShape"></param>
        /// <returns></returns>
        public static Vector2 GetCollisionSolvingVector(AABBShape aabbShape, CircleShape circleShape)
        {
            // Berechnung der Distanz und des nächsten Punktes des AABB zum Circle
            Vector2 distanceVector = circleShape.Position - aabbShape.Position;
            Vector2 closestPoint = distanceVector;
            closestPoint.X = MathHelper.Clamp(closestPoint.X, -aabbShape.Center.X, aabbShape.Center.X);
            closestPoint.Y = MathHelper.Clamp(closestPoint.Y, -aabbShape.Center.Y, aabbShape.Center.Y);

            // Wenn sich der Vektor nicht verändert hat, befindet sich der Kreismittelpunkt innerhalb des AABB
            bool inside = false;
            if(distanceVector == closestPoint)
            {
                inside = true;

                // Anpassung des nächsten Punktes an Hand der Entfernungen
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

            // Der Normalenvektor wird aus der Distanz und dem nächsten Punkte bestimmt
            Vector2 normaleVector = distanceVector - closestPoint;
            float distance = normaleVector.LengthSquared();
            float radius = circleShape.RadiusScaled;

            // Nur wenn diese Distanz kleiner als der radius ist oder das Circle innerhalb liegt, wird Collisionhandling betrieben
            if(distance <= radius * radius || inside)
            {
                distance = (float)Math.Sqrt(distance);
                normaleVector.Normalize();
                normaleVector = new Vector2(Math.Abs(normaleVector.X), Math.Abs(normaleVector.Y));

                return normaleVector * (radius - distance);
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Kollisionsauflösung zwischen AABB und OBB
        /// </summary>
        /// <param name="aabbShape"></param>
        /// <param name="obbShape"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Kollisionsauflösung zwischen OBB und Circle
        /// </summary>
        /// <param name="obbShape"></param>
        /// <param name="circleShape"></param>
        /// <returns></returns>
        public static Vector2 GetCollisionSolvingVector(OBBShape obbShape, CircleShape circleShape)
        {
            // Rotiere die Position des Kreises um das OBB und erschaffe damit ein AABB - Circle Problem
            Vector2 distanceVector = circleShape.Position - obbShape.Position;
            Vector2 newPosition = obbShape.Position + 
                                  Vector2.Transform(distanceVector, Matrix.CreateRotationZ(MathHelper.ToRadians(-obbShape.Rotation)));
            CircleShape newCircleShape = circleShape.Clone(newPosition);
            AABBShape newAabbShape = obbShape.CloneToAABBShape();

            Vector2 coliisionRescolving = GetCollisionSolvingVector(newAabbShape, newCircleShape);
            if(coliisionRescolving == Vector2.Zero)
                return Vector2.Zero;

            // Rotiere den Vector zurück
            return Vector2.Transform(coliisionRescolving, Matrix.CreateRotationZ(MathHelper.ToRadians(obbShape.Rotation)));
        }

        /// <summary>
        /// Kollisionsauflösung zwischen Circle und OBB
        /// </summary>
        /// <param name="circleShape"></param>
        /// <param name="obbShape"></param>
        /// <returns></returns>
        public static Vector2 GetCollisionSolvingVector(CircleShape circleShape, OBBShape obbShape)
        {
            return GetCollisionSolvingVector(obbShape, circleShape);
        }

        /// <summary>
        /// Kollisionsauflösung zwischen OBB und AABB
        /// </summary>
        /// <param name="obbShape"></param>
        /// <param name="aabbShape"></param>
        /// <returns></returns>
        public static Vector2 GetCollisionSolvingVector(OBBShape obbShape, AABBShape aabbShape)
        {
            return GetCollisionSolvingVector(aabbShape, obbShape);
        }

        /// <summary>
        /// Kollisionsauflösung zwischen Circle und ABBB
        /// </summary>
        /// <param name="circelShape"></param>
        /// <param name="aabbShape"></param>
        /// <returns></returns>
        public static Vector2 GetCollisionSolvingVector(CircleShape circelShape, AABBShape aabbShape)
        {
            return GetCollisionSolvingVector(aabbShape, circelShape);
        }


        /// <summary>
        /// Berechnet das SAT (Seperate Axis Theorem).
        /// </summary>
        /// <param name="axisListA">Liste der Achsen der ersten Shape</param>
        /// <param name="axisListB">Liste der Achsen der zweiten Shape</param>
        /// <param name="verticesA">Liste der Ecken der ersten Shape</param>
        /// <param name="verticesB">Liste der Ecken der zweiten Shape</param>
        /// <returns></returns>
        private static Vector2 calculateSAT(List<Vector2> axisListA, List<Vector2> axisListB, List<Vector2> verticesA, List<Vector2> verticesB)
        {
            // Berechnung der kleinsten Überlappung für die Achsen der beiden Shapes
            Tuple<Vector2, float> firstAxisOverlap = calculateOverlapAxis(axisListA, verticesA, verticesB);
            if(firstAxisOverlap.Item1 == Vector2.Zero)
                return Vector2.Zero;
            Tuple<Vector2, float> secondAxisOverlap = calculateOverlapAxis(axisListB, verticesA, verticesB);
            if(secondAxisOverlap.Item1 == Vector2.Zero)
                return Vector2.Zero;

            // Entscheidung welche Überlappung die kleinere war
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

        /// <summary>
        /// Berechnet wieviel (der geringste Wert) und auf welcher Achse sich die die Punkte überschneiden.
        /// </summary>
        /// <param name="axisList">Liste der Achsen</param>
        /// <param name="VerticesA">Liste der Ecken der ersten Shape</param>
        /// <param name="VerticesB">Liste der Ecken der zweiten Shape</param>
        /// <returns></returns>
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

        /// <summary>
        /// Erstellt eine Projektion von Punkten auf eine Achse.
        /// </summary>
        /// <param name="axis">die Achse, auf welche projeziert werden soll</param>
        /// <param name="vertices">Liste an Punkten</param>
        /// <returns></returns>
        private static Projection calculateProjectionOnAxis(Vector2 axis, List<Vector2> vertices)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            foreach(Vector2 vertex in vertices)
            {
                // Projektion der Vektoren auf die Achse
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
