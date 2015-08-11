using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace NoNameGame.Extensions
{
    public static class RectangleExtension
    {
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
    }
}
