using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoNameGame.Managers;
using NoNameGame.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NoNameGame.Screens
{
    /// <summary>
    /// Eine Klasse, welche alle Aktionen (z.B. Übergange in andere Menüs) handhabt.
    /// </summary>
    [XmlInclude(typeof(MenuScreen))]
    public class MenuScreen : Screen
    {
        /// <summary>
        /// Ein Stack aller Menüs, die vor dem jetzigen aufgerufen wurden.
        /// </summary>
        Stack<Menu> lastMenus;
        /// <summary>
        /// Das jetzige Menü.
        /// </summary>
        Menu currentMenu;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public MenuScreen()
        {
            lastMenus = new Stack<Menu>();
        }
        
        public override void LoadContent()
        {
            base.LoadContent();

            XmlManager<Menu> menuLoader = new XmlManager<Menu>();
            currentMenu = menuLoader.Load(Path);
            currentMenu.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            foreach(Menu menu in lastMenus)
                menu.UnloadContent();
            currentMenu.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentMenu.Update(gameTime);

            if(InputManager.Instance.KeyPressed(Keys.Enter, Keys.Space) && !ScreenManager.Instance.ScreenChanged)
            {
                // Entscheidung was bei einzelnen Link Typen passiert.
                switch(currentMenu.MenuItems[currentMenu.CurrentItem].LinkType)
                {
                    case MenuItem.MenuItemLinkType.Screen:
                        ScreenManager.Instance.ChangeScreen(currentMenu.MenuItems[currentMenu.CurrentItem].LinkSource);
                        break;
                    case MenuItem.MenuItemLinkType.Menu:
                        // Speichere das alte Menu ab.
                        if(currentMenu.MenuItems[currentMenu.CurrentItem].LinkSource != String.Empty)
                        {
                            //currentMenu.MenuItems[currentMenu.CurrentItem].Image.ScalingEffect.FinishScaling();
                            lastMenus.Push(currentMenu);
                            XmlManager<Menu> menuLoad = new XmlManager<Menu>();
                            currentMenu = menuLoad.Load(currentMenu.MenuItems[currentMenu.CurrentItem].LinkSource);
                            currentMenu.LoadContent();
                        }
                        else
                            returnToLast();
                        break;
                    case MenuItem.MenuItemLinkType.Quit:
                        Game1.ExitGame = true;
                        break;
                }
            }
            else if(InputManager.Instance.KeyPressed(Keys.Escape) && !ScreenManager.Instance.ScreenChanged)
                returnToLast();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            currentMenu.Draw(spriteBatch);
        }

        /// <summary>
        /// Nimm das alte Menu aus dem Stack oder beendet dieses Memü, wenn nichts mehr im Stack ist.
        /// </summary>
        private void returnToLast()
        {
            currentMenu.UnloadContent();
            if(lastMenus.Count == 0)
                ScreenManager.Instance.ChangeScreen();
            else
                currentMenu = lastMenus.Pop();
        }
    }
}
