using System.Collections.Generic;

using Microsoft.Xna.Framework;

using NoNameGame.Maps;
using NoNameGame.Entities;

namespace NoNameGame.Screens.Managers
{
    public class ZoomingManager : GameplayScreenManager
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

        public override void LoadContent(ref Map map, params Entity[] entities)
        {
            base.LoadContent(ref map, entities);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
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

                for(int c = 0; c < map.Layers.Count; c++)
                    map.Layers[c].Scale *= (1 + currentZoom) / (1 + oldZoom);
                for(int c = 0; c < entities.Count; c++)
                {
                    entities[c].Image.Scale *= (1 + currentZoom) / (1 + oldZoom);
                    entities[c].Image.Position *= (1 + currentZoom) / (1 + oldZoom);
                    entities[c].MoveSpeedFactor *= (1 + currentZoom) / (1 + oldZoom);
                }

                if(Type == ZoomingType.OneTime)
                    IsActive = false;
            }
            base.Update(gameTime);
        }

        public override void AddEntity(Entity entity)
        {
            //TODO: Zooming auf die neue Entity anwenden
            base.AddEntity(entity);
        }

        public override void RemoveEntity(Entity entity)
        {
            //TODO: Zooming vom entfernten Entity herunternehmen
            base.RemoveEntity(entity);
        }
    }
}
