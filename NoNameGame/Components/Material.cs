using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Components
{
    /// <summary>
    /// Stellt das Material, welches ein Körper haben kann, dar.
    /// </summary>
    public enum Material
    {
        Rock,
        Wood,
        Metal,
        BouncyBall,
        SuperBall,
        Pillow,
        Static,
        Tile,
        None
    }

    /// <summary>
    /// Eine Erweiterung für das Enum Material.
    /// </summary>
    public static class MaterialExtension
    {
        /// <summary>
        /// Gibt die Elastizität oder "Bounciness" eines Körpers wieder.
        /// </summary>
        public static float GetRestitution(this Material material)
        {
            switch(material)
            {
                case Material.Rock:         return 0.6f;
                case Material.Wood:         return 0.3f;
                case Material.Metal:        return 1.2f;
                case Material.BouncyBall:   return 0.3f;
                case Material.SuperBall:    return 0.3f;
                case Material.Pillow:       return 0.1f;
                case Material.Static:       return 0.0f;
                case Material.Tile:         return 0.5f;
                default: /*None*/           return 0.0f;
            }
        }

        /// <summary>
        /// Gibt die Dichte eines Körpers wieder.
        /// </summary>
        public static float GetDensity(this Material material)
        {
            switch(material)
            {
                case Material.Rock:         return 0.1f;
                case Material.Wood:         return 0.2f;
                case Material.Metal:        return 0.05f;
                case Material.BouncyBall:   return 0.8f;
                case Material.SuperBall:    return 0.95f;
                case Material.Pillow:       return 0.2f;
                case Material.Static:       return 0.4f;
                case Material.Tile:         return float.PositiveInfinity;
                default: /*None*/           return -1.0f;
            }
        }
    }
}
