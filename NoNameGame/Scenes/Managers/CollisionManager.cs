using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NoNameGame.Entities;
using NoNameGame.Extensions;

namespace NoNameGame.Scenes.Managers
{
    class CollisionManager : SceneManager
    {
        public CollisionManager()
        {

        }

        public override void LoadContent(ref Scene scene)
        {
            base.LoadContent(ref scene);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Entity entity in scene.Entities)
            {
                collisionWithMap(entity);
            }

            base.Update(gameTime);
        }

        private void collisionWithMap(Entity entity)
        {
            if(entity.Body.Velocity != Vector2.Zero)
            {
                Vector2 collisionMovement = Vector2.Zero;
                List<Rectangle> collidingRectangles = scene.Map.GetCollidingTileRectangles(entity.Image.CurrentRectangle, entity.Body.CollisionLevel);

                if(collidingRectangles.Count != 0)
                {
                    foreach(Rectangle collisionRectangle in collidingRectangles)
                    {
                        Vector2 collisionSolving = collisionRectangle.GetIntersectionDepth(entity.Image.CurrentRectangle) * getCollisionSide(entity, collisionRectangle);

                        if(entity.Body.Velocity.X < 0)
                            collisionMovement.X = MathHelper.Max(collisionMovement.X, collisionSolving.X);
                        else if(entity.Body.Velocity.X > 0)
                            collisionMovement.X = MathHelper.Min(collisionMovement.X, collisionSolving.X);
                        if(entity.Body.Velocity.Y < 0)
                            collisionMovement.Y = MathHelper.Max(collisionMovement.Y, collisionSolving.Y);
                        else if(entity.Body.Velocity.Y > 0)
                            collisionMovement.Y = MathHelper.Min(collisionMovement.Y, collisionSolving.Y);
                    }
                }
                entity.Body.Position += collisionMovement;
            }
        }

        private Vector2 getCollisionSide(Entity entity, Rectangle entityRectangle)
        {
            Vector2 collisionHandlingDirection = Vector2.Zero;

            if(entity.Body.Velocity.X < 0 &&
                entity.Image.CurrentRectangle.Left < entityRectangle.Right && entity.Image.CurrentRectangle.Left > entityRectangle.Left)
                collisionHandlingDirection.X = 1;
            else if(entity.Body.Velocity.X > 0 &&
                    entity.Image.CurrentRectangle.Right > entityRectangle.Left && entity.Image.CurrentRectangle.Right < entityRectangle.Right)
                collisionHandlingDirection.X = -1;

            if(entity.Body.Velocity.Y < 0 &&
                    entity.Image.CurrentRectangle.Top < entityRectangle.Bottom && entity.Image.CurrentRectangle.Top > entityRectangle.Top)
                collisionHandlingDirection.Y = 1;
            else if(entity.Body.Velocity.Y > 0 &&
                    entity.Image.CurrentRectangle.Bottom > entityRectangle.Top && entity.Image.CurrentRectangle.Bottom < entityRectangle.Bottom)
                collisionHandlingDirection.Y = -1;

            if(collisionHandlingDirection.X != 0 && collisionHandlingDirection.Y != 0)
            {
                float verticalDiff = 0;
                float horizontalDiff = 0;

                if(collisionHandlingDirection.X < 0)
                    horizontalDiff = entity.Image.CurrentRectangle.Right - entityRectangle.Left;
                else if(collisionHandlingDirection.X > 0)
                    horizontalDiff = entityRectangle.Right - entity.Image.CurrentRectangle.Left;

                if(collisionHandlingDirection.Y < 0)
                    verticalDiff = entity.Image.CurrentRectangle.Bottom - entityRectangle.Top;
                else if(collisionHandlingDirection.Y > 0)
                    verticalDiff = entityRectangle.Bottom - entity.Image.CurrentRectangle.Top;

                if(verticalDiff <= horizontalDiff)
                    collisionHandlingDirection.X = 0;
                else
                    collisionHandlingDirection.Y = 0;
            }

            return collisionHandlingDirection;
        }
    }
}
