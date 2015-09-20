using Microsoft.Xna.Framework;

namespace NoNameGame.Extensions
{
    public static class Vector2Extension
    {
        public static Vector2 ConvertToIntVector2(this Vector2 vector)
        {
            return new Vector2((int)vector.X, (int)vector.Y);
        }
    }
}
