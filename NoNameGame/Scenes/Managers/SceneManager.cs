using Microsoft.Xna.Framework;

namespace NoNameGame.Scenes.Managers
{
    public class SceneManager
    {
        protected Scene scene;

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
