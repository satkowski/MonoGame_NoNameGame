using System;
using NoNameGame.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoNameGame.Menus
{
    /// <summary>
    /// Stellt ein Item eines Menüs dar.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Ein Enum, welches angibt, wie sich das Item verhalten soll, wenn es ausgewählt wurde.
        /// Mit was ist es also verlinkt.
        /// </summary>
        public enum MenuItemLinkType
        {
            Screen,
            Menu,
            Quit
        }

        /// <summary>
        /// Das Bild, welches für diesen Menüpunkt angezeigt werden soll.
        /// </summary>
        public Image Image;
        /// <summary>
        /// Mit was dieses Memü verlinkt wurde.
        /// </summary>
        public MenuItemLinkType LinkType;
        /// <summary>
        /// Gibt an, wo sich die Verlinkung befindet.
        /// </summary>
        public string LinkSource;
        /// <summary>
        /// Die Position des Menü Items.
        /// </summary>
        public Vector2 Position;

        public MenuItem()
        {
            LinkType = MenuItemLinkType.Quit;
            LinkSource = String.Empty;
            Position = Vector2.Zero;
        }

        public void LoadContent()
        {
            Image.LoadContent();
        }

        public void UnloadConent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Image.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch, Position);
        }
    }
}
