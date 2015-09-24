using System.Collections.Generic;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Managers;

namespace NoNameGame.Maps
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layers;
        [XmlIgnore]
        public Vector2 Position;

        [XmlIgnore]
        public Rectangle CamMovingRectangle { private set; get; }

        public Map ()
        {
            Layers = new List<Layer>();
            Position = Vector2.Zero;

            CamMovingRectangle = Rectangle.Empty;
        }

        public void LoadContent ()
        {
            Vector2 maxLayerSize = Vector2.Zero;
            foreach (Layer layer in Layers)
            {
                layer.LoadContent();
                maxLayerSize.X = maxLayerSize.X < layer.Size.X ? layer.Size.X : maxLayerSize.X;
                maxLayerSize.Y = maxLayerSize.Y < layer.Size.Y ? layer.Size.Y : maxLayerSize.Y;
            }

            CamMovingRectangle = new Rectangle((int)(ScreenManager.Instance.Dimensions.X / 4),
                                               (int)(ScreenManager.Instance.Dimensions.Y / 4),
                                               (int)(maxLayerSize.X - ScreenManager.Instance.Dimensions.X / 2),
                                               (int)(maxLayerSize.Y - ScreenManager.Instance.Dimensions.Y / 2));
        }

        public void UnloadContent ()
        {
            foreach (Layer layer in Layers)
                layer.UnloadContent();
        }

        public void Update (GameTime gameTime)
        {
            foreach (Layer layer in Layers)
                layer.Update(gameTime, Position);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (Layer layer in Layers)
                layer.Draw(spriteBatch);
        }

        public List<Rectangle> GetCollidingTileRectangles (Rectangle entityRectangle, int entityLevel)
        {
            List<Rectangle> collidingRectangles = new List<Rectangle>();
            foreach (Layer layer in Layers)
                if(layer.CollisionLevel == entityLevel)
                    collidingRectangles.AddRange(layer.GetCollidingTileRectangles(entityRectangle));

            return collidingRectangles;
        }
    }
}
