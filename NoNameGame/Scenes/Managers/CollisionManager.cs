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
            foreach(Entity entity in scene.Entities)
            {
                collisionWithMap(entity);
            }

            base.Update(gameTime);
        }

        private void collisionWithMap(Entity entity)
        {
            Vector2 collisionMovement = Vector2.Zero;
            List<Shape> collidingShapes = getCollidingTileShapes(entity.Shape, entity.Body.CollisionLevel);

            if(collidingShapes.Count != 0)
            {
                foreach(Shape collisionShape in collidingShapes)
                {
                    Vector2 collisionSolving = collisionShape.GetIntersectionDepth(entity.Shape);
                    collisionSolving *= getCollisionSide(entity.Body.Velocity, collisionSolving);

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

        private List<Shape> getCollidingTileShapes(Shape entityShape, int entityLevel)
        {
            List<Shape> collidingShape = new List<Shape>();

            foreach(Layer layer in scene.Map.Layers)
            {
                if(layer.CollisionLevel != entityLevel)
                    continue;
                foreach(Tile tile in layer.TileMap)
                    if(tile.Shape.Intersects(entityShape))
                        collidingShape.Add(tile.Shape);
            }
            return collidingShape;
        }

        private Vector2 getCollisionSide(Vector2 velocity, Vector2 penetration)
        {
            Vector2 collisionHandlingDirection = Vector2.Zero;

            if(velocity.X < 0 && penetration.X != 0)
                collisionHandlingDirection.X = 1;
            else if(velocity.X > 0 && penetration.X != 0)
                collisionHandlingDirection.X = -1;

            if(velocity.Y < 0 && penetration.Y != 0)
                collisionHandlingDirection.Y = 1;
            else if(velocity.Y > 0 && penetration.Y != 0)
                collisionHandlingDirection.Y = -1;

            if(collisionHandlingDirection.X != 0 && collisionHandlingDirection.Y != 0)
            {
                if(penetration.Y <= penetration.X)
                    collisionHandlingDirection.X = 0;
                else
                    collisionHandlingDirection.Y = 0;
            }

            return collisionHandlingDirection;
        }
    }
}
