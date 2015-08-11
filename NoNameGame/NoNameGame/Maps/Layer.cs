using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Images;

namespace NoNameGame.Maps
{
    public class Layer
    {
        List<Tile> tileMap;

        public TileSheet TileSheet;
        public Vector2 Offset;
        public Vector2 TileDimensions;
        public TileMapString TileMapString;
        public int CollisionLevel;

        public Layer ()
        {
            Offset = Vector2.Zero;
            TileMapString = new TileMapString();
            tileMap = new List<Tile>();
            TileDimensions = Vector2.Zero;
            CollisionLevel = -1;
        }

        public void LoadContent ()
        {
            TileSheet.LoadContent();

            Vector2 position = -Vector2.One;
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

                            newTile.LoadContent(this, new Vector2(valueX, valueY), position * TileDimensions, rotation);
                            tileMap.Add(newTile);
                        }
                    }
                }
                position.X = -1;
            }
        }

        public void UnloadContent ()
        {
            TileSheet.UnloadContent();
        }

        public void Update (GameTime gameTime)
        {
            foreach (Tile tile in tileMap)
                tile.Update(gameTime);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tileMap)
                tile.Draw(spriteBatch);
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
