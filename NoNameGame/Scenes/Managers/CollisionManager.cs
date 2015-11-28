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
            for(int c = 0; c < scene.Entities.Count; c++)
            {
                // Prüft sich nicht selbst
                if(c == counter)
                    continue;

                entity.Body.Position += scene.Entities[c].Shape.GetCollisionSolvingVector(entity.Shape);
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
                    entity.Body.Position += tile.Shape.GetCollisionSolvingVector(entity.Shape);
            }
        }
    }
}
