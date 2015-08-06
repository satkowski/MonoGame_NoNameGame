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

        public TileMapString ()
        {
            Rows = new List<string>();
        }
    }
}
