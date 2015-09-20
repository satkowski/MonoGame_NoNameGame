using System.Xml.Serialization;

using Microsoft.Xna.Framework;

using NoNameGame.Extensions;

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

        Layer layer;
        Vector2 mapPosition;

        public TileRotation Rotation;
        public Vector2 DestinationPosition;
        public Rectangle TileSheetRectangle;
        [XmlIgnore]
        public Rectangle CurrentDestinationRectangle { private set; get; }
        [XmlIgnore]
        public Vector2 ScaledOrigin { private set; get; }
        [XmlIgnore]
        public Vector2 Origin { private set; get; }

        public Tile ()
        {
            Rotation = TileRotation.None;
            DestinationPosition = Vector2.Zero;
            TileSheetRectangle = Rectangle.Empty;
            CurrentDestinationRectangle = Rectangle.Empty;
            ScaledOrigin = Vector2.Zero;
            Origin = Vector2.Zero;
        }

        public void LoadContent(Layer layer, Vector2 tileSheetPosition, Vector2 mapPosition, TileRotation rotation) 
        {
            this.layer = layer;
            layer.OnScaleChange += updateRectangle;
            this.mapPosition = mapPosition;
            Rotation = rotation;
            DestinationPosition = mapPosition * layer.TileDimensions * layer.Scale;
            TileSheetRectangle = new Rectangle((int)(tileSheetPosition.X * layer.TileDimensions.X), (int)(tileSheetPosition.Y * layer.TileDimensions.Y), 
                                               (int)layer.TileDimensions.X, (int)layer.TileDimensions.Y);

            Origin = new Vector2(TileSheetRectangle.Width / 2, TileSheetRectangle.Height * layer.Scale / 2);
            ScaledOrigin = new Vector2(TileSheetRectangle.Width * layer.Scale / 2, TileSheetRectangle.Height * layer.Scale / 2);

            CurrentDestinationRectangle = new Rectangle((int)(DestinationPosition.X + layer.Offset.X), (int)(DestinationPosition.Y + layer.Offset.Y),
                                                        (int)(TileSheetRectangle.Width * layer.Scale), (int)(TileSheetRectangle.Height * layer.Scale));
        }

        public void Update (GameTime gameTime)
        {
            updateRectangle(null, null);
        }

        private void updateRectangle(object sender, System.EventArgs e)
        {
            ScaledOrigin = new Vector2(TileSheetRectangle.Width * layer.Scale / 2, TileSheetRectangle.Height * layer.Scale / 2);            
            DestinationPosition =  mapPosition * layer.TileDimensions * layer.Scale;
            DestinationPosition = DestinationPosition.ConvertToIntVector2();
            CurrentDestinationRectangle = new Rectangle((int)(DestinationPosition.X + layer.Offset.X),
                                                        (int)(DestinationPosition.Y + layer.Offset.Y),
                                                        (int)(TileSheetRectangle.Width * layer.Scale), (int)(TileSheetRectangle.Height * layer.Scale));
        }
    }
}
