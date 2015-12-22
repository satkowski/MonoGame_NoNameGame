namespace NoNameGame.Managers
{
    /// <summary>
    /// Handhabt alle IDs die im Laufe des Programmes erzeugt werden.
    /// </summary>
    public class IDManager
    {

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
        private static IDManager instance;

        /// <summary>
        /// Gibt eine ID für eine Entity zurück.
        /// </summary>
        public ulong EntityID
        { get { return entityID++; } }
        private ulong entityID;
        /// <summary>
        /// Gibt eine ID für ein Tile zurück.
        /// </summary>
        public ulong TileID
        { get { return tileID++; } }
        private ulong tileID;

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
