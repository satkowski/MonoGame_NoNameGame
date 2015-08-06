using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace NoNameGame.Maps
{
    public class TileMapString
    {
        [XmlElement("Row")]
        public List<string> Rows;
        public string Rotation90Tiles;
        public string Rotation180Tiles;
        public string Rotation270Tiles;

        public TileMapString ()
        {
            Rows = new List<string>();
        }
    }
}
