using System;
using System.Collections.Generic;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NoNameGame.Images;
using NoNameGame.Maps;
using NoNameGame.Extensions;
using NoNameGame.Entities.Abilities;

namespace NoNameGame.Entities
{
    public class Entity
    {
        public enum EntityType
        {
            Player,
            Enemy,
            Shot
        }

        Dictionary<string, EntityAbility> abilitiesList;

        public EntityType Type;
        public Image Image;
        [XmlIgnore]
        public Vector2 MoveVelocity;
        [XmlIgnore]
        public float MoveSpeedFactor;
        public float MoveSpeed;
        public Vector2 CollisionMovement;
        public int CollisionLevel;
        [XmlElement("ImageEffect")]
        public List<string> ImageEffects;
        [XmlElement("Ability")]
        public List<string> Abilities;
        public PlayerFollowingAbility PlayerFollowingAbility;
        public MovingAbility MovingAbility;
        public ShootingAbility ShootingAbility;
        public UserControlledAbility UserControlledAbility;

        public Entity ()
        {
            Type = EntityType.Enemy;
            MoveVelocity = Vector2.Zero;
            MoveSpeedFactor = 1.0f;
            CollisionMovement = Vector2.Zero;
            CollisionLevel = 0;
            ImageEffects = new List<string>();
            abilitiesList = new Dictionary<string, EntityAbility>();
            Abilities = new List<string>();
        }

        public virtual void LoadContent (EntityType type)
        {
            Type = type;
            Image.Effects = ImageEffects;
            Image.LoadContent();

            setAbility<PlayerFollowingAbility>(ref PlayerFollowingAbility);
            setAbility<MovingAbility>(ref MovingAbility);
            setAbility<ShootingAbility>(ref ShootingAbility);
            setAbility<UserControlledAbility>(ref UserControlledAbility);
            foreach(string abilitiesName in Abilities)
                ActivateAbility(abilitiesName);
        }

        void setAbility<T>(ref T ability, string abilityName = "")
        {
            if(ability == null)
                ability = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (ability as EntityAbility).IsActive = true;
                var obj = this;
                (ability as EntityAbility).LoadContent(ref obj);
            }
            if(abilityName != "")
                abilityName = abilityName.Insert(0, "-");

            string abilityKeyName = ability.GetType().ToString().Replace("NoNameGame.Entities.Abilities.", "") + abilityName;
            if(Abilities.Contains(abilityKeyName))
                abilitiesList.Add(abilityKeyName, (ability as EntityAbility));
        }

        public void ActivateAbility(string abilityName)
        {
            if(abilitiesList.ContainsKey(abilityName))
            {
                abilitiesList[abilityName].IsActive = true;
                var obj = this;
                abilitiesList[abilityName].LoadContent(ref obj);
            }
        }

        public void DeactivateAbility(string abilityName)
        {
            if(abilitiesList.ContainsKey(abilityName))
            {
                abilitiesList[abilityName].IsActive = false;
                abilitiesList[abilityName].UnloadContent();
            }
        }

        public virtual void UnloadContent ()
        {
            Image.UnloadContent();
        }

        public virtual void Update (GameTime gameTime, Map map)
        {
            MoveVelocity = Vector2.Zero;

            foreach(var ability in abilitiesList)
                ability.Value.Update(gameTime);

            Image.Position += MoveVelocity * MoveSpeedFactor;

            collisionHandling(map);
            Image.Position += CollisionMovement;

            Image.Update(gameTime);
        }

        public virtual void Draw (SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }

        private void collisionHandling (Map map)
        {
            CollisionMovement = Vector2.Zero;
            if (MoveVelocity != Vector2.Zero)
            {
                List<Rectangle> collidingRectangles = map.GetCollidingTileRectangles(Image.CurrentRectangle, CollisionLevel);

                if (collidingRectangles.Count != 0)
                {
                    foreach (Rectangle collisionRectangle in collidingRectangles)
                    {
                        Vector2 collisionSolving = collisionRectangle.GetIntersectionDepth(Image.CurrentRectangle) * getCollisionSide(collisionRectangle);

                        if (MoveVelocity.X < 0)
                            CollisionMovement.X = MathHelper.Max(CollisionMovement.X, collisionSolving.X);
                        else if (MoveVelocity.X > 0)
                            CollisionMovement.X = MathHelper.Min(CollisionMovement.X, collisionSolving.X);
                        if (MoveVelocity.Y < 0)
                            CollisionMovement.Y = MathHelper.Max(CollisionMovement.Y, collisionSolving.Y);
                        else if (MoveVelocity.Y > 0)
                            CollisionMovement.Y = MathHelper.Min(CollisionMovement.Y, collisionSolving.Y);
                    }
                }
            }
        }

        private Vector2 getCollisionSide (Rectangle entityRectangle)
        {
            Vector2 collisionHandlingDirection = Vector2.Zero;

            if (MoveVelocity.X < 0 &&
                Image.CurrentRectangle.Left < entityRectangle.Right && Image.CurrentRectangle.Left > entityRectangle.Left)
                collisionHandlingDirection.X = 1;
            else if(MoveVelocity.X > 0 &&
                    Image.CurrentRectangle.Right > entityRectangle.Left && Image.CurrentRectangle.Right < entityRectangle.Right)
                collisionHandlingDirection.X = -1;

            if (MoveVelocity.Y < 0 &&
                    Image.CurrentRectangle.Top < entityRectangle.Bottom && Image.CurrentRectangle.Top > entityRectangle.Top)
                collisionHandlingDirection.Y = 1;
            else if (MoveVelocity.Y > 0 &&
                    Image.CurrentRectangle.Bottom > entityRectangle.Top && Image.CurrentRectangle.Bottom < entityRectangle.Bottom)
                collisionHandlingDirection.Y = -1;

            if (collisionHandlingDirection.X != 0 && collisionHandlingDirection.Y != 0)
            {
                float verticalDiff = 0;
                float horizontalDiff = 0;

                if (collisionHandlingDirection.X < 0)
                    horizontalDiff = Image.CurrentRectangle.Right - entityRectangle.Left;
                else if (collisionHandlingDirection.X > 0)
                    horizontalDiff = entityRectangle.Right - Image.CurrentRectangle.Left;

                if (collisionHandlingDirection.Y < 0)
                    verticalDiff = Image.CurrentRectangle.Bottom - entityRectangle.Top;
                else if (collisionHandlingDirection.Y > 0)
                    verticalDiff = entityRectangle.Bottom - Image.CurrentRectangle.Top;

                if (verticalDiff <= horizontalDiff)
                    collisionHandlingDirection.X = 0;
                else
                    collisionHandlingDirection.Y = 0;
            }

            return collisionHandlingDirection;
        }
    }
}
