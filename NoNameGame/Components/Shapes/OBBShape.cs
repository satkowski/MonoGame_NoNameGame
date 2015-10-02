using System;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(OBBShape))]
    public class OBBShape : AABBShape
    {
        public override bool Intersect<S>(S shape)
        {
            throw new NotImplementedException();
        }

        protected override void OnScaleChange(float newScale)
        {
            throw new NotImplementedException();
        }
    }
}
