using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Components.Shapes
{
    public enum ShapeType
    {
        AABB,
        OBB,
        Circle,
        Polygon
    }

    public static class ShapeTypeExtension
    {
        public static Type GetTypeType(this ShapeType shape)
        {
            if(shape == ShapeType.AABB)
                return typeof(AABBShape);
            else //if(shape == ShapeType.OBB)
                return typeof(OBBShape);
            //TODO: Anderen implementieren            
        }

        public static ShapeType GetShapeType(Shape shape)
        {
            if(shape.GetType().Equals(typeof(AABBShape)))
                return ShapeType.AABB;
            else //if(shape.GetType().Equals(typeof(OBBShape)))
                return ShapeType.OBB;
            //TODO Andere implementieren
        }
    }
}
