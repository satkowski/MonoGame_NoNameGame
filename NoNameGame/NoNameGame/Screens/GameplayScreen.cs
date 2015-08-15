using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Maps;
using NoNameGame.Managers;
using NoNameGame.Entities;
using NoNameGame.Extensions;

namespace NoNameGame.Screens
{
    public class GameplayScreen : Screen
    {
        public Map Map;
        public UserControlledEntity Player;

        public GameplayScreen ()
        {
            Map = new Maps.Map();
            Player = new UserControlledEntity();
        }

        public override void LoadContent ()
        {
            base.LoadContent();

            XmlManager<Map> mapLoader = new XmlManager<Map>();
            Map = mapLoader.Load("Load/Maps/Map_004.xml");
            Map.LoadContent();

            XmlManager<UserControlledEntity> playerLoader = new XmlManager<UserControlledEntity>();
            Player = playerLoader.Load("Load/Entities/Players/Player_01.xml");
            Player.LoadContent();
        }

        public override void UnloadContent ()
        {
            Map.UnloadContent();
            Player.UnloadContent();
        }

        public override void Update (GameTime gameTime)
        {
            Map.Update(gameTime);
            Player.Update(gameTime, Map);

            if (Player.Image.CurrentRectangle.In(Map.CamMovingRectangle))
            {
                Vector2 offsetChange = new Vector2(Player.Image.CurrentRectangle.X - Player.Image.PrevRectangle.X,
                                                   Player.Image.CurrentRectangle.Y - Player.Image.PrevRectangle.Y);
                Map.Offset += offsetChange;
                Player.Offset += offsetChange;
            }
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }
    }
}
