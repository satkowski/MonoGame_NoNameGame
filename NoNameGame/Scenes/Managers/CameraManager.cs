using Microsoft.Xna.Framework;
using NoNameGame.Managers;
using NoNameGame.Extensions;
using NoNameGame.Entities;
using NoNameGame.Maps;

namespace NoNameGame.Scenes.Managers
{
    public class CameraManager : SceneManager
    {
        public override void LoadContent(ref Scene scene)
        {
            base.LoadContent(ref scene);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 offset = (ScreenManager.Instance.Dimensions / 2 - scene.Players[0].Body.Position).RoundDownToIntVector2();
            foreach(Entity entity in scene.Entities)
                entity.Image.Offset = offset;
            foreach(Layer layer in scene.Map.Layers)
                layer.TileSheet.Offset = offset;


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

            base.Update(gameTime);
        }
    }
}
