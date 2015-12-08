using System;
using Microsoft.Xna.Framework;
using NoNameGame.Extensions;

namespace NoNameGame.Entities.Abilities
{
    /// <summary>
    /// Stellt die Fähigkeit dar, das sich eine Enity bewegen kann.
    /// </summary>
    public class MovingAbility : EntityAbility
    {
        /// <summary>
        /// Ein Enum, welches angiebt, wie sich bewegt werden soll.
        /// </summary>
        public enum MovingType
        {
            OneWay,
            Circle
        }

        /// <summary>
        /// Ein Enum, welches angiebt, wie es gehandhabt werden soll, wenn die Bewegung ihr Ende erreicht hat.
        /// </summary>
        public enum EndingType
        {
            Standing,
            Repeating,
            DisappearingWithAnimation,
            DisappearingWithoutAnimation
        }

        Vector2 offset;

        /// <summary>
        /// Der Bewegungstyp.
        /// </summary>
        public MovingType Moving;
        /// <summary>
        /// der Endtyp.
        /// </summary>
        public EndingType Ending;
        /// <summary>
        /// Der Startpunkt.
        /// </summary>
        public Vector2 Start;
        /// <summary>
        /// Der Endpunkt.
        /// </summary>
        public Vector2 End;
        /// <summary>
        /// Der Mittelpnukt, der Kreisbewegung.
        /// </summary>
        public Vector2 Center;
        /// <summary>
        /// Der Radius des Kreises.
        /// </summary>
        public Vector2 Radius;

        /// <summary>
        /// Wird gefeuert, wenn das Objekt, das Ende seiner Bewegung erreicht hat.
        /// </summary>
        public event EventHandler OnEndingReached;

        /// <summary>
        /// Baiskonstrukto,
        /// </summary>
        public MovingAbility()
        {
            offset = Vector2.Zero;
            Moving = MovingType.OneWay;
            Ending = EndingType.Standing;
            Start = Vector2.Zero;
            End = Vector2.Zero;
            Center = Vector2.Zero;
            Radius = Vector2.Zero;
        }

        public override void LoadContent(ref Entity entity)
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
                // Bewegung nur auf einer Linie
                if(Moving == MovingType.OneWay)
                {
                    if(Start != End)
                    {
                        if(offset == Vector2.Zero)
                            offset = End.GetAngleValues(Start).Value;

                        Vector2 moveVelocity = offset * entity.Body.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        bool finishedX = true;
                        bool finishedY = true;

                        // Je nach dem, wo sich die Entity befindet, bewegt diese sich in eine andere Richtung
                        if(offset.X < 0 && entity.Body.Position.X + moveVelocity.X <= End.X)
                            entity.Body.Velocity.X = entity.Body.Position.X - End.X;
                        else if(offset.X > 0 && entity.Body.Position.X + moveVelocity.X >= End.X)
                            entity.Body.Velocity.X = End.X - entity.Body.Position.X;
                        else
                        {
                            entity.Body.Velocity.X = moveVelocity.X;
                            finishedX = false;
                        }

                        // Je nach dem, wo sich die Entity befindet, bewegt diese sich in eine andere Richtung
                        if(offset.Y < 0 && entity.Body.Position.Y + moveVelocity.Y <= End.Y)
                            entity.Body.Velocity.Y = entity.Body.Position.Y - End.Y;
                        else if(offset.Y > 0 && entity.Body.Position.Y + moveVelocity.Y >= End.Y)
                            entity.Body.Velocity.Y = End.Y - entity.Body.Position.Y;
                        else
                        {
                            entity.Body.Velocity.Y = moveVelocity.Y;
                            finishedY = false;
                        }

                        // Wenn die Endposition erreich wurde
                        if(finishedX && finishedY)
                        {
                            offset = Vector2.Zero;
                            if(Ending == EndingType.Repeating)
                            {
                                Vector2 tempStart = Start;
                                Start = End;
                                End = tempStart;
                            }
                            else if(Ending == EndingType.DisappearingWithAnimation)
                            {
                                //TODO: Implementieren
                            }
                            else if(Ending == EndingType.DisappearingWithoutAnimation)
                            {
                                IsActive = false;
                                entity.UnloadContent();
                                if(OnEndingReached != null)
                                    OnEndingReached(entity, null);
                            }
                            else
                                IsActive = false;
                        }
                    }
                }
                // Bewegung in einem Kreis
                else if(Moving == MovingType.Circle)
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
