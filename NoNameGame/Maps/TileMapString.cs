﻿using System.Collections.Generic;

using System.Xml.Serialization;

namespace NoNameGame.Maps
{
    /// <summary>
    /// Stellt eine String-Kodierung der Tiles in einem Layer dar.
    /// </summary>
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
