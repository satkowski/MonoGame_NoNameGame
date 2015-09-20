using System;
using System.Collections.Generic;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Images;
using NoNameGame.Extensions;

namespace NoNameGame.Maps
{
    public class Layer
    {
        float scale;
        List<Tile> tileMap;

        public TileSheet TileSheet;
        public Vector2 Offset;
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                TileScaledOrigin = new Vector2(TileDimensions.X * Scale / 2, TileDimensions.Y * Scale / 2);
                if(OnScaleChange != null)
                    OnScaleChange((TileDimensions * Scale).ConvertToIntVector2(), null);
            }
        }
        public Vector2 TileDimensions;
        public TileMapString TileMapString;
        [XmlIgnore]
        public Vector2 TileOrigin
        { private set; get; }
        [XmlIgnore]
        public Vector2 TileScaledOrigin
        { private set; get; }
        [XmlIgnore]
        public Vector2 Size
        { private set; get; }
        public int CollisionLevel;

        public event EventHandler OnScaleChange;

        public Layer ()
        {
            Offset = Vector2.Zero;
            Scale = 1.0f;
            TileMapString = new TileMapString();
            tileMap = new List<Tile>();
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
            foreach (string row in TileMapString.Rows)
            {
                position.Y++;

                string[] split = row.Split(']');
                foreach (string s in split)
                {
                    if (s != String.Empty)
                    {
                        position.X++;
                        if (!s.Contains("x"))
                        {
                            Tile newTile = new Tile();

                            String str = s.Replace("[", "");
                            int valueX = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int valueY = int.Parse(str.Substring(str.IndexOf(':') + 1));

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
                            tileMap.Add(newTile);
                        }
                    }
                }
                maxX = maxX < position.X ? (int)position.X : maxX;
                position.X = -1;
            }
            Size = new Vector2(maxX, tileMap.Count) * TileDimensions;
        }

        public void UnloadContent ()
        {
            TileSheet.UnloadContent();
        }

        public void Update (GameTime gameTime, Vector2 mapPosition)
        {
            foreach (Tile tile in tileMap)
                tile.Update(gameTime);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach(Tile tile in tileMap)
                TileSheet.Draw(spriteBatch, TileOrigin, TileScaledOrigin, Scale, tile);
        }

        public List<Rectangle> GetCollidingTileRectangles (Rectangle entityRectangle)
        {
            List<Rectangle> collidingRectangle = new List<Rectangle>();
            foreach (Tile tile in tileMap)
                if (tile.CurrentDestinationRectangle.Intersects(entityRectangle))
                    collidingRectangle.Add(tile.CurrentDestinationRectangle);

            return collidingRectangle;
        }
    }
}
