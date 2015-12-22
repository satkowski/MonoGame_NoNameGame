using Microsoft.Xna.Framework;
using NoNameGame.Components;
using NoNameGame.Components.Shapes;
using NoNameGame.Managers;
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
        /// ID des Objektes.
        /// </summary>
        public ulong ID
        { get; private set; }        

        /// <summary>
        /// Die Tilerotation.
        /// </summary>
        public TileRotation Rotation;
        /// <summary>
        /// Das Rechteck, mit welchem von einem TileSheet heraus, dieses Tile gemalt wird.
        /// </summary>
        public Rectangle TileSheetRectangle;
        /// <summary>
        /// Das Shape des Tiles.
        /// </summary>
        public AABBShape Shape;
        /// <summary>
        /// Der Körper des Tiles.
        /// </summary>
        public Body Body;

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
            TileSheetRectangle = Rectangle.Empty;
            Shape = new AABBShape();
            Body = new Body();
            ID = IDManager.Instance.TileID;
        }

        public void LoadContent(Layer layer, Vector2 tileSheetPosition, Vector2 mapPosition, TileRotation rotation)
        {
            this.layer = layer;
            layer.OnScaleChange += updateRectangle;
            mapTilePosition = mapPosition;
            Rotation = rotation;
            TileSheetRectangle = new Rectangle((int)(tileSheetPosition.X * layer.TileDimensions.X), (int)(tileSheetPosition.Y * layer.TileDimensions.Y), 
                                               (int)layer.TileDimensions.X, (int)layer.TileDimensions.Y);
            Body.Position = mapPosition * layer.TileDimensions * layer.Scale;

            Shape.OnAreaChanged += delegate
            { Body.Area = Shape.Area; };

            Body.LoadContent(Material.Static);
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
            Body.Position =  mapTilePosition * (Vector2)sender;
        }

    }
}
