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
    public class Collision
    {
        public string ID
        { get; private set; }
        public Shape FirstShape
        { get; private set; }
        public Shape SecondShape
        { get; private set; }
        public Body FirstBody
        { get; private set; }
        public Body SecondBody
        { get; private set; }

        public Vector2 Resolving
        { get; private set; }

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

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
