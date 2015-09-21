﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Managers;
using NoNameGame.Maps;

namespace NoNameGame.Entities
{
    public class UserControlledEntity : Entity
    {
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

        public override void Update (GameTime gameTime, Map map)
        {
            MoveVelocity = Vector2.Zero;

            float offset = 1.0f;
            if(InputManager.Instance.KeyDown(Keys.D, Keys.Right, Keys.A, Keys.Left)
               && InputManager.Instance.KeyDown(Keys.W, Keys.Up, Keys.S, Keys.Down))
                offset = 0.707106781f;

            if (InputManager.Instance.KeyDown(Keys.D, Keys.Right))
                MoveVelocity.X = MoveSpeed * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputManager.Instance.KeyDown(Keys.A, Keys.Left))
                MoveVelocity.X = -MoveSpeed * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputManager.Instance.KeyDown(Keys.W, Keys.Up))
                MoveVelocity.Y = -MoveSpeed * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputManager.Instance.KeyDown(Keys.S, Keys.Down))
                MoveVelocity.Y = MoveSpeed * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Image.Position += MoveVelocity * MoveSpeedFactor;

            base.Update(gameTime, map);
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
