using Microsoft.Xna.Framework;
using NoNameGame.Extensions;
using System;

namespace NoNameGame.Entities.Abilities
{
    public class PlayerFollowingAbility : EntityAbility
    {
        public Vector2 PlayerPosition;

        public PlayerFollowingAbility()
        {

        }

        public override void LoadContent(ref AutomatedEntity entity)
        {
            base.LoadContent(ref entity);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(IsActive && PlayerPosition != null)
            {
                Vector2? offset = PlayerPosition.GetAngleValues(entity.Image.Position);
                if(offset.HasValue)
                    entity.MoveVelocity += offset.Value * entity.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }
    }
}
