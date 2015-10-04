using System;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(OBBShape))]
    public class OBBShape : AABBShape
    {
        protected override void OnScaleChange(float newScale)
        {
            throw new NotImplementedException();
        }
    }
}
