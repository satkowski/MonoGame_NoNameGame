using Microsoft.Xna.Framework;
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
                Vector2 positionOffset = PlayerPosition - entity.Image.Position;
                if(positionOffset != Vector2.Zero)
                {
                    positionOffset /= new Vector2(Math.Abs(positionOffset.X), Math.Abs(positionOffset.Y));
                    double distance = Math.Sqrt((PlayerPosition.X - entity.Image.Position.X) * (PlayerPosition.X - entity.Image.Position.X)
                                      + (PlayerPosition.Y - entity.Image.Position.Y) * (PlayerPosition.Y - entity.Image.Position.Y));
                    double offsetX = Math.Cos((PlayerPosition.X - entity.Image.Position.X) / distance);
                    double offsetY = Math.Cos((PlayerPosition.Y - entity.Image.Position.Y) / distance);

                    entity.MoveVelocity += new Vector2((float)offsetX, (float)offsetY) * positionOffset * entity.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            base.Update(gameTime);
        }
    }
}
