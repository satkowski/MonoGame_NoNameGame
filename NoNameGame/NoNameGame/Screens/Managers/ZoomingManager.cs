using System.Collections.Generic;

using Microsoft.Xna.Framework;

using NoNameGame.Maps;
using NoNameGame.Entities;

namespace NoNameGame.Screens.Managers
{
    public class ZoomingManager
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

        Map map;
        List<Entity> entities;
        List<float> originalMapScales;
        List<float> originalEntityScales;
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
            originalMapScales = new List<float>();
            entities = new List<Entity>();
            originalEntityScales = new List<float>();
            currentZoom = 0.0f;
            MinZoom = 0.0f;
            MaxZoom = 0.0f;
            currentZoom = 0.0f;
            ZoomingFactor = 0.0f;
            Type = ZoomingType.None;
            Direction = ZoomingDirection.In;
        }

        public void LoadContent(ref Map map, params Entity[] entities)
        {
            this.map = map;
            this.entities.AddRange(entities);

            foreach(Layer layer in map.Layers)
                originalMapScales.Add(layer.TileSheet.Scale);
            foreach(Entity entity in entities)
                originalEntityScales.Add(entity.Image.Scale);
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            if(IsActive && Type != ZoomingType.None)
            {
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
                    map.Layers[c].Scale = originalMapScales[c] * (1 + currentZoom);
                for(int c = 0; c < entities.Count; c++)
                {
                    entities[c].Image.Scale = originalEntityScales[c] * (1 + currentZoom);
                }

                if(Type == ZoomingType.OneTime)
                    IsActive = false;
            }
        }
    }
}
