using System;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(OBBShape))]
    public class OBBShape : Shape
    {
        public override bool Intersect<S>(S shape)
        {
            throw new NotImplementedException();
        }
    }
}
