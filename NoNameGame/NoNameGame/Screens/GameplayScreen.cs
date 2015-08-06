using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Maps;
using NoNameGame.Managers;

namespace NoNameGame.Screens
{
    public class GameplayScreen : Screen
    {
        public Map Map;

        public GameplayScreen ()
        {
        }

        public override void LoadContent ()
        {
            base.LoadContent();

            XmlManager<Map> mapLoader = new XmlManager<Map>();
            Map = mapLoader.Load("Load/Maps/Map_001.xml");
            Map.LoadContent();
        }

        public override void UnloadContent ()
        {
            Map.UnloadContent();
        }

        public override void Update (GameTime gameTime)
        {
            Map.Update(gameTime);
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
        }
    }
}
