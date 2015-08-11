using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using MapEditor.Managers;
using MapEditor.Images;

namespace MapEditor.Maps
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layers;

        public Map ()
        {
            Layers = new List<Layer>();
        }

        public void CreateNewLayer (ContentManager content, Vector2 tileDimension, Vector2 offset, int collisionLevel, string imagePaths)
        {
            Layer newLayer = new Layer();
            newLayer.TileDimensions = tileDimension;
            newLayer.Offset = offset;
            newLayer.CollisionLevel = collisionLevel;

            TileSheet tileSheet = new TileSheet();
            tileSheet.Path = imagePaths;
            newLayer.TileSheet = tileSheet;

            newLayer.Initialize(content);
            Layers.Add(newLayer);
        }

        public void Save (string filePath)
        {
            foreach (Layer layer in Layers)
                layer.Save();

            XmlManager<Map> mapXML = new XmlManager<Map>();
            mapXML.Save(filePath, this);
        }


        public void Initialize (ContentManager content)
        {
            foreach (Layer layer in Layers)
                layer.Initialize(content);
        }

        public void Draw (SpriteBatch spriteBatch, Vector2 windowPosition)
        {
            foreach (Layer layer in Layers)
                layer.Draw(spriteBatch, windowPosition);
        }
    }
}
