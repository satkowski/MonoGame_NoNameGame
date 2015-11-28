using System;

using System.IO;
using System.Xml.Serialization;

namespace NoNameGame.Managers
{
    /// <summary>
    /// Eine Klasse, welche das Auslesen und Schreiben einer XML-Datei handhanbt.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlManager<T>
    {
        /// <summary>
        /// Der Typ des Objekts, welches geladen werden soll.
        /// </summary>
        public Type Type;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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

        /// <summary>
        /// Speichert ein Objekt.
        /// </summary>
        /// <param name="path">Pfad in welchen geschrieben werden soll</param>
        /// <param name="obj">das Objekt, was gespeichert werden soll</param>
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
