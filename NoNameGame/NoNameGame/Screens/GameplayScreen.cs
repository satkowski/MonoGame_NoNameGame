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
            Player.Update(gameTime, Map);
            Map.Update(gameTime);

            Player.Image.Offset = ScreenManager.Instance.Dimensions / 2 - Player.Image.Position;
            foreach (Layer layer in Map.Layers)
                layer.TileSheet.Offset = Player.Image.Offset;

            //Vector2 actualOffset = Player.Image.Offset;
            //Vector2 offsetChange = Vector2.Zero;
            //if (Player.Image.CurrentRectangle.InVerticalDirection(Map.CamMovingRectangle))
            //    offsetChange.Y -= Player.MoveVelocity.Y + Player.CollisionMovement.Y;
            //else
            //{
            //    if (Player.Image.CurrentRectangle.OverlapTop(Map.CamMovingRectangle))
            //        offsetChange.Y = -actualOffset.Y;
            //    else if (Player.Image.CurrentRectangle.OverlapBottom(Map.CamMovingRectangle))
            //        offsetChange.Y = ScreenManager.Instance.Dimensions.Y / 2 + actualOffset.Y;
            //}
            //if (Player.Image.CurrentRectangle.InHorizontalDirection(Map.CamMovingRectangle))
            //    offsetChange.X -= Player.MoveVelocity.X + Player.CollisionMovement.X;
            //else
            //{
            //    if (Player.Image.CurrentRectangle.OverlapLeft(Map.CamMovingRectangle))
            //        offsetChange.X = -actualOffset.X;
            //    else if (Player.Image.CurrentRectangle.OverlapRight(Map.CamMovingRectangle))
            //        offsetChange.X = ScreenManager.Instance.Dimensions.X / 2 + actualOffset.X;
            //}

            //if (offsetChange != Vector2.Zero)
            //{
            //    foreach (Layer layer in Map.Layers)
            //        layer.TileSheet.Offset += offsetChange;
            //    Player.Image.Offset += offsetChange;
            //}
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }
    }
}
