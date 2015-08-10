using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MapEditor.Managers;

namespace MapEditor.Images
{
    public class Image
    {
        Vector2 origin;
        ContentManager content;

        [XmlIgnore]
        public Texture2D Texture;
        public string Path;
        public float Scale;
        public float Rotation;
        public float Alpha;
        public Color Color;
        public Vector2 Position;
        public Rectangle SourceRectangle;
        public Rectangle CurrentRectangle;
        public Rectangle PrevRectangle;

        public Image ()
        {
            Position = Vector2.Zero;
            Scale = 1.0f;
            Alpha = 1.0f;
            Rotation = 0.0f;
            Path = String.Empty;
            SourceRectangle = Rectangle.Empty;
            PrevRectangle = Rectangle.Empty;
            CurrentRectangle = Rectangle.Empty;
            Color = Color.White;
            origin = Vector2.Zero;
        }

        public void Initialize (ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);
            if (SourceRectangle == Rectangle.Empty)
                SourceRectangle = Texture.Bounds;
        }

        public void Draw (SpriteBatch spriteBatch, Vector2 windowPosition)
        {
            spriteBatch.Draw(Texture, Position + origin - windowPosition, SourceRectangle, Color.White, Rotation, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
