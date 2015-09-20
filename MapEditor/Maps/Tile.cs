using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Maps
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

        private Layer layer;
        
        public TileRotation Rotation;
        public Vector2 DestinationPosition;
        public Rectangle TileSheetRectangle;
        [XmlIgnore]
        public Rectangle CurrentDestinationRectangle { private set; get; }
        [XmlIgnore]
        public Vector2 MapPosition
        { private set; get; }

        public Tile ()
        {
            Rotation = TileRotation.None;
            DestinationPosition = Vector2.Zero;
            TileSheetRectangle = Rectangle.Empty;
            CurrentDestinationRectangle = Rectangle.Empty;
            MapPosition = Vector2.Zero;
        }

        public void Initialize(Layer layer, Vector2 tileSheetPosition, Vector2 mapPosition, TileRotation rotation) 
        {
            this.layer = layer;
            Rotation = rotation;
            this.MapPosition = mapPosition;
            DestinationPosition = mapPosition * layer.TileDimensions;
            TileSheetRectangle = new Rectangle((int)(tileSheetPosition.X * layer.TileDimensions.X), (int)(tileSheetPosition.Y * layer.TileDimensions.Y), 
                                               (int)layer.TileDimensions.X, (int)layer.TileDimensions.Y);

            CurrentDestinationRectangle = new Rectangle((int)(DestinationPosition.X + layer.Offset.X * layer.Scale), (int)(DestinationPosition.Y + layer.Offset.Y * layer.Scale),
                                                        (int)(TileSheetRectangle.Width * layer.Scale), (int)(TileSheetRectangle.Height * layer.Scale));
        }

        public void Update (GameTime gameTime)
        {
            CurrentDestinationRectangle = new Rectangle((int)(DestinationPosition.X + layer.Offset.X), (int)(DestinationPosition.Y + layer.Offset.Y),
                                                        (int)(TileSheetRectangle.Width * layer.Scale), (int)(TileSheetRectangle.Height * layer.Scale));
        }

        public void Draw (SpriteBatch spriteBatch, Vector2 windowPosition, bool scaled)
        {
            layer.TileSheet.Draw(spriteBatch, layer.Scale, layer.TileDimensions, windowPosition, this, layer.Origin, scaled);
        }
    }
}
