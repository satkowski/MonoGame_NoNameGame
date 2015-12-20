using Microsoft.Xna.Framework;
using System;

namespace NoNameGame.Extensions
{
    /// <summary>
    /// Erweiterung der Vector2 Klasse aus dem Monogame Framework.
    /// </summary>
    public static class Vector2Extension
    {
        /// <summary>
        /// Rundet einen Vektor auf ganze Werte.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>der gerundete Vektor</returns>
        public static Vector2 RoundDownToIntVector2(this Vector2 vector)
        {
            return new Vector2((int)Math.Round(vector.X, MidpointRounding.ToEven), 
                               (int)Math.Round(vector.Y, MidpointRounding.ToEven));
        }

        /// <summary>
        /// Berechnet den Vektor zwischen 2 Punkten. Dieser ist normalisiert und 
        /// </summary>
        /// <param name="vecA"></param>
        /// <param name="vecB">der andere Vektor</param>
        /// <returns>Vektor mit Anteil der Bewgung in die Richtungen</returns>
        public static Vector2 GetNormalVectorToVector(this Vector2 vecA, Vector2 vecB)
        {
            Vector2 normalVector = vecA - vecB;
            normalVector.Normalize();
            return normalVector;
        }
    }
}
