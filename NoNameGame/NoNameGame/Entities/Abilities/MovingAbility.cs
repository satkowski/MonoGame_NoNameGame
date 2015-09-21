using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NoNameGame.Extensions;

namespace NoNameGame.Entities.Abilities
{
    public class MovingAbility : EntityAbility
    {
        public enum MovingType
        {
            OneWay,
            TwoWay,
            Circle
        }

        public MovingType Type;
        public Vector2 Start;
        public Vector2 End;
        public Vector2 Center;
        public Vector2 Radius;

        public MovingAbility()
        {
            Type = MovingType.OneWay;
            Start = Vector2.Zero;
            End = Vector2.Zero;
            Center = Vector2.Zero;
            Radius = Vector2.Zero;
        }

        public override void LoadContent(ref AutomatedEntity entity)
        {
            base.LoadContent(ref entity);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(IsActive)
            {
                if(Type == MovingType.OneWay || Type == MovingType.TwoWay)
                {
                    if(Start != End)
                    {
                        Vector2? offset = Start.GetAngleValues(End);
                        if(offset.HasValue)
                            entity.MoveVelocity += offset.Value * entity.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //TODO: Schauen, ob das Ziel erreicht wurde
                    }
                }
                else if(Type == MovingType.Circle)
                {
                    if(Radius != Vector2.Zero)
                    {
                        //TODO: Implementieren
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
