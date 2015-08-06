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

        public TileSheet ()
        {
            Alpha = 1.0f;
            Path = String.Empty;
            Color = Color.White;
        }

        public void Initialize (ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);
        }

        public void Draw (SpriteBatch spriteBatch, Tile tile)
        {
            spriteBatch.Draw(Texture, tile.DestinationPosition + tile.Origin, tile.TileSheetRectangle, Color * Alpha, 
                             tile.Rotation.GetRotationValue(), tile.Origin, tile.Scale, SpriteEffects.None, 0.0f);
        }
    }
}
