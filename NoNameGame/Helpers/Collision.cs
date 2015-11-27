using Microsoft.Xna.Framework;
using NoNameGame.Components.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Helpers
{
    public class Collision
    {
        public Shape FirstShape
        { private set; get; }
        public Shape SecondShape
        { private set; get; }
        public Vector2 FirstVelocity
        { private set; get; }
        public Vector2 SecondVelocity
        { private set; get; }
        public Vector2 NormalVector;
        public float Penetration;

        public Collision(Shape firstShape, Vector2 firstVelocity, Shape secondShape, Vector2 secondVelocity)
        {
            FirstShape = firstShape;
            FirstVelocity = FirstVelocity;
            SecondShape = secondShape;
            SecondVelocity = secondVelocity;
        }
    }
}
