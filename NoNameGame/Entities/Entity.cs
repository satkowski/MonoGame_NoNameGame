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

        public virtual void LoadContent ()
        {
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

        public virtual void Update (GameTime gameTime)
        {
            MoveVelocity = Vector2.Zero;

            foreach(var ability in abilitiesList)
                ability.Value.Update(gameTime);

            Image.Position += MoveVelocity * MoveSpeedFactor;

            Image.Update(gameTime);
        }

        public virtual void Draw (SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
