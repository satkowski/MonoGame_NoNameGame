using System.Collections.Generic;

using Microsoft.Xna.Framework;

using NoNameGame.Maps;
using NoNameGame.Entities;

namespace NoNameGame.Screens.Managers
{
    public class ZoomingManager
    {
        Map map;
        List<Entity> entities;
        List<float> originalMapScales;
        List<float> originalEntityScales;
        float currentZoom;

        public bool IsActive;
        public float MinZoom;
        public float MaxZoom;
        public float ZoomingFactor;

        public ZoomingManager()
        {
            IsActive = false;
            originalMapScales = new List<float>();
            currentZoom = 0.0f;
            MinZoom = 0.0f;
            MaxZoom = 0.0f;
            currentZoom = 0.0f;
            ZoomingFactor = 0.0f;
        }

        public void LoadContent(ref Map map, ref List<Entity> entities)
        {
            this.map = map;
            this.entities = entities;

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
            if(IsActive)
            {
                currentZoom += (float)gameTime.ElapsedGameTime.TotalMilliseconds * ZoomingFactor;
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
                    map.Layers[c].Scale = originalMapScales[c] * currentZoom;
                for(int c = 0; c < entities.Count; c++)
                    entities[c].Image.Scale = originalEntityScales[c] * currentZoom;
            }
        }
    }
}
