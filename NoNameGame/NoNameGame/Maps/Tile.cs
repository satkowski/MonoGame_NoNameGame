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
        Vector2 mapTilePosition;

        public TileRotation Rotation;
        public Vector2 Position;
        public Rectangle TileSheetRectangle;
        [XmlIgnore]
        public Rectangle CurrentRectangle { private set; get; }

        public Tile ()
        {
            Rotation = TileRotation.None;
            Position = Vector2.Zero;
            TileSheetRectangle = Rectangle.Empty;
            CurrentRectangle = Rectangle.Empty;
        }

        public void LoadContent(Layer layer, Vector2 tileSheetPosition, Vector2 mapPosition, TileRotation rotation) 
        {
            this.layer = layer;
            layer.OnScaleChange += updateRectangle;
            this.mapTilePosition = mapPosition;
            Rotation = rotation;
            Position = mapPosition * layer.TileDimensions * layer.Scale;
            TileSheetRectangle = new Rectangle((int)(tileSheetPosition.X * layer.TileDimensions.X), (int)(tileSheetPosition.Y * layer.TileDimensions.Y), 
                                               (int)layer.TileDimensions.X, (int)layer.TileDimensions.Y);

            CurrentRectangle = new Rectangle((int)(Position.X - layer.TileScaledOrigin.X), (int)(Position.Y - layer.TileScaledOrigin.X),
                                             (int)(TileSheetRectangle.Width * layer.Scale), (int)(TileSheetRectangle.Height * layer.Scale));
        }

        public void Update (GameTime gameTime)
        {
            updateRectangle((layer.TileDimensions * layer.Scale), null);
        }

        private void updateRectangle(object sender, System.EventArgs e)
        {
            Position =  mapTilePosition * (Vector2)sender;
            CurrentRectangle = new Rectangle((int)(Position.X - layer.TileScaledOrigin.X), (int)(Position.Y - layer.TileScaledOrigin.X),
                                             (int)(TileSheetRectangle.Width * layer.Scale), (int)(TileSheetRectangle.Height * layer.Scale));
        }
    }
}
