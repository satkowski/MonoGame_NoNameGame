﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Managers;

namespace NoNameGame.Images
{
    public class Image
    {
        Vector2 origin;
        ContentManager content;
        Vector2 position;

        [XmlIgnore]
        public Texture2D Texture;
        public string Path;
        public float Scale;
        public float Rotation;
        public float Alpha;
        public Color Color;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPositionChange(position, null);
            }
        }
        public Rectangle SourceRectangle;
        public Rectangle CurrentRectangle;
        public Rectangle PrevRectangle;
        public int MergeOffset;

        public event EventHandler OnPositionChange;

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
            MergeOffset = 0;
            Color = Color.White;
            origin = Vector2.Zero;
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
            origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);

            PrevRectangle = CurrentRectangle;
            CurrentRectangle = new Rectangle((int)(Position.X), (int)(Position.Y),
                                             (int)(SourceRectangle.Width * Scale), (int)(SourceRectangle.Height * Scale));
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position + origin, SourceRectangle, Color.White, Rotation, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
