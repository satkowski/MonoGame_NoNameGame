
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using NoNameGame.Managers;
using System.Xml.Serialization;
using System;

namespace NoNameGame.Screens
{
    /// <summary>
    /// Die abstrakte Grundklasse aller Bildschirme des Spiels.
    /// </summary>
    [XmlInclude(typeof(GameplayScreen))]
    [XmlInclude(typeof(MenuScreen))]
    public abstract class Screen
    {
        /// <summary>
        /// Der ContentManager des Spieles.
        /// </summary>
        protected ContentManager content;
        /// <summary>
        /// Der Pfad zu einem Inhalt der in diesem Screen geladen werden soll.
        /// </summary>
        public string Path;
        /// <summary>
        /// Ob dieser Screen, falls er im ScreenManager Stack steckt, noch sichtbar ist.
        /// </summary>
        public bool ViewableInStack;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Screen()
        {
            Path = String.Empty;
            ViewableInStack = true;
        }

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
