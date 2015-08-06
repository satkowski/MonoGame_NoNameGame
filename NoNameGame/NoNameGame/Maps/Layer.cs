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
        public int Offset;
        public Vector2 TileDimensions;
        public TileMapString TileMapString;

        public Layer ()
        {
            Offset = 0;
            TileMapString = new TileMapString();
            tileMap = new List<Tile>();
            TileDimensions = Vector2.Zero;
        }

        public void LoadContent ()
        {
            TileSheet.LoadContent();

            Vector2 position = -TileDimensions;
            foreach (string row in TileMapString.Rows)
            {
                position.Y += TileDimensions.Y;

                string[] split = row.Split(']');
                foreach (string s in split)
                {
                    if (s != String.Empty)
                    {
                        position.X += TileDimensions.X;
                        if (!s.Contains("x"))
                        {
                            Tile newTile = new Tile();

                            String str = s.Replace("[", "");
                            int valueX = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int valueY = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            newTile.LoadContent(this, new Vector2(valueX, valueY), position);
                            tileMap.Add(newTile);
                        }
                    }
                }
                position.X = -TileDimensions.X;
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
    }
}
