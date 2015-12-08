using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using NoNameGame.Maps;
using System.Collections.Generic;
using System;

namespace NoNameGame.Components.Shapes
{
    /// <summary>
    /// Stellt ein AABB (Axis Alignment Bounding Box) dar.
    /// </summary>
    [XmlInclude(typeof(AABBShape))]
    [XmlInclude(typeof(OBBShape))]
    public class AABBShape : Shape
    {
        private Vector2 size;

        /// <summary>
        /// Die Größe des AABB.
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set
            {
                size = value;
                onThingsForAreaChanged();
            }
        }
        /// <summary>
        /// Der Mittelpunkt des AABB vom Koordinatenursprung aus.
        /// </summary>
        public Vector2 Center
        { get { return Size / 2; } }
        /// <summary>
        /// Die linke Seite des AABB.
        /// </summary>
        public float Left
        { get { return -Center.X; } }
        /// <summary>
        /// Die linke Seite des AABB von der Position aus.
        /// </summary>
        public float LeftFromPosition
        { get { return position.X - Left; } }
        /// <summary>
        /// Die obere Seite des AABB.
        /// </summary>
        public float Top
        { get { return -Center.Y; } }
        /// <summary>
        /// Die obere Seite des AABB von der Position aus.
        /// </summary>
        public float TopFromPosition
        { get { return position.Y - Top; } }
        /// <summary>
        /// Die rechte Seite des AABB.
        /// </summary>
        public float Right
        { get { return Center.X; } }
        /// <summary>
        /// Die rechte Seite des AABB von der Position aus.
        /// </summary>
        public float RightFromPosition
        { get { return position.X + Right; } }
        /// <summary>
        /// Die untere Seite des AABB.
        /// </summary>
        public float Bottom
        { get { return Center.Y; } }
        /// <summary>
        /// Die untere Seite des AABB von der Position aus.
        /// </summary>
        public float BottomFromPosition
        { get { return position.Y + Bottom; } }
        /// <summary>
        /// Die Liste aller Eckpunkte einer Shape.
        /// </summary>
        public override List<Vector2> Vertices
        {   get
            {
                List<Vector2> newVertices = new List<Vector2>();
                newVertices.Add(Position + new Vector2(Left, Top));
                newVertices.Add(Position + new Vector2(Right, Top));
                newVertices.Add(Position + new Vector2(Right, Bottom));
                newVertices.Add(Position + new Vector2(Left, Bottom));

                return newVertices;
            }
        }
        /// <summary>
        /// Gibt die Fläche der Form zurück.
        /// </summary>
        public override float Area
        {
            get
            {
                float area = Size.X * Size.Y * Scale;
                if(area == 0)
                    area = 0.000000000001f;
                return area;
            }
        }

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public AABBShape()
        {
            Type = ShapeType.AABB;
        }

        /// <summary>
        /// Speziellerer Konstruktor. Wird genutzt zum klonen.
        /// </summary>
        /// <param name="position">die Position</param>
        /// <param name="size">die Größe</param>
        /// <param name="scale">die Skalierung</param>
        protected AABBShape(Vector2 position, Vector2 size, float scale = 1.0f)
            : base(position, scale)
        {
            Type = ShapeType.AABB;
            Size = size;
        }

        /// <summary>
        /// Lädt die Standarddaten in das Shape. Mit Body als Grundlage.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="width">die Breite</param>
        /// <param name="height">die Höhe</param>
        /// <param name="scale">die Skalierung</param>
        public void LoadContent(Body body, float width, float height, float scale)
        {
            Size = new Vector2(width, height);
            this.scale = scale;
            base.LoadContent(body);
        }
        /// <summary>
        /// Lädt die Standarddaten in das Shape. Mit Tile als Grundlage.
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="width">die Breite</param>
        /// <param name="height">die Höhe</param>
        /// <param name="scale">die Skalierung</param>
        public void LoadContent(Tile tile, float width, float height, float scale)
        {
            Size = new Vector2(width, height);
            this.scale = scale;
            base.LoadContent(tile);
        }

        /// <summary>
        ///  Methode, welche darauf reagiert, wenn sich die Skalierung verändert.
        /// </summary>
        /// <param name="newScale">die neue Skalierung</param>
        protected override void onScaleChange(float newScale)
        {
            Size /= Scale;
            Size *= newScale;
        }

        /// <summary>
        /// Klont das AABB auf dem es aufgerufen wird.
        /// </summary>
        /// <returns>die Kopie dieses AABB</returns>
        public AABBShape Clone()
        {
            return new AABBShape(Position, Size, Scale);
        }
    }
}
