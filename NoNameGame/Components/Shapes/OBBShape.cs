using System;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlRoot(ElementName = "ShapeData", Namespace = "NoNameGame.Components.Shapes")]
    public class OBBShape : Shape
    {
        public override bool Intersect<S>(S shape)
        {
            throw new NotImplementedException();
        }
    }
}
