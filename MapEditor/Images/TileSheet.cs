using System;
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

        public void Draw (SpriteBatch spriteBatch, float scale, Vector2 tileDimensions, Vector2 windowPosition, Tile tile, Vector2 origin, bool scaled)
        {
            if(Texture != null)
            {

                if(!scaled)
                    scale = 64 / tileDimensions.X;
                Vector2 scaledOrigin = new Vector2(tileDimensions.X * scale / 2, tileDimensions.Y * scale / 2);
                spriteBatch.Draw(Texture, tile.MapPosition * tileDimensions * scale + scaledOrigin + DrawOffset - windowPosition, tile.TileSheetRectangle, Color * Alpha,
                                 tile.Rotation.GetRotationValue(), origin, scale, SpriteEffects.None, 0.0f);
            }
        }
    }
}
