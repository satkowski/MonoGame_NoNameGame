
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using NoNameGame.Managers;

namespace NoNameGame.Screens
{
    public class Screen
    {
        protected ContentManager content;

        public virtual void LoadContent ()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent ()
        {

        }

        public virtual void Update (GameTime gameTime)
        {

        }

        public virtual void Draw (SpriteBatch spriteBatch)
        {

        }
    }
}
