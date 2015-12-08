using Microsoft.Xna.Framework;
using System;
using System.Xml.Serialization;

namespace NoNameGame.Components
{
    /// <summary>
    /// Stellt den Körper einer Entity dar. Verschiedene Inhalte werden gespeichert, wie z.B. Geschweindugkeit oder Position.
    /// </summary>
    public class Body
    {
        Vector2 position;

        /// <summary>
        /// Die Beschleunigung des Lörpers.
        /// </summary>
        public Vector2 Velocity;
        /// <summary>
        /// Die Geschwindigkeit des Körpers.
        /// </summary>
        public float Speed;
        /// <summary>
        /// Der Geschwindigkeitsfaktor, welcher mit der Geschwindigkeit multipliziert werden kann.
        /// </summary>
        public float SpeedFactor;
        /// <summary>
        /// Gibt an, ob sich der Körper in der vorigen Berechnung rotiert hat.
        /// </summary>
        [XmlIgnore]
        public bool Rotated;
        /// <summary>
        /// Die Fläche, die diese Objekt einnimmt.
        /// </summary>
        [XmlIgnore]
        public float Area;
        /// <summary>
        /// Das Material, welches das Objekt hat.
        /// </summary>
        public Material Material;
        /// <summary>
        /// <summary>
        /// Gibt die relative Masse (im Bezug auf die Fläche, die es braucht) an.
        /// </summary>
        [XmlIgnore]
        public float MassRelativ
        { get { return Material.GetDensity() * Area; } }

        /// <summary>
        /// die absolute Position auf der Ebene
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                if(OnPositionChange != null)
                    OnPositionChange(position, null);
            }
        }
        /// <summary>
        /// Das Level/Layer auf dem sich der Körper befindet.
        /// </summary>
        public int CollisionLevel;

        /// <summary>
        /// Wenn die Position verändert wird, wird dieses Event aufgerufen.
        /// </summary>
        public event EventHandler OnPositionChange;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
        public Body()
        {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Speed = 0.0f;
            SpeedFactor = 1.0f;
            CollisionLevel = 0;
            Rotated = false;
            Area = 1;
            Material = Material.None;
        }

        public void LoadContent()
        {
            if(OnPositionChange != null)
                OnPositionChange(position, null);
        } 

        public void UnloadContent()
        {

        }
    }
}
