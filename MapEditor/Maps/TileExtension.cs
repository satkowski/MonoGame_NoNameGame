﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MapEditor.Maps
{
    public static class TileExtension
    {
        public static float GetRotationValue (this Tile.TileRotation rotation)
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

        public static Tile.TileRotation GetValueFromInt (this Tile.TileRotation r, int value)
        {
            switch (value)
            {
                case 1:
                    return Tile.TileRotation.Clockwise90;
                case 2:
                    return Tile.TileRotation.Clockwise180;
                case 3:
                    return Tile.TileRotation.Clockwise270;
                default:
                    return Tile.TileRotation.None;
            }
        }
    }
}