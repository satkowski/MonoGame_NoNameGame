
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using NoNameGame.Maps;
using NoNameGame.Managers;
using NoNameGame.Entities;
using NoNameGame.Screens.Managers;
using NoNameGame.Extensions;

namespace NoNameGame.Screens
{
    public class GameplayScreen : Screen
    {
        ZoomingManager zoomingManager;

        public Map Map;
        public UserControlledEntity Player;

        public GameplayScreen ()
        {
            zoomingManager = new ZoomingManager();
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

            zoomingManager.LoadContent(ref Map, Player);


            zoomingManager.Type = ZoomingManager.ZoomingType.OneTime;
            zoomingManager.MaxZoom = 3.0f;
            zoomingManager.MinZoom = -0.85f;
            zoomingManager.ZoomingFactor = 0.001f;
        }

        public override void UnloadContent ()
        {
            Map.UnloadContent();
            Player.UnloadContent();
            zoomingManager.UnloadContent();
        }

        public override void Update (GameTime gameTime)
        {
            if(InputManager.Instance.KeyDown(Keys.OemPlus)) 
            {
                zoomingManager.Direction = ZoomingManager.ZoomingDirection.In;
                zoomingManager.IsActive = true;
            }
            else if(InputManager.Instance.KeyDown(Keys.OemMinus))
            {
                zoomingManager.Direction = ZoomingManager.ZoomingDirection.Out;
                zoomingManager.IsActive = true;
            }
            zoomingManager.Update(gameTime);

            Player.Update(gameTime, Map);
            Map.Update(gameTime);

            //Vector2 offset = ScreenManager.Instance.Dimensions / 2 - Player.Image.ZoomedPosition;
            //Player.Image.Offset = offset.ConvertToIntVector2();
            //foreach (Layer layer in Map.Layers)
            //    layer.TileSheet.Offset = Player.Image.Offset;

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
