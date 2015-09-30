using Microsoft.Xna.Framework;
using NoNameGame.Components.Shapes;

namespace NoNameGame.Components
{
    public class Body
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Shape Shape;
        public ShapeType ShapeType;

        public Body()
        {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            ShapeType = ShapeType.AABB;
        }

        public void LoadContent()
        {
      
        } 

        public void UnloadContent()
        {

        }
    }
}
