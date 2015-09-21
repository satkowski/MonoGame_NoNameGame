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
        public static Vector2 ConvertToIntVector2(this Vector2 vector)
        {
            return new Vector2((int)vector.X, (int)vector.Y);
        }
    }
}
