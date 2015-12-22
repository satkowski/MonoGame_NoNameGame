using Microsoft.Xna.Framework;

namespace NoNameGame.Images.Effects
{
    /// <summary>
    /// Die abstrakte Grundklasse aller Effekte.
    /// </summary>
    public abstract class ImageEffect
    {
        /// <summary>
        /// Das Bild auf dem dieser Effekt behandelt werden soll.
        /// </summary>
        protected Image image;

        /// <summary>
        /// Ist der Effekt aktiv.
        /// </summary>
        public bool IsActive;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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
