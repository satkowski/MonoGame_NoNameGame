using System;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Maps;
using NoNameGame.Managers;

namespace NoNameGame.Images
{
    /// <summary>
    /// Stellt ein TileSheet dar.
    /// </summary>
    public class TileSheet
    {
        /// <summary>
        /// Der Contetnmanager des Spiels.
        /// </summary>
        ContentManager content;

        /// <summary>
        /// Die Textur des TileSheets.
        /// </summary>
        [XmlIgnore]
        public Texture2D Texture;
        /// <summary>
        /// Der Pfad zum Bild des TileSheets.
        /// </summary>
        public string Path;
        /// <summary>
        /// Der Alphawert des TileSheets.
        /// </summary>
        public float Alpha;
        /// <summary>
        /// Die Skalierung des TileSheets.
        /// </summary>
        public float Scale;
        /// <summary>
        /// Die Farbe der Transperenten Teile des TileSheets.
        /// </summary>
        public Color Color;

        /// <summary>
        /// Die Verschiebung des TileSheets.
        /// </summary>
        public Vector2 Offset;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public TileSheet ()
        {
            Alpha = 1.0f;
            Path = String.Empty;
            Scale = 1.0f;
            Color = Color.White;

            Offset = Vector2.Zero;
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

        public void Draw (SpriteBatch spriteBatch, Vector2 origin, Vector2 scaledOrigin, float scale, Tile tile)
        {
            spriteBatch.Draw(Texture, tile.Body.Position + Offset, tile.TileSheetRectangle, Color * Alpha, 
                             tile.Rotation.GetRotationValue(), origin, scale, SpriteEffects.None, 0.0f);
        }
    }
}
