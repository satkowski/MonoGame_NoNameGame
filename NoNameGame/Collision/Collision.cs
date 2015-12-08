using Microsoft.Xna.Framework;
using NoNameGame.Components;
using NoNameGame.Components.Shapes;
using NoNameGame.Entities;
using NoNameGame.Maps;

namespace NoNameGame.Collision
{
    /// <summary>
    /// Eine Darstellung einer Kollision zwischen 2 Objekten.
    /// </summary>
    public class Collision
    {
        private Vector2 firstPosition;
        private Vector2 secondPosition;

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
            firstPosition = firstEntity.Body.Position;
            SecondShape = secondEntity.Shape;
            SecondBody = secondEntity.Body;
            secondPosition = secondEntity.Body.Position;
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
            firstPosition = entity.Body.Position;
            SecondShape = entity.Shape;
            SecondBody = null;
            secondPosition = tile.Position;
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

            // Falls die Kollision mit einem Tile stattfindet, soll die Entity und nicht das Tile verschoben werden.
            if(SecondBody == null)
                FirstBody.Position += Resolving;
            else if(FirstBody.Velocity != Vector2.Zero || SecondBody.Velocity != Vector2.Zero)
                movementCausedCollision();
            else if(FirstBody.Rotated || SecondBody.Rotated)
                rotationCausedCollision();
        }

        /// <summary>
        /// Löst die Kollision auf, welche durch Bewegung erzeugt wurde.
        /// </summary>
        private void movementCausedCollision()
        {
            // Hier wird entschieden wer sich bewegt hat und dementsprechen wird gehandelt.
            if(FirstBody.Velocity != Vector2.Zero && SecondBody.Velocity == Vector2.Zero)
            {
                // Falls es schon ein Kollisionhandling mit dem Objekt gab, muss damit anders umgegangen werden.
                if(FirstBody.Position == firstPosition)
                    FirstBody.Position += Resolving;
                else
                    changeThisCollision();
            }
            else if(FirstBody.Velocity == Vector2.Zero && SecondBody.Velocity != Vector2.Zero)
            {
                // Falls es schon ein Kollisionhandling mit dem Objekt gab, muss damit anders umgegangen werden.
                if(SecondBody.Position == secondPosition)
                    SecondBody.Position -= Resolving;
                else
                    changeThisCollision();
            }
            // TODO: Wenn sich beide bewegt haben -> möglicherweise verändern
            else if(FirstBody.Velocity != Vector2.Zero && SecondBody.Velocity != Vector2.Zero)
            {
                double massSum = FirstBody.MassRelativ + SecondBody.MassRelativ;

                if(FirstBody.MassRelativ < 0 || SecondBody.MassRelativ < 0)
                {
                    if(FirstBody.MassRelativ >= 0 && SecondBody.MassRelativ < 0)
                        FirstBody.Position += Resolving;
                    else if(FirstBody.MassRelativ < 0 && SecondBody.MassRelativ >= 0)
                        SecondBody.Position -= Resolving;
                }
                else
                {
                    FirstBody.Position += Resolving * (float)(1 - (1 / (massSum / FirstBody.MassRelativ)));
                    SecondBody.Position -= Resolving * (float)(1 - (1 / (massSum / SecondBody.MassRelativ)));
                }
            }
        }

        /// <summary>
        /// Löst die Kollision auf, welche durch Rotation erzeugt wurde.
        /// </summary>
        private void rotationCausedCollision()
        {
            // Hier wird entschieden wer sich rotiert hat und dementsprechen wird gehandelt.
            if(FirstBody.Rotated && !SecondBody.Rotated)
            {
                // Falls es schon ein Kollisionhandling mit dem Objekt gab, muss damit anders umgegangen werden.
                if(FirstBody.Position == firstPosition)
                    FirstBody.Position += Resolving;
                else
                    changeThisCollision();
            }
            else if(!FirstBody.Rotated && SecondBody.Rotated)
            {
                // Falls es schon ein Kollisionhandling mit dem Objekt gab, muss damit anders umgegangen werden.
                if(SecondBody.Position == firstPosition)
                    SecondBody.Position -= Resolving;
                else
                    changeThisCollision();
            }
            // TODO: Wenn sich beide rotiert sind -> möglicherweise verändern
            else if(FirstBody.Rotated && SecondBody.Rotated)
            {
                // Falls einer der beiden ein Kreis ist, hat sich dieser durch die Rotation nicht verändert
                if(FirstShape.Type == ShapeType.Circle || SecondShape.Type == ShapeType.Circle)
                {
                    if(FirstShape.Type == ShapeType.OBB)
                        FirstBody.Position += Resolving;
                    else if(SecondShape.Type == ShapeType.OBB)
                        SecondBody.Position -= Resolving;
                }
                else
                {
                    double massSum = FirstBody.MassRelativ + SecondBody.MassRelativ;

                    if(FirstBody.MassRelativ < 0 || SecondBody.MassRelativ < 0)
                    {
                        if(FirstBody.MassRelativ >= 0 && SecondBody.MassRelativ < 0)
                            FirstBody.Position += Resolving;
                        else if(FirstBody.MassRelativ < 0 && SecondBody.MassRelativ >= 0)
                            SecondBody.Position -= Resolving;
                    }
                    else
                    {
                        FirstBody.Position += Resolving * (float)(1 - (1 / (massSum / FirstBody.MassRelativ)));
                        SecondBody.Position -= Resolving * (float)(1 - (1 / (massSum / SecondBody.MassRelativ)));
                    }
                }
            }
        }

        /// <summary>
        /// Berechnet die Kollision neu und verändert demenstprechend die Parameter. Ruft erneut CollisionResolving auf.
        /// </summary>
        private void changeThisCollision()
        {
            // Es wird nun erneut die Kollision zwischen den beiden Objektfromen berechnet.
            Resolving = FirstShape.GetCollisionSolvingVector(SecondShape);
            firstPosition = FirstBody.Position;
            secondPosition = SecondBody.Position;

            ResolveCollision();
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
