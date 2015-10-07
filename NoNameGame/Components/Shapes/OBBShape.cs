using NoNameGame.Maps;
using System;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    [XmlInclude(typeof(OBBShape))]
    public class OBBShape : AABBShape
    {
        public float Rotation;

        public OBBShape()
        {
            Rotation = 0.0f;
        }
        
        public void LoadContent(Body body, float width, float height, float scale, float rotation)
        {
            Rotation = rotation;
            base.LoadContent(body, width, height, scale);
        }

        public void LoadConent(Tile tile, float width, float height, float scale, float rotation)
        {
            Rotation = rotation;
            base.LoadContent(tile, width, height, scale);
        }
    }
}
