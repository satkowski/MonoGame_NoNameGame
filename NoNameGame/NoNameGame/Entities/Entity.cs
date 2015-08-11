using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Images;

namespace NoNameGame.Entities
{
    public class Entity
    {
        public Image Image;
        [XmlIgnore]
        public Vector2 MoveVelocity;
        public int CollisionLevel;

        protected Entity ()
        {
            MoveVelocity = Vector2.Zero;
            CollisionLevel = 0;
        }

        public virtual void LoadContent ()
        {
            Image.LoadContent();
        }

        public virtual void UnloadContent ()
        {
            Image.UnloadContent();
        }

        public virtual void Update (GameTime gameTime)
        {
            Image.Update(gameTime);
        }

        public virtual void Draw (SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
