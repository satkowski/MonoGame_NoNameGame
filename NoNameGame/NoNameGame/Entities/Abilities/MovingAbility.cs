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

        Vector2 offset;

        public MovingType Type;
        public Vector2 Start;
        public Vector2 End;
        public Vector2 Center;
        public Vector2 Radius;

        public MovingAbility()
        {
            offset = Vector2.Zero;
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
                        if(offset == Vector2.Zero)
                            offset = End.GetAngleValues(Start).Value;

                        Vector2 moveVelocity = offset * entity.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        bool finished = false;

                        if(offset.X < 0 && entity.Image.Position.X + moveVelocity.X < End.X
                           || offset.Y < 0 && entity.Image.Position.Y + moveVelocity.Y < End.Y)
                        {
                            moveVelocity = entity.Image.Position - End;
                            finished = true;
                        }
                        else if(offset.X > 0 && entity.Image.Position.X + moveVelocity.X > End.X
                                || offset.Y > 0 && entity.Image.Position.Y + moveVelocity.Y > End.Y)
                        {
                            moveVelocity = End - entity.Image.Position;
                            finished = true;
                        }
                        else
                            entity.MoveVelocity = moveVelocity;

                        if(finished)
                        {
                            if(Type == MovingType.OneWay)
                                IsActive = false;
                            else if(Type == MovingType.TwoWay)
                            {
                                offset = Vector2.Zero;
                                Vector2 tempStart = Start;
                                Start = End;
                                End = tempStart;
                            }
                        }
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
