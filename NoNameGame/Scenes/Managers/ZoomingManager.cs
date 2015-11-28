using Microsoft.Xna.Framework;
using NoNameGame.Managers;
using Microsoft.Xna.Framework.Input;
using NoNameGame.Maps;
using NoNameGame.Entities;

namespace NoNameGame.Scenes.Managers
{

    /// <summary>
    /// Eine Klasse, welche das Zoomen in eine Szene handhabt.
    /// </summary>
    public class ZoomingManager : SceneManager
    {
        public enum ZoomingType
        {
            None,
            OneTime,
            Allways
        }

        public enum ZoomingDirection
        {
            In = 1,
            Out = -1
        }
        
        float currentZoom;

        public bool IsActive;
        public float MinZoom;
        public float MaxZoom;
        public float ZoomingFactor;
        public ZoomingType Type;
        public ZoomingDirection Direction;

        public ZoomingManager()
        {
            IsActive = false;
            currentZoom = 0.0f;
            MinZoom = 0.0f;
            MaxZoom = 0.0f;
            currentZoom = 0.0f;
            ZoomingFactor = 0.0f;
            Type = ZoomingType.None;
            Direction = ZoomingDirection.In;
        }

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
            if(InputManager.Instance.KeyDown(Keys.OemPlus))
            {
                Direction = ZoomingDirection.In;
                IsActive = true;
            }
            else if(InputManager.Instance.KeyDown(Keys.OemMinus))
            {
                Direction = ZoomingDirection.Out;
                IsActive = true;
            }

            if(IsActive && Type != ZoomingType.None)
            {
                float oldZoom = currentZoom;
                currentZoom += (float)gameTime.ElapsedGameTime.TotalMilliseconds * (int)Direction * ZoomingFactor;

                if(currentZoom < MinZoom)
                {
                    currentZoom = MinZoom;
                    IsActive = false;
                }
                else if(currentZoom > MaxZoom)
                {
                    currentZoom = MaxZoom;
                    IsActive = false;
                }

                // Anpassung aller Variablen, die durch den Zoom verändert werden
                foreach(Layer layer in scene.Map.Layers)
                    layer.Scale *= (1 + currentZoom) / (1 + oldZoom);
                foreach(Entity entity in scene.Entities)
                {
                    entity.Image.Scale *= (1 + currentZoom) / (1 + oldZoom);
                    entity.Body.Position *= (1 + currentZoom) / (1 + oldZoom);
                    entity.Body.SpeedFactor *= (1 + currentZoom) / (1 + oldZoom);
                }

                if(Type == ZoomingType.OneTime)
                    IsActive = false;
            }
            base.Update(gameTime);
        }
    }
}
