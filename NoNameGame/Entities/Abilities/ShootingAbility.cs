using System;

using Microsoft.Xna.Framework;

using NoNameGame.Managers;
using NoNameGame.Extensions;

namespace NoNameGame.Entities.Abilities
{
    /// <summary>
    /// Stellt die Fähigkeit dar, dass eine Enity andere Entities schießen kann.
    /// </summary>
    public class ShootingAbility : EntityAbility
    {
        /// <summary>
        /// Ein Enum, welches die Art, wie geschossen werden soll, angiebt.
        /// </summary>
        public enum ShootingType
        {
            AgainstPlayer,
            OneDirection,
        }
        
        /// <summary>
        /// Die Zeit, welche schon vergangen ist.
        /// </summary>
        int elapsedTime;

        /// <summary>
        /// Der Schusstyp.
        /// </summary>
        public ShootingType Type;
        /// <summary>
        /// Der Pfad zum Bild des Schusses.
        /// </summary>
        public string ShotEntityPath;
        /// <summary>
        /// Die Startposition, in dem der Schuss erscheinen soll.
        /// </summary>
        public Vector2 StartPosition;
        /// <summary>
        /// Der Zielpunkt des Schusses.
        /// </summary>
        public Vector2 DestinationPosition;
        /// <summary>
        /// Der Intervall, in dem Schüsse gefeuert werden sollen.
        /// </summary>
        public int ShootingInterval;

        /// <summary>
        /// Wird gefeuert, wenn ein neues Schussobjekt erstellt wurde.
        /// </summary>
        public event EventHandler OnNewShotEntityCreated;

        /// <summary>
        /// Basiskonstruktor.
        /// </summary>
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
                // Wenn die Zeit reif ist, wird ein neuer Schuss erstellt
                if(elapsedTime >= ShootingInterval)
                {
                    elapsedTime = 0;
                    
                    // Ein neuer Schuss wird erstellt
                    XmlManager<Entity> entityLoader = new XmlManager<Entity>();
                    Entity newShotEntity = entityLoader.Load(ShotEntityPath);
                    newShotEntity.LoadContent();
                    
                    int offset = -1;
                    if(DestinationPosition.Y - StartPosition.Y > 0)
                        offset = 1;
                    // Die Rotation, in der der neue Schuss abgefeuert werden soll, wird berechnet
                    newShotEntity.Image.Rotation = MathHelper.PiOver2 + offset * (float)Math.Acos(DestinationPosition.GetNormalVectorToVector(StartPosition).X);

                    newShotEntity.Body.Position = StartPosition;
                    newShotEntity.MovingAbility.Start = StartPosition;
                    newShotEntity.MovingAbility.End = DestinationPosition;
                    newShotEntity.MovingAbility.IsActive = true;

                    // Der neue Schuss wird an den Eventhandler gegeben, damit dieser auch im Spiel beachtet wird
                    if(OnNewShotEntityCreated != null)
                        OnNewShotEntityCreated(newShotEntity, null);
                }
            }
        }
    }
}
