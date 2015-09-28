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
                entity.Image.Position += entity.CollisionMovement;
            }

            base.Update(gameTime);
        }

        private void collisionWithMap(Entity entity)
        {
            entity.CollisionMovement = Vector2.Zero;
            if(entity.MoveVelocity != Vector2.Zero)
            {
                List<Rectangle> collidingRectangles = scene.Map.GetCollidingTileRectangles(entity.Image.CurrentRectangle, entity.CollisionLevel);

                if(collidingRectangles.Count != 0)
                {
                    foreach(Rectangle collisionRectangle in collidingRectangles)
                    {
                        Vector2 collisionSolving = collisionRectangle.GetIntersectionDepth(entity.Image.CurrentRectangle) * getCollisionSide(entity, collisionRectangle);

                        if(entity.MoveVelocity.X < 0)
                            entity.CollisionMovement.X = MathHelper.Max(entity.CollisionMovement.X, collisionSolving.X);
                        else if(entity.MoveVelocity.X > 0)
                            entity.CollisionMovement.X = MathHelper.Min(entity.CollisionMovement.X, collisionSolving.X);
                        if(entity.MoveVelocity.Y < 0)
                            entity.CollisionMovement.Y = MathHelper.Max(entity.CollisionMovement.Y, collisionSolving.Y);
                        else if(entity.MoveVelocity.Y > 0)
                            entity.CollisionMovement.Y = MathHelper.Min(entity.CollisionMovement.Y, collisionSolving.Y);
                    }
                }
            }
        }

        private Vector2 getCollisionSide(Entity entity, Rectangle entityRectangle)
        {
            Vector2 collisionHandlingDirection = Vector2.Zero;

            if(entity.MoveVelocity.X < 0 &&
                entity.Image.CurrentRectangle.Left < entityRectangle.Right && entity.Image.CurrentRectangle.Left > entityRectangle.Left)
                collisionHandlingDirection.X = 1;
            else if(entity.MoveVelocity.X > 0 &&
                    entity.Image.CurrentRectangle.Right > entityRectangle.Left && entity.Image.CurrentRectangle.Right < entityRectangle.Right)
                collisionHandlingDirection.X = -1;

            if(entity.MoveVelocity.Y < 0 &&
                    entity.Image.CurrentRectangle.Top < entityRectangle.Bottom && entity.Image.CurrentRectangle.Top > entityRectangle.Top)
                collisionHandlingDirection.Y = 1;
            else if(entity.MoveVelocity.Y > 0 &&
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
