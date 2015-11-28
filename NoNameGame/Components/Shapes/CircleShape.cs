using Microsoft.Xna.Framework;
using NoNameGame.Maps;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

namespace NoNameGame.Components.Shapes
{
    /// <summary>
    /// Stellt ein Kreis Shape dar.
    /// </summary>
    [XmlInclude(typeof(CircleShape))]
    public class CircleShape : Shape
    {
        /// <summary>
        /// Der Radius des Kreises.
        /// </summary>
        public float Radius;
        /// <summary>
        /// Der skalierte Radius.
        /// </summary>
        public float RadiusScaled
        {
            get { return Radius * Scale; }
        }
        /// <summary>
        /// Die Liste aller Eckpunkte einer Shape.
        /// </summary>
        public override List<Vector2> Vertices
        {
            get
            {
                return new List<Vector2>();
            }
        }

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public CircleShape()
        {
            Type = ShapeType.AABB;
        }

        /// <summary>
        /// Speziellerer Konstruktor. Wird genutzt zum klonen.
        /// </summary>
        /// <param name="position">die Position</param>
        /// <param name="radius">der Radius</param>
        /// <param name="scale">die Skalierung</param>
        protected CircleShape(Vector2 position, float radius, float scale = 1.0f)
            : base(position, scale)
        {
            Type = ShapeType.AABB;
            Radius = radius;
            this.scale = scale;
        }

        /// <summary>
        /// Lädt die Standarddaten in das Shape. Mit Body als Grundlage.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="radius">der Radius</param>
        /// <param name="scale">die Skalierung</param>
        public void LoadContent(Body body, float radius, float scale)
        {
            Radius = radius;
            this.scale = scale;
            base.LoadContent(body);
        }

        /// <summary>
        /// Lädt die Standarddaten in das Shape. Mit Tile als Grundlage.
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="radius">der Radius</param>
        /// <param name="scale">die Skalierung</param>
        public void LoadContent(Tile tile, float radius, float scale)
        {
            Radius = radius;
            this.scale = scale;
            base.LoadContent(tile);
        }

        /// <summary>
        /// Methode, welche darauf reagiert, wenn sich die Skalierung verändert.
        /// </summary>
        /// <param name="newScale">die neue Skalierung</param>
        protected override void OnScaleChange(float newScale)
        {
            Radius /= Scale;
            Radius *= newScale;
        }

        /// <summary>
        /// Klont den Kreis auf dem es aufgerufen wird.
        /// </summary>
        /// <returns>die Kope dieses Kreises</returns>
        public CircleShape Clone()
        {
            return new CircleShape(Position, Radius, Scale);
        }

        /// <summary>
        /// Klont den Kreis auf dem es aufgerufen wird mit einer neuen Position.
        /// </summary>
        /// <param name="newPosition">neue Position</param>
        /// <returns>die Kopie des Kreises mit neuer Position</returns>
        public CircleShape Clone(Vector2 newPosition)
        {
            return new CircleShape(newPosition, Radius, Scale);
        }
    }
}
