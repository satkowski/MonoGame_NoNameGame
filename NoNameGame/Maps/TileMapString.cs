using System.Collections.Generic;

using System.Xml.Serialization;

namespace NoNameGame.Maps
{
    /// <summary>
    /// Stellt eine String-Kodierung der Tiles in einem Layer dar.
    /// </summary>
    public class TileMapString
    {
        /// <summary>
        /// Alle Reihen des aktuellen Layers.
        /// </summary>
        [XmlElement("Row")]
        public List<string> Rows;
        /// <summary>
        /// Alle Tiles, die in diesem Layer um 90° gedreht werden sollen.
        /// </summary>
        public string Rotation90Tiles;
        /// <summary>
        /// Alle Tiles, die in diesem Layer um 180° gedreht werden sollen.
        /// </summary>
        public string Rotation180Tiles;
        /// <summary>
        /// Alle Tiles, die in diesem Layer um 270° gedreht werden sollen.
        /// </summary>
        public string Rotation270Tiles;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public TileMapString ()
        {
            Rows = new List<string>();
        }
    }
}