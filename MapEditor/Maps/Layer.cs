﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MapEditor.Images;
using Microsoft.Xna.Framework.Content;

namespace MapEditor.Maps
{
    public class Layer
    {
        List<List<Tile>> tileMap;

        public TileSheet TileSheet;
        [XmlIgnore]
        public Image TileSheetImage;
        public int Offset;
        public Vector2 TileDimensions;
        public TileMapString TileMapString;

        public Layer ()
        {
            Offset = 0;
            TileMapString = new TileMapString();
            tileMap = new List<List<Tile>>();
            TileDimensions = Vector2.Zero;
        }

        public void ReplaceTiles (Vector2 postion, Rectangle selectedRegion)
        {
            Vector2 startIndex = new Vector2(postion.X / TileDimensions.X, postion.Y / TileDimensions.Y);
            Vector2 tileIndex = new Vector2(selectedRegion.X, selectedRegion.Y - 1);
            Vector2 mapIndex = Vector2.Zero;

            for (int cY = (int)startIndex.Y; cY <= startIndex.Y + selectedRegion.Height; cY++)
            {
                if (TileSheet == null)
                    return;

                tileIndex.X = selectedRegion.X;
                tileIndex.Y++;
                for (int cX = (int)startIndex.X; cX <= startIndex.X + selectedRegion.Width; cX++)
                {
                    if (tileIndex.X * TileDimensions.X > TileSheet.Texture.Width ||
                       tileIndex.Y * TileDimensions.Y > TileSheet.Texture.Height)
                        mapIndex = -Vector2.One;
                    else
                        mapIndex = tileIndex;

                    try
                    {
                        tileMap[cY][cX].TileSheetRectangle.X = (int)mapIndex.X;
                        tileMap[cY][cX].TileSheetRectangle.Y = (int)mapIndex.Y;
                    }
                    catch (Exception e) // Wenn die jetzige Map zu klein ist, wird diese erweitert
                    {
                        if (tileMap.Count == 0)
                        {
                            List<Tile> tempTileMap = new List<Tile>();
                            tempTileMap.Add(null);
                            tileMap.Add(tempTileMap);
                        }
                        while (tileMap.Count <= cY)
                        {
                            List<Tile> tempTileMap = new List<Tile>();
                            for (int c = 0; c < tileMap[0].Count; c++)
                                tempTileMap.Add(null);
                            tileMap.Add(tempTileMap);
                        }

                        while (tileMap[cY].Count <= cX)
                            tileMap[cY].Add(null);

                        tileMap[cY][cX].TileSheetRectangle.X = (int)mapIndex.X;
                        tileMap[cY][cX].TileSheetRectangle.Y = (int)mapIndex.Y;
                    }
                    tileIndex.X++;
                }
            }
        }

        public void Initialize (ContentManager content)
        {
            TileSheet.Initialize(content);
            TileSheetImage.Path = TileSheet.Path;
            TileSheetImage.Initialize(content);

            Vector2 position = -Vector2.One;
            foreach (string row in TileMapString.Rows)
            {
                position.Y++;
                string[] split = row.Split(']');
                List<Tile> tempTileMap = new List<Tile>();
                foreach (string s in split)
                {
                    position.X++;
                    Tile newTile;
                    int valueX, valueY;
                    if (s != String.Empty && !s.Contains('x'))
                    {
                        newTile = new Tile();

                        string str = s.Replace("[", "");
                        valueX = int.Parse(str.Substring(0, str.IndexOf(':')));
                        valueY = int.Parse(str.Substring(str.IndexOf(':') + 1));

                        Tile.TileRotation rotation;
                        if (TileMapString.Rotation90Tiles.Contains("[" + position.X.ToString() + ":" + position.Y.ToString() + "]"))
                            rotation = Tile.TileRotation.Clockwise90;
                        else if (TileMapString.Rotation180Tiles.Contains("[" + position.X.ToString() + ":" + position.Y.ToString() + "]"))
                            rotation = Tile.TileRotation.Clockwise180;
                        else if (TileMapString.Rotation270Tiles.Contains("[" + position.X.ToString() + ":" + position.Y.ToString() + "]"))
                            rotation = Tile.TileRotation.Clockwise270;
                        else
                            rotation = 0.0f;

                        newTile.Initialize(this, new Vector2(valueX, valueY), position * TileDimensions, rotation);
                    }
                    else
                        newTile = null;

                    tempTileMap.Add(newTile);
                }
                tileMap.Add(tempTileMap);

                position.X = -1;
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (List<Tile> tileRow in tileMap)
                foreach (Tile tile in tileRow)
                    tile.Draw(spriteBatch);
        }

        public void DrawTile (SpriteBatch spriteBatch, Vector2 position)
        {
            position /= TileDimensions;

            tileMap[(int)position.X][(int)position.Y].Draw(spriteBatch);
        }
    }
}
