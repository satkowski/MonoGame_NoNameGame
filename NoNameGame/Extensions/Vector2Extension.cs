using Microsoft.Xna.Framework;
using System;

namespace NoNameGame.Extensions
{
    public static class Vector2Extension
    {
        public static Vector2 RoundDownToIntVector2(this Vector2 vector)
        {
            return new Vector2((int)Math.Round(vector.X, MidpointRounding.ToEven), 
                               (int)Math.Round(vector.Y, MidpointRounding.ToEven));
        }

        public static Vector2? GetAngleValues(this Vector2 vecA, Vector2 vecB)
        {
            // Berechnet den Winkel zwischen 2 Vektoren
            double distance = Math.Sqrt((vecA.X - vecB.X) * (vecA.X - vecB.X) + (vecA.Y - vecB.Y) * (vecA.Y - vecB.Y));
            if(distance != 0)
                return new Vector2((float)((vecA.X - vecB.X) / distance), (float)((vecA.Y - vecB.Y) / distance));
            return null;
        }
    }
}
