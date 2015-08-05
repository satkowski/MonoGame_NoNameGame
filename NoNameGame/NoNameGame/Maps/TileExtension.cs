using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace NoNameGame.Maps
{
    public class TileExtension
    {
        public float GetRotationValue (this Tile.TileRotation rotation)
        {
            switch (rotation)
            {            
                case Tile.TileRotation.Clockwise90:
                    return MathHelper.PiOver2;  
                case Tile.TileRotation.Clockwise180:
                    return MathHelper.Pi;      
                case Tile.TileRotation.Clockwise270:
                    return -MathHelper.PiOver2;
                default: // None
                    return 0.0f;
            }
        }
    }
}
