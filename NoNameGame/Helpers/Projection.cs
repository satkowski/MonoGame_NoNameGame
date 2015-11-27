using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Helpers
{
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
