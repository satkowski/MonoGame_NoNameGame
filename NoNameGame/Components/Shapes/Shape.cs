using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    public abstract class Shape
    {
        //TODO: Ordentliche Serializierung für XML finden
        public abstract bool Intersect<S>(S shape) where S : Shape;
    }
}
