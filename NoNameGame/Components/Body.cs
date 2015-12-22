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
        Vector2 movingDirection;

        /// <summary>
        /// Die Richtung in die sich der Körper bewegt. Ist immer normalisiert.
        /// </summary>
        public Vector2 MovingDirection
        {
            get { return movingDirection; }
            set
            {
                if(value != Vector2.Zero)
                    value.Normalize();
                movingDirection = value;
            }
        }
        /// <summary>
        /// Die aktuelle Geschwindigkeit des Körpers. Passt sich automatisch an die maximal mögliche Gescwindigkeit an.
        /// </summary>
        public float VelocityCurrent;
        /// <summary>
        /// Die maximale Geschwindigkeit des Körpers.
        /// </summary>
        public float VelocityMax;
        /// <summary>
        /// Gibt die aktuelle Bewegung wieder.
        /// </summary>
        public Vector2 MovingVector
        { get { return MovingDirection * VelocityCurrent; } }

        /// <summary>
        /// Die Beschleunigung des Körpers.
        /// </summary>
        public float AccelerationValue;
        /// <summary>
        /// Der Beschleunigungsfaktor.
        /// </summary>
        public float AccelerationFactor;
        public float Acceleration
        { get { return AccelerationFactor * AccelerationValue; } }
        /// <summary>
        /// Gibt an, ob sich der Körper in der vorigen Berechnung rotiert hat.
        /// </summary>
        [XmlIgnore]
        public bool Rotated;
        /// <summary>
        /// Gibt an, ob sich der Körper in der vorherigen Berechnung bewegt hat.
        /// </summary>
        [XmlIgnore]
        public bool Moved;
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
        public float Mass
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
            VelocityCurrent = 0.0f;
            VelocityMax = 0.0f;
            AccelerationValue = 0.0f;
            AccelerationFactor = 1.0f;
            CollisionLevel = 0;
            Rotated = false;
            Moved = false;
            Area = 1;
            Material = Material.None;
        }

        public void LoadContent()
        {
            if(OnPositionChange != null)
                OnPositionChange(position, null);
        }

        public void LoadContent(Material material)
        {
            Material = material;
            if(OnPositionChange != null)
                OnPositionChange(position, null);
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            if(!Moved)
            {
                // Abbrembsen, falls sich nicht mehr aktiv bewegt wurde
                VelocityCurrent *= 1 - 4 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(VelocityCurrent <= VelocityMax / 100)
                {
                    VelocityCurrent = 0.0f;
                    MovingDirection = Vector2.Zero;
                }
            }
            Position += MovingVector;
        }

        /// <summary>
        /// Die Veränderung des aktuellen Bewegunsvektors.
        /// Verbindet diesen Vektor mit dem MovingVector und berechnet damit einen neuen.
        /// Weiterhin wird dadurch automatisch die Geschwindigkeit mit angepasst.
        /// </summary>
        /// <param name="changeMovingVector">die Veränderung des Bewegungsvektors</param>
        /// <param name="collision"></param>
        public void ChangeMovingVector(Vector2 changeMovingVector, bool collided = false)
        {
            // Prüft ob es überhaupt eine Veränderung der Bewegung gab
            if(changeMovingVector == Vector2.Zero)
                return;

            Vector2 newMovingVector;
            // Entscheiden welcher der neue Bewegungsvektor ist
            if(collided)
                newMovingVector = changeMovingVector;
            else
               newMovingVector = MovingVector + changeMovingVector;

            float velocity = newMovingVector.Length();
            // Setzen der neuen Geschwindigkeit.
            if(velocity > VelocityMax)
                VelocityCurrent = VelocityMax;
            else
                VelocityCurrent = velocity;
            // Setzen der neuen Bewegungsrichtung.
            MovingDirection = newMovingVector;
            if(collided)
                Position += MovingVector;
            // Nur wenn dies nicht durch eine Kollision ausgelöst wurde, wurde der Körper von selbst bewegt.
            Moved = !collided;
        }
    }
}
