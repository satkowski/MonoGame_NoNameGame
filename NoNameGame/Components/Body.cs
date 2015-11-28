using Microsoft.Xna.Framework;
using System;

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
        }

        public void LoadContent()
        {
      
        } 

        public void UnloadContent()
        {

        }
    }
}
