﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using NoNameGame.Managers;

namespace NoNameGame.Entities.Abilities
{
    public class UserControlledAbility : EntityAbility
    {
        public override void LoadContent(ref Entity entity)
        {
            base.LoadContent(ref entity);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Wenn sich sowohl vertikal als auch horizontal bewegt wird, wird der offset angepasst.
            // Dieser ist einfach nur der cos von 45 Grad
            float offset = 1.0f;
            if(InputManager.Instance.KeyDown(Keys.D, Keys.Right, Keys.A, Keys.Left)
               && InputManager.Instance.KeyDown(Keys.W, Keys.Up, Keys.S, Keys.Down))
                offset = 0.707106781f;

            if(InputManager.Instance.KeyDown(Keys.D, Keys.Right))
                entity.Body.Velocity.X = entity.Body.Speed * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if(InputManager.Instance.KeyDown(Keys.A, Keys.Left))
                entity.Body.Velocity.X = -entity.Body.Speed * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(InputManager.Instance.KeyDown(Keys.W, Keys.Up))
                entity.Body.Velocity.Y = -entity.Body.Speed * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if(InputManager.Instance.KeyDown(Keys.S, Keys.Down))
                entity.Body.Velocity.Y = entity.Body.Speed * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }
    }
}
