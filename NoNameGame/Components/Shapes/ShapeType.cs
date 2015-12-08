using System;

namespace NoNameGame.Components.Shapes
{
    /// <summary>
    /// Ein Enum, welches die verschiedenen Shape Typen angiebt.
    /// </summary>
    public enum ShapeType
    {
        AABB,
        OBB,
        Circle,
        Polygon
    }

    /// <summary>
    /// Eine Erweiterung für das Enum ShapeType.
    /// </summary>
    public static class ShapeTypeExtension
    {

        /// <summary>
        /// Konvertiert den aktuellen ShapeType in den dazugehörigen Type.
        /// </summary>
        /// <param name="shape">der ShapeType, der konvertiert werden soll</param>
        /// <returns></returns>
        public static Type GetTypeType(this ShapeType shape)
        {
            if(shape == ShapeType.AABB)
                return typeof(AABBShape);
            else if(shape == ShapeType.Circle)
                return typeof(CircleShape);
            else //if(shape == ShapeType.OBB)
                return typeof(OBBShape);
            //TODO: Anderen implementieren            
        }

        /// <summary>
        /// Konvertiert den aktuellen Type in den dazugehörigen ShapeType.
        /// </summary>
        /// <param name="shape">ein Shape, dessen Type genommen werden soll</param>
        /// <returns></returns>
        public static ShapeType GetShapeType(Shape shape)
        {
            if(shape.GetType().Equals(typeof(AABBShape)))
                return ShapeType.AABB;
            else if(shape.GetType().Equals(typeof(CircleShape)))
                return ShapeType.Circle;
            else //if(shape.GetType().Equals(typeof(OBBShape)))
                return ShapeType.OBB;
            //TODO Andere implementieren
        }
    }
}
