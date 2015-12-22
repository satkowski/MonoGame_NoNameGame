using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoNameGame.Images.Effects;
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
        /// Gibt die Ausrichtung der Menü Items wieder.
        /// </summary>
        public enum MenuAlignment
        {
            Left_Top,
            Right_Bottom,
            Center
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
        /// Der Name des Effektes der auf den Menü Items genutzt wird.
        /// </summary>
        string effektName;

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
        /// Gib die Ausrichtung der Items an.
        /// </summary>
        public MenuAlignment Alignment;
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
        /// Der ImageEffect der auf den Menü Items benutzt werden soll, wenn diese ausgwählt werden.
        /// </summary>
        public ImageEffect MenuItemImageEffect;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Menu()
        {
            menuItemPositions = new List<Vector2>();
            CurrentItem = 0;
            MenuItems = new List<MenuItem>();
            Axis = MenuAxis.Y;
            Alignment = MenuAlignment.Center;
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

            effektName = MenuItemImageEffect.GetType().ToString().Replace("NoNameGame.Images.Effects.", "");
            // Die einzelnen Menü Items laden.
            foreach(MenuItem menuItem in MenuItems)
            {
                menuItem.LoadContent();
                menuItem.Image.OnScaleChange += Image_OnScaleChange;
                menuItem.Image.AddEffecte(MenuItemImageEffect.Copy());
            }
            MenuItems[CurrentItem].Image.ActivateEffect(effektName);

            alignMenuItems();
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
                MenuItems[lastItem].Image.DeactivateEffect(effektName);
                MenuItems[CurrentItem].Image.ActivateEffect(effektName);
            }

            foreach(MenuItem menuItem in MenuItems)
                menuItem.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(MenuItem menuItem in MenuItems)
                menuItem.Draw(spriteBatch);
        }

        /// <summary>
        /// Ordnet alle Menü Items anhand der Position des Menüs, dem Offset und der Achsenrichtung an.
        /// </summary>
        void alignMenuItems()
        {
            // Berechne die Größe des Menüs in Richtung der Mneübewegung.
            Vector2 size = new Vector2(-ItemOffset);
            foreach(MenuItem menuItem in MenuItems)
            {
                if(Axis == MenuAxis.X)
                {
                    size.X += menuItem.Image.SourceRectangle.Width + ItemOffset;
                    size.Y = Math.Max(size.Y, menuItem.Image.SourceRectangle.Height);
                }
                else if(Axis == MenuAxis.Y)
                {
                    size.X = Math.Max(size.X, menuItem.Image.SourceRectangle.Width);
                    size.Y += menuItem.Image.SourceRectangle.Height + ItemOffset;
                }
            }
            // Berechnung der Position des ersten Items.
            Vector2 position = Position;
            if(Axis == MenuAxis.X)
                position.X -= size.X / 2;
            else if(Axis == MenuAxis.Y)
                position.Y -= size.Y / 2;

            // Anordnung der Items anhand der ausgerechneten Dimension.
            foreach(MenuItem menuItem in MenuItems)
            {
                Vector2 newPosition = position;
                // Die Richtung in dem ein Bild veschoben muss um richtig Ausgerichtet zu sein.
                float offset = 0;
                if(Alignment == MenuAlignment.Left_Top)
                    offset = -0.5f;
                else if(Alignment == MenuAlignment.Right_Bottom)
                    offset = 0.5f;

                if(offset != 0)
                {
                    if(Axis == MenuAxis.X)
                        newPosition.Y += offset * (size.Y - menuItem.Image.SourceRectangle.Height);
                    else if(Axis == MenuAxis.Y)
                        newPosition.X += offset * (size.X - menuItem.Image.SourceRectangle.Width);
                }
                menuItem.Position = newPosition;
                // Verschieben der Position.
                if(Axis == MenuAxis.X)
                    position.X += menuItem.Image.SourceRectangle.Width + ItemOffset;
                else if(Axis == MenuAxis.Y)
                    position.Y += menuItem.Image.SourceRectangle.Height + ItemOffset;
            }
        }
        
        private void Image_OnScaleChange(object sender, EventArgs e)
        {

        }
    }
}
