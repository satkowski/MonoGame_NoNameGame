using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using NoNameGame.Screens;

namespace NoNameGame.Managers
{
    class ScreenManager
    {
        private static ScreenManager instance;
        Screen currentScreen;
        Screen nextScreen;

        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        public ContentManager Content { private set; get; }
        public Vector2 Dimensions { private set; get; }
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        private ScreenManager ()
        {
            Dimensions = new Vector2(640, 480);
            currentScreen = new GameplayScreen();
        }

        public void LoadContent (ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public void UnloadContent ()
        {
            Content.Unload();
            currentScreen.UnloadContent();
        }

        public void Update (GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
    }
}
