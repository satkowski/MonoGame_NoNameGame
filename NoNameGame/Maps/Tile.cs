using Microsoft.Xna.Framework;
using NoNameGame.Components.Shapes;
using NoNameGame.Extensions;
using System;

namespace NoNameGame.Maps
{
    /// <summary>
    /// Stellt ein einzelnes Tile dar.
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Enum, welches die Rotation eines Tiles angiebt.
        /// </summary>
        public enum TileRotation 
        {
            None = 0,
            Clockwise90 = 1,
            Clockwise180 = 2,
            Clockwise270 = 3
        }

        Layer layer;
        Vector2 mapTilePosition;

        /// <summary>
        /// Die Tilerotation.
        /// </summary>
        public TileRotation Rotation;
        /// <summary>
        /// Die Position des Tiles.
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Das Rechteck, mit welchem von einem TileSheet heraus, dieses Tile gemalt wird.
        /// </summary>
        public Rectangle TileSheetRectangle;
        /// <summary>
        /// Das Shape des Tiles.
        /// </summary>
        public AABBShape Shape;

        /// <summary>
        /// Wird gefeuert, wenn sich die Position des Tiles ändert.
        /// </summary>
        public event EventHandler OnPositionChange;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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
            mapTilePosition = mapPosition;
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

        /// <summary>
        /// Aktualisiert die Position anhand einer neuen Skalierung.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateRectangle(object sender, System.EventArgs e)
        {
            Position =  mapTilePosition * (Vector2)sender;
        }

    }
}
