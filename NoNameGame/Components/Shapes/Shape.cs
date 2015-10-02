using System;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(AABBShape))]
    [XmlInclude(typeof(OBBShape))]
    public abstract class Shape
    {
        protected Body body;
        protected float scale;

        public float Scale
        {
            get { return scale; }
            set
            {
                OnScaleChange(value);
                scale = value;
            }
        }

        public Shape()
        {
            scale = 1.0f;
        }
        
        public virtual void LoadContent(Body body)
        {
            this.body = body;
        }

        protected abstract void OnScaleChange(float newScale);

        public abstract bool Intersect<S>(S shape) where S : Shape;
    }
}
