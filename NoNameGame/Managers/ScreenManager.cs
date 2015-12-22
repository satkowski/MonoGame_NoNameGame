
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using NoNameGame.Screens;

namespace NoNameGame.Managers
{
    /// <summary>
    /// Eine Klasse, welches alles handhabt, was mit den verschiedenen Bildschirmen zutun hat.
    /// </summary>
    class ScreenManager
    {
        /// <summary>
        /// Gibt den aktuell angezeigten Bildschirm an.
        /// </summary>
        Screen currentScreen;

        /// <summary>
        /// Das GraphicDevice des Spieles.
        /// </summary>
        public GraphicsDevice GraphicsDevice;
        /// <summary>
        /// Der SpriteBatch des Spieles, auf welchem gemalt wird.
        /// </summary>
        public SpriteBatch SpriteBatch;
        /// <summary>
        /// Der ContentManager des Spieles.
        /// </summary>
        public ContentManager Content
        { get; private set; }

        /// <summary>
        /// Die Dimension, welche der Bildschirm gerade hat.
        /// </summary>
        public Vector2 Dimensions
        { get; private set; }
        /// <summary>
        /// Die aktuelle Instanz des ScreenManagers.
        /// </summary>
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }
        private static ScreenManager instance;

        /// <summary>
        /// Konstruktor zur Erstellung einer Singletoninstanz.
        /// </summary>
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
