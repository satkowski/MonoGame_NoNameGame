using Microsoft.Xna.Framework;
using NoNameGame.Managers;
using Microsoft.Xna.Framework.Input;

namespace NoNameGame.Scenes.Managers
{
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

                for(int c = 0; c < scene.Map.Layers.Count; c++)
                    scene.Map.Layers[c].Scale *= (1 + currentZoom) / (1 + oldZoom);
                for(int c = 0; c < scene.Entities.Count; c++)
                {
                    scene.Entities[c].Image.Scale *= (1 + currentZoom) / (1 + oldZoom);
                    scene.Entities[c].Image.Position *= (1 + currentZoom) / (1 + oldZoom);
                    scene.Entities[c].MoveSpeedFactor *= (1 + currentZoom) / (1 + oldZoom);
                }

                if(Type == ZoomingType.OneTime)
                    IsActive = false;
            }
            base.Update(gameTime);
        }
    }
}
