using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Maps;
using NoNameGame.Managers;

namespace NoNameGame.Images
{
    public class TileSheet
    {
        ContentManager content;

        [XmlIgnore]
        public Texture2D Texture;
        public string Path;
        public float Alpha;
        public Color Color;

        public TileSheet ()
        {
            Alpha = 1.0f;
            Path = String.Empty;
            Color = Color.White;
        }

        public void LoadContent ()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);
        }

        public void UnloadContent ()
        {
            content.Unload();
        }

        public void Update (GameTime gameTime)
        {
        }

        public void Draw (SpriteBatch spriteBatch, Tile tile)
        {
            spriteBatch.Draw(Texture, tile.DestinationPosition + tile.Origin + tile.Offset, tile.TileSheetRectangle, Color * Alpha, 
                             tile.Rotation.GetRotationValue(), tile.Origin, tile.Scale, SpriteEffects.None, 0.0f);
        }
    }
}
