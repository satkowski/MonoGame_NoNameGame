using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Managers
{
    /// <summary>
    /// Handhabt alle IDs die im Laufe des Programmes erzeugt werden.
    /// </summary>
    public class IDManager
    {
        private static IDManager instance;
        private ulong entityID;
        private ulong tileID;

        /// <summary>
        /// Die aktuelle Instanz des InputManagers.
        /// </summary>
        public static IDManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new IDManager();
                return instance;
            }
        }

        /// <summary>
        /// Gibt eine ID für eine Entity zurück.
        /// </summary>
        public ulong EntityID
        { get { return entityID++; } }
        /// <summary>
        /// Gibt eine ID für ein Tile zurück.
        /// </summary>
        public ulong TileID
        { get { return tileID++; } }

        /// <summary>
        /// Konstruktor zur Erstellung einer Singletoninstanz.
        /// </summary>
        private IDManager()
        {
            entityID = 0;
            tileID = 0;
        }
    }
}
