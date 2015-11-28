using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Helpers
{
    /// <summary>
    /// Stellt eine Projektion mehrerer Punkte auf einen Vektor dar.
    /// </summary>
    public class Projection
    {
        public float Min
        { get; }
        public float Max
        { get; }

        public Projection(float min, float max)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Gibt an, ob sich 2 Projektionen überlagern.
        /// </summary>
        /// <param name="first">erste Projektion</param>
        /// <param name="second">zweite Projetion</param>
        /// <returns>überlagern die Projektionen sich</returns>
        public static bool Overlap(Projection first, Projection second)
        {
            if(first.Min < second.Min && second.Min < first.Max ||
               first.Max < second.Max && second.Max < first.Max)
                return true;
            if(second.Min < first.Min && first.Min < second.Max ||
               second.Min < first.Max && first.Max < first.Max)
                return true;
            return false;
        }

        /// <summary>
        /// Gibt den Wert an, mit dem sich 2 Projektionen überlagern.
        /// </summary>
        /// <param name="first">erste Projektion</param>
        /// <param name="second">zweite Projetion</param>
        /// <returns>Wert der Überlagerung</returns>
        public static float GetOverlap(Projection first, Projection second)
        {
            float firstOverlap = first.Max - second.Min;
            float secondOverlap = second.Max - first.Min;

            if(firstOverlap < secondOverlap)
                return firstOverlap;
            else
                return secondOverlap;
        }
    }
}
