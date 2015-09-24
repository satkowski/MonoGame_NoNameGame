using System;

using System.IO;
using System.Xml.Serialization;

namespace NoNameGame.Managers
{
    public class XmlManager<T>
    {
        public Type Type;

        public XmlManager ()
        {
            Type = typeof(T);
        }

        /// <summary>
        /// Lädt ein Object vom Typ "T" aus dem Pfad.
        /// </summary>
        /// <param name="path">Pfad zur XML zum Laden</param>
        /// <returns>Eine Instanz der Klasse T</returns>
        public T Load (string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }

        public void Save (string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
