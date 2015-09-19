using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace NoNameGame.Maps.Effects
{
    public class ZoomingEffect : MapEffect
    {
        List<float> originalScales;
        float currentZoom;

        public float MinZoom;
        public float MaxZoom;
        public float ZoomingFactor;

        public ZoomingEffect()
        {
            originalScales = new List<float>();
            currentZoom = 0.0f;
            MinZoom = 0.0f;
            MaxZoom = 0.0f;
            currentZoom = 0.0f;
            ZoomingFactor = 0.0f;
        }

        public override void LoadContent(ref Map map)
        {
            base.LoadContent(ref map);

            foreach(Layer layer in map.Layers)
                originalScales.Add(layer.TileSheet.Scale);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(IsActive)
            {
                for(int c = 0; c < map.Layers.Count; c++)
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
                    map.Layers[c].Scale = originalScales[c] * currentZoom;
                }
            }
        }
    }
}
