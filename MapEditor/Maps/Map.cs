using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using MapEditor.Managers;

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

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (Layer layer in Layers)
                layer.Draw(spriteBatch);
        }
    }
}
