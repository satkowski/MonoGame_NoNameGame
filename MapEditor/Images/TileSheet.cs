﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MapEditor.Maps;
using MapEditor.Managers;

namespace MapEditor.Images
{
    public class TileSheet
    {
        ContentManager content;

        [XmlIgnore]
        public Texture2D Texture;
        public string Path;
        [XmlIgnore]
        public float Alpha;
        [XmlIgnore]
        public Color Color;
        [XmlIgnore]
        public Vector2 DrawOffset;

        public TileSheet ()
        {
            Alpha = 1.0f;
            Path = String.Empty;
            Color = Color.White;
            DrawOffset = Vector2.Zero;
        }

        public void Initialize (ContentManager content, Vector2 offset)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            DrawOffset = offset;

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);
        }

        public void Draw (SpriteBatch spriteBatch, float scale, Vector2 tileDimesion, Vector2 windowPosition, Tile tile, Vector2 scaledOrigin)
        {
            if(Texture != null)
            {
                if(scaledOrigin != Vector2.Zero)
                    spriteBatch.Draw(Texture, tile.MapPosition * tileDimesion * scale + scaledOrigin + DrawOffset - windowPosition, tile.TileSheetRectangle, Color * Alpha,
                                     tile.Rotation.GetRotationValue(), tile.Origin, scale, SpriteEffects.None, 0.0f);
                else
                    spriteBatch.Draw(Texture, tile.DestinationPosition + tile.Origin + DrawOffset - windowPosition, tile.TileSheetRectangle, Color * Alpha,
                                     tile.Rotation.GetRotationValue(), tile.Origin, 1.0f, SpriteEffects.None, 0.0f);
            }
        }
    }
}
