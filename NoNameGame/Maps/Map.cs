﻿using System.Collections.Generic;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Managers;

namespace NoNameGame.Maps
{
    /// <summary>
    /// Stellt eine ganze Map dar.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Die Ebenen der Map.
        /// </summary>
        [XmlElement("Layer")]
        public List<Layer> Layers;

        /// <summary>
        /// Das Rechteck, in welchem sich die Kamere bewegn darf.
        /// </summary>
        [XmlIgnore]
        public Rectangle CamMovingRectangle { private set; get; }

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Map ()
        {
            Layers = new List<Layer>();

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
                layer.Update(gameTime);
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (Layer layer in Layers)
                layer.Draw(spriteBatch);
        }
    }
}
