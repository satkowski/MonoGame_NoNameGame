using Microsoft.Xna.Framework;
using NoNameGame.Components.Shapes;
using NoNameGame.Extensions;
using System;

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
        public AABBShape Shape;

        public event EventHandler OnPositionChange;

        public Tile ()
        {
            Rotation = TileRotation.None;
            Position = Vector2.Zero;
            TileSheetRectangle = Rectangle.Empty;
            Shape = new AABBShape();
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

            Shape.LoadContent(this, layer.TileDimensions.X, layer.TileDimensions.Y, layer.Scale);
        }

        public void Update (GameTime gameTime)
        {
            updateRectangle((layer.TileDimensions * layer.Scale), null);
        }

        private void updateRectangle(object sender, System.EventArgs e)
        {
            Position =  mapTilePosition * (Vector2)sender;
        }
    }
}
