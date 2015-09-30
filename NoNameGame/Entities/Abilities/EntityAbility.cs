﻿using Microsoft.Xna.Framework;

namespace NoNameGame.Entities.Abilities
{
    public class EntityAbility
    {
        protected Entity entity;

        public bool IsActive;

        public EntityAbility()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Entity entity)
        {
            this.entity = entity;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}