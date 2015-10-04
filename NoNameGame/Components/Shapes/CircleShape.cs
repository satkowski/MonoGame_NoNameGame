using Microsoft.Xna.Framework;
using NoNameGame.Maps;

namespace NoNameGame.Components.Shapes
{
    public class CircleShape : Shape
    {
        public Vector2 Center
        { get { return position; } }
        public float Radius;

        public void LoadContent(Body body, float radius, float scale)
        {
            Radius = radius;
            this.scale = scale;
            base.LoadContent(body);
        }

        public void LoadContent(Tile tile, float radius, float scale)
        {
            Radius = radius;
            this.scale = scale;
            base.LoadContent(tile);
        }

        protected override void OnScaleChange(float newScale)
        {
            Radius /= Scale;
            Radius *= newScale;
        }
    }
}
