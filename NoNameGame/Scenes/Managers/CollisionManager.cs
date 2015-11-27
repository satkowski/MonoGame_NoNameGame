using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NoNameGame.Entities;
using NoNameGame.Extensions;
using NoNameGame.Components.Shapes;
using NoNameGame.Maps;

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
            int counter = 0;
            foreach(Entity entity in scene.Entities)
            {
                collisionWithEntities(entity, counter);
                collisionWithMap(entity);
                counter++;
            }

            base.Update(gameTime);
        }

        private void collisionWithEntities(Entity entity, int counter)
        {
            Vector2 collisionMovement = Vector2.Zero;
            
            bool firstCollision = true;
            for(int c = 0; c < scene.Entities.Count; c++)
            {
                if(c == counter)
                    continue;
                Vector2 collisionResolving = scene.Entities[c].Shape.GetCollisionSolvingVector(entity.Shape);
                if(collisionResolving == Vector2.Zero)
                    continue;

                if(firstCollision)
                {
                    if(entity.Body.Velocity.X < 0)
                        collisionMovement.X = collisionResolving.X;
                    else if(entity.Body.Velocity.X > 0)
                        collisionMovement.X = -collisionResolving.X;
                    if(entity.Body.Velocity.Y < 0)
                        collisionMovement.Y = collisionResolving.Y;
                    else if(entity.Body.Velocity.Y > 0)
                        collisionMovement.Y -= collisionResolving.Y;

                    firstCollision = false;
                    continue;
                }

                if(entity.Body.Velocity.X < 0)
                    collisionMovement.X = MathHelper.Max(collisionMovement.X, collisionResolving.X);
                else if(entity.Body.Velocity.X > 0)
                    collisionMovement.X = MathHelper.Min(collisionMovement.X, -collisionResolving.X);
                if(entity.Body.Velocity.Y < 0)
                    collisionMovement.Y = MathHelper.Max(collisionMovement.Y, collisionResolving.Y);
                else if(entity.Body.Velocity.Y > 0)
                    collisionMovement.Y = MathHelper.Min(collisionMovement.Y, -collisionResolving.Y);
            }
            entity.Body.Position += collisionMovement;
        }

        private void collisionWithMap(Entity entity)
        {
            Vector2 collisionMovement = Vector2.Zero;

            foreach(Layer layer in scene.Map.Layers)
            {
                if(layer.CollisionLevel != entity.Body.CollisionLevel)
                    continue;

                bool firstCollision = true;
                foreach(Tile tile in layer.TileMap)
                {
                    Vector2 collisionResolving = tile.Shape.GetCollisionSolvingVector(entity.Shape);
                    if(collisionResolving == Vector2.Zero)
                        continue;

                    if(firstCollision)
                    {
                        if(entity.Body.Velocity.X < 0)
                            collisionMovement.X = collisionResolving.X;
                        else if(entity.Body.Velocity.X > 0)
                            collisionMovement.X = -collisionResolving.X;
                        if(entity.Body.Velocity.Y < 0)
                            collisionMovement.Y = collisionResolving.Y;
                        else if(entity.Body.Velocity.Y > 0)
                            collisionMovement.Y -= collisionResolving.Y;

                        firstCollision = false;
                        continue;
                    }

                    if(entity.Body.Velocity.X < 0)
                        collisionMovement.X = MathHelper.Max(collisionMovement.X, collisionResolving.X);
                    else if(entity.Body.Velocity.X > 0)
                        collisionMovement.X = MathHelper.Min(collisionMovement.X, -collisionResolving.X);
                    if(entity.Body.Velocity.Y < 0)
                        collisionMovement.Y = MathHelper.Max(collisionMovement.Y, collisionResolving.Y);
                    else if(entity.Body.Velocity.Y > 0)
                        collisionMovement.Y = MathHelper.Min(collisionMovement.Y, -collisionResolving.Y);
                }
            }
            entity.Body.Position += collisionMovement;
        }
    }
}
