using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace NoNameGame.Maps
{
    public class Tile
    {
        public enum TileRotation 
        {
            None = 0,
            Clockwise90 = 1,
            Clockwise180 = 2,
            Clockwise270 = 3
        }

        public TileRotation Rotation;
        public float Scale;
        public Vector2 DestinationPosition;
        public Vector2 TileSheetPosition;

        public Tile ()
        {
            Rotation = TileRotation.None;
            Scale = 1.0f;
            DestinationPosition = Vector2.Zero;
            TileSheetPosition = Vector2.Zero;
        }
    }
}
