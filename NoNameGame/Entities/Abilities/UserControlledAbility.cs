using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using NoNameGame.Managers;

namespace NoNameGame.Entities.Abilities
{
    /// <summary>
    /// Stellt die Fähigkeit dar, dass eine Enity von einem Spieler kontrolliert werden kann.
    /// </summary>
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

            Vector2 newMovingVector = Vector2.Zero;
            if(InputManager.Instance.KeyDown(Keys.D, Keys.Right))
                newMovingVector.X = entity.Body.AccelerationValue * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if(InputManager.Instance.KeyDown(Keys.A, Keys.Left))
                newMovingVector.X = -entity.Body.AccelerationValue * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(InputManager.Instance.KeyDown(Keys.W, Keys.Up))
                newMovingVector.Y = -entity.Body.AccelerationValue * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if(InputManager.Instance.KeyDown(Keys.S, Keys.Down))
                newMovingVector.Y = entity.Body.AccelerationValue * offset * (float)gameTime.ElapsedGameTime.TotalSeconds;
            entity.Body.ChangeMovingVector = newMovingVector;

            base.Update(gameTime);
        }
    }
}
