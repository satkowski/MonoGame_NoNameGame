using Microsoft.Xna.Framework;

namespace NoNameGame.Entities.Abilities
{
    /// <summary>
    /// Abstrakte Grundklasse, von welcher alle anderen Ability erben.
    /// </summary>
    public abstract class EntityAbility
    {
        /// <summary>
        /// Das Objekt auf welches die Fähigkeit wirken soll.
        /// </summary>
        protected Entity entity;

        /// <summary>
        /// Ist dieser Effekt aktiv.
        /// </summary>
        public bool IsActive;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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
