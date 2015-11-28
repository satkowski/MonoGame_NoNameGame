using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Managers;
using NoNameGame.Scenes.Managers;
using NoNameGame.Scenes;

namespace NoNameGame.Screens
{
    /// <summary>
    /// Stellt einen Spielbildschirm (in dem das Spiel stattfindet) dar.
    /// </summary>
    public class GameplayScreen : Screen
    {
        ZoomingManager zoomingManager;
        CameraManager cameraManager;
        CollisionManager collisionManager;

        Scene Scene;

        public GameplayScreen ()
        {
            zoomingManager = new ZoomingManager();
            collisionManager = new CollisionManager();
            cameraManager = new CameraManager();
        }

        public override void LoadContent ()
        {
            base.LoadContent();

            XmlManager<Scene> sceneLoader = new XmlManager<Scene>();
            Scene = sceneLoader.Load("Load/Scenes/Scene_001.xml");
            Scene.LoadContent();

            zoomingManager.LoadContent(ref Scene);
            zoomingManager.Type = ZoomingManager.ZoomingType.OneTime;
            zoomingManager.MaxZoom = 3.0f;
            zoomingManager.MinZoom = -0.85f;
            zoomingManager.ZoomingFactor = 0.001f;
            collisionManager.LoadContent(ref Scene);
            cameraManager.LoadContent(ref Scene);
        }

        public override void UnloadContent ()
        {
            Scene.UnloadContent();
            zoomingManager.UnloadContent();
            collisionManager.UnloadContent();
            cameraManager.UnloadContent();
        }

        public override void Update (GameTime gameTime)
        {
            Scene.Update(gameTime);
            zoomingManager.Update(gameTime);
            collisionManager.Update(gameTime);
            cameraManager.Update(gameTime);
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            Scene.Draw(spriteBatch);
        }
    }
}
