using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NoNameGame.Entities;
using NoNameGame.Maps;
using NoNameGame.Collisions;

namespace NoNameGame.Scenes.Managers
{
    /// <summary>
    /// Eine Klasse, welche die Kollisionen in einer Szene handhabt.
    /// </summary>
    class CollisionManager : SceneManager
    {
        /// <summary>
        /// Die Aufzählung aller Kollisionen, die in einem Zyklus aufgetaucht sind.
        /// </summary>
        private Dictionary<string, Collision> collisions;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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
            collisions = new Dictionary<string, Collision>();
            // Geht durch alle Entities und prüft für jedes die Collision
            foreach(Entity entity in scene.Entities)
            {
                collisionWithEntities(entity, counter);
                collisionWithMap(entity);
                counter++;
            }
            foreach(KeyValuePair<string, Collision> collision in collisions)
                collision.Value.ResolveCollision();

            base.Update(gameTime);
        }

        /// <summary>
        /// Führt die Kollisionsbehebung zwischen Entities aus.
        /// </summary>
        /// <param name="entity">eine Entity</param>
        /// <param name="counter">des Objekts, aktuelle Stelle in der Liste</param>
        private void collisionWithEntities(Entity entity, int counter)
        {
            for(int c = 0; c < scene.Entities.Count; c++)
            {
                // Prüft sich nicht selbst
                if(c == counter)
                    continue;

                Vector2 collisionResolving = scene.Entities[c].Shape.GetCollisionSolvingVector(entity.Shape);
                if(collisionResolving != Vector2.Zero)
                {
                    Collision collision = new Collision(entity, scene.Entities[c], collisionResolving);
                    if(!collisions.ContainsKey(collision.ID))
                        collisions.Add(collision.ID, collision);
                }
            }
        }

        /// <summary>
        /// Führt die Kollisionsbehebung zwischen einer Entity und der Map aus.
        /// </summary>
        /// <param name="entity">eine Entity</param>
        private void collisionWithMap(Entity entity)
        {
            foreach(Layer layer in scene.Map.Layers)
            {
                if(layer.CollisionLevel != entity.Body.CollisionLevel)
                    continue;

                foreach(Tile tile in layer.TileMap)
                {
                    Vector2 collisionResolving = tile.Shape.GetCollisionSolvingVector(entity.Shape);
                    if(collisionResolving != Vector2.Zero)
                    {
                        Collision collision = new Collision(entity, tile, collisionResolving);
                        if(!collisions.ContainsKey(collision.ID))
                            collisions.Add(collision.ID, collision);
                    }
                }
            }
        }
    }
}
