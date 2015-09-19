
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private Layer layer;
        
        public TileRotation Rotation;
        public Vector2 DestinationPosition;
        public Rectangle TileSheetRectangle;
        [XmlIgnore]
        public Rectangle PrevDestinationRectangle { private set; get; }
        [XmlIgnore]
        public Rectangle CurrentDestinationRectangle { private set; get; }
        [XmlIgnore]
        public Vector2 Origin { private set; get; }

        public Tile ()
        {
            Rotation = TileRotation.None;
            DestinationPosition = Vector2.Zero;
            TileSheetRectangle = Rectangle.Empty;
            PrevDestinationRectangle = Rectangle.Empty;
            CurrentDestinationRectangle = Rectangle.Empty;
            Origin = Vector2.Zero;
        }

        public void LoadContent(Layer layer, Vector2 tileSheetPosition, Vector2 destinationPosition, TileRotation rotation) 
        {
            this.layer = layer;
            Rotation = rotation;
            DestinationPosition = destinationPosition;
            TileSheetRectangle = new Rectangle((int)(tileSheetPosition.X * layer.TileDimensions.X), (int)(tileSheetPosition.Y * layer.TileDimensions.Y), 
                                               (int)layer.TileDimensions.X, (int)layer.TileDimensions.Y);

            Origin = new Vector2(TileSheetRectangle.Width / 2, TileSheetRectangle.Height / 2);

            CurrentDestinationRectangle = new Rectangle((int)(DestinationPosition.X + layer.Offset.X), (int)(DestinationPosition.Y + layer.Offset.Y),
                                                        (int)(TileSheetRectangle.Width * layer.Scale), (int)(TileSheetRectangle.Height * layer.Scale));
        }

        public void Update (GameTime gameTime)
        {
            PrevDestinationRectangle = CurrentDestinationRectangle;
            CurrentDestinationRectangle = new Rectangle((int)(DestinationPosition.X + layer.Offset.X), 
                                                        (int)(DestinationPosition.Y + layer.Offset.Y),
                                                        (int)(TileSheetRectangle.Width * layer.Scale), (int)(TileSheetRectangle.Height * layer.Scale));
        }
    }
}
