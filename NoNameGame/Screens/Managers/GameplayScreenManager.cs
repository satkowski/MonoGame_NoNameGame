using System.Collections.Generic;

using Microsoft.Xna.Framework;

using NoNameGame.Maps;
using NoNameGame.Entities;

namespace NoNameGame.Screens.Managers
{
    public class GameplayScreenManager
    {
        protected Map map;
        protected List<Entity> entities;

        public GameplayScreenManager()
        {
            entities = new List<Entity>();
        }

        public virtual void LoadContent(ref Map map, params Entity[] entities)
        {
            this.map = map;
            this.entities.AddRange(entities);
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public virtual void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }
    }
}
