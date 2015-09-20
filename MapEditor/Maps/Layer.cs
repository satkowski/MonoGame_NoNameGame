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
        Vector2 size;

        public TileSheet TileSheet;
        [XmlIgnore]
        public Image TileSheetImage;
        public Vector2 Offset;
        public float Scale;
        public Vector2 TileDimensions;
        public int CollisionLevel;
        public TileMapString TileMapString;
        [XmlIgnore]
        public bool Active;
        [XmlIgnore]
        public Vector2 Size
        {
            get { return size; }
            set
            {
                size = value;
                if(OnSizeChanged != null)
                    OnSizeChanged(this, null);
            }
        }
        public event EventHandler OnSizeChanged;

        public Layer ()
        {
            Offset = Vector2.Zero;
            TileMapString = new TileMapString();
            tileMap = new List<List<Tile>>();
            TileDimensions = Vector2.One;
            TileSheet = new TileSheet();
            TileSheetImage = new Image();
            Active = false;
            Size = Vector2.Zero;
            CollisionLevel = -1;
            Scale = 1.0f;
        }

        public void Save ()
        {
            TileMapString.Rows = new List<string>();
            TileMapString.Rotation90Tiles = String.Empty;
            TileMapString.Rotation180Tiles = String.Empty;
            TileMapString.Rotation270Tiles = String.Empty;

            for (int cY = 0; cY < tileMap.Count; cY++)
            {
                string row = String.Empty;
                for (int cX = 0; cX < tileMap[cY].Count; cX++)
                {
                    if (tileMap[cY][cX] == null)
                        row += "[x:x]";
                    else
                    {
                        Vector2 tilePosition = new Vector2(tileMap[cY][cX].TileSheetRectangle.X / TileDimensions.X, tileMap[cY][cX].TileSheetRectangle.Y / TileDimensions.Y);
                        row += "[" + tilePosition.X.ToString() + ":" + tilePosition.Y.ToString() + "]";

                        string posStr = "[" + cX.ToString() + ":" + cY.ToString() + "]";
                        if (tileMap[cY][cX].Rotation == Tile.TileRotation.Clockwise90)
                            TileMapString.Rotation90Tiles += posStr;
                        else if (tileMap[cY][cX].Rotation == Tile.TileRotation.Clockwise180)
                            TileMapString.Rotation180Tiles += posStr;
                        else if (tileMap[cY][cX].Rotation == Tile.TileRotation.Clockwise270)
                            TileMapString.Rotation270Tiles += posStr;                        
                    }
                }
                TileMapString.Rows.Add(row);
            }
        }

        public void ReplaceTiles (Vector2 postion, Rectangle selectedRegion, Vector2 windowPosition)
        {
            Vector2 startIndex = new Vector2((postion.X + windowPosition.X) / TileDimensions.X, 
                                             (postion.Y + windowPosition.Y) / TileDimensions.Y);
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
                        //if (tileMap[cY][cX] == null)
                        //{
                        //    tileMap[cY][cX] = new Tile();
                        //    tileMap[cY][cX].Initialize(this, mapIndex * TileDimensions, new Vector2(cX, cY) * TileDimensions, Tile.TileRotation.None);
                        //}
                        tileMap[cY][cX].TileSheetRectangle.X = (int)(mapIndex.X * TileDimensions.X);
                        tileMap[cY][cX].TileSheetRectangle.Y = (int)(mapIndex.Y * TileDimensions.Y);
                        tileMap[cY][cX].Rotation = MapEditor.WindowParts.Tile.Rotation;
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

                        if (tileMap[cY][cX] == null)
                        {
                            tileMap[cY][cX] = new Tile();
                            tileMap[cY][cX].Initialize(this, mapIndex * TileDimensions, new Vector2(cX, cY), Tile.TileRotation.None);
                        }

                        tileMap[cY][cX].TileSheetRectangle.X = (int)(mapIndex.X * TileDimensions.X);
                        tileMap[cY][cX].TileSheetRectangle.Y = (int)(mapIndex.Y * TileDimensions.Y);
                        tileMap[cY][cX].Rotation = MapEditor.WindowParts.Tile.Rotation;
                    }
                    tileIndex.X++;
                }
            }

            int maxX = 0;
            foreach(List<Tile> row in tileMap)
                maxX = maxX < row.Count ? row.Count : maxX;
            Size = new Vector2(maxX, tileMap.Count) * TileDimensions;
        }

        public void Initialize (ContentManager content)
        {
            TileSheet.Initialize(content, Offset);
            TileSheetImage.Path = TileSheet.Path;
            TileSheetImage.Initialize(content);

            Vector2 position = -Vector2.One;
            int maxX = 0;
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
                        
                        newTile.Initialize(this, new Vector2(valueX, valueY), position,rotation);
                    }
                    else
                        newTile = null;

                    tempTileMap.Add(newTile);
                }
                tileMap.Add(tempTileMap);

                position.X = -1;
                maxX = maxX < tempTileMap.Count ? tempTileMap.Count : maxX;
            }
            size = new Vector2(maxX, tileMap.Count) * TileDimensions;
        }

        public void Draw (SpriteBatch spriteBatch, Vector2 windowPosition)
        {
            if(Active)
                foreach (List<Tile> tileRow in tileMap)
                    foreach (Tile tile in tileRow)
                        if(tile != null)
                            tile.Draw(spriteBatch, windowPosition, new Vector2(TileDimensions.X * Scale / 2, TileDimensions.Y * Scale / 2));
        }

        public void DrawTile (SpriteBatch spriteBatch, Vector2 position, float scale, Tile.TileRotation rotation)
        {
            position /= TileDimensions;

            TileSheet.DrawOffset = Vector2.Zero;

            Tile drawTile = new Tile();
            drawTile.Initialize(this, position, Vector2.Zero, rotation);
            drawTile.Draw(spriteBatch, Vector2.Zero, Vector2.Zero);

            TileSheet.DrawOffset = Offset;
        }
    }
}