using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(AABBShape))]
    [XmlInclude(typeof(OBBShape))]
    public abstract class Shape
    {
        protected Body body;
        
        public virtual void LoadContent(Body body)
        {
            this.body = body;
        }

        public abstract bool Intersect<S>(S shape) where S : Shape;
    }
}
