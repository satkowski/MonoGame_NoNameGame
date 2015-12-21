using Microsoft.Xna.Framework;
using NoNameGame.Extensions;

namespace NoNameGame.Entities.Abilities
{
    /// <summary>
    /// Stellt die Fähigkeit dar, dass eine Enity einen Spieler verfolgen kann.
    /// </summary>
    public class PlayerFollowingAbility : EntityAbility
    {
        /// <summary>
        /// Die Position des Spielers.
        /// </summary>
        public Vector2 PlayerPosition;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public PlayerFollowingAbility()
        {

        }

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
            if(IsActive && PlayerPosition != null)
            {
                Vector2 offset = PlayerPosition.GetNormalVectorToVector(entity.Body.Position);
                entity.Body.ChangeMovingVector(offset * entity.Body.Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            base.Update(gameTime);
        }
    }
}
