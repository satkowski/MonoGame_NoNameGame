using Microsoft.Xna.Framework;
using NoNameGame.Components;
using NoNameGame.Components.Shapes;
using NoNameGame.Entities;
using NoNameGame.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameGame.Helpers
{
    /// <summary>
    /// Eine Darstellung einer Kollision zwischen 2 Objekten.
    /// </summary>
    public class Collision
    {
        /// <summary>
        /// ID der Kollision.
        /// </summary>
        public string ID
        { get; private set; }
        /// <summary>
        /// Shape des ersten Objektes.
        /// </summary>
        public Shape FirstShape
        { get; private set; }
        /// <summary>
        /// Shape des zweiten Objektes.
        /// </summary>
        public Shape SecondShape
        { get; private set; }
        /// <summary>
        /// Body des ersten Objektes.
        /// </summary>
        public Body FirstBody
        { get; private set; }
        /// <summary>
        /// Body des zweiten Objektes.
        /// </summary>
        public Body SecondBody
        { get; private set; }

        /// <summary>
        /// Ein Vektor, welche die Kollision auflösen würde.
        /// </summary>
        public Vector2 Resolving
        { get; private set; }

        /// <summary>
        /// Konstruktor für 2 Entities.
        /// </summary>
        /// <param name="firstEntity">die erste Entity</param>
        /// <param name="secondEntity">die zweite Entity</param>
        /// <param name="resolving">der Kollisionsausflösungsvektor</param>
        public Collision(Entity firstEntity, Entity secondEntity, Vector2 resolving)
        {
            FirstShape = firstEntity.Shape;
            FirstBody = firstEntity.Body;
            SecondShape = secondEntity.Shape;
            SecondBody = secondEntity.Body;
            Resolving = resolving;

            if(firstEntity.ID < secondEntity.ID)
                ID = "E" + firstEntity.ID + "-" + "E" + secondEntity.ID;
            else
                ID = "E" + secondEntity.ID + "-" + "E" + firstEntity.ID;
        }

        /// <summary>
        /// Konstruktor für eine Entity und ein Tile.
        /// </summary>
        /// <param name="entity">eine Entity</param>
        /// <param name="tile">ein Tile</param>
        /// <param name="resolving">Kollisionsausflösungsvektor</param>
        public Collision(Entity entity, Tile tile, Vector2 resolving)
        {
            FirstShape = entity.Shape;
            FirstBody = entity.Body;
            SecondShape = entity.Shape;
            SecondBody = null;
            Resolving = resolving;

            ID = "E" + entity.ID + "-" + "T" + tile.ID;
        }

        /// <summary>
        /// Löst die Kollision, die dieses Objekt darstellt, auf.
        /// </summary>
        public void ResolveCollision()
        {
            if(Resolving == Vector2.Zero)
                return;

            if(SecondBody == null)
                FirstBody.Position += Resolving;
            else
            {
                if(FirstBody.Velocity != Vector2.Zero && SecondBody.Velocity == Vector2.Zero)
                    FirstBody.Position += Resolving;
                else if(FirstBody.Velocity == Vector2.Zero && SecondBody.Velocity != Vector2.Zero)
                    SecondBody.Position -= Resolving;
                else if(FirstBody.Velocity != Vector2.Zero && SecondBody.Velocity != Vector2.Zero)
                    return;
                else
                {

                }
            }
        }

        /// <summary>
        /// Vergleich der Gleichheit über die ID.
        /// </summary>
        /// <param name="obj">eine anderes Collision-Objekt</param>
        /// <returns>sind die beiden Kollisionen gleich</returns>
        public override bool Equals(object obj)
        {
            try
            {
                Collision collision = (Collision)obj;
                if(collision.ID.Equals(ID))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// HashCode der Kollision über die ID.
        /// </summary>
        /// <returns>der HashCode</returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
