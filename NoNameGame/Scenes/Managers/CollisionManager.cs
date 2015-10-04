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
                    Vector2 collisionSolving = collisionShape.GetIntersectionDepth(entity.Shape) * getCollisionSide(entity, collisionShape);

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

        private Vector2 getCollisionSide(Entity entity, Shape entityShapeB)
        {
            Vector2 collisionHandlingDirection = Vector2.Zero;
            dynamic shapeA = Convert.ChangeType(entity.Shape, entity.Shape.Type.GetTypeType());
            dynamic shapeB = Convert.ChangeType(entityShapeB, entityShapeB.Type.GetTypeType());

            if(entity.Body.Velocity.X < 0 &&
                shapeA.Left < shapeB.Right && shapeA.Left > shapeB.Left)
                collisionHandlingDirection.X = 1;
            else if(entity.Body.Velocity.X > 0 &&
                    shapeA.Right > shapeB.Left && shapeA.Right < shapeB.Right)
                collisionHandlingDirection.X = -1;

            if(entity.Body.Velocity.Y < 0 &&
                    shapeA.Top < shapeB.Bottom && shapeA.Top > shapeB.Top)
                collisionHandlingDirection.Y = 1;
            else if(entity.Body.Velocity.Y > 0 &&
                    shapeA.Bottom > shapeB.Top && shapeA.Bottom < shapeB.Bottom)
                collisionHandlingDirection.Y = -1;

            if(collisionHandlingDirection.X != 0 && collisionHandlingDirection.Y != 0)
            {
                float verticalDiff = 0;
                float horizontalDiff = 0;

                if(collisionHandlingDirection.X < 0)
                    horizontalDiff = shapeA.Right - shapeB.Left;
                else if(collisionHandlingDirection.X > 0)
                    horizontalDiff = shapeB.Right - shapeA.Left;

                if(collisionHandlingDirection.Y < 0)
                    verticalDiff = shapeA.Bottom - shapeB.Top;
                else if(collisionHandlingDirection.Y > 0)
                    verticalDiff = shapeB.Bottom - shapeA.Top;

                if(verticalDiff <= horizontalDiff)
                    collisionHandlingDirection.X = 0;
                else
                    collisionHandlingDirection.Y = 0;
            }

            return collisionHandlingDirection;
        }
    }
}
