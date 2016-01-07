using Microsoft.Xna.Framework;
using NoNameGame.Components;
using NoNameGame.Components.Shapes;
using NoNameGame.Entities;
using NoNameGame.Maps;
using System;

namespace NoNameGame.Collisions
{
    /// <summary>
    /// Eine Darstellung einer Kollision zwischen 2 Objekten.
    /// </summary>
    class Collision
    {
        /// <summary>
        /// Die Position des ersten kollidierten Objektes.
        /// </summary>
        Vector2 firstPosition;
        /// <summary>
        /// Die Position des zweiten kollidierten Objektes.
        /// </summary>
        Vector2 secondPosition;

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
        {
            get
            {
                return ResolvingNormal * Penetration;
            }
        }
        /// <summary>
        /// Der Normaliserte Vektor der Kollisionsauflösung.
        /// </summary>
        public Vector2 ResolvingNormal
        {
            get { return resolvingNormal; }
            private set
            {
                if(value != Vector2.Zero)
                    value.Normalize();
                resolvingNormal = value;
            }
        }
        private Vector2 resolvingNormal;
        /// <summary>
        /// Die Tiefe der Kollision.
        /// </summary>
        public float Penetration
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
            firstPosition = firstEntity.Body.Position;
            SecondShape = secondEntity.Shape;
            SecondBody = secondEntity.Body;
            secondPosition = secondEntity.Body.Position;
            Penetration = resolving.Length();
            ResolvingNormal = resolving;

            if(firstEntity.ID < secondEntity.ID)
                ID = firstEntity.IDString + "-" + secondEntity.IDString;
            else
                ID = secondEntity.IDString + "-" + firstEntity.IDString;
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
            firstPosition = entity.Body.Position;
            SecondShape = tile.Shape;
            SecondBody = tile.Body;
            secondPosition = tile.Body.Position;
            Penetration = resolving.Length();
            ResolvingNormal = resolving;

            ID = entity.IDString + "-" + tile.IDString;
        }

        /// <summary>
        /// Löst die Kollision, die dieses Objekt darstellt, auf.
        /// </summary>
        public void ResolveCollision()
        {
            if(ResolvingNormal == Vector2.Zero)
                return;

            // Wenn sich keiner der beiden Körper bewegt hat, muss dennoch die Kollision behoben werden
            if(FirstBody.MovingDirection != Vector2.Zero || SecondBody.MovingDirection != Vector2.Zero)
            {
                Vector2 relativVelocity = SecondBody.MovingVector - FirstBody.MovingVector;
                float velocityAlongCollisionNormal = Vector2.Dot(relativVelocity, ResolvingNormal);

                float restitution = Math.Min(FirstBody.Material.GetRestitution(), SecondBody.Material.GetRestitution());
                float impulseValue = -(1 + restitution) * velocityAlongCollisionNormal;
                impulseValue /= 1 / FirstBody.Mass + 1 / SecondBody.Mass;

                Vector2 impulse = impulseValue * ResolvingNormal;

                FirstBody.ChangeMovingVector((-1 / FirstBody.Mass) * impulse, true);
                SecondBody.ChangeMovingVector((1 / SecondBody.Mass) * impulse, true);
            }
            else
            {
                // TODO: Wenn sich beide nicht bewegen
            }

            //// Falls sich einer der beiden Körper sich bewegt oder rotiertz hat.
            //else if(FirstBody.Moved || SecondBody.Moved ||
            //        FirstBody.Rotated || SecondBody.Rotated)
            //{
            //    float firstOffset = 0.0f;
            //    float secondOffset = 0.0f;

            //    // Falls es vorher schonmal eine Kollisionsauflösung an einem der beiden Objekten gab.
            //    if(FirstBody.Position != firstPosition || SecondBody.Position != secondPosition)
            //        changeThisCollision();
            //    else
            //    {
            //        float massSum = FirstBody.Mass + SecondBody.Mass;

            //        // Falls die Masse von einer Entity unendlich (-1) ist oder 0 ist
            //        // Oder die Masse des ersten Körpers kleiner ist als des zweiten und der erste sich bewegte
            //        if(SecondBody.Mass < 0 || FirstBody.Mass == 0 ||
            //           (FirstBody.Mass <= SecondBody.Mass &&
            //           (FirstBody.Moved && SecondBody.Moved)))
            //            firstOffset = 1.0f;
            //        // Falls die Masse von einer Entity unendlich (-1) ist oder 0 ist
            //        // Oder die Masse des zweiten Körpers kleiner ist als des ersten und der zweite sich bewegte
            //        else if(FirstBody.Mass < 0 || SecondBody.Mass == 0 ||
            //                (FirstBody.Mass > SecondBody.Mass &&
            //                (!FirstBody.Moved && !SecondBody.Moved)))
            //            secondOffset = -1.0f;
            //        else
            //        {
            //            firstOffset = (1 - (1 / (massSum / FirstBody.Mass)));
            //            secondOffset = -(1 - (1 / (massSum / SecondBody.Mass)));
            //        }
            //    }

            //    FirstBody.ChangeMovingVector(Resolving * firstOffset, true);
            //    SecondBody.ChangeMovingVector(Resolving * secondOffset, true);
            //}
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
        /// HashCode der Kollision über die ID (String).
        /// </summary>
        /// <returns>der HashCode</returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
