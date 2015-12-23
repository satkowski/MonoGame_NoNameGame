using Microsoft.Xna.Framework;
using System;
using System.Xml.Serialization;

namespace NoNameGame.Images.Effects
{
    /// <summary>
    /// Die abstrakte Grundklasse aller Effekte.
    /// </summary>
    public abstract class ImageEffect
    {

        /// <summary>
        /// Ist der Effekt aktiv.
        /// </summary>
        public bool IsActive;

        /// <summary>
        /// Das Bild auf dem dieser Effekt behandelt werden soll.
        /// </summary>
        [XmlIgnore]
        public Image Image
        { get; private set; }

        /// <summary>
        /// Wird gefeuert, wenn dieser Effekt fertig ist.
        /// </summary>
        public event EventHandler OnEffectFinished;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image image)
        {
            this.Image = image;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Kopieren dieses Effekts.
        /// </summary>
        public abstract ImageEffect Copy();

        protected void CopyEvents(ImageEffect imageEffect)
        {
            imageEffect.OnEffectFinished += this.OnEffectFinished;
        }

        /// <summary>
        /// Dreht den Effekt herum.
        /// </summary>
        /// <param name="active">ob der Effekt danach aktiviert werden soll</param>
        public abstract void RevertEffect(bool active);

        /// <summary>
        /// Feuert das Event, wenn es aufgerufen wird.
        /// </summary>
        protected void onEffectFinished()
        {
            if(OnEffectFinished != null)
                OnEffectFinished(this, null);
        }
        // TODO: Effekt Finished für die anderen Effekte auch implementieren
    }
}
