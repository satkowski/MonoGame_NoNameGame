using Microsoft.Xna.Framework;
using NoNameGame.Maps;
using System;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(AABBShape))]
    [XmlInclude(typeof(OBBShape))]
    public abstract class Shape
    {
        protected Vector2 position;
        protected float scale;

        public Vector2 Position
        { get { return position; } }
        public float Scale
        {
            get { return scale; }
            set
            {
                OnScaleChange(value);
                scale = value;
            }
        }
        public ShapeType Type;

        public Shape()
        {
            scale = 1.0f;
        }
        
        public virtual void LoadContent(Body body)
        {
            body.OnPositionChange += delegate
            { position = body.Position; };
        }

        public virtual void LoadContent(Tile tile)
        {
            tile.OnPositionChange += delegate
            { position = tile.Position; };
            position = tile.Position;
        }

        public bool Intersects(Shape shape)
        {
            dynamic thisDerived = Convert.ChangeType(this, Type.GetTypeType());
            dynamic shapeDerived = Convert.ChangeType(shape, shape.Type.GetTypeType());
            
            return ShapeCollisionManager.Intersects(thisDerived, shapeDerived);
        }

        public Vector2 GetCollisionSolvingVector(Shape shape, Vector2 velocity)
        {
            dynamic thisDerived = Convert.ChangeType(this, Type.GetTypeType());
            dynamic shapeDerived = Convert.ChangeType(shape, shape.Type.GetTypeType());

            return ShapeCollisionManager.GetCollisionSolvingVector(thisDerived, shapeDerived, velocity);
        }

        protected abstract void OnScaleChange(float newScale);
    }
}
