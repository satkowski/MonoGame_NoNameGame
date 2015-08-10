using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Managers;
using Microsoft.Xna.Framework.Input;

namespace NoNameGame.Entities
{
    public class UserControlledEntity : Entity
    {
        public float MoveSpeed;

        public UserControlledEntity ()
        {
            MoveSpeed = 0.0f;
        }

        public override void LoadContent ()
        {
            base.LoadContent();
        }

        public override void UnloadContent ()
        {
            base.UnloadContent();
        }

        public override void Update (GameTime gameTime)
        {
            MoveVelocity = Vector2.Zero;

            if (InputManager.Instance.KeyDown(Keys.D, Keys.Right))
                MoveVelocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputManager.Instance.KeyDown(Keys.A, Keys.Left))
                MoveVelocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputManager.Instance.KeyDown(Keys.W, Keys.Up))
                MoveVelocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputManager.Instance.KeyDown(Keys.S, Keys.Down))
                MoveVelocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Image.Position += MoveVelocity;

            base.Update(gameTime);
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
