
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using NoNameGame.Screens;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

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
        Stack<Screen> lastScreens;

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
            lastScreens = new Stack<Screen>();
        }

        public void LoadContent (ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");


            XmlManager<Screen> screenLoader = new XmlManager<Screen>();
            currentScreen = screenLoader.Load("Load/Screens/TitleMenuScreen.xml");
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
            foreach(Screen screen in lastScreens)
                if(screen.ViewableInStack)
                    screen.Draw(spriteBatch);

            currentScreen.Draw(spriteBatch);
        }

        /// <summary>
        /// Ändert den aktuelle Screen zu einem anderen. Ist der Pfad leer wird der letzte Screen vor dem jetzigen genutzt.
        /// </summary>
        /// <param name="path">der Pfad zum neuen Screen</param>
        public void ChangeScreen(string path = "")
        {
            // Wenn der Pfad leer ist, wird der letzte Screen genommen
            if(path == String.Empty && lastScreens.Count != 0)
            {
                currentScreen.UnloadContent();
                currentScreen = lastScreens.Pop();
            }
            else if(path != String.Empty)
            {
                // Ob der Bildschirm in den Stack kann.
                if(currentScreen.Stackable)
                    lastScreens.Push(currentScreen);
                else
                    currentScreen.UnloadContent();

                XmlManager<Screen> screenLoader = new XmlManager<Screen>();
                currentScreen = screenLoader.Load(path);
                currentScreen.LoadContent();

                // Ob der Bildschirm nur alleine existieren kann.
                if(currentScreen.SinglyScreen)
                {
                    foreach(Screen screen in lastScreens)
                        screen.UnloadContent();
                    lastScreens.Clear();
                }                    
            }
        }
    }
}
