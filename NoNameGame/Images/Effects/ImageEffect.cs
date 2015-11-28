using Microsoft.Xna.Framework;

namespace NoNameGame.Images.Effects
{
    /// <summary>
    /// Die abstrakte Grundklasse aller Effekte.
    /// </summary>
    public abstract class ImageEffect
    {
        protected Image image;

        public bool IsActive;

        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image image)
        {
            this.image = image;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
