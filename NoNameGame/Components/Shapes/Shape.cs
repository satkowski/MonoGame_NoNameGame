using Microsoft.Xna.Framework;
using NoNameGame.Maps;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(AABBShape))]
    [XmlInclude(typeof(OBBShape))]
    [XmlInclude(typeof(CircleShape))]
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
        public abstract List<Vector2> Vertices
        {
            get;
        }

        public Shape()
        {
            scale = 1.0f;
        }

        protected Shape(Vector2 position, float scale = 1.0f)
        {
            this.position = position;
            this.scale = scale;
        }
        
        public virtual void LoadContent(Body body)
        {
            body.OnPositionChange += delegate
            { position = body.Position; };
            Type = ShapeTypeExtension.GetShapeType(this);
        }

        public virtual void LoadContent(Tile tile)
        {
            tile.OnPositionChange += delegate
            { position = tile.Position; };
            position = tile.Position;
            Type = ShapeTypeExtension.GetShapeType(this);
        }

        public Vector2 GetCollisionSolvingVector(Shape shape)
        {
            dynamic thisDerived = Convert.ChangeType(this, Type.GetTypeType());
            dynamic shapeDerived = Convert.ChangeType(shape, shape.Type.GetTypeType());

            return ShapeCollisionManager.GetCollisionSolvingVector(thisDerived, shapeDerived);
        }

        protected abstract void OnScaleChange(float newScale);
    }
}
