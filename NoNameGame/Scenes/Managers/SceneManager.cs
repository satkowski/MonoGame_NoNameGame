using Microsoft.Xna.Framework;

namespace NoNameGame.Scenes.Managers
{
    /// <summary>
    /// Die abstrakte Grundklasse, welche alle Screne Ereignisse managt.
    /// </summary>
    public abstract class SceneManager
    {
        /// <summary>
        /// Die Szene, auf welcher dieser Manager arbeiten soll.
        /// </summary>
        protected Scene scene;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public SceneManager()
        {
        }

        public virtual void LoadContent(ref Scene scene)
        {
            this.scene = scene;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
