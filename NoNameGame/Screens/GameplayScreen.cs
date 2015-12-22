using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Managers;
using NoNameGame.Scenes.Managers;
using NoNameGame.Scenes;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Input;

namespace NoNameGame.Screens
{
    /// <summary>
    /// Stellt einen Spielbildschirm (in dem das Spiel stattfindet) dar.
    /// </summary>
    [XmlInclude(typeof(GameplayScreen))]
    public class GameplayScreen : Screen
    {
        /// <summary>
        /// Der Zoomingmanager der aktuelle Spielbildschirms.
        /// </summary>
        ZoomingManager zoomingManager;
        /// <summary>
        /// Der CameraManager der aktuelle Spielbildschirms.
        /// </summary>
        CameraManager cameraManager;
        /// <summary>
        /// Der CollisionManager der aktuelle Spielbildschirms.
        /// </summary>
        CollisionManager collisionManager;

        /// <summary>
        /// Die Szene, die gerade gespielt wird.
        /// </summary>
        Scene Scene;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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
            Scene = sceneLoader.Load(Path);
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

            if(InputManager.Instance.KeyPressed(Keys.Escape))
                ScreenManager.Instance.ChangeScreen("Load/Screens/GameMenuScreen.xml");
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            Scene.Draw(spriteBatch);
        }
    }
}
