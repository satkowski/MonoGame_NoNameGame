using System;
using System.Collections.Generic;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Images;

namespace NoNameGame.Maps
{
    /// <summary>
    /// Stellt eine Ebene dar.
    /// </summary>
    public class Layer
    {
        float scale;

        /// <summary>
        /// Das TileSheet der aktuellen Ebene.
        /// </summary>
        public TileSheet TileSheet;
        /// <summary>
        /// Der Offset der Ebene.
        /// </summary>
        public Vector2 Offset;
        /// <summary>
        /// Die Skalierung der Ebene.
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                TileScaledOrigin = new Vector2(TileDimensions.X * Scale / 2, TileDimensions.Y * Scale / 2);
                if(OnScaleChange != null)
                    OnScaleChange((TileDimensions * Scale), null);
            }
        }
        /// <summary>
        /// Die Größe einzelner Tiles.
        /// </summary>
        public Vector2 TileDimensions;
        /// <summary>
        /// Stellt die aktuelle Ebene als Strings dar.
        /// </summary>
        public TileMapString TileMapString;
        /// <summary>
        /// Eine Liste aller Tiles dieser Ebene.
        /// </summary>
        [XmlIgnore]
        public List<Tile> TileMap
        { private set; get; }
        /// <summary>
        /// Der Ursprung der Tiles.
        /// </summary>
        [XmlIgnore]
        public Vector2 TileOrigin
        { private set; get; }
        /// <summary>
        /// Der Skalierte Ursprung der Tiles.
        /// </summary>
        [XmlIgnore]
        public Vector2 TileScaledOrigin
        { private set; get; }
        /// <summary>
        /// Die Größe der Tiles.
        /// </summary>
        [XmlIgnore]
        public Vector2 Size
        { private set; get; }
        /// <summary>
        /// Das Level, in dem mit der Ebene interagiert wird.
        /// </summary>
        public int CollisionLevel;

        /// <summary>
        /// Wird gefeuert, wenn sich die Skalierung ändert.
        /// </summary>
        public event EventHandler OnScaleChange;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Layer ()
        {
            Offset = Vector2.Zero;
            Scale = 1.0f;
            TileMapString = new TileMapString();
            TileMap = new List<Tile>();
            TileDimensions = Vector2.Zero;
            CollisionLevel = -1;
            Size = Vector2.Zero;
            TileScaledOrigin = Vector2.Zero;
            TileOrigin = Vector2.Zero;
        }

        public void LoadContent ()
        {
            TileSheet.LoadContent();
            TileOrigin = new Vector2(TileDimensions.X / 2, TileDimensions.Y * Scale / 2);
            TileScaledOrigin = new Vector2(TileDimensions.X * Scale / 2, TileDimensions.Y * Scale / 2);

            Vector2 position = -Vector2.One;
            int maxX = 0;
            // Gehe durch den gesamten String durch, welcher das Layer darstellt
            foreach (string row in TileMapString.Rows)
            {
                position.Y++;

                // Gehe durch die einzelne Zeile
                string[] split = row.Split(']');
                foreach (string s in split)
                {
                    if (s != String.Empty)
                    {
                        position.X++;
                        // Nur abarbeiten, wenn es kein leere Tile ist
                        if (!s.Contains("x"))
                        {
                            Tile newTile = new Tile();

                            // Auslesen, welches Tile es aus dem Tilesheet ist
                            String str = s.Replace("[", "");
                            int valueX = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int valueY = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            // Auslesen ob das Tile eine Rotation besitzt
                            Tile.TileRotation rotation;
                            if (TileMapString.Rotation90Tiles.Contains("[" + position.X.ToString() + ":" + position.Y.ToString() + "]"))
                                rotation = Tile.TileRotation.Clockwise90;
                            else if (TileMapString.Rotation180Tiles.Contains("[" + position.X.ToString() + ":" + position.Y.ToString() + "]"))
                                rotation = Tile.TileRotation.Clockwise180;
                            else if (TileMapString.Rotation270Tiles.Contains("[" + position.X.ToString() + ":" + position.Y.ToString() + "]"))
                                rotation = Tile.TileRotation.Clockwise270;
                            else
                                rotation = 0.0f;

                            newTile.LoadContent(this, new Vector2(valueX, valueY), position, rotation);
                            TileMap.Add(newTile);
                        }
                    }
                }
                maxX = maxX < position.X ? (int)position.X : maxX;
                position.X = -1;
            }
            Size = new Vector2(maxX, TileMap.Count) * TileDimensions;
        }

        public void UnloadContent ()
        {
            TileSheet.UnloadContent();
        }

        public void Update (GameTime gameTime)
        {
            foreach (Tile tile in TileMap)
                tile.Update(gameTime);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach(Tile tile in TileMap)
                TileSheet.Draw(spriteBatch, TileOrigin, TileScaledOrigin, Scale, tile);
        }
    }
}
