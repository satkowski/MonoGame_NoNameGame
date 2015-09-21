using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Entities.Abilities;
using NoNameGame.Maps;

namespace NoNameGame.Entities
{
    public class AutomatedEntity : Entity
    {
        Dictionary<string, EntityAbility> abilitiesList;

        [XmlElement("Ability")]
        public List<string> Abilities;
        public PlayerFollowingAbility PlayerFollowingAbility;
        public MovingAbility MovingAbility;

        public AutomatedEntity()
        {
            abilitiesList = new Dictionary<string, EntityAbility>();
            Abilities = new List<string>();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            setAbility<PlayerFollowingAbility>(ref PlayerFollowingAbility);
            setAbility<MovingAbility>(ref MovingAbility);
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

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, Map map)
        {
            MoveVelocity = Vector2.Zero;

            foreach(var ability in abilitiesList)
                ability.Value.Update(gameTime);

            Image.Position += MoveVelocity * MoveSpeedFactor;

            base.Update(gameTime, map);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
