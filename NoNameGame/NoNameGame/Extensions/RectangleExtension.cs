using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace NoNameGame.Extensions
{
    public static class RectangleExtension
    {
        /// <summary>
        /// Wie tief ist die Überschneidung dieses Rechtecks mit einem anderen
        /// </summary>
        /// <param name="rectA">Dieses Rechteck</param>
        /// <param name="rectB">Das Rechteck zum berechnen der Überschneidungstiefe</param>
        /// <returns></returns>
        public static Vector2 GetIntersectionDepth (this Rectangle rectA, Rectangle rectB)
        {
            Vector2 halfDimensionA = new Vector2(rectA.Width / 2.0f, rectA.Height / 2.0f);
            Vector2 halfDimensionB = new Vector2(rectB.Width / 2.0f, rectB.Height / 2.0f);

            Vector2 centerA = new Vector2(rectA.Left + halfDimensionA.X, rectA.Top + halfDimensionA.Y);
            Vector2 centerB = new Vector2(rectB.Left + halfDimensionB.X, rectB.Top + halfDimensionB.Y);

            Vector2 distance = centerA - centerB;
            Vector2 minDistance = halfDimensionA + halfDimensionB;

            if (Math.Abs(distance.X) >= minDistance.X || Math.Abs(distance.Y) >= minDistance.Y)
                return Vector2.Zero;

            Vector2 depth;
            depth.X = minDistance.X - Math.Abs(distance.X);
            depth.Y = minDistance.Y - Math.Abs(distance.Y);

            return depth;
        }

        /// <summary>
        /// Befindet sich dieses Rechteck in einem anderen auf der Y-Achse
        /// </summary>
        /// <param name="rectA">Dieses Rechteck</param>
        /// <param name="rectB">Das Recteck in dem sich dieses befinden soll</param>
        /// <returns></returns>
        public static bool InVerticalDirection (this Rectangle rectA, Rectangle rectB)
        {
            return rectA.Top >= rectB.Top && rectA.Top <= rectB.Bottom &&
                   rectA.Bottom <= rectB.Bottom && rectA.Bottom >= rectB.Top;
        }

        /// <summary>
        /// Befindet sich dieses Rechteck in einem anderen auf der X-Achse
        /// </summary>
        /// <param name="rectA">Dieses Rechteck</param>
        /// <param name="rectB">Das Recteck in dem sich dieses befinden soll</param>
        /// <returns></returns>
        public static bool InHorizontalDirection (this Rectangle rectA, Rectangle rectB)
        {
            return rectA.Right <= rectB.Right && rectA.Right >= rectB.Left &&
                   rectA.Left >= rectB.Left && rectA.Left <= rectB.Right;
        }

        /// <summary>
        /// Überschneidet dieses Rechteck ein anderes auf der linken Seite
        /// </summary>
        /// <param name="rectA">Dieses Rechteck</param>
        /// <param name="rectB">Das Recteck welches überschnitten sein soll</param>
        /// <returns></returns>
        public static bool OverlapLeft (this Rectangle rectA, Rectangle rectB)
        {
            return rectA.Right <= rectB.Right && rectA.Right >= rectB.Left;
        }


        /// <summary>
        /// Überschneidet dieses Rechteck ein anderes auf der rechten Seite
        /// </summary>
        /// <param name="rectA">Dieses Rechteck</param>
        /// <param name="rectB">Das Recteck welches überschnitten sein soll</param>
        /// <returns></returns>
        public static bool OverlapRight (this Rectangle rectA, Rectangle rectB)
        {
            return rectA.Left >= rectB.Left && rectA.Left <= rectB.Right;
        }


        /// <summary>
        /// Überschneidet dieses Rechteck ein anderes auf der oberen Seite
        /// </summary>
        /// <param name="rectA">Dieses Rechteck</param>
        /// <param name="rectB">Das Recteck welches überschnitten sein soll</param>
        /// <returns></returns>
        public static bool OverlapTop (this Rectangle rectA, Rectangle rectB)
        {
            return rectA.Bottom <= rectB.Bottom && rectA.Bottom >= rectB.Top;
        }


        /// <summary>
        /// Überschneidet dieses Rechteck ein anderes auf der unteren Seite
        /// </summary>
        /// <param name="rectA">Dieses Rechteck</param>
        /// <param name="rectB">Das Recteck welches überschnitten sein soll</param>
        /// <returns></returns>
        public static bool OverlapBottom (this Rectangle rectA, Rectangle rectB)
        {
            return rectA.Top >= rectB.Top && rectA.Top <= rectB.Bottom;
        }
    }
}
