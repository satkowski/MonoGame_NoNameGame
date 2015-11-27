﻿using System;

using Microsoft.Xna.Framework;

using NoNameGame.Managers;
using NoNameGame.Extensions;

namespace NoNameGame.Entities.Abilities
{
    public class ShootingAbility : EntityAbility
    {
        public enum ShootingType
        {
            AgainstPlayer,
            OneDirection,
        }
        
        int elapsedTime;

        public ShootingType Type;
        public string ShotEntityPath;
        public Vector2 StartPosition;
        public Vector2 DestinationPosition;
        public int ShootingInterval;

        public event EventHandler OnNewShotEntityCreated;

        public ShootingAbility()
        {
            Type = ShootingType.OneDirection;
            ShotEntityPath = string.Empty;
            StartPosition = Vector2.Zero;
            DestinationPosition = Vector2.Zero;
            elapsedTime = 0;
            ShootingInterval = 0;
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
            base.Update(gameTime);

            if(IsActive && ShootingInterval > 0)
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if(elapsedTime >= ShootingInterval)
                {
                    elapsedTime = 0;
                    
                    XmlManager<Entity> entityLoader = new XmlManager<Entity>();
                    Entity newShotEntity = entityLoader.Load(ShotEntityPath);
                    newShotEntity.LoadContent();
                    
                    int offset = -1;
                    if(DestinationPosition.Y - StartPosition.Y > 0)
                        offset = 1;
                    newShotEntity.Image.Rotation = MathHelper.PiOver2 + offset * (float)Math.Acos(DestinationPosition.GetAngleValues(StartPosition).Value.X);

                    newShotEntity.Body.Position = StartPosition;
                    newShotEntity.MovingAbility.Start = StartPosition;
                    newShotEntity.MovingAbility.End = DestinationPosition;
                    newShotEntity.MovingAbility.IsActive = true;

                    if(OnNewShotEntityCreated != null)
                        OnNewShotEntityCreated(newShotEntity, null);
                }
            }
        }
    }
}
