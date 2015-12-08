using Microsoft.Xna.Framework;
using NoNameGame.Collision;
using NoNameGame.Maps;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    /// <summary>
    /// Eine abstrakte Klasse, welche die Basis für alle Formen darstellt, die in diesem Spiel vorkommen.
    /// </summary>
    [XmlInclude(typeof(AABBShape))]
    [XmlInclude(typeof(OBBShape))]
    [XmlInclude(typeof(CircleShape))]
    public abstract class Shape
    {
        protected Vector2 position;
        protected float scale;

        /// <summary>
        /// die absolute Position auf der Ebene
        /// </summary>
        public Vector2 Position
        { get { return position; } }
        /// <summary>
        /// die Skalierung des Shapes
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set
            {
                onScaleChange(value);
                scale = value;
                onThingsForAreaChanged();
            }
        }
        /// <summary>
        /// Gibt an was für ein Typ diese Shape ist. Sollte von jeder abgeleiteten Klasse selbst gesetzt werden.
        /// </summary>
        public ShapeType Type;
        /// <summary>
        /// Die Liste aller Eckpunkte einer Shape.
        /// </summary>
        public abstract List<Vector2> Vertices
        { get; }
        /// <summary>
        /// Gibt die Fläche der Form zurück.
        /// </summary>
        public abstract double Area
        { get; }

        public event EventHandler OnAreaChanged;

        /// <summary>
        /// Basiskonstkruktor.
        /// </summary>
        public Shape()
        {
            scale = 1.0f;
        }

        /// <summary>
        /// Speziellerer Konstruktor. Wird genutzt zum klonen.
        /// </summary>
        /// <param name="position">die Position</param>
        /// <param name="scale">die Skalierung</param>
        protected Shape(Vector2 position, float scale = 1.0f)
        {
            this.position = position;
            this.scale = scale;
        }
        
        /// <summary>
        /// Lädt die Standarddaten in das Shape. Mit Body als Grundlage.
        /// </summary>
        /// <param name="body"></param>
        public virtual void LoadContent(Body body)
        {
            body.OnPositionChange += delegate
            { position = body.Position; };
            Type = ShapeTypeExtension.GetShapeType(this);

            if(OnAreaChanged != null)
                OnAreaChanged(Area, null);
        }

        /// <summary>
        /// Lädt die Standarddaten in das Shape. Mit Tile als Grundlage.
        /// </summary>
        /// <param name="tile"></param>
        public virtual void LoadContent(Tile tile)
        {
            tile.OnPositionChange += delegate
            { position = tile.Position; };
            position = tile.Position;
            Type = ShapeTypeExtension.GetShapeType(this);
        }

        /// <summary>
        /// Berechnet einen Vektor um eine Kollision zwischen diesen und einer anderen Shape aufzulösen.
        /// </summary>
        /// <param name="shape">eine andere Shape</param>
        /// <returns></returns>
        public Vector2 GetCollisionSolvingVector(Shape shape)
        {
            // Die Shapes werden dynamisch mit ihren jeweiligen Typ erzeugt.
            // Das erspart einen, das jede abgeleitete Klasse zu jeder anderen Klasse eine extra Methode hat.
            // Diese wurden dann in die "ShapeCollisionManager" Klasse ausgelagert.
            dynamic thisDerived = Convert.ChangeType(this, Type.GetTypeType());
            dynamic shapeDerived = Convert.ChangeType(shape, shape.Type.GetTypeType());

            return ShapeCollisionManager.GetCollisionSolvingVector(thisDerived, shapeDerived);
        }

        protected void onThingsForAreaChanged()
        {
            if(OnAreaChanged != null)
                OnAreaChanged(Area, null);
        }

        /// <summary>
        /// Abstrakte Methode, welche darauf reagiert, wenn sich die Skalierung verändert.
        /// </summary>
        /// <param name="newScale">die neue Skalierung</param>
        protected abstract void onScaleChange(float newScale);
    }
}
