using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoNameGame.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NoNameGame.Menus
{
    /// <summary>
    /// Stellt ein Menü dar.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// In welcher Richtung sollen sich die Menü Items als erstes Verteilen.
        /// </summary>
        public enum MenuAxis
        {
            X,
            Y
        }

        /// <summary>
        /// Die Positionen aller Items.
        /// </summary>
        List<Vector2> menuItemPositions;
        /// <summary>
        /// Alle Tasten die im Menü zurückgehen.
        /// </summary>
        Keys[] keysBackwards;
        /// <summary>
        /// Alle Tasten die im Menü vorwärtsgehen.
        /// </summary>
        Keys[] keysForwards;

        /// <summary>
        /// Liste aller Items, die dieses Menu beinhaltet.
        /// </summary>
        [XmlElement("MenuItem")]
        public List<MenuItem> MenuItems;
        /// <summary>
        /// Gibt an, in welche Richtung die Elemente angeordnet werden sollen.
        /// </summary>
        public MenuAxis Axis;
        /// <summary>
        /// Der Abstand zwischen 2 Items in diesem Menü.
        /// </summary>
        public int ItemOffset;
        /// <summary>
        /// Die Position an der das Menü erscheinen soll.
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Das gerade ausgewählte Item.
        /// </summary>
        public int CurrentItem;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Menu()
        {
            menuItemPositions = new List<Vector2>();
            CurrentItem = 0;
            MenuItems = new List<MenuItem>();
            Axis = MenuAxis.Y;
            ItemOffset = 0;
            Position = Vector2.Zero;
        }

        public void LoadContent()
        {
            // Entscheiden welche Tasten zurück- und welche vorwärtsgehen.
            if(Axis == MenuAxis.X)
            {
                keysForwards = new Keys[] { Keys.Right, Keys.D };
                keysBackwards = new Keys[] { Keys.Left, Keys.A };
            }
            else if(Axis == MenuAxis.Y)
            {
                keysForwards = new Keys[] { Keys.Down, Keys.S };
                keysBackwards = new Keys[] { Keys.Up, Keys.W };
            }

            foreach(MenuItem menuItem in MenuItems)
                menuItem.LoadContent();
            allignMenuItems();
        }

        public void UnloadContent()
        {
            foreach(MenuItem menuItem in MenuItems)
                menuItem.UnloadConent();
        }

        public void Update(GameTime gameTime)
        {
            int lastItem = CurrentItem;

            if(InputManager.Instance.KeyPressed(keysForwards))
                CurrentItem++;
            else if(InputManager.Instance.KeyPressed(keysBackwards))
                CurrentItem--;

            if(CurrentItem < 0)
                CurrentItem = MenuItems.Count - 1;
            else if(CurrentItem >= MenuItems.Count)
                CurrentItem = 0;

            if(lastItem != CurrentItem)
            {
                //Skalierung
            }

            for(int c = 0; c < MenuItems.Count; c++)
                if(lastItem == 0 || CurrentItem == 0)
                    MenuItems[c].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(MenuItem menuItem in MenuItems)
                menuItem.Draw(spriteBatch);
        }

        /// <summary>
        /// Ordnet alle Menü Items anhand der Position des Menüs, dem Offset und der Achsenrichtung an.
        /// </summary>
        void allignMenuItems()
        {
            // Berechne die Größe des Menüs in Richtung der Mneübewegung.
            int size = -ItemOffset;
            foreach(MenuItem menuItem in MenuItems)
            {
                if(Axis == MenuAxis.X)
                    size += menuItem.Image.SourceRectangle.Width + ItemOffset;
                else if(Axis == MenuAxis.Y)
                    size += menuItem.Image.SourceRectangle.Height + ItemOffset;
            }
            // Berechnung der Position des ersten Items.
            Vector2 position = Position;
            if(Axis == MenuAxis.X)
                position.X -= size / 2;
            else if(Axis == MenuAxis.Y)
                position.Y -= size / 2;

            // Anordnung der Items anhand der ausgerechneten Dimension.
            foreach(MenuItem menuItem in MenuItems)
            {
                menuItem.Position = position;
                if(Axis == MenuAxis.X)
                    position.X += menuItem.Image.SourceRectangle.Width + ItemOffset;
                else if(Axis == MenuAxis.Y)
                    position.Y += menuItem.Image.SourceRectangle.Height + ItemOffset;
            }
        }
    }
}
