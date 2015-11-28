using Microsoft.Xna.Framework;
using NoNameGame.Maps;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NoNameGame.Components.Shapes
{
    /// <summary>
    /// Stellt ein OBB (Oriented Bounding Box) Shape dar.
    /// </summary>
    [XmlInclude(typeof(OBBShape))]
    public class OBBShape : AABBShape
    {
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                rotationX = new Vector2((float)Math.Cos(rotation), -(float)Math.Sin(rotation));
                rotationY = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation));
            }
        }

        protected Vector2 rotationX;
        protected Vector2 rotationY;
        public new Vector2 Left
        { get { return Position + Vector2.Transform(new Vector2(-Center.X, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))); } }
        public new Vector2 Top
        { get { return Position + Vector2.Transform(new Vector2(0, -Center.Y), Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))); } }
        public new Vector2 Right
        { get { return Position + Vector2.Transform(new Vector2(Center.X, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))); } }
        public new Vector2 Bottom
        { get { return Position + Vector2.Transform(new Vector2(0, Center.Y), Matrix.CreateRotationZ(MathHelper.ToRadians(rotation))); } }
        public override List<Vector2> Vertices
        {
            get
            {
                List<Vector2> newVertices = new List<Vector2>();
                newVertices.Add(Position + (Left - Position) + (Top - Position));
                newVertices.Add(Position + (Right - Position) + (Top - Position));
                newVertices.Add(Position + (Right - Position) + (Bottom - Position));
                newVertices.Add(Position + (Left - Position) + (Bottom - Position));

                return newVertices;
            }
        }

        public OBBShape()
        {
            Type = ShapeType.OBB;
            Rotation = 0.0f;
        }

        /// <summary>
        /// Speziellerer Konstruktor. Wird genutzt zum klonen.
        /// </summary>
        /// <param name="position">die Position</param>
        /// <param name="size">die Größe</param>
        /// <param name="rotation">die Rotation</param>
        /// <param name="scale">die Skalierung</param>
        protected OBBShape(Vector2 position, Vector2 size, float rotation = 0.0f, float scale = 1.0f)
            : base(position, size, scale)
        {
            Type = ShapeType.OBB;
            Rotation = rotation;
        }

        /// <summary>
        /// Lädt die Standarddaten in das Shape. Mit Body als Grundlage.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="width">die Breite</param>
        /// <param name="height">die Höhe</param>
        /// <param name="scale">die Skalierung</param>
        /// <param name="rotation">die Rotation</param>
        public void LoadContent(Body body, float width, float height, float scale, float rotation)
        {
            Rotation = rotation;
            base.LoadContent(body, width, height, scale);
        }

        /// <summary>
        /// Lädt die Standarddaten in das Shape. Mit Tile als Grundlage.
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="width">die Breite</param>
        /// <param name="height">die Höhe</param>
        /// <param name="scale">die Skalierung</param>
        /// <param name="rotation">die Rotation</param>
        public void LoadConent(Tile tile, float width, float height, float scale, float rotation)
        {
            Rotation = rotation;
            base.LoadContent(tile, width, height, scale);
        }

        /// <summary>
        /// Klont das OBB auf dem es aufgerufen wird.
        /// </summary>
        /// <returns>die Kopie dieses OBB</returns>
        public new OBBShape Clone()
        {
            return new OBBShape(Position, Size, Rotation, Scale);
        }

        /// <summary>
        /// Klont das OBB auf dem es aufgerufen wird als AABB (also ohne Rotation).
        /// </summary>
        /// <returns>die Kopie dieses OBB als AABB</returns>
        public AABBShape CloneToAABBShape()
        {
            return base.Clone();
        }
    }
}
