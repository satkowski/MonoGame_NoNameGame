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
    /// <summary>
    /// Eine Klasse, welche die Kollisionen in einer Szene handhabt.
    /// </summary>
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
            // Geht durch alle Entities und prüft für jedes die Collision
            foreach(Entity entity in scene.Entities)
            {
                collisionWithEntities(entity, counter);
                collisionWithMap(entity);
                counter++;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Führt die Kollisionsbehebung zwischen Entities aus.
        /// </summary>
        /// <param name="entity">eine Entity</param>
        /// <param name="counter">des Objekts, aktuelle Stelle in der Liste</param>
        private void collisionWithEntities(Entity entity, int counter)
        {
            Vector2 collisionMovement = Vector2.Zero;
            
            bool firstCollision = true;
            for(int c = 0; c < scene.Entities.Count; c++)
            {
                // Prüft sich nicht selbst
                if(c == counter)
                    continue;
                collisionWithSomething(entity, scene.Entities[c].Shape, ref firstCollision, ref collisionMovement);
            }
            entity.Body.Position += collisionMovement;
        }

        /// <summary>
        /// Führt die Kollisionsbehebung zwischen einer Entity und der Map aus.
        /// </summary>
        /// <param name="entity">eine Entity</param>
        private void collisionWithMap(Entity entity)
        {
            Vector2 collisionMovement = Vector2.Zero;

            foreach(Layer layer in scene.Map.Layers)
            {
                if(layer.CollisionLevel != entity.Body.CollisionLevel)
                    continue;

                bool firstCollision = true;
                foreach(Tile tile in layer.TileMap)
                    collisionWithSomething(entity, tile.Shape, ref firstCollision, ref collisionMovement);
            }
            entity.Body.Position += collisionMovement;
        }

        /// <summary>
        /// Führt die Kollisionsbehebung zwischen einer Enity und einer Shape aus.
        /// </summary>
        /// <param name="entity">eine Entity</param>
        /// <param name="shape">die Shape</param>
        /// <param name="firstCollision">ist das die erste Kollision der Entity</param>
        /// <param name="collisionMovement">der Kollisionsbehebungsvektor</param>
        private void collisionWithSomething(Entity entity, Shape shape, ref bool firstCollision, ref Vector2 collisionMovement)
        {
            // Wenn die Collision gleich 0 ist, braucht man nichts mehr machen
            Vector2 collisionResolving = shape.GetCollisionSolvingVector(entity.Shape);
            if(collisionResolving == Vector2.Zero)
                return;

            // Setzt beim ersten mal die werte (falls es mehrere Kollisionen gleichzeitig gibt)
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
                return;
            }

            // Entscheidet in welche Richtung die Kollision aufgelöst werden soll
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
}
