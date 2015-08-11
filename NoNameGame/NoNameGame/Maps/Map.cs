using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoNameGame.Maps
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layers;

        public Map ()
        {
            Layers = new List<Layer>();
        }

        public void LoadContent ()
        {
            foreach (Layer layer in Layers)
                layer.LoadContent();
        }

        public void UnloadContent ()
        {
            foreach (Layer layer in Layers)
                layer.UnloadContent();
        }

        public void Update (GameTime gameTime)
        {
            foreach (Layer layer in Layers)
                layer.Update(gameTime);
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
                collidingRectangles.AddRange(layer.GetCollidingTileRectangles(entityRectangle));

            return collidingRectangles;
        }
    }
}
