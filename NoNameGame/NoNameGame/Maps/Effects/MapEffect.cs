using Microsoft.Xna.Framework;

namespace NoNameGame.Maps.Effects
{
    public abstract class MapEffect
    {
        protected Map map;

        public bool IsActive;

        public MapEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Map map)
        {
            this.map = map;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
