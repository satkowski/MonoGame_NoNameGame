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

        public Vector2 Velocity;
        public float Speed;
        /// <summary>
        /// Der Geschwindigkeitsfaktor, welcher mit der Geschwindigkeit multipliziert werden kann.
        /// </summary>
        public float SpeedFactor;
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
        public int CollisionLevel;

        /// <summary>
        /// Wenn die Position verändert wird, wird dieses Event aufgerufen.
        /// </summary>
        public event EventHandler OnPositionChange;

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
