using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using NoNameGame.Components.Shapes;

namespace NoNameGame.Collisions
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
            // Achsen für die AABB
            List<Vector2> aabbAxis = new List<Vector2>();
            aabbAxis.Add(new Vector2(0, 1));
            aabbAxis.Add(new Vector2(1, 0));

            return calculateSAT(aabbAxis, aabbAxis, aabbShapeA.Vertices, aabbShapeB.Vertices, aabbShapeA.Position, aabbShapeB.Position);
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
            float minDistance = circleShapeA.Radius + circleShapeB.Radius;

            // Nur wenn die Distanz kleiner ist, wird eine Collisionhandling betrieben
            if(distanceVector.LengthSquared() <= minDistance * minDistance)
            {
                float distance = distanceVector.Length();
                // Wenn ein Kreis genau auf dem anderen ist, wird dieser immer um den Radius nach rechts verschoben
                if(distance != 0)
                    return (distanceVector / distance) * (minDistance - distance);
                else
                    return new Vector2(1, 0) * circleShapeA.Radius;
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
            // Achsen für das OBB A
            List<Vector2> obbAxisA = new List<Vector2>();
            obbAxisA.Add(obbShapeA.Top);
            obbAxisA[0] /= obbAxisA[0].Length();
            obbAxisA.Add(obbShapeA.Right);
            obbAxisA[1] /= obbAxisA[1].Length();
            // Achsen für das OBB B
            List<Vector2> obbAxisB = new List<Vector2>();
            obbAxisB.Add(obbShapeB.Top);
            obbAxisB[0] /= obbAxisB[0].Length();
            obbAxisB.Add(obbShapeB.Right);
            obbAxisB[1] /= obbAxisB[1].Length();

            return calculateSAT(obbAxisA, obbAxisB, obbShapeA.Vertices, obbShapeB.Vertices, obbShapeA.Position, obbShapeB.Position);
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
            float radius = circleShape.Radius;

            // Nur wenn diese Distanz kleiner als der radius ist oder das Circle innerhalb liegt, wird Collisionhandling betrieben
            if(distance <= radius * radius || inside)
            {
                distance = (float)Math.Sqrt(distance);
                normaleVector.Normalize();

                return -normaleVector * (radius - distance);
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
            obbAxis[0] /= obbAxis[0].Length();
            obbAxis.Add(obbShape.Right);
            obbAxis[1] /= obbAxis[1].Length();

            return calculateSAT(aabbAxis, obbAxis, aabbShape.Vertices, obbShape.Vertices, aabbShape.Position, obbShape.Position);
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
                                  Vector2.Transform(distanceVector, Matrix.CreateRotationZ(-obbShape.Rotation));
            CircleShape newCircleShape = circleShape.Clone(newPosition);
            AABBShape newAabbShape = obbShape.CloneToAABBShape();

            Vector2 coliisionRescolving = GetCollisionSolvingVector(newAabbShape, newCircleShape);
            if(coliisionRescolving == Vector2.Zero)
                return Vector2.Zero;

            // Rotiere den Vector zurück
            return Vector2.Transform(coliisionRescolving, Matrix.CreateRotationZ(obbShape.Rotation));
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
        /// <param name="positionA">Position der ersten Form</param>
        /// <param name="positionB">Position der zweiten Form</param>
        /// <returns></returns>
        private static Vector2 calculateSAT(List<Vector2> axisListA, List<Vector2> axisListB, List<Vector2> verticesA, List<Vector2> verticesB, Vector2 positionA, Vector2 positionB)
        {
            // Berechnung der kleinsten Überlappung für die Achsen der beiden Shapes
            Tuple<Vector2, float> firstAxisOverlap = calculateOverlapAxis(axisListA, verticesA, verticesB);
            if(firstAxisOverlap.Item1 == Vector2.Zero)
                return Vector2.Zero;
            Tuple<Vector2, float> secondAxisOverlap = calculateOverlapAxis(axisListB, verticesA, verticesB);
            if(secondAxisOverlap.Item1 == Vector2.Zero)
                return Vector2.Zero;

            // Entscheidung welche Überlappung die kleinere war
            Tuple<Vector2, float> finalAxisOverlap;
            if(firstAxisOverlap.Item2 < secondAxisOverlap.Item2)
                finalAxisOverlap = firstAxisOverlap;
            else
                finalAxisOverlap = secondAxisOverlap;

            int offset = 0;
            // Entscheidung in welche Richtung die Verschiebung stattfinden soll
            float firstPositionProjection = Vector2.Dot(finalAxisOverlap.Item1, positionA);
            float secondPositionProjection = Vector2.Dot(finalAxisOverlap.Item1, positionB);
            if(firstPositionProjection < secondPositionProjection)
                offset = 1;
            else
                offset = -1;

            return finalAxisOverlap.Item1 * finalAxisOverlap.Item2 * offset;
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
                Projection firstProjection = calculateProjectionOnAxis(axis, VerticesA);
                Projection secondProjection = calculateProjectionOnAxis(axis, VerticesB);

                // Testen ob die beiden Projektionen sich überlappen
                if(!Projection.Overlap(firstProjection, secondProjection))
                    return new Tuple<Vector2, float>(Vector2.Zero, 0f);
                float newOverlap = Projection.GetOverlap(firstProjection, secondProjection);
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
                if(projection > max)
                    max = projection;
            }
            return new Projection(min, max);
        }
    }
}
