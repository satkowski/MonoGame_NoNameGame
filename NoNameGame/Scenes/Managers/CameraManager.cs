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

            base.Update(gameTime);
        }
    }
}
