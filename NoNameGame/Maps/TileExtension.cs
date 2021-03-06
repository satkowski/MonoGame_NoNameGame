﻿
using Microsoft.Xna.Framework;

namespace NoNameGame.Maps
{
    /// <summary>
    /// Diese Klasse erweitert das Enum TileRotation in Tile.
    /// </summary>
    public static class TileExtension
    {
        /// <summary>
        /// Gibt den Rotationswert des jeweilgen Enumwertes zurück.
        /// </summary>
        /// <param name="rotation"></param>
        /// <returns>die Rotation</returns>
        public static float GetRotationValue (this Tile.TileRotation rotation)
        {
            // Berechnet mit welchen Wert sich rotiert werden soll
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
